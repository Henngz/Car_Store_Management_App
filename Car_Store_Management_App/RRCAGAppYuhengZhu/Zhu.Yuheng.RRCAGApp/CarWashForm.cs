using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Zhu.Yuheng.Business;

namespace Zhu.Yuheng.RRCAGApp
{
    public partial class CarWashForm : ACE.BIT.ADEV.Forms.CarWashForm
    {
        // Declare collections.
        private List<string> packageItems;
        private List<string> fragranceItems;
        private BindingList<string> interiorItems;
        private BindingList<string> exteriorItems;    

        // Declare BindingSource of those collections.
        private BindingSource packageItemsSource;
        private BindingSource fragranceItemsSource;
        private BindingSource interiorItemsSource;
        private BindingSource exteriorItemsSource;
        string fragranceChosen;

        // Declare an instance of CarWashInvoice class.
        private CarWashInvoice carWashInvoice;
        decimal packageCost;
        decimal fragranceCost;
        decimal pst = 0.00m;
        decimal gst = 0.12m;

        // Declare a BindingSource of carWashInvoice
        private BindingSource carWashInvoiceSource;


        /// <summary>
        /// The class is to initialize an instance of CarWashForm.
        /// </summary>
        public CarWashForm()
        {
            InitializeComponent();

            // Initialize a field.
            this.packageItems = new List<string>();
            this.fragranceItems = new List<string>();
            this.interiorItems = new BindingList<string>();
            this.exteriorItems = new BindingList<string>();


            // Add data to the packageItems collection.
            this.packageItems.Add("Standard");
            this.packageItems.Add("Deluxe");
            this.packageItems.Add("Executive");
            this.packageItems.Add("Luxury");

            // Add Pine to fragranceItems collection.
            this.fragranceItems.Add("Pine");

            // Add data to the fragranceItems collection.
            string filepath = @"fragrances.txt";

            // Creates a FileStream which opens the file with read only access
            FileStream fileStream;
            fileStream = new FileStream(filepath, FileMode.Open, FileAccess.Read);

            // Creates a StreamReader which reads text from a FileStream.
            StreamReader fileReader;
            fileReader = new StreamReader(fileStream);

            // Loop while there is more data to read.
            while (fileReader.Peek() != -1)
            {
                // Read a record (line) from the file.
                string record = fileReader.ReadLine();

                char[] delimiters = { ',' };

                // Each field is stored as an element in the array.
                string[] fields = record.Split(delimiters);

                // Get different values with an inner Enumeration from the filed array.
                string fragranceType = fields[((int)Fragrances.FieldIndex.FragrancesType)];
                double price = double.Parse(fields[(int)Fragrances.FieldIndex.Price]);

                this.fragranceItems.Add(fragranceType);
            }

            BindControls();

            // Set the default value of cboPackage is blank.
            this.cboPackage.SelectedIndex = -1;

            // Subscribe event handlers of different selection of combo box.
            this.Load += CarWashForm_Load;
            this.cboPackage.SelectedIndexChanged += CboPackage_SelectedIndexChanged;
            this.cboFragrance.SelectedIndexChanged += CboFragrance_SelectedIndexChanged;
            this.mnuFileClose.Click += MnuFileClose_Click;
            this.mnuToolsGenerateInvoice.Click += MnuToolsGenerateInvoice_Click;  
            
        }

        /// <summary>
        /// Handles the Click event of the MnuToolsGenerateInvoice.
        /// </summary>
        private void MnuToolsGenerateInvoice_Click(object sender, EventArgs e)
        {
            CarWashInvoiceForm form = new CarWashInvoiceForm(this.carWashInvoiceSource, this);           
            form.Show();
           
        }

