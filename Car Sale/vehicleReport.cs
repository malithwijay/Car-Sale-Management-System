using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Car_Sale
{
    public partial class vehicleReport : Form
    {
        public vehicleReport()
        {
            InitializeComponent();
        }

        private void vehicleReport_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'DataSet_Vehicle.Vehicle' table. You can move, or remove it, as needed.
            
        }

        private void button_search_Click(object sender, EventArgs e)
        {
            try
            {

            this.VehicleTableAdapter.Fill(this.DataSet_Vehicle.Vehicle,Convert.ToInt32(textBox_min.Text), Convert.ToInt32(textBox_max.Text));

            }
            catch(Exception)
            { }
            this.reportViewer1.RefreshReport();
            this.reportViewer1.RefreshReport();
        }
    }
}
