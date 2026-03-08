namespace SampleLibrary005
{
    public class BankAccount
    {
        public string Owner { get; set; }
        public decimal Balance { get; set; }
        public bool IsFrozen { get; set; }

        public void Deposit(decimal amount)
        {
            Balance += amount;
        }
    }
}
