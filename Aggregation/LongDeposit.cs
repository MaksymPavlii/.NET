namespace Aggregation
{
    public class LongDeposit : Deposit
    {
        public LongDeposit(decimal depositAmount, int depositPeriod)
               : base(depositAmount, depositPeriod)
        {          
        }
        public override decimal Income()
        {
            decimal interest = 0.15m;
            decimal amount = Amount;
            if (Period == 0)
            {
                return 0;
            }
            if (Period > 6)
            {
                for (int i = 6; i < Period; i++)
                    amount += System.Math.Round(amount * interest,2);
                
            }
            return System.Math.Round(amount - Amount,2);

        }
    }
}