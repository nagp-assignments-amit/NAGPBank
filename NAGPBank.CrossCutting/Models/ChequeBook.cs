using NAGPBank.CrossCutting.Types;
using System;

namespace NAGPBank.CrossCutting.Models
{
    public class ChequeBook
    {
        public int Id { get; set; }
        public string ChequeBookNumber { get; set; }
        public int CustomerId { get; set; }
        public string AccountNumber { get; set; }
        public DateTime OrderedOn { get; set; } = DateTime.UtcNow;
        public ChequeBookStatus Status { get; set; } = ChequeBookStatus.Issued;
    }
}
