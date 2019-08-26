namespace MoneyManager.BLL.Interfaces.Models.Transaction
{
    public class UpdateTransactionResult
    {
        public bool IsCategoryExists { get; set; }

        public bool IsAssetExists { get; set; }

        public bool IsAmountPositive { get; set; }
    }
}
