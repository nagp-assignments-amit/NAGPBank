using NAGPBank.CrossCutting.Types;
using System;

namespace NAGPBank.CrossCutting.Models
{
    public class Transactions
    {
        public int Id { get; set; }
        public string AccountNumber { get; set; }
        public DateTime TransactionOn { get; set; } = DateTime.UtcNow;
        public TransactionMode TransactionMode { get; set; }
        public string TransferToAccountNumber { get; set; }
        public int Amount { get; set; }
    }
}
