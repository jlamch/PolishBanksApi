using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BankCore
{
    public class BankProvider
    {
        private readonly string NbpBankAddress = "http://www.nbp.pl/banki_w_polsce/ewidencja/dz_bank_jorg.txt";

        public async Task<IEnumerable<Bank>> GetBanks()
        {
            var content = await DownloadBanks();
            return GetBanks(content);
        }

        public async Task<IEnumerable<Bank>> GetBanksWithDivisions()
        {
            var content = await DownloadBanks();
            return GetBanksWithDivisions(content);
        }

        public async Task<string> DownloadBanks()
        {
            Debug.WriteLine("GetBanks");
            string contentString = string.Empty;

            using (var http = new HttpClient())
            {
                http.DefaultRequestHeaders.Add("Accept", "text/plain");

                var sw = new Stopwatch();
                sw.Start();
                HttpResponseMessage response = await http.GetAsync(NbpBankAddress);

                /// Still wrong encoding
                using (var reader = new StreamReader(await response.Content.ReadAsStreamAsync(), Encoding.Default, true))
                {
                    //Encoding.GetEncoding(28592)
                    contentString = reader.ReadToEnd();
                    var ce = reader.CurrentEncoding;
                }
                //contentString = await response.Content.ReadAsStringAsync();

                sw.Stop();
                Debug.WriteLine(sw.ElapsedMilliseconds);
                Debug.WriteLine(response, contentString);
                sw = null;
            }

            return contentString;
        }

        private IEnumerable<Bank> GetBanksWithDivisions(string content)
        {
            var splited1 = content.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            var banks = splited1.Select(s1 =>
               {
                   var s = s1.Split(new char[] { '\t' });
                   var bank = new Bank();
                   if (s != null)
                   {
                       bank.Id = s[0].Trim();
                       bank.Name = s[1].Trim();
                       bank.DivisionId = s[4].Trim();
                       bank.DivisionName = s[5].Trim();
                   }

                   return bank;
               });

            return banks;
        }

        private IEnumerable<Bank> GetBanks(string content)
        {
            var splited1 = content.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            var banks = splited1
                .Select(s1 =>
                   {
                       var s = s1.Split(new char[] { '\t' });
                       var bank = new Bank();
                       if (s != null)
                       {
                           bank.Id = s[0].Trim();
                           bank.Name = s[1].Trim();
                       }

                       return bank;
                   })
               .GroupBy(b => b.Id)
               .Select(group => group.First())
               .ToList();

            return banks;
        }
    }
}