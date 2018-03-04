using BankCore;
using NUnit.Framework;
using System.Threading.Tasks;

namespace BankTests
{
    [TestFixture]
    public class UnitTest1
    {
        [Test]
        public async Task TestDownloadBanks()
        {
            var provider = new BankProvider();
            var result = await provider.DownloadBanks();
            Assert.IsNotEmpty(result);
        }

        [Test]
        public async Task TestGetBanksWithDivisions()
        {
            var provider = new BankProvider();
            var result = await provider.GetBanksWithDivisions();

            Assert.NotNull(result);
        }

        [Test]
        public async Task TestGetBanks()
        {
            var provider = new BankProvider();
            var result = await provider.GetBanks();

            Assert.NotNull(result);
        }
    }
}