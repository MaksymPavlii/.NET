namespace Aggregation
{
    public class BaseDeposit : Deposit
    {
        
        public BaseDeposit(decimal depositAmount, int depositPeriod)
               : base(depositAmount, depositPeriod)
        {
        }

        public override decimal Income()
        {
            decimal interest = 0.05m;
            decimal amount = Amount;
            if (Period == 0)
            {
                return 0;
            }         
            else { 
            for (int i = 0; i < Period; i++)
                amount += System.Math.Round(amount * interest,2);
              
            }
            return System.Math.Round(amount - Amount, 2);
        }
    }
}