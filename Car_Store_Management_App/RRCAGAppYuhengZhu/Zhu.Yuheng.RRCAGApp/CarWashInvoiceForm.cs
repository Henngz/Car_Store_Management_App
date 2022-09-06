using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zhu.Yuheng.RRCAGApp
{
    public partial class CarWashInvoiceForm : ACE.BIT.ADEV.Forms.CarWashInvoiceForm
    {
        private BindingSource carWashInvoiceSource;
        private CarWashForm carWashform;


        public CarWashInvoiceForm(BindingSource source,CarWashForm form)
        {
            InitializeComponent();

            this.carWashInvoiceSource = source;
            this.carWashform = form;

            BindControls();

            this.FormClosed += CarWashInvoiceForm_FormClosed;
        }

        /// <summary>
        ///// Handle the FormClosed event of CarWashInvoiceForm
        /// </summary>
        private void CarWashInvoiceForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.carWashform.CarWashForm_Reset(this.carWashform);
        }

    /// <summary>
    /// Bind data source to different item.
    /// </summary>
    public void BindControls()
        {
            Binding packagePriceBind = new Binding("Text", this.carWashInvoiceSource, "PackageCost");
            this.lblPackagePrice.DataBindings.Add(packagePriceBind);

            Binding fragrancePriceBind = new Binding("Text", this.carWashInvoiceSource, "FragranceCost");
            this.lblFragrancePrice.DataBindings.Add(fragrancePriceBind);
            
            Binding subtotalBind = new Binding("Text", this.carWashInvoiceSource, "SubTotal");
            this.lblSubtotal.DataBindings.Add(subtotalBind);

            Binding provincialSalesTaxBind = new Binding("Text", this.carWashInvoiceSource, "ProvincialSalesTaxCharged");
            this.lblProvincialSalesTax.DataBindings.Add(provincialSalesTaxBind);

            Binding goodsAndServicesTaxBind = new Binding("Text", this.carWashInvoiceSource, "GoodsAndServicesTaxCharged");
            this.lblGoodsAndServicesTax.DataBindings.Add(goodsAndServicesTaxBind);

            Binding totalBind = new Binding("Text", this.carWashInvoiceSource, "Total");
            this.lblTotal.DataBindings.Add(totalBind);

            // Format values of labels.
            packagePriceBind.FormattingEnabled = true;
            packagePriceBind.FormatString = "C";
            subtotalBind.FormattingEnabled = true;
            subtotalBind.FormatString = "C";
            totalBind.FormattingEnabled = true;
            totalBind.FormatString = "C";
            provincialSalesTaxBind.FormattingEnabled = true;
            provincialSalesTaxBind.FormatString = "n2";
            fragrancePriceBind.FormattingEnabled = true;
            fragrancePriceBind.FormatString = "n2";
        }

        
        private void CarWashInvoiceForm_Load(object sender, EventArgs e)
        {
            
        }
    }
}
