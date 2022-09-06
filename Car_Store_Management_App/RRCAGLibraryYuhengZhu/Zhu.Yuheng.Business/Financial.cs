/*
 * Name: Yuheng Zhu
 * Program: Business Information Technology
 * Course: ADEV-1008 Programming 2
 * Created: 2022-01-23
 * Updated: 2022-01-24
 */

using System;

/**
 * This static class contains functionality that includes financial functions.
 *
 * @author Yuheng Zhu
 * @version 1.0.0
 */
namespace Zhu.Yuheng.Business
{
    public static class Financial
    {
        /// <summary>
        /// The method is to Returns the payment amount for an annuity based on periodic, fixed payments and a fixed interest rate.
        /// </summary>
        /// <param name="rate">the interest rate per period.</param>
        /// <param name="numberOfPaymentPeriods">the total number of payment periods in the annuity.</param>
        /// <param name="presentValue">the present value (or lump sum) that a series of payments to be paid in the future is worth now.</param>
        /// <returns> the payment amount for an annuity based on periodic, fixed payments and a fixed interest rate.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Occurs when the rate is less than 0 and greater than 1,
        /// and the number of payments is less than or equal to zero,
        /// and the number of payments is less than or equal to zero.
        /// </exception>
        public static decimal GetPayment(decimal rate, int numberOfPaymentPeriods, decimal presentValue)
        {
            //Define exceptions
            if (rate < 0)
            {
                throw new ArgumentOutOfRangeException("rate", "The argument cannot be less than 0.");
            }
            if (rate > 1)
            {
                throw new ArgumentOutOfRangeException("rate", "The argument cannot be greater than 1.");
            }
            if (numberOfPaymentPeriods <= 0)
            {
                throw new ArgumentOutOfRangeException("numberOfPaymentPeriods", "The argument cannot be less than or equal to 0.");
            }
            if (presentValue <= 0)
            {
                throw new ArgumentOutOfRangeException("presentValue", "The argument cannot be less than or equal to 0.");
            }

            // Declare variables
            decimal futureValue = 0;
            decimal type = 0;
            decimal payment = 0;

            if (rate == 0)
                payment = presentValue / numberOfPaymentPeriods;
            else
                payment = rate * (futureValue + presentValue * (decimal)Math.Pow((double)(1 + rate), (double)numberOfPaymentPeriods)) / (((decimal)Math.Pow((double)(1 + rate), (double)numberOfPaymentPeriods) - 1) * (1 + rate * type));

            return Math.Round(payment, 2);
        }
    }
}