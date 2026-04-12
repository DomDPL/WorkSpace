namespace DPLRef.eCommerce.Engines.Remmitance
{
    internal class RemittanceCalculationEngine : EngineBase, IRemittanceCalculationEngine
    {
        public RemittanceCalculationResult CalculateFee(int sellerId, decimal total)
        {
            // @TODO: Make this real
            // For this version we are just going to do something incredibly easy and just return 
            // a flat rate. Real version Would need to do something more complicated.

            var result = new RemittanceCalculationResult
            {
                FeeAmount = total * 0.10M
            };
            result.RemittanceAmount = total - result.FeeAmount;
            return result;
        }
    }
}