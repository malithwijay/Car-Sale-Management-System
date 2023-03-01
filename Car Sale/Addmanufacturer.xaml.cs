using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using static System.Char;
using static System.String;

namespace Car_Sale
{
    /// <summary>
    /// Interaction logic for Viewcar.xaml
    /// </summary>
    public partial class Addmanufacturer : Window
    {
        public Addmanufacturer()
        {
            InitializeComponent();
        }
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //con = new SqlConnection("Data Source=DESKTOP-0UIQMQB;Initial Catalog=CarSaleNew;Integrated Security=True");
            //con = new SqlConnection("Data Source=Sajana;Initial Catalog=DB_VehicleSale;Integrated Security=True");
            con = new SqlConnection("Data Source=NOTEBOOK-MALITH;Initial Catalog=CarSaleNew;Integrated Security=True");
            loadmanufacturer();
            //loadmodel();
        }
        private void Btn_Clear_Click(object sender, RoutedEventArgs e)
        {
            txt_Manufacturer_ID.Text = null;
            txt_Manufacturer_Name.Text = null;
            txt_error_Manufacturer_ID.Text = null;
            txt_error_Manufacturer_Name.Text = null;
        }

        private void Btn_add_manu_com_Click(object sender, RoutedEventArgs e)
        {
            if (IsNullOrEmpty(txt_Manufacturer_Name.Text) || IsNullOrWhiteSpace(txt_Manufacturer_Name.Text) || txt_Manufacturer_Name.Text.Any(IsSymbol) || txt_Manufacturer_Name.Text.Any(IsDigit))
            {
                txt_error_Manufacturer_Name.Text = "Invalid Company Name!";
                txt_Manufacturer_Name.Focus();
            }
            else
            {
                try
                {
                    con.Open();
                    cmd = new SqlCommand("insert into Manufactured_Campany values('" + txt_Manufacturer_ID.Text + "')", con);
                    int i = cmd.ExecuteNonQuery();
                    if (i == 1)
                    {
                        MessageBox.Show("Successfully Manufacturer Company Added", "All Done!", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    con.Close();
                }
                catch (DuplicateNameException)
                {
                    MessageBox.Show("This Manufacturer Company already exists!", "Duplicate Data Found!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (SqlException)
                {
                    MessageBox.Show("Manufacturer Not Added,\n*The Company is Already Exists\n*Check your Input again", "Error...!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (Exception)
                {
                    MessageBox.Show("something is going wrong!", "Oops!...", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                clearitems();
                loadmanufacturer();
            }
        }

        private void Add_model_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txt_Manufacturer_ID.Text))
            {
                txt_error_Manufacturer_ID.Text = "";
                try
                {
                    con.Open();
                    cmd = new SqlCommand("insert into Model (MCID,ModelName) values('" + txt_Manufacturer_ID.Text + "','" + txt_Manufacturer_Name.Text + "')", con);
                    int i = cmd.ExecuteNonQuery();
                    if (i == 1)
                    {
                        MessageBox.Show("Successfully Model Added");
                    }
                    con.Close();
                }
                catch (SqlException)
                {
                    MessageBox.Show("Model Not Added,\n*The Company is Already Exists\n*Check your Input again", "Error...!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (Exception)
                {
                    MessageBox.Show("something is going wrong!", "Oops!...", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                clearitems();
                loadmanufacturer();
            }
            else
            {
                txt_error_Manufacturer_ID.Text = "Please Select a Manufacturer";
                txt_Manufacturer_ID.Focus();
            }
        }
        private void loadmanufacturer()
        {
            string manucom;
            txt_Manufacturer_ID.Items.Clear();
            con.Open();
            cmd = new SqlCommand("Select * from Manufactured_Campany", con);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                if (!dr.IsDBNull(0))
                {
                    manucom = dr.GetString(0);
                    txt_Manufacturer_ID.Items.Add(manucom);
                }
            }
            //manucom = txt_Manufacturer_ID.Text;
            con.Close();
            loadmodel(txt_Manufacturer_ID.Text);
        }
        private void clearitems()
        {
            txt_Manufacturer_ID.Text = null;
            txt_Manufacturer_Name.Text = null;
        }
        private void loadmodel(string brand)
        {
            txt_Manufacturer_Name.Items.Clear();
            con.Open();

            if (!(txt_Manufacturer_ID.SelectedIndex == -1))
            {
                cmd = new SqlCommand("Select * from Model where MCID = @a", con);
                cmd.Parameters.AddWithValue("@a", brand);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    if (!dr.IsDBNull(2))
                    {
                        string model = dr.GetString(2);
                        txt_Manufacturer_Name.Items.Add(model);
                    }
                }
            }

            con.Close();
        }

        private void txt_Manufacturer_ID_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (txt_Manufacturer_ID.SelectedIndex == -1)
            {
                txt_error_Manufacturer_ID.Text = "Please Select a Manufacturer";
            }
            else
            {
                string model = txt_Manufacturer_ID.SelectedItem.ToString();
                txt_error_Manufacturer_ID.Text = model;
                txt_error_Manufacturer_ID.Visibility = Visibility.Hidden;
                loadmodel(model);
            }
        }
    }
}
