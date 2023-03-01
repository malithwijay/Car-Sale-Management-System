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
using System.Data;

namespace Car_Sale
{
    /// <summary>
    /// Interaction logic for Updatesupplier.xaml
    /// </summary>
    public partial class Updatesupplier : Window
    {
        public Updatesupplier()
        {
            InitializeComponent();
        }
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter da;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {            
            con = new SqlConnection("Data Source=Sajana;Initial Catalog=DB_VehicleSale;Integrated Security=True");
            loadtable();
        }

        private void Btn_Clear_Click(object sender, RoutedEventArgs e)
        {
            mode_viewer.Text = "Now you are in Add Supplier mode";
            clearvalues();
            txt_Supplier_ID.IsReadOnly = false;
            txt_First_Name.IsReadOnly = false;
            txt_Lastt_Name.IsReadOnly = false;
            txt_Telephone.IsReadOnly = false;
            txt_Email.IsReadOnly = false;
        }

        private void Supplier_view_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            txt_Supplier_ID.IsReadOnly = true;
            txt_First_Name.IsReadOnly = true;
            txt_Lastt_Name.IsReadOnly = true;
            txt_Telephone.IsReadOnly = true;
            txt_Email.IsReadOnly = true;

            DataGrid gd = (DataGrid)sender;
            DataRowView row_selected = gd.SelectedItem as DataRowView;
            if (row_selected != null)
            {
                mode_viewer.Text = "Now you are in Update and Delete mode Press Clear to goto Add Supplier";
                txt_Supplier_ID.Text = row_selected["NIC"].ToString();
                txt_First_Name.Text = row_selected["FirstName"].ToString();
                txt_Lastt_Name.Text = row_selected["LastName"].ToString();
                txt_Telephone.Text = row_selected["Telephone"].ToString();
                txt_Email.Text = row_selected["Email"].ToString();
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                con.Open();
                cmd = new SqlCommand("insert into Supplier (NIC,FirstName,LastName,Telephone,Email) values ('" + txt_Supplier_ID.Text + "','" + txt_First_Name.Text + "','" + txt_Lastt_Name.Text + "'," +
                    "'" + txt_Telephone.Text + "','" + txt_Email.Text + "')", con);
                int i = cmd.ExecuteNonQuery();
                if (i == 1)
                {
                    MessageBox.Show("New Supplier have been added Successfully", "Data Saved !", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    
                }
                con.Close();
                cmd.Dispose();
            }
            catch (SqlException)
            {
                MessageBox.Show("Supplier Not saved successfully\n*If Supplier ID Already Excits Data won't be saved\n Please Check the Supplier ID again !!!", "Error !", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong please check all error messages else\n*The your user type dosen't" +
                    "have permissions to do this operation\n*The data is already Exicits \n*Close this menu and reload it and try to do this operation again", "Error !", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            clearvalues();
            loadtable();
        }

        private void clearvalues()
        {
            txt_Supplier_ID.Text = "";
            txt_First_Name.Text = "";
            txt_Lastt_Name.Text = "";
            txt_Telephone.Text = "";
            txt_Email.Text = "";
        }
        private void loadtable()
        {
            con.Open();
            da = new SqlDataAdapter("select NIC,FirstName,LastName,Telephone,Email from Supplier", con);
            DataSet ds = new DataSet();
            da.Fill(ds, "LoadDataBinding");
            Supplier_view.DataContext = ds;
            con.Close();
        }
        

        private void update_btn_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                con.Open();
                cmd = new SqlCommand("update Supplier set FirstName = '"+txt_First_Name.Text+"' , LastName = '"+txt_Lastt_Name.Text+"', " +
                    "Telephone='"+txt_Telephone.Text+"', Email='"+txt_Email.Text+"' where NIC = '" + txt_Supplier_ID.Text + "'", con);
                int i = cmd.ExecuteNonQuery();
                if (i == 1)
                {
                    MessageBox.Show("Supplier is Updated", "Done !", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
                cmd.Dispose();
                con.Close();
            }
            catch (SqlException)
            {
                MessageBox.Show("Supplier Not saved successfully\n*If Supplier ID Already Excits Data won't be saved\n Please Check the Supplier ID again !!!", "Error !", MessageBoxButton.OK, MessageBoxImage.Error);
                throw;
            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong please check all error messages else\n*The your user type dosen't" +
                    "have permissions to do this operation\n*The data is already Exicits \n*Close this menu and reload it and try to do this operation again", "Error !", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            clearvalues();
            loadtable();
        }

        private void delete_btn_Click(object sender, RoutedEventArgs e)
        {
            
            try
            {
                con.Open();
                cmd = new SqlCommand("Delete Supplier where NIC = '" + txt_Supplier_ID.Text + "'", con);
                int i = cmd.ExecuteNonQuery();
                if (i == 1)
                {
                    MessageBox.Show("Supplier is Deleted", "Done !", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
                cmd.Dispose();
                con.Close();
            }
            catch (SqlException)
            {
                MessageBox.Show("Supplier Not saved successfully\n*If Supplier ID Already Excits Data won't be saved\n Please Check the Supplier ID again !!!", "Error !", MessageBoxButton.OK, MessageBoxImage.Error);
                throw;
            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong please check all error messages else\n*The your user type dosen't" +
                    "have permissions to do this operation\n*The data is already Exicits \n*Close this menu and reload it and try to do this operation again", "Error !", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            clearvalues();
            loadtable();
        }
    }
}
