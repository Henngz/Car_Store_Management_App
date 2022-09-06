/*
 * Name: Yuheng Zhu
 * Program: Business Information Technology
 * Course: ADEV-1008 Programming 2
 * Created: 2022-01-21
 * Updated: 2022-01-22
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/**
 * This program represents a quote for the sale of a vehicle.
 *
 * @author Yuheng Zhu
 * @version 1.0.0
 */
namespace Zhu.Yuheng.Business
{
    public class SalesQuote
    {
        // Declare fields
        private decimal vehicleSalePrice;
        private decimal tradeInAmount;
        private decimal salesTaxRate;
        private Accessories accessoriesChosen;
        private ExteriorFinish exteriorFinishChosen;

        /// <summary>
        /// Gets and sets the sale price of the vehicle.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"
        /// Occurs when the property is set to less than or equal to 0
        /// </exception>
        public decimal VehicleSalePrice
        {
            get
            {
                return this.vehicleSalePrice;
            }

            set
            {   
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException("value", "The value cannot be less than or equal to 0.");
                }

                this.vehicleSalePrice = value;
            }
        }

        /// <summary>
        /// Gets and sets the trade in amount.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"
        /// Occurs when the property is set to less than 0.
        /// </exception>
        public decimal TradeInAmount
        {
            get
            {
                return this.tradeInAmount;
            }

            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("value", "The value cannot be less than 0.");
                }

