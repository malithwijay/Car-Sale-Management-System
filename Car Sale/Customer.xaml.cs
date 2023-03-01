using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace Car_Sale
{
    /// <summary>
    /// Interaction logic for Customer.xaml
    /// </summary>
    public partial class Customer : UserControl
    {
        public Customer()
        {
            InitializeComponent();
        }
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter da;
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //con = new SqlConnection("Data Source=DESKTOP-0UIQMQB;Initial Catalog=CarSaleNew;Integrated Security=True");
            con = new SqlConnection("Data Source=NOTEBOOK-MALITH;Initial Catalog=CarSaleNew;Integrated Security=True");
            //con = new SqlConnection("Data Source=Sajana;Initial Catalog=DB_VehicleSale;Integrated Security=True");
            loadtable();
        }
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            Addcustomer obj = new Addcustomer();
            obj.Show();
        }
        private void Customer_view_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            DataGrid dg = (DataGrid)sender;
            DataRowView rowselected = dg.SelectedItem as DataRowView;
            if (rowselected != null)
            {
                ed_cus_id.Text = rowselected["CustomerNIC"].ToString();
                ed_cus_fname.Text = rowselected["FirstName"].ToString();
                ed_cus_lname.Text = rowselected["LastName"].ToString();
                ed_cus_gender.Text = rowselected["Gender"].ToString();
                ed_cus_telephone.Text = rowselected["Telephone"].ToString();
                ed_cus_email.Text = rowselected["Email"].ToString();
                ed_cus_addressno.Text = rowselected["AddressNO"].ToString();
                ed_cus_addline1.Text = rowselected["FirstAddressLine"].ToString();
                ed_cus_addline2.Text = rowselected["SecondAddressLine"].ToString();
                ed_cus_city.Text = rowselected["City"].ToString();


            }
        }
        private void update_btn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(ed_cus_id.Text))
            {
                MessageBox.Show("Select a field", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                try
                {
                    con.Open();
                    cmd = new SqlCommand("update Customer set FirstName = '" + ed_cus_fname.Text + "', LastName = '" + ed_cus_lname.Text + "', Gender = '" + ed_cus_gender.Text + "' , Telephone = '" + ed_cus_telephone.Text + "', Email ='" + ed_cus_email.Text + "',AddressNO ='" + ed_cus_addressno.Text + "'," +
                        "FirstAddressLine='" + ed_cus_addline1.Text + "',SecondAddressLine ='" + ed_cus_addline2.Text + "',City='" + ed_cus_city.Text + "' where CustomerNIC='" + ed_cus_id.Text + "'", con);
                    int i = cmd.ExecuteNonQuery();
                    if (i == 1)
                    {
                        MessageBox.Show("Data Updated");

                    }
                }
                catch (SqlException)
                {
                    MessageBox.Show("Customer Not updated successfully\n*If Customer ID Already Exist Data won't be saved\n Please Check the Customer ID again !!!", "Error !", MessageBoxButton.OK, MessageBoxImage.Error);
                    //throw;
                }
                catch (Exception)
                {
                    MessageBox.Show("Something went wrong please check all error messages else\n*The your user type dosen't" +
                        "have permissions to do this operation\n*The data is already Exicits \n*Close this menu and reload it and try to do this operation again", "Error !", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                loadtable();


            }



        }

        private void Customer_view_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {

            DataGrid dg = (DataGrid)sender;
            DataRowView rowselected = dg.SelectedItem as DataRowView;
            if (rowselected != null)
            {
                ed_cus_id.Text = rowselected["CustomerNIC"].ToString();
                ed_cus_fname.Text = rowselected["FirstName"].ToString();
                ed_cus_lname.Text = rowselected["LastName"].ToString();
                ed_cus_gender.Text = rowselected["Gender"].ToString();
                ed_cus_telephone.Text = rowselected["Telephone"].ToString();
                ed_cus_email.Text = rowselected["Email"].ToString();
                ed_cus_addressno.Text = rowselected["AddressNO"].ToString();
                ed_cus_addline1.Text = rowselected["FirstAddressLine"].ToString();
                ed_cus_addline2.Text = rowselected["SecondAddressLine"].ToString();
                ed_cus_city.Text = rowselected["City"].ToString();

                con.Close();
            }
        }
        private void loadtable()
        {
            Customer_view.DataContext = null;
            try
            {
                con.Open();
                da = new SqlDataAdapter("select CustomerNIC,FirstName,LastName,Gender,Telephone,Email,AddressNO,FirstAddressLine,SecondAddressLine,City from Customer", con);
                DataSet ds = new DataSet();
                da.Fill(ds, "LoadDataBinding");
                Customer_view.DataContext = ds;
                con.Close();
            }
            catch (InvalidOperationException)
            {

            }
            catch (Exception)
            {
                MessageBox.Show("Something is Wrong", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }

}

