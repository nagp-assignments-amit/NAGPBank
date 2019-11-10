using System;
using System.Collections.Generic;
using System.Text;

namespace NAGPBank.CrossCutting.Dto
{
    public class ResponseDto
    {
        public ResponseDto(string message)
        {
            this.Message = message;
        }
        public string Message { get; set; }
    }
}
