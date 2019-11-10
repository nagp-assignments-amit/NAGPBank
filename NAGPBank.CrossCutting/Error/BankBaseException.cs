using System;

namespace NAGPBank.CrossCutting.Error
{
    public class BankBaseException : Exception
    {
        private int _code;
        private string _description;

        public int Code
        {
            get => _code;
        }
        public string Description
        {
            get => _description;
        }

        public BankBaseException(string message)
            :base(message)
        {

        }

        public BankBaseException(string message, int code)
            : base(message)
        {
            _code = code;
        }
        public BankBaseException(string message, string description, int code) : base(message)
        {
            _code = code;
            _description = description;
        }
    }
}
