using System;
using System.Collections.Generic;
using System.Text;

namespace NAGPBank.CrossCutting.Dto
{
    public class TransferAccountDto
    {
        public string AccountNumber { get; set; }
        public string IFSC { get; set; }
        public string BranchName { get; set; }
    }
}
