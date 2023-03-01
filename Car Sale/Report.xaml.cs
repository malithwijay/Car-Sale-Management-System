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
    /// Interaction logic for Report.xaml
    /// </summary>
    public partial class Report : UserControl
    {
        public Report()
        {
            InitializeComponent();
        }

        private void btn_vehicle_report_Click(object sender, RoutedEventArgs e)
        {
            vehicleReport obj = new vehicleReport();
            obj.Show();

        }

        private void btn_sale_report_Click(object sender, RoutedEventArgs e)
        {
            saleReport obj = new saleReport();
            obj.Show();
        }

        private void btn_Bill_report_Click(object sender, RoutedEventArgs e)
        {
            billReport obj = new billReport();
            obj.Show();
        }
    }
}
