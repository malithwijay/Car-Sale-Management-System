using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Car_Sale
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Window
    {
        int usertype = 0;
        string username = "", userphoto = "";
        public Dashboard(int type)
        {
            InitializeComponent();
            usertype = type;
        }
        
        SqlConnection con;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
             DateTime today = DateTime.Now;
             txt_date.Text = today.ToString();
        }
        private void Btn_Close_Click(object sender, RoutedEventArgs e)
        {
            SystemCommands.CloseWindow(this);
        }

        private void Btn_Restore_Click(object sender, RoutedEventArgs e)
        {

            BtnMaximize.Visibility = Visibility.Visible;
            BtnRestore.Visibility = Visibility.Hidden;
            icon_restore.Visibility = Visibility.Hidden;
            icon_maximize.Visibility = Visibility.Visible;
            SystemCommands.RestoreWindow(this);
        }
        private void Btn_Maximize_Click(object sender, RoutedEventArgs e)
        {
            BtnMaximize.Visibility = Visibility.Hidden;
            BtnRestore.Visibility = Visibility.Visible;
            icon_maximize.Visibility = Visibility.Hidden;
            icon_restore.Visibility = Visibility.Visible;
            SystemCommands.MaximizeWindow(this);
        }
        private void Btn_Minimize_Click(object sender, RoutedEventArgs e)
        {
            SystemCommands.MinimizeWindow(this);
        }

        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonOpenMenu.Visibility = Visibility.Visible;
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
        }

        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
            ButtonCloseMenu.Visibility = Visibility.Visible;
        }

        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {


            UserControl usc = null;
            GridMain.Children.Clear();
            MainWindowContent.Children.Clear();
            MainWindowButton.Children.Clear();
           

            switch (((ListViewItem)((ListView)sender).SelectedItem).Name)
            {

                case "ItemLivechart":
                    Users.Visibility = Visibility.Hidden;
                    usc = new Livechart(usertype);
                    GridMain.Children.Add(usc);

                    break;

                case "ItemSalesman":
                    Users.Visibility = Visibility.Hidden;
                    usc = new Salesman(usertype);
                    GridMain.Children.Add(usc);

                    break;

                case "ItemVehicle":
                    Users.Visibility = Visibility.Hidden;
                    usc = new Vehicle(usertype);
                    GridMain.Children.Add(usc);

                    break;

                case "ItemSale":
                    Users.Visibility = Visibility.Hidden;
                    usc = new Sale();
                    GridMain.Children.Add(usc);

                    break;

                case "ItemSupplier":

                    Users.Visibility = Visibility.Hidden;
                    usc = new Supplier(usertype);
                    GridMain.Children.Add(usc);

                    break;

                case "ItemCustomer":
                    Users.Visibility = Visibility.Hidden;
                    if (usertype == 0 || usertype == 1)
                    {
                        usc = new Customer();
                        GridMain.Children.Add(usc);
                    }
                    else
                    {
                        MessageBox.Show("Your user type cannot access this", "Error...!", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                    break;

                case "ItemReport":
                    Users.Visibility = Visibility.Hidden;
                    if (usertype==0||usertype==1)
                    {
                        usc = new Report();
                        GridMain.Children.Add(usc);
                    }
                    else
                    {
                        MessageBox.Show("Your user type cannot access this", "Error...!", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                    break;



                case "ItemDashboard":
                    Users.Visibility = Visibility.Visible;
                    txt_name.Text = "";
                    txt_userType.Text = "";
                    Dashboard obj = new Dashboard(usertype);
                    obj.getuserdetails(username,userphoto);
                    obj.Show();
                    this.Close();

                    break;


                default:

                    break;
            }

        }

        Login userlog = new Login();
        private void logout_btn_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show(this, "Are you sure to logout", "Logout", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
            {
                
                userlog.Show();
                this.Close();
            }
            else
            {
            }
        }

        private void Register_btn_Click(object sender, RoutedEventArgs e)
        {
            if(usertype==0)
            {
                UserRegistration obj = new UserRegistration();
                obj.Show();
            }
            else
            {
                MessageBox.Show("Your user type cannot access this", "Error...!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public void getuserdetails(string name,string photo)
        {
            username = name;
            userphoto = photo;
            txt_name.Text = "";
            txt_userType.Text = "";


            txt_name.Text = ($"Hi {username}");
            if(usertype==0)
            {
                txt_userType.Text=("Admin User");
            }
            else if(usertype==1)
            {
                txt_userType.Text = ("Manager User");
            }
            else if(usertype==2)
            {
                txt_userType.Text = ("Ordinery User");
            }
            else
            {
                txt_userType.Text = ("Error User Type");
            }

            //load user photo
            if(!(string.IsNullOrEmpty(photo)))
            {
                Bitmap userpic = new Bitmap($"{photo}");
                BitmapImage bi = userpic.ToBitmapImage();
                img_user.Source = bi;
            }
        }
    }
}
