using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;


namespace Car_Sale
{
    /// <summary>
    /// Interaction logic for Livechart.xaml
    /// </summary>
    public partial class Livechart : UserControl
    {
        int usertype = 0;
        public Livechart(int type)
        {
            InitializeComponent();
            usertype = type;
        }

        SqlConnection con;

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (usertype == 0 || usertype == 1)
            {
                loadchart();
                loadchart_available_and_not();
                
            }
            else
            {
                MessageBox.Show("Your user type is not have access to live charts", "Error...!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void loadchart()
        {
            //con = new SqlConnection("Data Source=DESKTOP-0UIQMQB;Initial Catalog=CarSaleNew;Integrated Security=True");
            con = new SqlConnection("Data Source=NOTEBOOK-MALITH;Initial Catalog=CarSaleNew;Integrated Security=True");
            //con = new SqlConnection("Data Source=Sajana;Initial Catalog=DB_VehicleSale;Integrated Security=True");

            try
            {
                con.Open();
                SqlCommand countSaleQuery = new SqlCommand("Select Count(SaleID) from Sale", con);
                int countSale = Convert.ToInt32(countSaleQuery.ExecuteScalar());

                SqlCommand countVehQuery = new SqlCommand("Select Count(VehicleID) from Vehicle", con);
                int countVeh = Convert.ToInt32(countVehQuery.ExecuteScalar());

                SqlCommand countMCQuery = new SqlCommand("Select Count(MCName) from Manufactured_Campany", con);
                int countMC = Convert.ToInt32(countMCQuery.ExecuteScalar());

                SqlCommand countMODQuery = new SqlCommand("Select Count(modelID) from Model", con);
                int countMOD = Convert.ToInt32(countMODQuery.ExecuteScalar());

                SqlCommand countSupplierQuery = new SqlCommand("Select Count(NIC) from Supplier", con);
                int countSupplier = Convert.ToInt32(countSupplierQuery.ExecuteScalar());

                SqlCommand countSalesmanQuery = new SqlCommand("Select Count(SalesmanID) from Salesman", con);
                int countSalesman = Convert.ToInt32(countSalesmanQuery.ExecuteScalar());

                SqlCommand countCustomerQuery = new SqlCommand("Select Count(CustomerNIC) from Customer", con);
                int countCustomer = Convert.ToInt32(countCustomerQuery.ExecuteScalar());

                Car_sale_pie.Series.Clear();
                Car_sale_pie.Series.Add(new PieSeries { DataLabels = true, Title = "Sale", StrokeThickness = 0, Values = new ChartValues<int> { countSale } });
                Car_sale_pie.Series.Add(new PieSeries { DataLabels = true, Title = "Vehicle", StrokeThickness = 0, Values = new ChartValues<int> { countVeh } });
                Car_sale_pie.Series.Add(new PieSeries { DataLabels = true, Title = "Manufactured Company", StrokeThickness = 0, Values = new ChartValues<int> { countMC } });
                Car_sale_pie.Series.Add(new PieSeries { DataLabels = true, Title = "Model", StrokeThickness = 0, Values = new ChartValues<int> { countMOD } });
                Car_sale_pie.Series.Add(new PieSeries { DataLabels = true, Title = "Supplier", StrokeThickness = 0, Values = new ChartValues<int> { countSupplier } });
                Car_sale_pie.Series.Add(new PieSeries { DataLabels = true, Title = "Salesman", StrokeThickness = 0, Values = new ChartValues<int> { countSalesman } });
                Car_sale_pie.Series.Add(new PieSeries { DataLabels = true, Title = "Customer", StrokeThickness = 0, Values = new ChartValues<int> { countCustomer } });


                countSaleQuery.Dispose();
                countVehQuery.Dispose();
                countMCQuery.Dispose();
                countMODQuery.Dispose();
                countSupplierQuery.Dispose();
                countSalesmanQuery.Dispose();
                countCustomerQuery.Dispose();

                con.Close();
            }
            catch (SqlException)
            {
                MessageBox.Show("Database connection in a vulnerability or not data inserted yet!", "Warrning!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (TimeoutException)
            {
                MessageBox.Show("Your Connection Time Out! Please sure your connection is real!", "Time Out!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (TimeZoneNotFoundException)
            {
                MessageBox.Show("Can't find real Time Zone!", "Time Zone Not Found!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (InvalidTimeZoneException)
            {
                MessageBox.Show("Your current TimeZone is useless!... Please! setup real Time Zone!", "Invalid Time Zone!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (InvalidProgramException)
            {
                MessageBox.Show("You have invalid Microsoft Intermediate Language or missing it! Please!... install MSIL and related metadata! ", "MSIL Not Found!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception)
            {
                MessageBox.Show("Something is going wrong!", "Oops!...", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void loadchart_available_and_not()
        {
            //con = new SqlConnection("Data Source=DESKTOP-0UIQMQB;Initial Catalog=CarSaleNew;Integrated Security=True");
            con = new SqlConnection("Data Source=NOTEBOOK-MALITH;Initial Catalog=CarSaleNew;Integrated Security=True");
            //con = new SqlConnection("Data Source=Sajana;Initial Catalog=DB_VehicleSale;Integrated Security=True");

            try
            {
                con.Open();

                SqlCommand countAVehQuery = new SqlCommand("Select Count(VehicleID) from Vehicle where VehicleStatus = 'Available'", con);
                int countAVeh = Convert.ToInt32(countAVehQuery.ExecuteScalar());

                SqlCommand countSoldVehQuery = new SqlCommand("Select Count(VehicleID) from Vehicle where VehicleStatus = 'Sold'", con);
                int countSoldVeh = Convert.ToInt32(countSoldVehQuery.ExecuteScalar());

                Veh_Avilable_pie.Series.Clear();
                Veh_Avilable_pie.Series.Add(new PieSeries { DataLabels = true, Title = "Available", StrokeThickness = 0, Values = new ChartValues<int> { countAVeh } });
                Veh_Avilable_pie.Series.Add(new PieSeries { DataLabels = true, Title = "Sold", StrokeThickness = 0, Values = new ChartValues<int> { countSoldVeh } });

                countAVehQuery.Dispose();
                countSoldVehQuery.Dispose();

                con.Close();
            }
            catch (SqlException)
            {
                MessageBox.Show("Database connection in a vulnerability or not data inserted yet!", "Warring!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (TimeoutException)
            {
                MessageBox.Show("Your Connection Time Out! Please sure your connection is real!", "Time Out!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (TimeZoneNotFoundException)
            {
                MessageBox.Show("Can't find real Time Zone!", "Time Zone Not Found!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (InvalidTimeZoneException)
            {
                MessageBox.Show("Your current TimeZone is useless!... Please! setup real Time Zone!", "Invalid Time Zone!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (InvalidProgramException)
            {
                MessageBox.Show("You have invalid Microsoft Intermediate Language or missing it! Please!... install MSIL and related metadata! ", "MSIL Not Found!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception)
            {
                MessageBox.Show("Something is going wrong!", "Oops!...", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        
    }
}