        /// <summary>
        /// Handles the Click event of the MnuFileClose.
        /// </summary>
        private void MnuFileClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the CboPackage.
        /// </summary>
        private void CboFragrance_SelectedIndexChanged(object sender, EventArgs e)
        {
            fragranceChosen = this.cboFragrance.Text;
            this.interiorItems.RemoveAt(0);
            this.interiorItems.Insert(0, "Fragrance - " + fragranceChosen);
            
            // Get the price of different fragrance.
            if (this.cboFragrance.SelectedIndex == 1)
            {
                fragranceCost = 2.75m;
            }
            else if(this.cboFragrance.SelectedIndex == 2)
            {
                fragranceCost = 1.5m;
            }
            else if (this.cboFragrance.SelectedIndex == 3)
            {
                fragranceCost = 2.25m;
            }
            else if (this.cboFragrance.SelectedIndex == 4)
            {
                fragranceCost = 0.75m;
            }
            else if (this.cboFragrance.SelectedIndex == 5)
            {
                fragranceCost = 2.0m;
            }
            else if (this.cboFragrance.SelectedIndex == 0)
            {
                fragranceCost = 0m;
            }

            // Declare an instance of CarWashInvoice class.
            carWashInvoice = new CarWashInvoice(pst, gst, packageCost, fragranceCost);
            this.carWashInvoiceSource.DataSource = this.carWashInvoice;

        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the CboPackage.
        /// </summary>
        private void CboPackage_SelectedIndexChanged(object sender, EventArgs e)
        {
            fragranceChosen = this.cboFragrance.Text;
            this.interiorItems.Clear();
            this.exteriorItems.Clear();

            // Identify which package is chosen, then show the matched interior items.
            if (this.cboPackage.SelectedIndex == 0)
            {
                this.interiorItems.Add("Fragrance - " + fragranceChosen);
                this.exteriorItems.Add("Hand Wash");
                packageCost = 7.50m;

            }
            else if(this.cboPackage.SelectedIndex == 1)
            {
                this.interiorItems.Add("Fragrance - " + fragranceChosen);
                this.interiorItems.Add("Shampoo Carpets");
                this.exteriorItems.Add("Hand Wash");
                this.exteriorItems.Add("Hand Wax");
                packageCost = 15.00m;
            }
            else if (this.cboPackage.SelectedIndex == 2)
            {
                this.interiorItems.Add("Fragrance - " + fragranceChosen);
                this.interiorItems.Add("Shampoo Carpets");
                this.interiorItems.Add("Shampoo Upholstery");
                this.exteriorItems.Add("Hand Wash");
                this.exteriorItems.Add("Hand Wax");
                this.exteriorItems.Add("Wheel Polish");
                packageCost = 35.00m;
            }
            else if (this.cboPackage.SelectedIndex == 3)
            {
                this.interiorItems.Add("Fragrance - " + fragranceChosen);
                this.interiorItems.Add("Shampoo Carpets");
                this.interiorItems.Add("Shampoo Upholstery");
                this.interiorItems.Add("Interior Protection Coat");
                this.exteriorItems.Add("Hand Wash");
                this.exteriorItems.Add("Hand Wax");
                this.exteriorItems.Add("Wheel Polish");
                this.exteriorItems.Add("Detail Engine Compartment");
                packageCost = 55.00m;
            }

            // Declare an instance of CarWashInvoice class.
            carWashInvoice = new CarWashInvoice(pst, gst, packageCost, fragranceCost);
            this.carWashInvoiceSource.DataSource = this.carWashInvoice;
        }

        /// <summary>
        /// Bind data source to different item.
        /// </summary>
        public void BindControls()
        {
            // Initialize a BindingSource
            this.packageItemsSource = new BindingSource();
            this.packageItemsSource.DataSource = this.packageItems;

            this.fragranceItemsSource = new BindingSource();
            this.fragranceItemsSource.DataSource = this.fragranceItems;

            //Using typeof() to bind Datasource because there is still no initialization of CarWashInvoice 
            this.carWashInvoiceSource = new BindingSource();
            this.carWashInvoiceSource.DataSource = typeof(CarWashInvoice);

            // Bind data source to Combo box.
            this.cboPackage.DataSource = this.packageItemsSource;       
            this.cboFragrance.DataSource = this.fragranceItemsSource;
            
            
            // Initialize a BindingSource and bind data to the data source.
            this.interiorItemsSource = new BindingSource();
            this.interiorItemsSource.DataSource = this.interiorItems;

            this.exteriorItemsSource = new BindingSource();
            this.exteriorItemsSource.DataSource = this.exteriorItems;

            // Bind data source to ListBox.
            this.lstInterior.DataSource = this.interiorItemsSource;
            this.lstExterior.DataSource = this.exteriorItemsSource;

            // Using simple binding to bind data for item.
            Binding subtotalBind = new Binding("Text", this.carWashInvoiceSource, "SubTotal");            
            this.lblSubtotal.DataBindings.Add(subtotalBind);

            Binding PSTBind = new Binding("Text", this.carWashInvoiceSource, "ProvincialSalesTaxCharged");
            this.lblProvincialSalesTax.DataBindings.Add(PSTBind);

            Binding GSTBind = new Binding("Text", this.carWashInvoiceSource, "GoodsAndServicesTaxCharged");
            this.lblGoodsAndServicesTax.DataBindings.Add(GSTBind);

            Binding totalBind = new Binding("Text", this.carWashInvoiceSource, "Total");
            this.lblTotal.DataBindings.Add(totalBind);

            // Format values of labels.
            PSTBind.FormattingEnabled = true;
            PSTBind.FormatString = "n2";
            subtotalBind.FormattingEnabled = true;
            subtotalBind.FormatString = "C";
            totalBind.FormattingEnabled = true;
            totalBind.FormatString = "C";

        }
        
        /// <summary>
        /// Handles the load event of the CarWashForm.
        /// </summary>
        private void CarWashForm_Load(object sender, EventArgs e)
        {          

        }

        // Declare a reset method. 
        public void CarWashForm_Reset(CarWashForm form)
        {
            this.cboFragrance.SelectedIndex = 0;
            this.cboPackage.SelectedIndex = -1;
            this.interiorItemsSource.Clear();
            this.exteriorItemsSource.Clear();
            this.carWashInvoiceSource.Clear();
        }
    }
}