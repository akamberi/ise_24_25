
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
        WITHDRAWAL,
        ISACTIVE
    }

    public enum TransactionStatus
    {
        PENDING,
        COMPLETED,
        FAILED
    }
    
    public enum FeeType
    {
        PercentageFee = 1,
        FlatFee = 2,
        AddressFee = 3
    }
}
