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
using System.Data;
using System.Data.OleDb;

namespace Zhu.Yuheng.RRCAGApp
{
    public partial class VehicleDataForm : ACE.BIT.ADEV.Forms.VehicleDataForm
    {
        // Declare some variables.
        private OleDbConnection connection;
        private OleDbDataAdapter adapter;
        private DataSet dataset;
        private BindingSource bindingSource;

        public VehicleDataForm()
        {           
            InitializeComponent();
            this.bindingSource = new BindingSource();
            this.Load += VehicleDataForm_Load;
        }

        /// <summary>
        /// Handle the load event of VehicleDataForm.
        /// </summary>
        private void VehicleDataForm_Load(object sender, EventArgs e)
        {
            InitialState();

            RetrieveDataFromTheDatabase();

            BindControls();
            this.mnuFileSave.Click += MnuFileSave_Click;
            this.mnuEditDelete.Click += MnuEditDelete_Click;
            this.FormClosing += VehicleDataForm_FormClosing;
            this.dgvVehicles.SelectionChanged += DgvVehicles_SelectionChanged;
            this.dgvVehicles.CellValueChanged += DgvVehicles_CellValueChanged;
            this.dgvVehicles.RowsAdded += DgvVehicles_RowsAdded;
        }

        /// <summary>
        /// Handles the RowsAdded event of DgvVehicles.
        /// </summary>
        private void DgvVehicles_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            if (dgvVehicles.CurrentRow != null)
            {
                dgvVehicles.CurrentRow.Cells["SoldBy"].Value= 0;
            }
            this.Text = "Vehicle Data";
        }

