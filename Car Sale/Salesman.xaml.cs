using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Car_Sale
{
    /// <summary>
    /// Interaction logic for Brands.xaml
    /// </summary>
    public partial class Salesman : UserControl
    {
        int usertype = 0;
        public Salesman(int type)
        {
            InitializeComponent();
            usertype = type;
        }

        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter da;
        SqlDataReader dr;

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //con = new SqlConnection("Data Source=DESKTOP-0UIQMQB;Initial Catalog=CarSaleNew;Integrated Security=True");
            //con = new SqlConnection("Data Source=Sajana;Initial Catalog=DB_VehicleSale;Integrated Security=True");
            con = new SqlConnection("Data Source=NOTEBOOK-MALITH;Initial Catalog=CarSaleNew;Integrated Security=True");
            loadtable();

        }
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (usertype == 0 || usertype == 1)
            {
                Addsalesman obj = new Addsalesman();
                obj.Show();
            }
            else
            {
                MessageBox.Show("Your user type cannot do this operation", "Error...!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void loadtable()
        {
            Salesman_view.DataContext = null;


            try
            {

                con.Open();
                da = new SqlDataAdapter("select SalesmanID,NIC,FirstName,LastName,Gender,Telephone,Email,JoinedDate,Salary from Salesman", con);
                DataSet ds = new DataSet();
                da.Fill(ds, "LoadDataBinding");
                Salesman_view.DataContext = ds;
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

        private void Salesman_view_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            DataGrid dg = (DataGrid)sender;
            DataRowView row_selected = dg.SelectedItem as DataRowView;
            if (row_selected != null)
            {
                cont_id.Text = row_selected["SalesmanID"].ToString();
                nic_id.Text = row_selected["NIC"].ToString();
                fname_id.Text = row_selected["FirstName"].ToString();
                lname_id.Text = row_selected["LastName"].ToString();
                gender_id.Text = row_selected["Gender"].ToString();
                tel_id.Text = row_selected["Telephone"].ToString();
                email_id.Text = row_selected["Email"].ToString();
                join_date_id.Text = row_selected["JoinedDate"].ToString();
                sal_id.Text = row_selected["Salary"].ToString();
            }

            try
            {
                string filelocation = "";

                con.Open();
                cmd = new SqlCommand("select ProfilePicture from Salesman where SalesmanID='" + cont_id.Text + "'", con);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    if (!dr.IsDBNull(0))
                    {
                        filelocation = dr.GetString(0);
                        if (!(string.IsNullOrEmpty(cont_id.Text)))
                        {
                            Bitmap prof = new Bitmap($"{filelocation}");
                            BitmapImage bi = prof.ToBitmapImage();
                            show_photo.Source = bi;
                        }
                    }
                }
                con.Close();
            }
            catch (SqlException)
            {
                MessageBox.Show("Sql Error", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception)
            {
                MessageBox.Show("Data Refreshed Successfully", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }

        }


        private void update_btn_Click(object sender, RoutedEventArgs e)
        {
            if (usertype == 0 || usertype == 1)
            {
                try
                {
                    con.Open();
                    cmd = new SqlCommand("update Salesman set NIC='" + nic_id.Text + "',FirstName='" + fname_id.Text + "',LastName='" + lname_id.Text + "',Gender='" + gender_id.Text + "'," +
                        "Telephone='" + tel_id.Text + "',Email='" + email_id.Text + "',Salary='" + sal_id.Text + "' where SalesmanID='" + cont_id.Text + "'", con);
                    int i = cmd.ExecuteNonQuery();
                    if (i == 1)
                    {
                        MessageBox.Show("Data Updated Successfully", "Done !", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    }
                    cmd.Dispose();
                    con.Close();
                }
                catch (SqlException)
                {
                    MessageBox.Show("Supplier Not saved successfully\n*If Supplier ID Already Excits Data won't be saved\n Please Check the Supplier ID again !!!", "Error !", MessageBoxButton.OK, MessageBoxImage.Error);
                    //throw;
                }
                catch (Exception)
                {
                    MessageBox.Show("Something went wrong please check all error messages else\n*The your user type dosen't" +
                        "have permissions to do this operation\n*The data is already Exicits \n*Close this menu and reload it and try to do this operation again", "Error !", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                loadtable();
            }
            else
            {
                MessageBox.Show("Your user type cannot do this operation", "Error...!", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }


        private void delete_btn_Click(object sender, RoutedEventArgs e)
        {
            if (usertype == 0 || usertype == 1)
            {
                try
                {
                    con.Open();
                    cmd = new SqlCommand("delete Salesman where SalesmanID = '" + cont_id.Text + "'", con);
                    int i = cmd.ExecuteNonQuery();
                    if (i == 1)
                    {
                        MessageBox.Show("Salesman Deleted Successfully");
                    }
                    con.Close();
                    cmd.Dispose();
                }
                catch (SqlException)
                {
                    MessageBox.Show("Supplier Not Deleted successfully\n*If Supplier ID Already Excits Data won't be saved\n Please Check the Supplier ID again !!!", "Error !", MessageBoxButton.OK, MessageBoxImage.Error);
                    //throw;
                }
                catch (Exception)
                {
                    MessageBox.Show("Something went wrong please check all error messages else\n*The your user type dosen't" +
                       "have permissions to do this operation\n*The data is already Exicits \n*Close this menu and reload it and try to do this operation again", "Error !", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                loadtable();
            }
            else
            {
                MessageBox.Show("Your user type cannot do this operation", "Error...!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void Salesman_view_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            DataGrid dg = (DataGrid)sender;
            DataRowView row_selected = dg.SelectedItem as DataRowView;
            if (row_selected != null)
            {

                cont_id.Text = row_selected["SalesmanID"].ToString();
                nic_id.Text = row_selected["NIC"].ToString();
                fname_id.Text = row_selected["FirstName"].ToString();
                lname_id.Text = row_selected["LastName"].ToString();
                gender_id.Text = row_selected["Gender"].ToString();
                tel_id.Text = row_selected["Telephone"].ToString();
                email_id.Text = row_selected["Email"].ToString();
                join_date_id.Text = row_selected["JoinedDate"].ToString();
                sal_id.Text = row_selected["Salary"].ToString();

                con.Close();
            }
        }

        private void refresh_btn_Click(object sender, RoutedEventArgs e)
        {
            loadtable();
        }
    }
}
