/*
 * Name: Yuheng Zhu
 * Program: Business Information Technology
 * Course: ADEV-1008 Programming 2
 * Created: 2022-01-23
 * Updated: 2022-01-24
 */

using System;

/**
 * This abstract class contains functionality that supports the business process of creating an invoice.
 *
 * @author Yuheng Zhu
 * @version 1.0.0
 */
namespace Zhu.Yuheng.Business
{
    public abstract class Invoice
    {
        //Declare fields
        public decimal provincialSalesTaxRate;
        public decimal goodsAndServicesTaxRate;

        /// <summary>
        /// Gets and sets the provincial sales tax rate.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"
        /// Ourrs when the property is set to less than 0 or greater than 1.
        /// </exception>
        public decimal ProvincialSalesTaxRate
        {
            get
            {
                return this.provincialSalesTaxRate;
            }

            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("value", "The value cannot be less than 0.");
                }

                if (value > 1)
                {
                    throw new ArgumentOutOfRangeException("value", "The value cannot be greater than 1.");
                }

                this.provincialSalesTaxRate = value;
            }
        }

        /// <summary>
        /// Gets and sets the goods and services tax rate.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"
        /// Occurs when the property is set to less than 0 or greater than 1.
        /// </exception>
        public decimal GoodsAndServicesTaxRate
        {
            get
            {
                return this.goodsAndServicesTaxRate;
            }

            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("value", "The value cannot be less than 0.");
                }

                if (value > 1)
                {
                    throw new ArgumentOutOfRangeException("value", "The value cannot be greater than 1.");
                }

                this.goodsAndServicesTaxRate = value;
            }
        }

        /// <summary>
        /// Gets the amount of provincial sales tax charged to the customer
        /// </summary>
        public abstract decimal ProvincialSalesTaxCharged
        {
            get;
        }

        /// <summary> 
        /// Gets the amount of goods and services tax charged to the customer.
        /// </summary>
        public abstract decimal GoodsAndServicesTaxCharged
        {
            get;
        }

        /// <summary>
        /// Gets the subtotal of the Invoice.
        /// </summary>
        public abstract decimal SubTotal
        {
            get;
        }

        /// <summary>
        /// Gets the total of the Invoice. The total is the sum of the subtotal and taxes.
        /// </summary>
        public decimal Total
        {
            get
            {
                decimal totalCost = ProvincialSalesTaxCharged + GoodsAndServicesTaxCharged + SubTotal;
                return totalCost;
            }

            private set
            {

            }
        }

        /// <summary>
        /// Initializes an instance of Invoice with a provincial and goods and services tax rates.
        /// </summary>
        /// <param name="provincialSalesTaxRate"> The rate of provincial tax charged to a customer.</param>
        /// <param name="goodsAndServicesTaxRate">The rate of goods and services tax charged to a customer.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Occurs when the provincialSalesTaxRate is less than 0 or greater than 1, 
        /// and the goodsAndServicesTaxRate is less than 0 or greater than 1.
        /// </exception>
        public Invoice(decimal provincialSalesTaxRate, decimal goodsAndServicesTaxRate)
        {
            //Define exceptions
            if (provincialSalesTaxRate < 0)
            {
                throw new ArgumentOutOfRangeException("value", "The value cannot be less than 0.");
            }

            if (provincialSalesTaxRate > 1)
            {
                throw new ArgumentOutOfRangeException("value", "The value cannot be greater than 1.");
            }

            if (goodsAndServicesTaxRate < 0)
            {
                throw new ArgumentOutOfRangeException("value", "The value cannot be less than 0.");
            }

            if (goodsAndServicesTaxRate > 1)
            {
                throw new ArgumentOutOfRangeException("value", "The value cannot be greater than 1.");
            }

            this.ProvincialSalesTaxRate = provincialSalesTaxRate;
            this.GoodsAndServicesTaxRate = goodsAndServicesTaxRate;
        } 
    }
}