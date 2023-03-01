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
    /// Interaction logic for Viewcar.xaml
    /// </summary>
    public partial class Viewcar : Window
    {
        public Viewcar()
        {
            InitializeComponent();
        }

        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        SqlDataAdapter da;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //con = new SqlConnection("Data Source=DESKTOP-0UIQMQB;Initial Catalog=CarSaleNew;Integrated Security=True");
            //con = new SqlConnection("Data Source=Sajana;Initial Catalog=DB_VehicleSale;Integrated Security=True");
            con = new SqlConnection("Data Source=NOTEBOOK-MALITH;Initial Catalog=CarSaleNew;Integrated Security=True");
            loadtable();
        }

        private void loadtable()
        {
            Car_view.DataContext = null;
            try
            {
                con.Open();
                da = new SqlDataAdapter("select VehicleID,MCID,SupplierID,YearOfMade,ChassisNumber,EngineNumber,EngineCapacity,FuelType,Transmissiontype,Condition,InColor,OutColor,ImportedDate,YearOfRegistered,mileage,PlateNumber,VehicleStatus,Price from Vehicle", con);
                DataSet ds = new DataSet();
                da.Fill(ds, "LoadDataBinding");
                Car_view.DataContext = ds;
                con.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "load sql");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "load super");
            }
        }
        private void filtertable(string filter)
        {
            Car_view.DataContext = null;
            string command = ($"select VehicleID,MCID,SupplierID,YearOfMade,ChassisNumber,EngineNumber,EngineCapacity,FuelType,Transmissiontype,Condition,InColor,OutColor,ImportedDate,YearOfRegistered,mileage,PlateNumber,VehicleStatus,Price from Vehicle where {filter} = '{txt_vehicle_ID.Text}'");
            try
            {
                con.Open();

                da = new SqlDataAdapter(command, con);
                DataSet ds = new DataSet();
                da.Fill(ds, "LoadDataBinding");
                Car_view.DataContext = ds;

                con.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "filter Sql");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "filter super");
            }
        }

        string vehID = "", supID = "", typeID = "";
        private void Car_view_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            DataGrid dg = (DataGrid)sender;
            DataRowView rowselected = dg.SelectedItem as DataRowView;

            if (rowselected != null)
            {
                vehID = rowselected["VehicleID"].ToString();
                supID = rowselected["SupplierID"].ToString();
                typeID = rowselected["MCID"].ToString();
            }
            //txt_vehicle_ID.Text = vehID;

            try
            {
                getsupplierdetails();
                getwarrentydetails();
                getvehicledetails();
                getbrandmodel();
            }
            catch (Exception) { }
        }
        private void search_select_cmb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void search_btn_Click(object sender, RoutedEventArgs e)
        {
            Car_view.DataContext = null;
            if (!string.IsNullOrEmpty(txt_vehicle_ID.Text))
            {
                if (search_select_cmb.SelectedIndex == 0)
                {
                    filtertable("VehicleID");
                }
                else if (search_select_cmb.SelectedIndex == 1)
                {
                    filtertable("YearOfMade");
                }
                else if (search_select_cmb.SelectedIndex == 2)
                {
                    filtertable("Transmissiontype");
                }
                else if (search_select_cmb.SelectedIndex == 3)
                {
                    filtertable("FuelType");
                }
                else if (search_select_cmb.SelectedIndex == 4)
                {
                    filtertable("Price>");
                }
            }
            else
            {
                loadtable();
            }
            try
            {
                getsupplierdetails();
                getwarrentydetails();
                getvehicledetails();
                getbrandmodel();
            }
            catch (Exception) { }
        }
        private void getsupplierdetails()
        {
            con.Open();
            cmd = new SqlCommand("select * from Supplier where NIC = '" + supID + "'", con);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                if (!dr.IsDBNull(0))
                {
                    txt_supplier_name.Text = dr.GetString(1);

                }
            }
            con.Close();
        }
        private void getwarrentydetails()
        {
            con.Open();
            cmd = new SqlCommand("select *from Warranty where VehicleID = '" + vehID + "'", con);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                if (!dr.IsDBNull(1))
                {
                    txt_service_provider.Text = dr.GetString(5);
                    txt_period.Text = (dr.GetInt32(2)).ToString();
                    if (!dr.IsDBNull(3))
                        txt_startdate.Text = dr.GetString(3);
                    if (!dr.IsDBNull(4))
                        txt_enddate.Text = dr.GetString(4);
                }
                else
                {
                    txt_service_provider.Text = "";
                    txt_period.Text = "";
                    txt_startdate.Text = "";
                    txt_enddate.Text = "";
                }
            }
            con.Close();
        }

        private void scan_btn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void delete_btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                con.Open();
                cmd = new SqlCommand("delete Warranty where VehicleID='" + vehID + "'", con);
                int i = cmd.ExecuteNonQuery();
                con.Close();
                if (i == 1)
                {
                    MessageBox.Show("Warrenty Details Deleted", "Done...!", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    con.Open();
                    cmd = new SqlCommand("delete Vehicle where VehicleID='" + vehID + "'", con);
                    int i1 = cmd.ExecuteNonQuery();
                    if (i1 == 1)
                    {
                        MessageBox.Show("Vehicle Details Deleted", "Done...!", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    }
                    else
                    {
                        MessageBox.Show("Vehicle Details Not Deleted", "Error...!", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    con.Close();
                }
                else
                {
                    MessageBox.Show("Warrenty Details Not Deleted", "Error...!", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
            catch (SqlException)
            {
                MessageBox.Show("Cannot delete vehicle from System", "Error...!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong", "Error...!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void getvehicledetails()
        {
            car_image.Source = null;
            con.Open();
            cmd = new SqlCommand("select Vehiclehphoto,Price,VehicleStatus from Vehicle where VehicleID='" + vehID + "'", con);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                //string location = dr.GetString(0);
                if (!dr.IsDBNull(0))
                {
                    string location = dr.GetString(0);
                    if (!(string.IsNullOrEmpty(location)))
                    {
                        Bitmap carimg = new Bitmap($"{location}");
                        BitmapImage bi = carimg.ToBitmapImage();
                        car_image.Source = bi;
                    }
                }
                if (!dr.IsDBNull(1))
                    txt_price.Text = (dr.GetDecimal(1)).ToString();
                if (!dr.IsDBNull(2))
                    txt_availability.Text = dr.GetString(2);
            }
            con.Close();
        }
        private void getbrandmodel()
        {
            con.Open();
            cmd = new SqlCommand("select MCID,ModelName from Model where modelID='" + typeID + "'", con);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                string brand = dr.GetString(0), model = dr.GetString(1);
                if (!dr.IsDBNull(0))
                {
                    txt_brand_model.Text = ($"{brand} {model}");
                }
            }
            con.Close();
        }
    }
}

