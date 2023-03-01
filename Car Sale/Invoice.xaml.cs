using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
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
using System.Windows.Shapes;
using System.Net;
using System.Net.Mail;
using System.ComponentModel;

namespace Car_Sale
{
    /// <summary>
    /// Interaction logic for Invoice.xaml
    /// </summary>
    public partial class Invoice : Window
    {
        string email;
        public Invoice(string cardetails, string cusname, string cusID, string invoNo, string date, string cash, string financeamo, string cusamount, string total, string mail)
        {
            InitializeComponent();
            txt_box_car_details.Text = cardetails;
            txt_Customer_Name.Text = cusname;
            txt_Customer_ID.Text = cusID;
            txt_Invoice_No.Text = invoNo;
            txt_Date.Text = date;
            txt_paid_date.Text = date;
            txt_cash_amount.Text = cash;
            txt_finance_amount.Text = financeamo;
            txt_customer_amount.Text = cusamount;
            txt_total_paid.Text = total;
            email = mail;                  
        }

        private void Button_Print_Click(object sender, RoutedEventArgs e)
        {
            printpage();
            this.Close();
        }
        private void printpage()
        {
            try
            {
                this.IsEnabled = false;
                PrintDialog printdialog = new PrintDialog();
                if (printdialog.ShowDialog() == true)
                {
                    printdialog.PrintVisual(print, "Invoice");
                }
            }
            finally
            {
                this.IsEnabled = true;
            }
            sendanemail();
        }
        private void sendanemail()
        {
            string mailbody = "Dear Valued Customer,\nThank you for visiting us and trusting us !\nYour Car Details\n" +
                "" + txt_box_car_details.Text + "\n Stay Connected with our Mail Service";
            //get customer email

            string smtp = "smtp.gmail.com", username = "helpcenter.kingscar@gmail.com", password = "kingscar234";

            SmtpClient clientdetails = new SmtpClient();
            clientdetails.Port = 587;
            clientdetails.Host = smtp.Trim();
            clientdetails.EnableSsl = true;
            clientdetails.DeliveryMethod = SmtpDeliveryMethod.Network;
            clientdetails.UseDefaultCredentials = false;
            clientdetails.Credentials = new NetworkCredential(username.Trim(), password.Trim());

            MailMessage maildetails = new MailMessage();
            maildetails.From = new MailAddress(username.Trim());
            maildetails.To.Add(email.Trim());
            maildetails.Subject = ("Thank You For Trusting Us !");
            maildetails.IsBodyHtml = true;
            maildetails.Body = mailbody.Trim();
            clientdetails.Send(maildetails);

            maildetails.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
            clientdetails.SendCompleted += new SendCompletedEventHandler(SendCompleteCallBack);
            string userstate = "Sending...";
            clientdetails.SendAsync(maildetails, userstate);
        }

        private void SendCompleteCallBack(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Cancelled)
                MessageBox.Show(string.Format("{0} send canceled.", e.UserState), "Message", MessageBoxButton.OK, MessageBoxImage.Information);
            if (e.Error != null)
                MessageBox.Show(string.Format("{0} {1}", e.UserState, e.Error), "Message", MessageBoxButton.OK, MessageBoxImage.Information);
            else
                MessageBox.Show("your message has been successfully sent", "Message", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
