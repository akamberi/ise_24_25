
namespace ISEPay.Common.Enums
{
    public enum UserStatus
    {
        APPROVED,
        REJECTED,
        PENDING,
        BLACKLIST
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
