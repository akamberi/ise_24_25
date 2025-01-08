
namespace ISEPay.Common.Enums
{
    public enum UserStatus
    {
        APPROVED,
        REJECTED,
        PENDING,
        Frozen
    }

    public enum FriendStatus
    {
        PENDING,
        ACCEPTED,
        REJECTED
    }

    public enum AccountStatus {
        ACTIVE,
        INACTIVE
    }

    public enum AccountType
    {
        STANDARD,
        SAVINGS,
        CHECKING
    }


    public enum TransactionType
    {
        TRANSFER,
        DEPOSIT,
        WITHDRAWAL
    }

    public enum TransactionStatus
    {
        PENDING,
        COMPLETED,
        FAILED
    }
}
