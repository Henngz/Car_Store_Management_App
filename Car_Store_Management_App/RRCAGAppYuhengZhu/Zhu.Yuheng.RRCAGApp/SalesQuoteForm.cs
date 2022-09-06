using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Zhu.Yuheng.Business;

/*
 * Name: Yuheng Zhu
 * Program: Business Information Technology
 * Course: ADEV-1008 Programming 2
 * Created: 2022-03-19
 * Updated: 2022-01-20
 */

namespace Zhu.Yuheng.RRCAGApp
{
    public partial class SalesQuoteForm : Form
    {
        SalesQuote salesQuote;

        decimal vehicleSalePrice,
                    tradeinValue;
        Accessories accessoriesChosen;
        ExteriorFinish exteriorFinishChosen;



    public SalesQuoteForm()
        {
            InitializeComponent();

            this.Load += SalesQuoteForm_Load;
            this.btnCalculateQuote.Click += BtnCalculateQuote_Click;
            this.chkStereoSystem.CheckedChanged += ChkAccessories_CheckedChanged;
            this.chkLeatherInterior.CheckedChanged += ChkAccessories_CheckedChanged;
            this.chkComputerNavigation.CheckedChanged += ChkAccessories_CheckedChanged;
            this.radStandard.CheckedChanged += RadStandard_CheckedChanged;
            this.radPearlized.CheckedChanged += RadPearlized_CheckedChanged;
            this.radCustomizedDetailing.CheckedChanged += RadCustomizedDetailing_CheckedChanged;
            this.nudNoOfYears.ValueChanged += NudNoOfYears_ValueChanged;
            this.nudAnnualInterestRate.ValueChanged += NudAnnualInterestRate_ValueChanged;
            this.txtVehicleSalePrice.TextChanged += TxtVehicleSalePrice_TextChanged;
            this.txtTradeinValue.TextChanged += TxtVehicleSalePrice_TextChanged;
            this.btnReset.Click += BtnReset_Click;
        }

        /// <summary>
        /// Handles the Click event of the BtnReset.
        /// </summary>
        private void BtnReset_Click(object sender, EventArgs e)
        {
            string message = "Do you want to reset the form?";
            string caption = "Reset Form";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            MessageBoxIcon icon = MessageBoxIcon.Warning;
            DialogResult result;

            result = MessageBox.Show(message, caption, buttons, icon, MessageBoxDefaultButton.Button2);

            switch (result)
            {
                case DialogResult.Yes:
                    this.SalesQuoteForm_Load(sender,e);
                    break;
            }
        }

        /// <summary>
        /// Handles the TextChanged event of the TxtVehicleSalePrice.
        /// </summary>
        private void TxtVehicleSalePrice_TextChanged(object sender, EventArgs e)
        {
            lblSummaryVehicleSalePrice.Text = String.Empty;
            lblOptions.Text = String.Empty;
            lblSubtotal.Text = String.Empty;
            lblSalesTax.Text = String.Empty;
            lblTotal.Text = String.Empty;
            lblTradein.Text = String.Empty;
            lblAmountDue.Text = String.Empty;
            lblMonthlyPayment.Text = String.Empty;
        }


        /// <summary>
        /// Handles the ValueChanged event of the NudAnnualInterestRate.
        /// </summary>
        private void NudAnnualInterestRate_ValueChanged(object sender, EventArgs e)
        {
            decimal rate = this.nudAnnualInterestRate.Value;
        }

        /// <summary>
        /// Handles the ValueChanged event of the NudNoOfYears.
        /// </summary>
        private void NudNoOfYears_ValueChanged(object sender, EventArgs e)
        {
            int numberOfPaymentPeriods = (int)this.nudNoOfYears.Value;
        }

        /// <summary>
        /// Handles the CheckedChanged event of the RadCustomizedDetailing
        /// </summary>
        private void RadCustomizedDetailing_CheckedChanged(object sender, EventArgs e)
        {
           exteriorFinishChosen = ExteriorFinish.Custom;

            if (this.lblSummaryVehicleSalePrice.Text != String.Empty)
            {
                this.BtnCalculateQuote_Click(sender, e);
            }
        }

