using NAGPBank.CrossCutting.Types;
using System;

namespace NAGPBank.CrossCutting.Models
{
    public class Account
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string AccountNumber { get; set; }
        public string IFSC { get; set; }
        public string BranchName { get; set; }
        public AccountStatus Status { get; set; } = AccountStatus.Active;
        public DateTime StartDate { get; set; } = DateTime.UtcNow;
        public DateTime? ClosedDate { get; set; }
    }
}
