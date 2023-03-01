using Microsoft.Win32;
using System;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Media.Imaging;
using static System.Char;
using static System.String;

namespace Car_Sale
{
    /// <summary>
    /// Interaction logic for Addcustomer.xaml
    /// </summary>
    public partial class Addcustomer : Window
    {
        public Addcustomer()
        {
            InitializeComponent();
        }
        SqlConnection con;
        SqlCommand cmd;



        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //con = new SqlConnection("Data Source=DESKTOP-0UIQMQB;Initial Catalog=CarSaleNew;Integrated Security=True");
            //con = new SqlConnection("Data Source=Sajana;Initial Catalog=DB_VehicleSale;Integrated Security=True");
            con = new SqlConnection("Data Source=NOTEBOOK-MALITH;Initial Catalog=CarSaleNew;Integrated Security=True");

            rdb_male.IsChecked = true;
        }
        private void Btn_Browse_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog picture = new OpenFileDialog();
                if (picture.ShowDialog() == true)
                {
                    Uri extention = new Uri(picture.FileName);
                    BitmapImage Image_Customer_BitmapImage = new BitmapImage(extention);
                    //Image_Customer.Source = Image_Customer_BitmapImage;

                    JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(Image_Customer_BitmapImage));
                    using (MemoryStream ms = new MemoryStream())
                    {
                        encoder.Save(ms);
                    }
                }
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show(this, "Your file not here!", "File Not Found!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (DirectoryNotFoundException)
            {
                MessageBox.Show(this, "Directory not found! May be ejected or destroyed!", "Directory Not Found!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (IOException)
            {
                MessageBox.Show(this, "Directory is working but, Something is wrong with Input & Output!", "I/O Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (NotSupportedException)
            {
                MessageBox.Show(this, "You must select Images only!", "File Not Support!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (OutOfMemoryException)
            {
                MessageBox.Show(this, "You have not enough memory to run this application right now!", "Out Of Memory!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception)
            {
                MessageBox.Show(this, "Something is going wrong! Please check what you trying to insert!", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Btn_Save_Click(object sender, RoutedEventArgs e)
        {


            string gender = txt_Gender.Text;
            if (rdb_male.IsChecked == true)
            {
                gender = "Male";
                txt_Gender.Text = "male";

            }
            else if (rdb_female.IsChecked == true)
            {
                gender = "Female";
                txt_Gender.Text = "female";
            }
            if (IsNullOrEmpty(txt_Customer_NIC.Text) || IsNullOrWhiteSpace(txt_Customer_NIC.Text))
            {
                ClearErrors();
                txt_error_Customer_NIC.Text = "NIC cannot be blank!";
                txt_Customer_NIC.Focus();

            }
            else if (!Regex.IsMatch(txt_Customer_NIC.Text, @"^([0-9]{9}[x|X|v|V]|[0-9]{12})$"))
            {
                ClearErrors();
                txt_error_Customer_NIC.Text = "This does not like NIC!";
                txt_Customer_NIC.Focus();
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
            else if (IsNullOrEmpty(txt_Address_No.Text) || IsNullOrWhiteSpace(txt_Address_No.Text) || txt_Address_No.Text.Any(IsLetter))
            {
                ClearErrors();
                txt_error_Address_No.Text = "Address Number cannot be blank or letters!";
                txt_Address_No.Focus();
            }
            else if (IsNullOrEmpty(txt_First_Address_Line.Text) || IsNullOrWhiteSpace(txt_First_Address_Line.Text) || txt_First_Address_Line.Text.Any(IsSymbol))
            {
                ClearErrors();
                txt_error_First_Address_Line.Text = "Invalid Address Line!";
                txt_Address_No.Focus();
            }
            else if (txt_Second_Address_Line.Text.Any(IsSymbol))
            {
                ClearErrors();
                txt_error_Second_Address_Line.Text = "Invalid Address Line!";
                txt_Second_Address_Line.Focus();
            }
            else if (IsNullOrEmpty(txt_Profile_City.Text) || IsNullOrWhiteSpace(txt_Profile_City.Text) || txt_Profile_City.Text.Any(IsSymbol) || txt_Profile_City.Text.Any(IsDigit))
            {
                ClearErrors();
                txt_error_City.Text = "City cannot be symbol, digit or blank!";
                txt_Profile_City.Focus();
            }
            else
            {
                ClearErrors();
                try
                {
                    con.Open();

                    //SqlCommand checkNIC = new SqlCommand("Select CustomerNIC from Customer", con);
                    //SqlCommand checkTelephone = new SqlCommand("Select Telephone from Customer", con);
                    //SqlCommand checkEmail = new SqlCommand("Select Email from Customer", con);
                    //SqlCommand checkADNO = new SqlCommand("Select AddressNO from Customer", con);
                    //SqlCommand checkADLONE = new SqlCommand("Select FirstAddressLine from Customer", con);
                    //SqlCommand checkADLTWO = new SqlCommand("Select SecondAddressLine from Customer", con);
                    //SqlCommand checkCity = new SqlCommand("Select City from Customer", con);

                    //if (txt_Customer_NIC.Text.Equals(checkNIC.ExecuteNonQuery()) == true)
                    //{
                    //    txt_error_Customer_NIC.Text = "This NIC is already in use!";
                    //    txt_Customer_NIC.Focus();
                    //}
                    //else if (txt_Telephone.Text.Equals(checkTelephone.ExecuteNonQuery()) == true)
                    //{
                    //    txt_error_Telephone.Text = "This Telephone already exists!";
                    //    txt_Telephone.Focus();
                    //}
                    //else if (txt_Email.Text.Equals(checkEmail.ExecuteNonQuery()) == true)
                    //{
                    //    txt_error_Email.Text = "This Email already exists!";
                    //    txt_Email.Focus();
                    //}
                    //else if ((txt_Address_No.Text.Equals(checkADNO.ExecuteNonQuery()) && (txt_First_Address_Line.Text.Equals(checkADLONE.ExecuteNonQuery())) && (txt_Second_Address_Line.Text.Equals(checkADLTWO.ExecuteNonQuery())) && (txt_Profile_City.Text.Equals(checkCity.ExecuteNonQuery()))) == true)
                    //{
                    //    txt_error_Address_No.Text = "This Address already exists!";
                    //    txt_Address_No.Focus();
                    //    txt_First_Address_Line.Focus();
                    //    txt_Second_Address_Line.Focus();
                    //    txt_Profile_City.Focus();
                    //}
                    //else
                    //{


                    cmd = new SqlCommand("Insert into Customer values ('" + txt_Customer_NIC.Text + "','" + txt_First_Name.Text + "','" + txt_Last_Name.Text + "','" + gender + "'," +
                    "'" + txt_Telephone.Text + "','" + txt_Email.Text + "','" + txt_Address_No.Text + "','" + txt_First_Address_Line.Text + "','" + txt_Second_Address_Line.Text + "','" + txt_Profile_City.Text + "')", con);
                    int i = cmd.ExecuteNonQuery();
                    if (i == 1)
                    {
                        MessageBox.Show("Data Saved");
                    }
                    con.Close();
                    //}
                }
                catch (SqlException)
                {
                    MessageBox.Show(this, "Something is wrong in Database!", "Database Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (FormatException)
                {
                    MessageBox.Show("You entered something wrong! Please!... check again!", "Invalid Format!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (TimeoutException)
                {
                    MessageBox.Show(this, "Your Connection Time Out! Please sure your connection is real!", "Time Out!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (InvalidTimeZoneException)
                {
                    MessageBox.Show("Your current TimeZone is useless!... Please! setup real Time Zone!", "Invalid Time Zone!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (InvalidProgramException)
                {
                    MessageBox.Show(this, "You have invalid Microsoft Intermediate Language or missing it! Please!... install MSIL and related metadata! ", "MSIL Not Found!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (TimeZoneNotFoundException)
                {
                    MessageBox.Show(this, "Can't find your Time Zone! Make sure you are in valid Time Zone or check BIOS!", "Time Zone Not Found!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (Exception)
                {
                    MessageBox.Show(this, "Something is going wrong! Please check what you trying to insert!", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                clearcontent();
            }
        }

        private void Btn_Clear_Click(object sender, RoutedEventArgs e)
        {
            clearcontent();
        }

        private void rdb_male_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            rdb_female.IsChecked = false;
        }

        private void rdb_female_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            rdb_male.IsChecked = false;
        }

        void ClearErrors()
        {
            txt_error_Address_No.Text = null;
            txt_error_City.Text = null;
            txt_error_Customer_NIC.Text = null;
            txt_error_Email.Text = null;
            txt_error_First_Address_Line.Text = null;
            txt_error_First_Name.Text = null;
            txt_error_Gender.Text = null;
            txt_error_Last_Name.Text = null;
            txt_error_Second_Address_Line.Text = null;
            txt_error_Telephone.Text = null;
        }
        private void clearcontent()
        {
            txt_Customer_NIC.Clear();
            txt_First_Name.Clear();
            txt_Last_Name.Clear();
            txt_Gender.Clear();
            txt_Telephone.Clear();
            txt_Email.Clear();
            txt_Address_No.Clear();
            txt_First_Address_Line.Clear();
            txt_Second_Address_Line.Clear();
            txt_Profile_City.Clear();

        }
    }
}
