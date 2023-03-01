using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using static System.Char;
using static System.String;

namespace Car_Sale
{
    /// <summary>
    /// Interaction logic for Supplier.xaml
    /// </summary>
    public partial class Supplier : UserControl
    {
        int usertype = 0;
        public Supplier(int type)
        {
            InitializeComponent();
            usertype = type;
        }
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter da;
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //con = new SqlConnection("Data Source=DESKTOP-0UIQMQB;Initial Catalog=CarSaleNew;Integrated Security=True");
            //con = new SqlConnection("Data Source=Sajana;Initial Catalog=DB_VehicleSale;Integrated Security=True");
            con = new SqlConnection("Data Source=NOTEBOOK-MALITH;Initial Catalog=CarSaleNew;Integrated Security=True");
            loadtable();
        }
        private void clearvalues()
        {
            txt_Supplier_ID.Text = "";
            txt_First_Name.Text = "";
            txt_Last_Name.Text = "";
            txt_Telephone.Text = "";
            txt_Email.Text = "";

        }
        private void loadtable()
        {
            Save.Visibility = Visibility.Hidden;
            Supplier_view.DataContext = null;
            try
            {
                con.Open();
                da = new SqlDataAdapter("select NIC,FirstName,LastName,Telephone,Email from Supplier", con);
                DataSet ds = new DataSet();
                da.Fill(ds, "LoadDataBinding");
                Supplier_view.DataContext = ds;
                con.Close();
            }
            catch (SqlException)
            {
                MessageBox.Show("Database connection did not work!", "Cannot find Sale!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (TimeoutException)
            {
                MessageBox.Show("Your Connection Time Out! Please sure your connection is real!", "Time Out of Sale!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (TimeZoneNotFoundException)
            {
                MessageBox.Show("Can't find real Time Zone!", "Time Zone Not Found in Sale!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (InvalidTimeZoneException)
            {
                MessageBox.Show("Your current TimeZone is useless!... Please! setup real Time Zone!", "Sale has Invalid Time Zone!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (InvalidProgramException)
            {
                MessageBox.Show("You have invalid Microsoft Intermediate Language or missing it! Please!... install MSIL and related metadata! ", "MSIL Not Found!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception)
            {
                MessageBox.Show("Something is going wrong in Sale!", "Oops!...", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (usertype == 0 || usertype == 1)
            {
                if (IsNullOrEmpty(txt_Supplier_ID.Text) || IsNullOrWhiteSpace(txt_Supplier_ID.Text))
                {
                    ClearErrors();
                    txt_error_Supplier_NIC.Text = "NIC cannot be blank!";
                    txt_Supplier_ID.Focus();
                }
                else if (!Regex.IsMatch(txt_Supplier_ID.Text, @"^([0-9]{9}[x|X|v|V]|[0-9]{12})$"))
                {
                    ClearErrors();
                    txt_error_Supplier_NIC.Text = "This does not like NIC!";
                    txt_Supplier_ID.Focus();
                }
                else if (IsNullOrEmpty(txt_First_Name.Text) || IsNullOrWhiteSpace(txt_First_Name.Text) || txt_First_Name.Text.Any(IsSymbol) || txt_First_Name.Text.Any(IsDigit))
                {
                    ClearErrors();
                    txt_error_First_Name.Text = "First Name cannot be symbols, digits or blank!";
                    txt_First_Name.Focus();
                }

                else if (IsNullOrEmpty(txt_Last_Name.Text) || IsNullOrWhiteSpace(txt_Last_Name.Text) || txt_Last_Name.Text.Any(IsSymbol) || txt_Last_Name.Text.Any(IsDigit))
                {
                    ClearErrors();
                    txt_error_Last_Name.Text = "Last Name cannot be symbols, digits or blank like First Name!";
                    txt_Last_Name.Focus();
                }

                else if (IsNullOrEmpty(txt_Telephone.Text) || IsNullOrWhiteSpace(txt_Telephone.Text))
                {
                    ClearErrors();
                    txt_error_Telephone.Text = "Phone Number cannot be blank!";
                    txt_Telephone.Focus();
                }
                else if (txt_Telephone.Text.Any(IsLetter) || txt_Telephone.Text.Length != 10)
                {
                    ClearErrors();
                    txt_error_Telephone.Text = "Invalid Phone Number!";
                    txt_Telephone.Focus();
                }
                else if (!Regex.IsMatch(txt_Telephone.Text, @"^7|0|(?:\+94)[0-9]{9,10}$"))
                {
                    ClearErrors();
                    txt_error_Telephone.Text = "Invalid Phone Number!";
                    txt_Telephone.Focus();
                }
                else if (IsNullOrEmpty(txt_Email.Text) || IsNullOrWhiteSpace(txt_Email.Text))
                {
                    ClearErrors();
                    txt_error_Email.Text = "Email cannot be blank!";
                    txt_Email.Focus();
                }
                else if (!Regex.IsMatch(txt_Email.Text, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"))
                {
                    ClearErrors();
                    txt_error_Email.Text = "Invalid Email!";
                    txt_Email.Focus();
                }
                else
                {
                    ClearErrors();
                    try
                    {
                        con.Open();

                        ClearErrors();

                        cmd = new SqlCommand("insert into Supplier (NIC,FirstName,LastName,Telephone,Email) values ('" + txt_Supplier_ID.Text + "','" + txt_First_Name.Text + "','" + txt_Last_Name.Text + "'," +
                            "'" + txt_Telephone.Text + "','" + txt_Email.Text + "')", con);
                        int i = cmd.ExecuteNonQuery();
                        if (i == 1)
                        {
                            MessageBox.Show("New Supplier have been added Successfully", "Data Saved !", MessageBoxButton.OK, MessageBoxImage.Information);

                        }
                        else { }

                        con.Close();
                        cmd.Dispose();
                    }
                    catch (FormatException)
                    {
                        MessageBox.Show("You trying to input invalid something!.. Please check again!", "Invalid Input Found!", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    catch (SqlException)
                    {
                        MessageBox.Show("Supplier Not saved successfully\n*If Supplier ID Already Excits Data won't be saved\n Please Check the Supplier ID again !!!", "Error !", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    catch (TimeoutException)
                    {
                        MessageBox.Show("Your Connection Time Out! Please sure your connection is real!", "Time Out!", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    catch (InvalidTimeZoneException)
                    {
                        MessageBox.Show("Your current TimeZone is useless!... Please! setup real Time Zone!", "Invalid Time Zone!", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    catch (TimeZoneNotFoundException)
                    {
                        MessageBox.Show("Can't find your Time Zone! Make sure you are in valid Time Zone or check BIOS!", "Time Zone Not Found!", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    catch (InvalidProgramException)
                    {
                        MessageBox.Show("You have invalid Microsoft Intermediate Language or missing it! Please!... install MSIL and related metadata! ", "MSIL Not Found!", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Something is going wrong! Please check what you trying to insert!", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                    clearvalues();
                    loadtable();
                }
            }
            else
            {
                MessageBox.Show("Your User type cannot do this operation", "Error...!", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        void ClearErrors()
        {
            txt_error_Email.Text = null;
            txt_error_First_Name.Text = null;
            txt_error_Last_Name.Text = null;
            txt_error_Supplier_NIC.Text = null;
            txt_error_Telephone.Text = null;
        }

        private void Btn_Clear_Click(object sender, RoutedEventArgs e)
        {
            Save.Visibility = Visibility.Visible;
            mode_txt.Text = "Now you are in Add Supplier mode";
            clearvalues();
            txt_Supplier_ID.IsReadOnly = false;
            txt_First_Name.IsReadOnly = false;
            txt_Last_Name.IsReadOnly = false;
            txt_Telephone.IsReadOnly = false;
            txt_Email.IsReadOnly = false;
            instruction_txt.Visibility = Visibility.Hidden;
        }

        private void Supplier_view_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            Save.Visibility = Visibility.Hidden;
            txt_Supplier_ID.IsReadOnly = true;
            txt_First_Name.IsReadOnly = true;
            txt_Last_Name.IsReadOnly = true;
            txt_Telephone.IsReadOnly = true;
            txt_Email.IsReadOnly = true;
            instruction_txt.Visibility = Visibility.Visible;

            DataGrid gd = (DataGrid)sender;
            DataRowView row_selected = gd.SelectedItem as DataRowView;
            if (row_selected != null)
            {
                mode_txt.Text = "Now you are in Update and Delete mode Press Clear to goto Add Supplier";
                txt_Supplier_ID.Text = row_selected["NIC"].ToString();
                txt_First_Name.Text = row_selected["FirstName"].ToString();
                txt_Last_Name.Text = row_selected["LastName"].ToString();
                txt_Telephone.Text = row_selected["Telephone"].ToString();
                txt_Email.Text = row_selected["Email"].ToString();
            }
        }

        private void update_btn_Click(object sender, RoutedEventArgs e)
        {
            if (usertype == 0 || usertype == 1)
            {
                try
                {
                    con.Open();
                    cmd = new SqlCommand("update Supplier set FirstName = '" + txt_First_Name.Text + "' , LastName = '" + txt_Last_Name.Text + "', " +
                        "Telephone='" + txt_Telephone.Text + "', Email='" + txt_Email.Text + "' where NIC = '" + txt_Supplier_ID.Text + "'", con);
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
                    MessageBox.Show("Supplier Not updated successfully\n*If Supplier ID Already Excits Data won't be saved\n Please Check the Supplier ID again !!!", "Error !", MessageBoxButton.OK, MessageBoxImage.Error);
                    throw;
                }
                catch (Exception)
                {
                    MessageBox.Show("Something went wrong please check all error messages else\n*The your user type dosen't" +
                        "have permissions to do this operation\n*The data is already Exists \n*Close this menu and reload it and try to do this operation again", "Error !", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                clearvalues();
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
                    MessageBox.Show("Supplier Not Deleted successfully\nThe Below resons can be occur\n *The supply of this Supplier already exsits\n*Hint :- Remove all the supplies from this supplier and try again to delete", "Error !", MessageBoxButton.OK, MessageBoxImage.Error);
                    loadtable();
                }
                catch (Exception)
                {
                    MessageBox.Show("Something went wrong please check all error messages else\n*The your user type dosen't" +
                        "have permissions to do this operation\n*The data is already Exists \n*Close this menu and reload it and try to do this operation again", "Error !", MessageBoxButton.OK, MessageBoxImage.Error);
                    loadtable();
                }
                clearvalues();
                loadtable();
            }
            else
            {
                MessageBox.Show("Your user type cannot do this operation", "Invalid User...!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Supplier_view_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            //Supplier_view.Items.Refresh();
            txt_Supplier_ID.IsReadOnly = true;
            txt_First_Name.IsReadOnly = true;
            txt_Last_Name.IsReadOnly = true;
            txt_Telephone.IsReadOnly = true;
            txt_Email.IsReadOnly = true;

            DataGrid gd = (DataGrid)sender;
            DataRowView row_selected = gd.SelectedItem as DataRowView;
            if (row_selected != null)
            {
                mode_txt.Text = "Now you are in Update and Delete mode Press Clear to goto Add Supplier";
                txt_Supplier_ID.Text = row_selected["NIC"].ToString();
                txt_First_Name.Text = row_selected["FirstName"].ToString();
                txt_Last_Name.Text = row_selected["LastName"].ToString();
                txt_Telephone.Text = row_selected["Telephone"].ToString();
                txt_Email.Text = row_selected["Email"].ToString();
            }
        }
    }
}