        /// <summary>
        /// Handles the CheckedChanged event of the RadPearlized.
        /// </summary>
        private void RadPearlized_CheckedChanged(object sender, EventArgs e)
        {
            exteriorFinishChosen = ExteriorFinish.Pearlized;

            if (this.lblSummaryVehicleSalePrice.Text != String.Empty)
            {
                this.BtnCalculateQuote_Click(sender, e);
            }
        }

        /// <summary>
        /// Handles the CheckedChanged event of the RadStandard.
        /// </summary>
        private void RadStandard_CheckedChanged(object sender, EventArgs e)
        {
            exteriorFinishChosen = ExteriorFinish.Standard;

            if (this.lblSummaryVehicleSalePrice.Text != String.Empty)
            {
                this.BtnCalculateQuote_Click(sender, e);
            }
        }

        /// <summary>
        /// Handles the CheckedChanged event of the Accessories check boxes.
        /// </summary>
        private void ChkAccessories_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkStereoSystem.Checked && !this.chkLeatherInterior.Checked && !this.chkComputerNavigation.Checked)
            {
                 accessoriesChosen = Accessories.SteroSystem;
            }
            else if (this.chkLeatherInterior.Checked && !this.chkStereoSystem.Checked && !this.chkComputerNavigation.Checked)
            {
                 accessoriesChosen = Accessories.LeatherInterior;
            }
            else if (this.chkComputerNavigation.Checked && !this.chkStereoSystem.Checked && !this.chkLeatherInterior.Checked)
            {
                 accessoriesChosen = Accessories.ComputerNavigation;
            }
            else if (this.chkStereoSystem.Checked && this.chkLeatherInterior.Checked && !this.chkComputerNavigation.Checked)
            {
                 accessoriesChosen = Accessories.StereoAndLeather;
            }
            else if (this.chkStereoSystem.Checked && this.chkComputerNavigation.Checked && !this.chkLeatherInterior.Checked)
            {
                 accessoriesChosen = Accessories.StereoAndNavigation;
            }
            else if (this.chkLeatherInterior.Checked && this.chkComputerNavigation.Checked && !this.chkStereoSystem.Checked)
            {
                 accessoriesChosen = Accessories.LeatherAndNavigation;
            }
            else if (this.chkStereoSystem.Checked && this.chkLeatherInterior.Checked && this.chkComputerNavigation.Checked)
            {
                 accessoriesChosen = Accessories.All;
            }
            else
            {
                 accessoriesChosen = Accessories.None;
            }

