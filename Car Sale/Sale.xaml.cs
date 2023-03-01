using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Car_Sale
{
    /// <summary>
    /// Interaction logic for Sale.xaml
    /// </summary>
    public partial class Sale : UserControl
    {
        public Sale()
        {
            InitializeComponent();
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
            loadbillNumberDate();
        }

        private void Btn_veh_Search_Click(object sender, RoutedEventArgs e)
        {
            Car_view.DataContext = null;

            try
            {
                con.Open();
                da = new SqlDataAdapter("select ModelName, Condition, YearOfMade, Price from Vehicle, model where Vehicle.MCID = model.modelID and VehicleID = '" + txt_vehicle_id.Text + "' and VehicleStatus like 'Available'", con);
                DataSet ds = new DataSet();
                da.Fill(ds, "LoadDataBinding");
                Car_view.DataContext = ds;
                con.Close();

                if (string.IsNullOrEmpty(carname))
                {
                    MessageBox.Show("*The car ID you entered is no valid\n*The car is Sold under this car ID\n *Or contact System Admistrator", "Unable to load Data !", MessageBoxButton.OK, MessageBoxImage.Error);
                    txt_vehicle_id.Clear();
                    txt_vehicle_id.Focus();
                }
                //else
                //{
                getcarpicture();
                loadvehicledata();
                //}
            }
            catch (SqlException)
            {
                MessageBox.Show("Something wrong with the database\n*Unable to load car details", "Error !", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong\n***Hint**\nReopen the menu and try !", "Error !", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            //getcarpicture();
            //loadvehicledata();

        }
        private void getcarpicture()
        {
            try
            {
                string location = "";
                con.Open();
                cmd = new SqlCommand("select Vehiclehphoto from vehicle where VehicleID='" + txt_vehicle_id.Text + "'", con);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    if (!dr.IsDBNull(0))
                    {
                        location = dr.GetString(0);
                        Bitmap carimg = new Bitmap($"{location}");
                        BitmapImage bi = carimg.ToBitmapImage();
                        carimageviewer.Source = bi;
                    }
                }
                con.Close();
            }
            catch (SqlException)
            {
                MessageBox.Show("Unable to load car Image details", "Error !", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong", "Error !", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        string carname = "";
        private void Car_view_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            DataGrid dg = (DataGrid)sender;
            DataRowView rowselected = dg.SelectedItem as DataRowView;
            if (rowselected != null)
            {
                carname = rowselected["ModelName"].ToString();
                txt_price.Text = rowselected["Price"].ToString();
            }
        }

        private void Car_view_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {

        }
        string name = "", email = "";
        private void Customer_view_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            DataGrid dg = (DataGrid)sender;
            DataRowView rowselected = dg.SelectedItem as DataRowView;
            if (rowselected != null)
            {
                name = rowselected["FirstName"].ToString();
                email = rowselected["Email"].ToString();
            }
        }

        private void Customer_view_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {

        }

        private void Btn_cus_Search_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                con.Open();
                da = new SqlDataAdapter("select CustomerNIC,FirstName,Telephone,Email from Customer where CustomerNIC ='" + txt_Cus_id.Text + "' or Telephone='" + txt_Cus_id.Text + "'", con);
                DataSet ds = new DataSet();
                da.Fill(ds, "LoadDataBinding");
                Customer_view.DataContext = ds;

                if (string.IsNullOrEmpty(name))
                {
                    MessageBox.Show("*The Customer ID you entered is not valid\n*The Customer not a regular one please add ID\n *Or contact System Admistrator", "Unable to load Data !", MessageBoxButton.OK, MessageBoxImage.Error);
                    txt_Cus_id.Clear();
                    txt_Cus_id.Focus();
                }

                con.Close();
                txt_cus_id.Text = txt_Cus_id.Text;
            }
            catch (SqlException)
            {
                MessageBox.Show("Unable to load customer details", "Error !", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong", "Error !", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void Btn_sup_Search_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Btn_Addcus_Search_Click(object sender, RoutedEventArgs e)
        {
            Addcustomer obj = new Addcustomer();
            obj.Show();
        }

        private void Btn_salesman_Search_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string[] name = { "First Name", "Last Name" };
                string id = "", location;
                con.Open();
                cmd = new SqlCommand("select SalesmanID,FirstName,LastName,ProfilePicture from salesman where SalesmanID='" + txt_Salesman_id.Text + "' or NIC ='" + txt_Salesman_id.Text + "'", con);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    if (!dr.IsDBNull(0))
                    {
                        id = dr.GetString(0);
                        name[0] = dr.GetString(1);
                        name[1] = dr.GetString(2);
                        location = dr.GetString(3);
                        if (!(string.IsNullOrEmpty(location)))
                        {
                            Bitmap smpic = new Bitmap($"{location}");
                            BitmapImage bi = smpic.ToBitmapImage();
                            img_salesman.Source = bi;
                        }
                        txt_Salesman_id.Text = id;
                        txt_Salesman_name.Text = ($"{name[0]} {name[1]}");
                    }
                    else
                    {
                        MessageBox.Show("Salesman ID you entered is invalid please check again", "Error...!", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                con.Close();
                if (string.IsNullOrEmpty(txt_Salesman_name.Text))
                {
                    MessageBox.Show("*The Salesman ID you entered is not valid\n*Or contact System Admistrator", "Unable to load Data !", MessageBoxButton.OK, MessageBoxImage.Error);
                    txt_Salesman_id.Clear();
                    txt_Salesman_id.Focus();
                }
            }
            catch (SqlException)
            {
                MessageBox.Show("Unable to load salesman details", "Error !", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong", "Error !", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void rdb_mr_Click(object sender, RoutedEventArgs e)
        {

        }

        private void rdb_mr_Checked(object sender, RoutedEventArgs e)
        {
            txt_cus_name.Text = ($"Mr. {name}");
        }

        private void rdb_mrs_Checked(object sender, RoutedEventArgs e)
        {
            txt_cus_name.Text = ($"Mrs. {name}");
        }

        private void rdb_miss_Checked(object sender, RoutedEventArgs e)
        {
            txt_cus_name.Text = ($"Miss. {name}");
        }

        private void rdb_finance_Checked(object sender, RoutedEventArgs e)
        {



        }

        private void rdb_Cash_Checked(object sender, RoutedEventArgs e)
        {


        }

        private void Btn_Save_Click(object sender, RoutedEventArgs e)
        {
            //savecontent();
            double tot = 0, finan = 0, cuscash = 0, cash = 0;
            if (string.IsNullOrEmpty(txt_vehicle_id.Text))
            {
                MessageBox.Show("Cannot Complete Sale Please Select a Vehicle", "Error...!", MessageBoxButton.OK, MessageBoxImage.Error);
                txt_vehicle_id.Focus();
            }
            else if (string.IsNullOrEmpty(txt_cus_id.Text))
            {
                MessageBox.Show("Cannot Complete Sale Please Select a Customer", "Error...!", MessageBoxButton.OK, MessageBoxImage.Error);
                txt_cus_id.Focus();
            }
            else if (string.IsNullOrEmpty(txt_Salesman_name.Text))
            {
                MessageBox.Show("Cannot Complete Sale Please Select a Salesman", "Error...!", MessageBoxButton.OK, MessageBoxImage.Error);
                txt_Salesman_id.Focus();
            }
            else if (string.IsNullOrEmpty(txt_cus_name.Text))
            {
                MessageBox.Show("Cannot Complete Enter Customer Name\n*(Select Customer MR./ MRS./ Miss)", "Error...!", MessageBoxButton.OK, MessageBoxImage.Error);
                txt_cus_name.Focus();
            }
            else if (txt_discount.Text.Any(char.IsLetter))
            {
                MessageBox.Show("Discount Amount Cannot Have Letters", "Error...!", MessageBoxButton.OK, MessageBoxImage.Error);
                txt_discount.Focus();
            }
            else if (pay_name.SelectedIndex == -1)
            {
                MessageBox.Show("Cannot Complete Select Payment Type", "Error...!", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            else if ((pay_name.SelectedIndex == 1) && (txt_finance_amount.Text.Any(char.IsLetter)))
            {
                //double tot = 0, finan = 0, cuscash = 0,cash=0;
                MessageBox.Show("Finance Amount Cannot Have Letters", "Error...!", MessageBoxButton.OK, MessageBoxImage.Error);
                txt_finance_amount.Focus();
            }
            else if ((pay_name.SelectedIndex == 1) && (txt_customer_amount.Text.Any(char.IsLetter)))
            {
                MessageBox.Show("Customer Amount Cannot Have Letters", "Error...!", MessageBoxButton.OK, MessageBoxImage.Error);
                txt_customer_amount.Focus();
            }
            else if ((pay_name.SelectedIndex == 1) && (string.IsNullOrEmpty(txt_finance_amount.Text)))
            {
                MessageBox.Show("Please Enter Finance Amount", "Error...!", MessageBoxButton.OK, MessageBoxImage.Error);
                txt_finance_amount.Focus();
            }
            else if ((pay_name.SelectedIndex == 1) && (string.IsNullOrEmpty(txt_customer_amount.Text)))
            {
                MessageBox.Show("Please Enter Customer Amount", "Error...!", MessageBoxButton.OK, MessageBoxImage.Error);
                txt_customer_amount.Focus();

                tot = Convert.ToDouble(txt_total_.Text);
                finan = Convert.ToDouble(txt_finance_amount.Text);
                cuscash = Convert.ToDouble(txt_customer_amount.Text);
                finan = finan + cuscash;
            }
            else if ((pay_name.SelectedIndex == 1) && (string.IsNullOrEmpty(txt_check_id.Text)))
            {
                MessageBox.Show("Please Enter Check ID", "Error...!", MessageBoxButton.OK, MessageBoxImage.Error);
                txt_check_id.Focus();
            }
            else if ((pay_name.SelectedIndex == 1) && (tot != finan))
            {
                MessageBox.Show("Customer amount and finance amount are not matching with total amount", "Error...!", MessageBoxButton.OK, MessageBoxImage.Error);
                txt_customer_amount.Focus();
                txt_finance_amount.Focus();
            }
            else if ((pay_name.SelectedIndex == 0) && (string.IsNullOrEmpty(txt_cash_amount.Text)))
            {
                MessageBox.Show("Please Enter Cash Amount", "Error...!", MessageBoxButton.OK, MessageBoxImage.Error);
                txt_cash_amount.Focus();
            }
            else if ((pay_name.SelectedIndex == 0) && (txt_cash_amount.Text.Any(char.IsLetter)))
            {
                MessageBox.Show("Cash Amount cannot have letters", "Error...!", MessageBoxButton.OK, MessageBoxImage.Error);
                txt_cash_amount.Focus();
            }
            else if ((pay_name.SelectedIndex == 0) && (tot != cash))
            {
                MessageBox.Show("Total amount and Cash Amount not matching", "Error...!", MessageBoxButton.OK, MessageBoxImage.Error);
                txt_cash_amount.Focus();
            }
            else
                savecontent();

        }
        private void savecontent()
        {
            newbill();
            saletable();
            updatecar();
            Invoice obj = new Invoice(txt_vehicle_details.Text, txt_cus_name.Text, txt_cus_id.Text, txt_bill_id.Text, txt_date.Text, txt_cash_amount.Text, txt_finance_amount.Text, txt_customer_amount.Text, txt_total_.Text, email);
            obj.Show();
        }

        private void Btn_Clear_Click(object sender, RoutedEventArgs e)
        {

        }
        private void loadbillNumberDate()
        {
            string bill_no = "BILL-10000000";
            try
            {
                con.Open();
                cmd = new SqlCommand("select max(BillID) from Bill", con);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    if (!dr.IsDBNull(0))
                    {
                        bill_no = dr.GetString(0);
                    }
                }
                con.Close();
            }
            catch (SqlException)
            {
                MessageBox.Show("Unable to load bill Number\nPlease close the menu an reopen", "Error...!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong while loading bill", "Error...!", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            string[] id = bill_no.Split('-');
            var id_no = Convert.ToInt32(id[1]);
            id_no = id_no + 2;
            txt_bill_id.Text = id[0] + "-" + id_no.ToString();

            txt_date.Text = (DateTime.Now.Date).ToString();
        }
        private void loadvehicledata()
        {
            string yom = "", engno = "", cassino = "", capacity = "", condition = "", plate = "", price = "", type = "", brand = "", model = "", warrenty = "";
            try
            {
                if (!(string.IsNullOrEmpty(txt_vehicle_id.Text)))
                {
                    con.Open();
                    cmd = new SqlCommand("select * from vehicle where vehicleID ='" + txt_vehicle_id.Text + "'", con);
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        yom = dr.GetString(3);
                        engno = dr.GetString(5);
                        cassino = dr.GetString(4);
                        capacity = dr.GetString(6);
                        condition = dr.GetString(8);
                        plate = dr.GetString(18);
                        price = (dr.GetDecimal(20)).ToString();
                        type = (dr.GetInt32(1)).ToString();
                    }
                    con.Close();

                    con.Open();
                    cmd = new SqlCommand("select * from Model where modelID='" + type + "'", con);
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        brand = dr.GetString(1);
                        model = dr.GetString(2);
                    }
                    con.Close();
                }
            }
            catch (SqlException)
            {
                MessageBox.Show("Unable to load Vehicle Basic Data Try Again", "Error..!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception)
            {
                //MessageBox.Show(ex.Message);
                MessageBox.Show("Something Went Wrong while loading vehicle Please Check Again", "Error...!", MessageBoxButton.OK, MessageBoxImage.Error);
            }


            warrenty = loadwarrenty();

            txt_vehicle_details.Text = ($"CAR DETAILS\n\n{brand} {model} ({condition})\tYOM{yom}\nENGINE NUMBER\t\t{engno}\nCHASSI NUMBER\t\t{cassino}" +
                $"\nENGINE CAPACITY\t{capacity}\nPLATE NUMEBR\t\t{plate} \n***(If Registered)***" +
                $"\nBASE PRICE\t\t{price}\n{warrenty}");
        }
        private void newbill()
        {
            string paytype = "";
            if (pay_name.SelectedIndex == 1)
            {
                paytype = "Finance";
                txt_cash_amount.Text = "0.00";
            }
            else if (pay_name.SelectedIndex == 0)
            {
                paytype = "Cash";
                txt_finance_amount.Text = "0.00";
                txt_customer_amount.Text = "0.00";
                txt_check_id.Text = "CXXXXX00";
            }

            try
            {
                con.Open();
                cmd = new SqlCommand("insert into Bill values('" + txt_bill_id.Text + "','" + txt_cus_id.Text + "','" + txt_date.Text + "','" + txt_discount.Text + "'," +
                    "'" + txt_price.Text + "','" + paytype + "','" + txt_finance_amount.Text + "','" + txt_customer_amount.Text + "'," +
                    "'" + txt_check_id.Text + "','" + txt_total_.Text + "')", con);
                int i = cmd.ExecuteNonQuery();
                if (i == 1)
                {
                    MessageBox.Show("Bill data successfully Saved", "Done...!", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Bill data not saved", "Error...!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                con.Close();
            }
            catch (SqlException)
            {
                MessageBox.Show("Unable to Save data into database\n***Please Contact System Administrator***", "Error...!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong", "Error !", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void saletable()
        {
            try
            {
                con.Open();
                cmd = new SqlCommand("insert into Sale(VehicleID,BillID,SalesmanID,SoldDateTime,Commission) values" +
                    "('" + txt_vehicle_id.Text + "','" + txt_bill_id.Text + "','" + txt_Salesman_id.Text + "','" + txt_date.Text + "','" + txt_Salesman_Commission_name.Text + "')", con);

                int i = cmd.ExecuteNonQuery();
                if (i == 1)
                {
                    MessageBox.Show("The Sale was Successfully Done", "Sale Done...!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
                else
                {
                    MessageBox.Show("The Sale was not Done", "Sale not Done...!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                con.Close();
            }
            catch (SqlException)
            {
                MessageBox.Show("Sale Data not Saved", "Error...!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong in sale please contact the system Administrator", "Error...!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void updatecar()
        {
            try
            {
                con.Open();
                cmd = new SqlCommand("update vehicle set VehicleStatus='Sold' where VehicleID='" + txt_vehicle_id.Text + "'", con);
                int i = cmd.ExecuteNonQuery();
                if (i == 1)
                {
                    MessageBox.Show("Vehicle Data Updated");
                }
                else
                {
                    MessageBox.Show("Vehicle Data Not Updated", "Error...!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (SqlException)
            {
                MessageBox.Show("Vehicle Data Not Updated\n***Hint***\nUpdate Data manualy using invoice", "Error...!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong in sale please contact the system Administrator", "Error...!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void calculatediscount()
        {
            double price = 0, discount = 0;
            price = Convert.ToDouble(txt_price.Text);
            if (!(string.IsNullOrEmpty(txt_discount.Text)))
            {
                discount = Convert.ToDouble(txt_discount.Text);
            }
            txt_total_.Text = (price - discount).ToString();
        }

        private void txt_cash_Selected(object sender, RoutedEventArgs e)
        {
            stack_cash.Visibility = Visibility.Visible;
            txt_type.Text = "cash";

            stack_finance.Visibility = Visibility.Hidden;
            calculatediscount();
        }

        private void txt_finance_Selected(object sender, RoutedEventArgs e)
        {
            stack_finance.Visibility = Visibility.Visible;
            txt_type.Text = "finance";


            stack_cash.Visibility = Visibility.Hidden;
            calculatediscount();
        }

        private void pay_name_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (pay_name.SelectedIndex == 0)
            {
                txt_finance_amount.Text = txt_price.Text;
                txt_customer_amount.Text = txt_price.Text;
                txt_check_id.Text = "CXXXXXXXX";
            }
            else if (pay_name.SelectedIndex == 1)
            {
                txt_cash_amount.Text = txt_price.Text;
            }
        }

        private string loadwarrenty()
        {
            string period = "", sadate = DateTime.Now.ToString(), sprovider = "", wstatus = "", warentydetails = "No Warrenty Details !";
            con.Open();
            cmd = new SqlCommand("select * from warranty where vehicleID='" + txt_vehicle_id + "'", con);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                if (!dr.IsDBNull(0))
                {
                    period = (dr.GetInt32(0)).ToString();
                    sprovider = dr.GetString(5);
                    wstatus = dr.GetString(6);
                }
            }
            warentydetails = ($"Car Warrenty...\nBest Service Provider : \t\t{sprovider}\nPeriod : \t\t{period}\nThis is a {wstatus} Warrenty.");
            con.Close();
            try
            {

            }
            catch (SqlException)
            {
                MessageBox.Show("Unable to load Warrenty Details please reopen and try", "Error...!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show("Something went wrong in sale please contact the system Administrator", "Error...!", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return warentydetails;
        }
    }
}