                this.tradeInAmount = value;
            }
        }

        /// <summary>
        /// Get and set the accessoriesChosen of the vehicle.
        /// </summary>
        /// <exception cref="System.ComponentModel.InvalidEnumArgumentException"
        /// Occurs when the property is set to an invalid value.
        /// </exception>
        public Accessories AccessoriesChosen
        {
            get
            {
                return this.accessoriesChosen;
            }

            set
            {
                if (!Enum.IsDefined(typeof(Accessories), AccessoriesChosen))
                {               
                    throw new System.ComponentModel.InvalidEnumArgumentException("value", (int)value, typeof(Accessories));
                    Console.WriteLine("The value is an invalid enumeration value");
                }

                this.accessoriesChosen = value;
            }
        }


        /// <summary>
        /// Get and set the exteriorFinishChosen of the vehicle.
        /// </summary>
        /// <exception cref="System.ComponentModel.InvalidEnumArgumentException"
        /// Occurs when the property is set to an invalid value.
        /// </exception>
        public ExteriorFinish ExteriorFinishChosen
        {
            get
            {
                return this.exteriorFinishChosen;
            }

            set
            {
                if (!Enum.IsDefined(typeof(ExteriorFinish), ExteriorFinishChosen))
                {
                    throw new System.ComponentModel.InvalidEnumArgumentException("value", (int)value, typeof(ExteriorFinish));
                    Console.WriteLine("The value is an invalid enumeration value");
                }

                this.exteriorFinishChosen = value;
            }
        }

        /// <summary>
        /// Gets the cost of accessories chosen.
        /// </summary>
        public decimal AccessoryCost
        {
            get
            {
                // Declare variables 
                decimal cosyOfAccessory = 0m;
                decimal costOfStereoSystem = 505.05m;
                decimal costOfLeatherInterior = 1010.10m;
                decimal costOfComputerNavigation = 1515.15m;
                

                // Use if statement to match different choices with costs.
                if (this.accessoriesChosen == Accessories.SteroSystem)
                {
                    cosyOfAccessory = costOfStereoSystem;
                }

                else if (this.accessoriesChosen == Accessories.LeatherInterior)
                {
                    cosyOfAccessory = costOfLeatherInterior;
                }

                else if (this.accessoriesChosen == Accessories.ComputerNavigation)
                {
                    cosyOfAccessory = costOfComputerNavigation;
                }

                else if (this.accessoriesChosen == Accessories.StereoAndLeather)
                {
                    cosyOfAccessory = costOfStereoSystem + costOfLeatherInterior;                   
                }

                else if (this.accessoriesChosen == Accessories.StereoAndNavigation)
                {
                    cosyOfAccessory = costOfStereoSystem + costOfComputerNavigation;
                }

                else if (this.accessoriesChosen == Accessories.LeatherAndNavigation)
                {
                    cosyOfAccessory = costOfLeatherInterior + costOfComputerNavigation;
                }

                else if (this.accessoriesChosen == Accessories.All)
                {
                    cosyOfAccessory = costOfStereoSystem + costOfComputerNavigation + costOfLeatherInterior;
                }

                else
                {
                    cosyOfAccessory = 0;             
                }

                return cosyOfAccessory;
            }

            private set 
            {

            }
        }

        /// <summary>
        /// Gets the cost of the exterior finish chosen.
        /// </summary>
        public decimal FinishCost
        {
            get
            {
                // Declare variables
                decimal costOfFinish = 0m;
                decimal costOfStandard = 202.02m;
                decimal costOfPearlized = 404.04m;
                decimal costOfCustom = 606.06m;

                // Use if statement to match different choices with costs.
                if (this.exteriorFinishChosen == ExteriorFinish.Standard)
                {
                    return costOfStandard;
                }

                else if (this.exteriorFinishChosen == ExteriorFinish.Pearlized)
                {
                    return costOfPearlized;
                }

                else if (this.exteriorFinishChosen == ExteriorFinish.Custom)
                {
                    return costOfCustom;
                }
                else
                {
                    return 0;
                }
            }

            private set
            {

            }
        }

        /// <summary>
        /// Gets the sum of the cost of the chosen accessories and exterior finish.
        /// </summary>
        public decimal TotalOptions
        {
            get
            {
                // Declare variables
                decimal costOfTotalOptions = AccessoryCost + FinishCost;

                // Using the Math.Round function to round the sum to two decimal places.
                return Math.Round(costOfTotalOptions, 2);
            }

            private set
            {

            }
        }

        /// <summary>
        /// Gets the sum of the vehicle’s sale price and the Accessory and Finish Cost. 
        /// </summary>
        public decimal SubTotal
        {
            get
            {
                // Declare variables
                decimal costOftSubTotal = VehicleSalePrice + TotalOptions;

                // Using the Math.Round function to round the sum to two decimal places.
                return Math.Round(costOftSubTotal, 2);
            }

            private set
            {

            }
        }

        /// <summary>
        ///  Gets the amount of tax to charge based on the subtotal.
        /// </summary>
        public decimal SalesTax
        {
            get
            {
                //Declare variables
                decimal costOfSalesTax = this.salesTaxRate * SubTotal;

                // Using the Math.Round function to round the sum to two decimal places.
                return Math.Round(costOfSalesTax, 2);
            }

            private set
            {

            }
        }

        /// <summary>
        /// Gets the sum of the subtotal and taxes.
        /// </summary>
        public decimal Total
        {
            get
            {
                // Declare variables
                decimal costOfTotal = SubTotal + SalesTax;
                return costOfTotal;
            }

            private set
            {

            }
        }

        /// <summary>
        /// Gets the result of subtracting the trade-in amount from the total.
        /// </summary>
        public decimal AmountDue
        {
            get
            {
                // Declare variables
                decimal differenceOfTotalAndTradeIn = Total - this.tradeInAmount;

                // Using the Math.Round function to round the difference to two decimal places.
                return Math.Round(differenceOfTotalAndTradeIn, 2);
            }

            private set
            {

            }
        }

        /// <summary>
        /// Initializes an instance of SalesQuote with a vehicle price, trade-in value, sales tax rate,
        /// accessories chosen and exterior finish chosen.
        /// </summary>
        /// <param name="vehicleSalePrice">the sale price of the vehicle</param>
        /// <param name="tradeInAmount">the trade in amount</param>
        /// <param name="salesTaxRate">the tax rate of sale price</param>
        /// <param name="accessoriesChosen">the accessories that were chosen</param>
        /// <param name="exteriorFinishChosen">the exterior finish that was chosen</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Occurs when the vehicleSalePrice is less than or equal to 0,
        /// or the tradeInAmount is less than 0, or the salesTaxRate less than 0 or greater than 1
        /// or the exteriorFinishChosen is an invalid enumeration value, or the accessoriesChosen is an invalid enumeration value.
        /// </exception>
        /// <exception cref="System.ComponentModel.InvalidEnumArgumentException"></exception>
        public SalesQuote(decimal vehicleSalePrice, decimal tradeInAmount, decimal salesTaxRate, Accessories accessoriesChosen, ExteriorFinish exteriorFinishChosen)
        {
            //Define exceptions
            if( vehicleSalePrice <= 0)
            {
                throw new ArgumentOutOfRangeException("vehicleSalePrice", "The argument cannot be less than or equal to 0.");
            }
    
            if(tradeInAmount < 0)
            {
                throw new ArgumentOutOfRangeException("tradeInAmount", "The argument cannot be less than 0.");
            }

            if(salesTaxRate < 0)
            {
                throw new ArgumentOutOfRangeException("salesTaxRate", "The argument cannot be less than 0.");
            }

            if(salesTaxRate > 1)
            {
                throw new ArgumentOutOfRangeException("salesTaxRate", "The argument cannot be greater than 1.");
            }

            if (!Enum.IsDefined(typeof(Accessories), accessoriesChosen))
            {
                throw new System.ComponentModel.InvalidEnumArgumentException("The argument is an invalid enumeration value.");
            }

            if (!Enum.IsDefined(typeof(ExteriorFinish), exteriorFinishChosen))
            {
                throw new System.ComponentModel.InvalidEnumArgumentException("The argument is an invalid enumeration value.");
            }

            this.VehicleSalePrice = vehicleSalePrice;
            this.TradeInAmount = tradeInAmount;
            this.salesTaxRate= salesTaxRate;
            this.AccessoriesChosen = accessoriesChosen;
            this.ExteriorFinishChosen = exteriorFinishChosen;
        }

        /// <summary>
        /// Initializes an instance of SalesQuote with a vehicle price, 
        /// trade-in value, sales tax rate.
        /// </summary>
        /// <param name="vehicleSalePrice">the sale price of the vehicle</param>
        /// <param name="tradeInAmount">the trade in amount</param>
        /// <param name="salesTaxRate">the tax rate of sale price</param>
        /// <param name="accessoriesChosen">the accessories that were chosen</param>
        /// <param name="exteriorFinishChosen">the exterior finish that was chosen</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Occurs when the vehicleSalePrice is less than or equal to 0,
        /// or the tradeInAmount is less than 0, or the salesTaxRate less than 0 or greater than 1
        /// </exception>
        public SalesQuote(decimal vehicleSalePrice, decimal tradeInAmount, decimal salesTaxRate)
        {
            //Define exceptions
            if (tradeInAmount < 0)
            {
                throw new ArgumentOutOfRangeException("tradeInAmount", "The argument cannot be less than 0.");
            }

            if (salesTaxRate < 0)
            {
                throw new ArgumentOutOfRangeException("salesTaxRate", "The argument cannot be less than 0.");
            }

            if (salesTaxRate > 1)
            {
                throw new ArgumentOutOfRangeException("salesTaxRate", "The argument cannot be greater than 1.");
            }

            this.VehicleSalePrice = vehicleSalePrice;
            this.TradeInAmount = tradeInAmount;
            this.salesTaxRate = salesTaxRate;
        }























































   }
}