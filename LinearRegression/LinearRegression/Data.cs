using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinearRegression
{
    public class Data
    {
        //Independent
        public double MonthsWorked { get; private set; }

        //Dependent
        public double MoneyReceived { get; private set; }

        public double MonthsTimesMoney { get; set; }

        public double Predicted_Money { get; private set; }

        public double slope = 0.0;
        public double intercept = 0.0;

        public Data(string line) 
        {
            string[] helper = line.Split(",");
            this.MonthsWorked = Math.Round(double.Parse(helper[0], NumberStyles.AllowDecimalPoint),2);
            this.MoneyReceived = Math.Round(Math.Abs(double.Parse(helper[1], NumberStyles.AllowDecimalPoint)), 2);

            MonthsTimesMoney = MonthsWorked * MoneyReceived;
        }
        public void Calculate_Y()
        {
            this.Predicted_Money = (double)slope * MonthsWorked + (double)intercept;
        }
    }
}
