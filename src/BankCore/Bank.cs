using System.Diagnostics;

namespace BankCore
{
    [DebuggerDisplay("Bank: ID = {Id}; Name {Name}; DivisionId = {DivisionId}; DivisionName ={DivisionName}")]
    public class Bank
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public string DivisionId { get; set; }

        public string DivisionName { get; set; }
    }
}