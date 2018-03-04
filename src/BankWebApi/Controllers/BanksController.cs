using BankCore;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BankWebApi.Controllers
{
    [Route("api/[controller]")]
    public class BanksController : Controller
    {
        private BankProvider _bankProvider;

        public BanksController()
        {
            _bankProvider = new BankProvider();
        }

        // GET api/banks
        [HttpGet]
        public IEnumerable<Bank> Get()
        {
            return _bankProvider.GetBanks().GetAwaiter().GetResult();
        }

        // GET api/banks/WithDivisions
        [Route("WithDivisions")]
        public IEnumerable<Bank> GetBanksWithDivisions()
        {
            return _bankProvider.GetBanksWithDivisions().GetAwaiter().GetResult();
        }
    }
}