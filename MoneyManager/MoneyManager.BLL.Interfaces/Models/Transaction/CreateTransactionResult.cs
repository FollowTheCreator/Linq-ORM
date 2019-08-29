namespace MoneyManager.BLL.Interfaces.Models.Transaction
{
    public class CreateTransactionResult
    {
        public bool IsTransactionCategoryExists { get; set; }

        public bool IsTransactionAssetExists { get; set; }

        public bool IsTransactionAmountPositive { get; set; }
    }
}
