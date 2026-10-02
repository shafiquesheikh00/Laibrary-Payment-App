namespace Laibrary_Payment_App.Models
{
    public static class PaymentModes
    {
        public const string Cash = "Cash";
        public const string UPI = "UPI";
        public const string Card = "Card";
        public const string BankTransfer = "BankTransfer";
        public const string Other = "Other";

        public static readonly string[] All = new[]
        {
            Cash,
            UPI,
            Card,
            BankTransfer,
            Other
        };

        public static bool IsValid(string? mode)
        {
            if (string.IsNullOrWhiteSpace(mode)) return false;
            return Array.Exists(All, m => string.Equals(m, mode.Trim(), StringComparison.OrdinalIgnoreCase));
        }

        public static string Normalize(string mode)
        {
            foreach (var m in All)
            {
                if (string.Equals(m, mode.Trim(), StringComparison.OrdinalIgnoreCase))
                {
                    return m;
                }
            }
            return Other;
        }
    }
}