        /// <summary>
        /// Handles the CellValueChanged event of DgvVehicles.
        /// </summary>
        private void DgvVehicles_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            this.Text = "*Vehicle Data";
            this.mnuFileSave.Enabled = true;
        }

        /// <summary>
        /// Handles the SelectionChanged event of DgvVehicles.
        /// </summary>
        private void DgvVehicles_SelectionChanged(object sender, EventArgs e)
        {
            // Check whether the selected row is new row.
            if (dgvVehicles.CurrentRow != null)
            {
                if (dgvVehicles.CurrentRow.Index == dgvVehicles.NewRowIndex)
                {
                    mnuEditDelete.Enabled = false;
                }
                else
                {
                    this.mnuEditDelete.Enabled = true;
                }
            }       
        }

        private void VehicleDataForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Text == "*Vehicle Data")
            {
                // Set windows alert
                string saveWindowMessage = " Do you wish to save the changes?";
                string saveWindowcaption = "Save";
                MessageBoxButtons saveWindowbuttons = MessageBoxButtons.YesNoCancel;
                MessageBoxIcon saveWindowicon = MessageBoxIcon.Warning;
                DialogResult saveWindowresult;

                saveWindowresult = MessageBox.Show(saveWindowMessage, saveWindowcaption, saveWindowbuttons, saveWindowicon, MessageBoxDefaultButton.Button3);

                if (saveWindowresult == DialogResult.No)
                {
                    // Close the form
                    e.Cancel = false;
                }
                else if (saveWindowresult == DialogResult.Cancel)
                {
                    // Prevent closing the form
                    e.Cancel = true;
                }
                else if (saveWindowresult == DialogResult.Yes)
                {
                    // Check if there is an exception for saving.
                    try
                    {
                        this.adapter.Update(this.dataset, "VehicleStock");
                        
                        e.Cancel = false;
                    }
                    catch (Exception)
                    {
                        // Set windows alert
                        string message = "An error occurred while saving the changes. Do you still wish to close this window?";
                        string caption = "Save Error";
                        MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                        MessageBoxIcon icon = MessageBoxIcon.Error;
                        DialogResult result;

                        result = MessageBox.Show(message, caption, buttons, icon, MessageBoxDefaultButton.Button2);

                        if (result == DialogResult.Yes)
                        {
                            // Close the form
                            e.Cancel = false;                          
                        }
                    }
                }
            }
            else if(Text == "Vehicle Data")
            {
                // Close the form
                e.Cancel = false;
            }

            this.dgvVehicles.EndEdit();
            this.bindingSource.EndEdit();
        }


        /// <summary>
        /// Handles the click event of MnuFileSave.
        /// </summary>
        private void MnuFileSave_Click(object sender, EventArgs e)
            {
                // Change the title of the form
                this.Text = "Vehicle Data";

                this.dgvVehicles.EndEdit();
                this.bindingSource.EndEdit();
                this.mnuFileSave.Enabled = false;

                try
                {
                    this.adapter.Update(this.dataset, "VehicleStock");
                }
                catch (Exception)
                {
                    // Set windows alert
                    string message = "An error occurred while saving the changes to the vehicle data.";
                    string caption = "Save Error";
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    MessageBoxIcon icon = MessageBoxIcon.Error;
                    DialogResult result;

                    result = MessageBox.Show(message, caption, buttons, icon, MessageBoxDefaultButton.Button1);
                }
            }

        /// <summary>
        /// Handles the click event of MnuEditDelete.
        /// </summary>
        private void MnuEditDelete_Click(object sender, EventArgs e)
        {
            try
            {
                // Get the item number of selected row.
                int selectedRowIndex = dgvVehicles.CurrentRow.Index;
                string itemValue = dgvVehicles.Rows[selectedRowIndex].Cells[1].Value.ToString();

                // Set windows alert
                string message = "Are you sure you want to permanently delete stock item " + itemValue + " ?";
                string caption = "Delete Stock Item";
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                MessageBoxIcon icon = MessageBoxIcon.Warning;
                DialogResult result;

                result = MessageBox.Show(message, caption, buttons, icon, MessageBoxDefaultButton.Button2);

                switch (result)
                {
                    case DialogResult.Yes:
                        int selectIndex = this.dgvVehicles.CurrentRow.Index;
                        this.dgvVehicles.Rows.RemoveAt(selectIndex);
                        this.adapter.Update(this.dataset, "VehicleStock");
                        this.Text = "Vehicle Data";
                        this.mnuEditDelete.Enabled = false;
                        break;
                }
            }
            catch (Exception)
            {
                // Set windows alert
                string message = "An error occurred while deleting the selected vehicle.";
                string caption = "Deletion Error";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                MessageBoxIcon icon = MessageBoxIcon.Error;
                DialogResult result;

                result = MessageBox.Show(message, caption, buttons, icon, MessageBoxDefaultButton.Button1);
            }
        }

        /// <summary>
        /// The method is to queries the database and populates a dataset.
        /// </summary>
        private void RetrieveDataFromTheDatabase()
        {
            try
            {   // Instantiate the connection to the database.
                string connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=AMDatabase.mdb";
                this.connection = new OleDbConnection(connString);
                this.connection.Open();

                // Declare and Instantiate command 
                OleDbCommand cmd = new OleDbCommand("SELECT * FROM VehicleStock", this.connection);

                //Instantiate dataset
                this.dataset = new DataSet();

                // Instantiate the adapter.
                this.adapter = new OleDbDataAdapter();
                this.adapter.SelectCommand = cmd;
                this.adapter.Fill(this.dataset, "VehicleStock");

                // Declare and Instantiate a command builder.
                OleDbCommandBuilder cmdBuilder = new OleDbCommandBuilder(this.adapter);
                cmdBuilder.ConflictOption = ConflictOption.OverwriteChanges;

                // Generate command automatically by command builder.
                this.adapter.InsertCommand = cmdBuilder.GetInsertCommand();
                this.adapter.UpdateCommand = cmdBuilder.GetUpdateCommand();
                this.adapter.DeleteCommand = cmdBuilder.GetDeleteCommand();
            }
            catch (Exception)
            {
                // Set windows alert
                string message = "Unable to load vehicle data.";
                string caption = "Data Load Error";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                MessageBoxIcon icon = MessageBoxIcon.Error;
                DialogResult result;

                result = MessageBox.Show(message, caption, buttons, icon, MessageBoxDefaultButton.Button1);
            }
        }

        /// <summary>
        /// Binds the controls on the form.
        /// </summary>
        private void BindControls()
        {
            this.bindingSource.DataSource = this.dataset.Tables[0];
            this.dgvVehicles.DataSource = this.bindingSource;
            this.dgvVehicles.Columns["ID"].Visible = false;
            this.dgvVehicles.Columns["SoldBy"].Visible = false;
        }

        /// <summary>
        /// Sets the initial state of the form.
        /// </summary>
        private void InitialState()
        {
            this.mnuFileSave.Enabled = false;
            this.mnuEditDelete.Enabled = false;           
        }
    }
}