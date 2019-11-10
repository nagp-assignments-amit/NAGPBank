namespace NAGPBank.CrossCutting.Types
{
    public enum AccountStatus
    {
        Active = 1,
        Closed
    }

    public enum TransactionMode
    {
        Withdrawal = 1,
        Deposit,
        Transfer
    }

    public enum ChequeBookStatus
    {
        Issued = 1,
        Blocked
    }
}
