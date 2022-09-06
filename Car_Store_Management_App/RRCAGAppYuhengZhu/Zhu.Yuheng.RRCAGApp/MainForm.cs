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


/*
 * Name: Yuheng Zhu
 * Program: Business Information Technology
 * Course: ADEV-1008 Programming 2
 * Created: 2022-03-19
 * Updated: 2022-01-20
 */
namespace Zhu.Yuheng.RRCAGApp
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            this.mnuFileExit.Click += MnuFileExit_Click;
            this.mnuHelpAbout.Click += MnuHelpAbout_Click;
            this.mnuFIleOpenSalesQuote.Click += MnuFIleOpenSalesQuote_Click;
            this.mnuFileOpenCarWash.Click += MnuFileOpenCarWash_Click;
            this.mnuDataVehicles.Click += MnuDataVehicles_Click;
        }

        /// <summary>
        /// Handles the click event of the Vehicles item.
        /// </summary>
        private void MnuDataVehicles_Click(object sender, EventArgs e)
        {
            VehicleDataForm form = new VehicleDataForm();           
            form.MdiParent = this;
            form.Show();
            form.Activate();
        }

        /// <summary>
        /// Handles the click event of the CarWash item.
        /// </summary>
        private void MnuFileOpenCarWash_Click(object sender, EventArgs e)
        {
            string path = @"fragrances.txt";

            // Using file.exists method to identify if the file is exist.       
            try
            {
                if (!File.Exists(path))
                {
                    string message = "Fragrances data file not found.";
                    string caption = "Data File Error";
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    MessageBoxIcon icon = MessageBoxIcon.Error;
                    DialogResult result;

                    result = result = MessageBox.Show(message, caption, buttons, icon, MessageBoxDefaultButton.Button1);
                }
                else
                {
                    CarWashForm form = new CarWashForm();
                    form.MdiParent = this;
                    form.Show();
                    form.Activate();              
                }
            }
            catch (Exception)
            {
                string message = "An error occurred while reading the data file.";
                string caption = "Data File Error";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                MessageBoxIcon icon = MessageBoxIcon.Error;
                DialogResult result;

                result = result = MessageBox.Show(message, caption, buttons, icon, MessageBoxDefaultButton.Button1);
            }
        }

        /// <summary>
        /// Handles the click event of the Sales Quote item.
        /// </summary>
        private void MnuFIleOpenSalesQuote_Click(object sender, EventArgs e)
        {
            SalesQuoteForm form = new SalesQuoteForm();
            form.ShowDialog();
        }

        /// <summary>
        /// Handles the click event of the about menu item.
        /// </summary>
        private void MnuHelpAbout_Click(object sender, EventArgs e)
        {
            AboutForm form = new AboutForm();
            form.ShowDialog();
        }

        /// <summary>
        /// Handles the click event of the exit menu item. 
        /// </summary>
        private void MnuFileExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void msFile_Click(object sender, EventArgs e)
        {

        }

        private void dataToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void mnuDataVehicles_Click(object sender, EventArgs e)
        {

        }

        private void menuStrip3_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}