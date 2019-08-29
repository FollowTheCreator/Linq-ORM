namespace MoneyManager.BLL.Interfaces.Models.Transaction
{
    public class UpdateTransactionResult
    {
        public bool IsTransactionExists { get; set; }

        public bool IsTransactionCategoryExists { get; set; }

        public bool IsTransactionAssetExists { get; set; }

        public bool IsTransactionAmountPositive { get; set; }
    }
}
