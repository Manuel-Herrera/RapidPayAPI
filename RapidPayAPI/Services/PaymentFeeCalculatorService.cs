namespace RapidPayAPI.Services
{
    public class PaymentFeeCalculator
    {
        private static PaymentFeeCalculator _instance;
        private static readonly object _lock = new object();
        private decimal _lastFeeAmount;
        private readonly Random _random;

        private PaymentFeeCalculator()
        {
            _random = new Random();
            _lastFeeAmount = 0;
        }

        public static PaymentFeeCalculator Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new PaymentFeeCalculator();
                    }
                    return _instance;
                }
            }
        }

        public decimal CalculateFee()
        {
            decimal randomDecimal = _random.Next(0, 200) / 100m;
            decimal newFeeAmount = _lastFeeAmount * randomDecimal;
            _lastFeeAmount = newFeeAmount;

            return newFeeAmount;
        }
    }

}
