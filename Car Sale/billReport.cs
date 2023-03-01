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
    public partial class billReport : Form
    {
        public billReport()
        {
            InitializeComponent();
        }

        private void customerReport_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'DataSet_Customer.Customer' table. You can move, or remove it, as needed.
            
        }

     
        private void button_search_Click_1(object sender, EventArgs e)
        {
            try
            {

            this.BillTableAdapter.Fill(this.DataSet_Bill.Bill, textBox_billNo.Text,dateTimePicker_from.Value,dateTimePicker_to.Value);

            }
            catch(Exception)
            { }
            this.reportViewer1.RefreshReport();
        }
    }
}
