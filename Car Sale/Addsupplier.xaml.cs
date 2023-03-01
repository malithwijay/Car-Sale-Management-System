using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data.SqlClient;

namespace Car_Sale
{
    /// <summary>
    /// Interaction logic for Addsupplier.xaml
    /// </summary>
    public partial class Addsupplier : Window
    {
        public Addsupplier()
        {
            InitializeComponent();
        }
        SqlConnection con;
        SqlCommand cmd;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            con = new SqlConnection("Data Source=Sajana;Initial Catalog=DB_VehicleSale;Integrated Security=True");
        }
        private void Btn_Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                con.Open();
                cmd = new SqlCommand("insert into Supplier (NIC,FirstName,LastName,Telephone,Email) values ('" + txt_Supplier_NIC.Text + "','" + txt_First_Name.Text + "','" + txt_Lastt_Name.Text + "'," +
                    "'" + txt_Telephone.Text + "','" + txt_Email.Text + "')", con);
                int i = cmd.ExecuteNonQuery();
                if (i == 1)
                {
                    MessageBox.Show("New Supplier have been added Successfully", "Data Saved !", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
                con.Close();
                cmd.Dispose();
            }
            catch(SqlException)
            {
                MessageBox.Show("Supplier Not saved successfully\n*If Supplier ID Already Excits Data won't be saved\n Please Check the Supplier ID again !!!", "Error !", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch(Exception)
            {
                MessageBox.Show("Something went wrong please check all error messages else\n*The your user type dosen't" +
                    "have permissions to do this operation\n*The data is already Exicits \n*Close this menu and reload it and try to do this operation again", "Error !", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        private void Btn_Clear_Click(object sender, RoutedEventArgs e)
        {
            txt_Supplier_NIC.Clear();
            txt_First_Name.Clear();
            txt_Lastt_Name.Clear();
            txt_Email.Clear();
            txt_Telephone.Clear();
        }

        
    }
}
