using System.Net;

namespace NAGPBank.CrossCutting.Error
{
    public class NotFoundException : BankBaseException
    {
        public NotFoundException(string message)
            :base(message, (int)HttpStatusCode.NotFound)
        {

        }
    }
}
