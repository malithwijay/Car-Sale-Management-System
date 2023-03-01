using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Car_Sale
{
    /// <summary>
    /// Interaction logic for Sale.xaml
    /// </summary>
    public partial class Vehicle : UserControl
    {
        int usertype = 0;
        public Vehicle(int type)
        {
            InitializeComponent();
            usertype = type;
        }

        private void BtnAddcar_Click(object sender, RoutedEventArgs e)
        {
            Addcar obj = new Addcar();
            obj.Show();

        }

        private void BtnHome_Click(object sender, RoutedEventArgs e)
        {
            Dashboard obj = new Dashboard(usertype);
            obj.Show();
        }

        private void BtnViewcar_Click(object sender, RoutedEventArgs e)
        {
            Viewcar obj = new Viewcar();
            obj.Show();

        }

        private void BtnManufacturer_Click(object sender, RoutedEventArgs e)
        {
            if(usertype==0 || usertype==1)
            {
                Addmanufacturer obj = new Addmanufacturer();
                obj.Show();
            }
            else
            {
                MessageBox.Show("Your user type cannot add car manufacturers \nPlease contact manager or admin", "Error...!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void update_btn_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void BtnUpdatecar_Click(object sender, RoutedEventArgs e)
        {
            if (usertype == 0 || usertype == 1)
            {
                Update_Car obj = new Update_Car();
                obj.Show();
            }
            else
            {
                MessageBox.Show("Your user type cannot Update car  \nPlease contact manager or admin", "Error...!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }
    }
}
