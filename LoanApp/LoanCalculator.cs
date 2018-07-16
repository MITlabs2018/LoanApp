using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanApp
{
    class LoanCalculator
    {
        private double interest; //monthly interest or daily interest
        private int duration;// days / months
        private double principalAmount; // initial amount
        private double numberOfTimes_Componded_Per_Period;
        //if interst is daily calculated and daily interes rate is given above value will be 1
        //if interst is monthly calculated and mothly interes rate is given above value will be 1

        private String Calculatingfrequency; //daily or mothly

        public LoanCalculator(double interest, int duration,String Calculatingfrequency,double principalAmount,double numberOfTimes_Componded_Per_Period)
        {
            this.interest = interest;
            this.duration = duration;
            this.principalAmount = principalAmount;
            this.Calculatingfrequency = Calculatingfrequency;
            this.numberOfTimes_Componded_Per_Period = numberOfTimes_Componded_Per_Period;
        }

        public double calc_Compund() { // returns the amount with interest after particular time period

            double amount;

            amount =Math.Pow((principalAmount * (1 + (interest / numberOfTimes_Componded_Per_Period))),(numberOfTimes_Componded_Per_Period* duration));

            return amount;
        }

        public double findToBePaid_Compund(int refNo)
        {
            double amount = 0;
            //tobePaid after last payment
            //SELECT * FROM installment WHERE installment.RefNo = 3
            //take the paid amounts, when an amount is paid calculate the amount to be paid

            return amount;
        }

        public double calc_Simple_Interest() {
            double amount = 0;


            return amount;
        }

    }
}
