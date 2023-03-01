using System;
using System.Data.SqlClient;
using System.Windows;

namespace Car_Sale
{
    public partial class Login : Window
    {
        SqlConnection con;
        SqlCommand cmd;
        public Login()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //con = new SqlConnection("Data Source=DESKTOP-0UIQMQB;Initial Catalog=CarSaleNew;Integrated Security=True");
            con = new SqlConnection("Data Source=NOTEBOOK-MALITH;Initial Catalog=CarSaleNew;Integrated Security=True");
            //con = new SqlConnection("Data Source=Sajana;Initial Catalog=DB_VehicleSale;Integrated Security=True");
        }
        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            int gettype = 0;
            // MessageBox.Show(this, "Treat customers as family", "Successfully Loggedin", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            // Dashboard obj = new Dashboard(gettype);
            // obj.Show();
            //this.Close();
            
            // int gettype = 0;
            string password = "", username = "", userphoto = "";
          


            if (string.IsNullOrEmpty(txt_username.Text))
            {
                txt_error.Text = "Username Cannot be blank Please Enter username !";
                txt_username.Focus();
            }
            else if (string.IsNullOrEmpty(txt_password.Password))
            {
                txt_error.Text = "Password Cannot be Blank Please Enter the Password !";
                txt_password.Focus();
            }
            else
            {
                txt_error.Text = "";
                try
                {
                    con.Open();
                    cmd = new SqlCommand("Select * from VehicleSaleUser where UserName='" + txt_username.Text + "'", con);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        if (!dr.IsDBNull(0))
                        {
                            username = dr.GetString(0);
                            password = dr.GetString(1);
                            gettype = Convert.ToInt32(dr.GetString(2));
                            if (!dr.IsDBNull(3))
                            {
                                userphoto = dr.GetString(3);
                            }
                        }
                        else
                        {
                            MessageBox.Show("No User found", "Error...!", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    con.Close();
                    if (username != txt_username.Text)
                    {
                        txt_error.Text = "Username you enterd is not existed Please check the username again";
                        txt_username.Focus();
                    }
                    else if (password != txt_password.Password)
                    {
                        txt_error.Text = "Password is wrong Please Check the password again";
                        txt_password.Focus();
                        txt_password.Clear();
                    }
                    else
                    {
                        MessageBox.Show(this, $"Treat customers as family ", "Successfully Loggedin", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        Dashboard obj1 = new Dashboard(gettype);
                        obj1.getuserdetails(username, userphoto);
                        obj1.Show();
                        this.Close();
                    }
                }
                catch (SqlException)
                {
                    MessageBox.Show(this, "Something Wrong with data source please contact System Maintainer", "Error !", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (Exception)
                {
                    MessageBox.Show(this, "Something went wrong Please contact system Maintainer", "Error !", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

        }

        private void BtnHelp_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Please Contact Manager for help", "Help", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