            if (this.lblSummaryVehicleSalePrice.Text != String.Empty)
            {
                this.BtnCalculateQuote_Click(sender, e);
            }
        }

        /// <summary>
        /// Handles the click event of CalculateQuote button.
        /// </summary>
        private void BtnCalculateQuote_Click(object sender, EventArgs e)
        {
            // Set padding between error sign and text box.
            this.errorProvider.SetIconPadding(this.txtVehicleSalePrice, 3);
            this.errorProvider.SetIconPadding(this.txtTradeinValue, 3);

            // Let error sign disappear after load.
            this.errorProvider.SetError(this.txtVehicleSalePrice, String.Empty);
            this.errorProvider.SetError(this.txtTradeinValue, String.Empty);
       
            // Identify whether there is no input in text box.
            // If there is no input, output error message "Vehicle price is a required field."
            if (this.txtVehicleSalePrice.Text == String.Empty)
            {
                this.errorProvider.SetError(this.txtVehicleSalePrice,
                                            "Vehicle price is a required field.");
            }
            else
            {
                // If input isn't numeric, catch the exception and output error message "Vehicle price cannot contain letters or special characters."
                // If input is numeric, then convert it to decimal type.
                try
                {
                    vehicleSalePrice = decimal.Parse(this.txtVehicleSalePrice.Text);
                }
                catch (FormatException)
                {
                    this.errorProvider.SetError(this.txtVehicleSalePrice,
                                                "Vehicle price cannot contain letters or special characters.");
                }
            }

            // Identify is the input less than or equal to zero.
            // If so, output error message"Vehicle price cannot be less than or equal to 0."
            if (vehicleSalePrice <= 0 && this.errorProvider.GetError(this.txtVehicleSalePrice).Equals(string.Empty))
            {
                this.errorProvider.SetError(this.txtVehicleSalePrice,
                                            "Vehicle price cannot be less than or equal to 0.");
            }

            // Identify whether there is no input in text box.
            // If there is no input, output error message "Trade-in value is a required field."
            if (this.txtTradeinValue.Text == String.Empty)
            {
                this.errorProvider.SetError(this.txtTradeinValue,
                                            "Trade-in value is a required field.");
            }
            else
            {
                // If input isn't numeric, catch the exception and output error message "Trade-in value cannot contain letters or special characters."
                // If input is numeric, then convert it to decimal type.
                try
                {
                    tradeinValue = decimal.Parse(this.txtTradeinValue.Text);
                }
                catch (FormatException)
                {
                    this.errorProvider.SetError(this.txtTradeinValue,
                                                "Trade-in value cannot contain letters or special characters.");
                }
            }
         
            // Identify is the input less than or equal to zero.
            // If so, output error message"Trade-in value cannot be less than 0."
            if (tradeinValue < 0 && this.errorProvider.GetError(this.txtTradeinValue).Equals(string.Empty))
            {
                this.errorProvider.SetError(this.txtTradeinValue,
                                            "Trade-in value cannot be less than 0.");
            }

            // Set error message when the input is greater than the vehicle sale price.
            if (this.errorProvider.GetError(this.txtVehicleSalePrice).Equals(string.Empty))
            {
                if (tradeinValue > vehicleSalePrice)
                {
                    this.errorProvider.SetError(this.txtTradeinValue,
                                            "Trade-in value cannot exceed the vehicle sale price.");
                }

                // Convert Sales Quote to Currency for label Vehicle sales quote
                this.lblSummaryVehicleSalePrice.Text = vehicleSalePrice.ToString("C2");

                // Construct SalesQuote instance
                decimal salesTaxRate = 0.12m;
                salesQuote = new SalesQuote(vehicleSalePrice, tradeinValue, salesTaxRate, accessoriesChosen, exteriorFinishChosen);


                this.lblOptions.Text = salesQuote.TotalOptions.ToString();
                this.lblSubtotal.Text = salesQuote.SubTotal.ToString("C2");
                this.lblSalesTax.Text = salesQuote.SalesTax.ToString();
                this.lblTotal.Text = salesQuote.Total.ToString("C2");

                decimal lblTradein = decimal.Parse(this.txtTradeinValue.Text) * (-1);
                this.lblTradein.Text = lblTradein.ToString();
                this.lblAmountDue.Text = salesQuote.AmountDue.ToString();

                // Calculate Monthly Payment by using Financial class.
                decimal rate = nudAnnualInterestRate.Value / 100 / 12;
                int numberOfPaymentPeriods = (int)nudNoOfYears.Value * 12;
                decimal presentValue = decimal.Parse(this.lblAmountDue.Text);

                decimal monthlyPayment = Financial.GetPayment(rate, numberOfPaymentPeriods, presentValue);

                this.lblMonthlyPayment.Text = Math.Round(monthlyPayment, 2).ToString("C2");

            }            
        }

        /// <summary>
        /// Handles the load event of this form. 
        /// </summary>
        private void SalesQuoteForm_Load(object sender, EventArgs e)
        {
            this.txtVehicleSalePrice.Text = String.Empty;
            this.txtTradeinValue.Text = "0";
            this.errorProvider.SetError(this.txtVehicleSalePrice, String.Empty);
            this.errorProvider.SetError(this.txtTradeinValue, String.Empty);
            this.chkStereoSystem.Checked = false;
            this.chkLeatherInterior.Checked = false;
            this.chkComputerNavigation.Checked = false;
            this.radStandard.Checked = true;
            this.nudAnnualInterestRate.Value = 5;
        }

        private void txtVehicleSalePrice_TextChanged(object sender, EventArgs e)
        {

        }

        private void grpAccessories_Enter(object sender, EventArgs e)
        {

        }

        private void chkLeatherInterior_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chkComputerNavigation_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtTradeinValue_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click_1(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void nudAnnualInterestRate_ValueChanged(object sender, EventArgs e)
        {

        }

        private void nudNoOfYears_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void lblMonthlyPayment_Click(object sender, EventArgs e)
        {

        }

        private void lblSalesTax_Click(object sender, EventArgs e)
        {

        }

        private void lblTradein_Click(object sender, EventArgs e)
        {

        }
    }
}
