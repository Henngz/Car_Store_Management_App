/*
 * Name: Yuheng Zhu
 * Program: Business Information Technology
 * Course: ADEV-1008 Programming 2
 * Created: 2022-01-22
 * Updated: 2022-01-23
 */

using System;
using Zhu.Yuheng.Business;

/**
 * This class contains functionality that supports the business process of 
 * creating an invoice for the car wash department.
 *
 * @author Yuheng Zhu
 * @version 1.0.0
 */
namespace Zhu.Yuheng.Business
{
    public class CarWashInvoice : Invoice
    {
        // Declare fields
        private decimal packageCost;
        private decimal fragranceCost;

        /// <summary>
        ///  Gets and sets the amount charged for the chosen package.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"
        /// Occurs when the value is less than 0.
        /// </exception>
        public decimal PackageCost
        {
            get
            {
                return this.packageCost;
            }

            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("value", "The value cannot be less than 0.");
                }

                this.packageCost = value;
            }
        }

        /// <summary>
        /// Gets and sets the amount charged for the chosen fragrance.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"
        /// Occurs when the property is set to less than 0. 
        /// </exception>
        public decimal FragranceCost
        {
            get
            {
                return this.fragranceCost;
            }

            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("value", "The value cannot be less than 0.");
                }

                this.fragranceCost = value;
            }
        }

        /// <summary>
        /// Gets the amount of provincial sales tax charged to the customer. 
        /// </summary>
        public override decimal ProvincialSalesTaxCharged
        {
            get
            {
                decimal TaxCharged = 0;
                return Math.Round(TaxCharged, 2);
            }
        }

        /// <summary>
        /// Gets the amount of goods and services tax charged to the customer.
        /// </summary>
        public override decimal GoodsAndServicesTaxCharged
        {
            get
            {
                decimal TaxCharged = (PackageCost + FragranceCost) * GoodsAndServicesTaxRate;
                return Math.Round(TaxCharged, 2);
            }
        }

        /// <summary>
        /// Gets the subtotal of the Invoice. The subtotal is the sum of the package and fragrance cost.
        /// </summary>
        public override decimal SubTotal
        {
            get
            {
                return PackageCost + FragranceCost;
            }
        }

        /// <summary>
        /// Initializes an instance of CarWashInvoice with a provincial and goods and services tax rates. 
        /// </summary>
        /// <param name="provincialSalesTaxRate">The rate of provincial tax charged to a customer.</param>
        /// <param name="goodsAndServicesTaxRate">The rate of goods and services tax charged to a customer.</param>
        public CarWashInvoice(decimal provincialSalesTaxRate, decimal goodsAndServicesTaxRate)
            : base(provincialSalesTaxRate, goodsAndServicesTaxRate)
        {
            this.PackageCost = 0;
            this.FragranceCost = 0;
        }

        /// <summary>
        /// Initializes an instance of CarWashInvoice with a provincial and goods,
        /// services tax rate, package cost and fragrance cost.
        /// </summary>
        /// <param name="provincialSalesTaxRate">The rate of provincial tax charged to a customer.</param>
        /// <param name="goodsAndServicesTaxRate">The rate of goods and services tax charged to a customer.</param>
        /// <param name="packCost">The cost of the chosen package.</param>
        /// <param name="fragranceCost">The cost of the chosen fragrance.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Occurs when the packCost is less than 0 and the fragranceCost is less than 0.
        /// </exception>
        public CarWashInvoice(decimal provincialSalesTaxRate, decimal goodsAndServicesTaxRate, decimal packCost, decimal fragranceCost)
            : base(provincialSalesTaxRate, goodsAndServicesTaxRate)
        {
            //Define exceptions
            if (packCost < 0)
            {
                throw new ArgumentOutOfRangeException("value", "The value cannot be less than 0.");
            }
            if (fragranceCost < 0)
            {
                throw new ArgumentOutOfRangeException("value", "The value cannot be less than 0.");
            }

            this.PackageCost = packCost;
            this.FragranceCost = fragranceCost;
        }
    }
}