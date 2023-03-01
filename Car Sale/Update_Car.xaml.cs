using Microsoft.Win32;
using QRCoder;
using System;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using ZXing;

namespace Car_Sale
{
    /// <summary>
    /// Interaction logic for Addcar.xaml
    /// </summary>
    public partial class Update_Car : Window
    {
        public Update_Car()
        {
            InitializeComponent();
        }
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;

        Bitmap img;

        BitmapImage bar_bi = new BitmapImage();
        BitmapImage qr_bi = new BitmapImage();
        BitmapImage car_image = new BitmapImage();
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //con = new SqlConnection("Data Source=DESKTOP-0UIQMQB;Initial Catalog=CarSaleNew;Integrated Security=True");
            con = new SqlConnection("Data Source=NOTEBOOK-MALITH;Initial Catalog=CarSaleNew;Integrated Security=True");
            //con = new SqlConnection("Data Source=Sajana;Initial Catalog=DB_VehicleSale;Integrated Security=True");
            manufacture_fill();
            supplierFill();
            //model_fill();

            //rdb_New_Car.IsChecked = true;

            date_Imported.DisplayDateEnd = DateTime.Now.Date;
        }

        string barloc = "", qrloc = "", vehloc = "";
        private void search_btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string manuid = "", condition = "", manu_com = "";
                con.Open();
                cmd = new SqlCommand("select * from vehicle where vehicleID='" + search_vehicle.Text + "'", con);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    if (!dr.IsDBNull(0))
                    {
                        txt_Vehical_ID.Text = dr.GetString(0);
                        manuid = (dr.GetInt32(1)).ToString();
                        txt_Supplier_ID.Text = dr.GetString(2);
                        txt_YOM.Text = dr.GetString(3);//yom
                        txt_Chassis_No.Text = dr.GetString(4);//chassi
                        txt_Engine_No.Text = dr.GetString(5);//engine num
                        txt_Engine_Capacity.Text = dr.GetString(6);//engine
                        txt_Fuel_Type.Text = dr.GetString(7);//fuel
                        cmb_Transmission_type.Text = dr.GetString(8);//transmission
                        barloc = dr.GetString(10);//bar
                        qrloc = dr.GetString(11);//qr
                        vehloc = dr.GetString(12);//vehphoto
                        txt_Interior_Color.Text = dr.GetString(13);//in
                        txt_Body_Color.Text = dr.GetString(14);//body
                        date_Imported.Text = (dr.GetDateTime(15)).ToString();//importeddate
                        txt_YOR.Text = dr.GetString(16);//yor
                        txt_Milage.Text = (dr.GetDecimal(17)).ToString();//milage
                        txt_plate_No.Text = dr.GetString(18);//plateno
                        condition = dr.GetString(9);//condition
                        txt_Price.Text = (dr.GetDecimal(20)).ToString();//price

                        search_vehicle.Text = dr.GetString(19);//availability
                        availability_txt.Content = ($"This Car is a {condition}\n You can change Availability \n use right text");

                        if(String.IsNullOrEmpty(txt_YOR.Text))
                        {
                            rdb_New_Car.IsChecked = true;
                        }
                        else
                        {
                            rdb_Used_Car.IsChecked = true;
                        }

                        img = new Bitmap($"{barloc}");
                        bar_bi = img.ToBitmapImage();
                        Img_Bar_Code.Source = bar_bi;

                        img = new Bitmap($"{qrloc}");
                        qr_bi = img.ToBitmapImage();
                        Image_QR.Source = qr_bi;

                        img = new Bitmap($"{vehloc}");
                        car_image = img.ToBitmapImage();
                        Image_Car.Source = car_image;
                    }
                }
                dr.Close();
                con.Close();

                //model_fill();
                con.Open();
                cmd = new SqlCommand("select * from model where modelID='" + manuid + "'", con);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    if (!dr.IsDBNull(0))
                    {
                        txt_error_Manufactured_Company.Text = dr.GetString(1);
                        txt_error_Type.Text = dr.GetString(2);
                        //model_fill();
                    }
                }
                dr.Close();
                con.Close();

                con.Open();
                cmd = new SqlCommand("select * from Warranty where VehicleID='" + txt_Vehical_ID.Text + "'", con);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    if (!dr.IsDBNull(0))
                    {
                        cmb_Servise_Provider.Text = dr.GetString(5);
                        txt_Period.Text = (dr.GetInt32(2)).ToString();
                        txt_Status.Text = dr.GetString(6);
                    }
                }
                dr.Close();
                con.Close();

                con.Open();
                cmd = new SqlCommand("select * from supplier where NIC = '" + txt_Supplier_ID.Text + "'", con);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    if (!dr.IsDBNull(0))
                    {
                        cmb_Supplier_Name.Text = dr.GetString(1);
                    }
                }
                dr.Close();
                con.Close();

                //vehloc = null;
                //qrloc = null;
                //barloc = null;
            }
            catch (SqlException)
            {
                MessageBox.Show("Database Error", "Error");
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                MessageBox.Show("Something Wrong", "Error");
            }
        }



        int saveimg = 0;

        string qrlocation = "", barlocation = "", photolocation = "", typeID = "", condition = "";
        private void Btn_Browse_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog picture = new OpenFileDialog();
                if (picture.ShowDialog() == true)
                {
                    Uri extention = new Uri(picture.FileName);
                    car_image = new BitmapImage(extention);
                    Image_Car.Source = car_image;

                    JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(car_image));
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

        string cardata = "Car Data";
        private void Btn_Get_QR_Click(object sender, RoutedEventArgs e)
        {
            
            if (rdb_New_Car.IsChecked == true)
            {
                cardata = "'" + cmb_Manufactured_Company.Text + "' '" + cmb_Type.Text + "'(Unregistered)\n" +
                "Warrenty Period :- '" + txt_Period.Text + "'\n Best Service Provider :- '" + cmb_Servise_Provider.Text + "'" +
                "\nImported Date :- '" + date_Imported.SelectedDate + "'\nManufactured Year :- '" + txt_YOM.Text + "'\n" +
                "Feather Details,\n Fuel Type :- '" + txt_Fuel_Type.Text + "'" +
                "\nPrice :- '" + txt_Price.Text + "'";
            }
            else if (rdb_Used_Car.IsChecked == true)
            {
                cardata = "'" + cmb_Manufactured_Company.Text + "' '" + cmb_Type.Text + "'(Registered)\n" +
                "Warrenty Period :- '" + txt_Period.Text + "'\n Best Service Provider :- '" + cmb_Servise_Provider.Text + "'" +
                "\nImported Date :- '" + date_Imported.SelectedDate + "'\nManufactured Year :- '" + txt_YOM.Text + "'\n" +
                "Feather Details,\n Fuel Type :- '" + txt_Fuel_Type.Text + "'" +
                "\nMilage :- '" + txt_Milage.Text + "'\n" +
                "Year Registered :- '" + txt_YOR.Text + "'\nPrice :- '" + txt_Price.Text + "'";
            }

            getqrcode(cardata);
            getbarcode(txt_Vehical_ID.Text);
        }

        private void Btn_Print_QR_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Btn_Save_Click(object sender, RoutedEventArgs e)
        {
            Btn_Get_QR_Click(sender, e);
            try
            {
                con.Open();
                cmd = new SqlCommand("update Vehicle set SupplierID ='" + txt_Supplier_ID.Text + "',YearOfMade = '" + txt_YOM.Text + "',ChassisNumber = '" + txt_Chassis_No.Text + "',EngineNumber ='" + txt_Engine_No.Text + "'  ,EngineCapacity = '" + txt_Engine_Capacity.Text + "',FuelType ='" + txt_Fuel_Type.Text + "',Transmissiontype ='" + cmb_Transmission_type.Text + "',Condition = '" + condition + "',BarCode = '" + barloc + "',QRCode = '" + qrloc + "',Vehiclehphoto = '" + vehloc + "',InColor ='" + txt_Interior_Color.Text + "', OutColor ='" + txt_Body_Color.Text + "', ImportedDate ='" + date_Imported.Text + "',YearOfRegistered = '" + txt_YOR.Text + "', Mileage = '" + txt_Milage.Text + "', PlateNumber = '" + txt_plate_No.Text + "', VehicleStatus='" + search_vehicle.Text + "',Price ='" + txt_Price.Text + "' where VehicleID = '" + txt_Vehical_ID.Text + "'", con);

                int i = cmd.ExecuteNonQuery();
                con.Close();
                cmd.Dispose();

                if (i == 1)
                {
                    MessageBox.Show(this, "Data Updated Successfully", "Done !", MessageBoxButton.OK, MessageBoxImage.Information);

                    saveimage();
                    getbarcode(txt_Vehical_ID.Text);
                    getqrcode(cardata);

                    con.Open();
                    cmd = new SqlCommand("update warranty set serviceprovider = '" + cmb_Servise_Provider.Text + "',period='" + txt_Period.Text + "',WarrantyStatus='" + txt_Status.Text + "' where VehicleID='" + txt_Vehical_ID.Text + "'", con);

                    int i1 = cmd.ExecuteNonQuery();
                    con.Close();

                    if (i1 == 1)
                    {
                        MessageBox.Show(this, "Warranty Has Updated");
                    }
                    else
                    {
                        MessageBox.Show(this, "warranty Updating Failed");
                    }

                }
                else
                {
                    MessageBox.Show(this, "Data Not Saved", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (SqlException)
            {
                MessageBox.Show("Somethig wrong with database", "Database Error...!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception)
            {
                MessageBox.Show("Something wrong try to redo operation", "Error...!", MessageBoxButton.OK, MessageBoxImage.Error);
            }           

        }

        private void Qrbutton_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Btn_Clear_Click(object sender, RoutedEventArgs e)
        {
            Img_Bar_Code.Source = null;
            Image_QR.Source = null;
            Image_Car.Source = null;
        }
        private void txt_Fuel_Type_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (txt_Fuel_Type.SelectedItem == diesel)
            {
                txt_error_Fuel_Type.Text = "Diesel";
            }
            else if (txt_Fuel_Type.SelectedItem == petrol)
            {
                txt_error_Fuel_Type.Text = "Petrol";
            }
            else if (txt_Fuel_Type.SelectedItem == hybrid)
            {
                txt_error_Fuel_Type.Text = "Hybrid";
            }
            else if (txt_Fuel_Type.SelectedItem == electric)
            {
                txt_error_Fuel_Type.Text = "Electric";
            }
        }
        private void cmb_Manufactured_Company_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txt_error_Type.Text = "";
            if (cmb_Manufactured_Company.SelectedIndex == -1)
            {
                txt_error_Manufactured_Company.Visibility = Visibility.Visible;
                txt_error_Manufactured_Company.Text = "Select Manufacturing Company";
            }
            else
            {
                cmb_Type.Items.Clear();
                string manu_com = cmb_Manufactured_Company.SelectedItem.ToString();
                txt_error_Manufactured_Company.Visibility = Visibility.Visible;
                txt_error_Manufactured_Company.Text = manu_com;
                model_fill();
                if (cmb_Type.SelectedIndex == -1)
                {
                    txt_error_Type.Visibility = Visibility.Visible;
                    txt_error_Type.Text = "Select model";
                }
            }
        }
        private void cmb_Type_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txt_error_Type.Visibility = Visibility.Visible;
            if (cmb_Type.SelectedIndex == -1)
            {
                txt_error_Type.Text = "Select model";
            }
            else
            {
                txt_error_Type.Text = cmb_Type.SelectedItem.ToString();
                getTypeID();
            }
            //getTypeID();
        }
        private void rdb_New_Car_Checked(object sender, RoutedEventArgs e)
        {
            rdb_Used_Car.IsChecked = false;
            txt_YOR.IsReadOnly = true;
            txt_Milage.IsReadOnly = true;
            txt_plate_No.IsReadOnly = true;

            //txt_YOR.Text = "0000";
            //txt_Milage.Text = "0";
            //txt_plate_No.Text = "XXXXXXXXX";

            date_Imported.IsEnabled = true;
            condition = "New Car";
        }

        private void rdb_Used_Car_Checked(object sender, RoutedEventArgs e)
        {
            rdb_New_Car.IsChecked = false;
            date_Imported.IsEnabled = false;

            //txt_YOR.IsReadOnly = false;
            //txt_Milage.IsReadOnly = false;
            //txt_plate_No.IsReadOnly = false;

            condition = "Used Car";
            date_Imported.SelectedDate = DateTime.Now.Date;
        }
        private void manufacture_fill()
        {
            try
            {
                con.Open();
                cmd = new SqlCommand("select * from Manufactured_Campany", con);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    if (!dr.IsDBNull(0))
                    {
                        string company = dr.GetString(0);
                        cmb_Manufactured_Company.Items.Add(company);
                        cmb_Servise_Provider.Items.Add(company);
                    }
                }
                con.Close();
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void model_fill()
        {
            try
            {
                cmb_Type.Items.Clear();
                con.Open();
                cmd = new SqlCommand("select * from model where MCID = '" + txt_error_Manufactured_Company.Text + "'", con);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    if (!dr.IsDBNull(0))
                    {
                        string model = dr.GetString(2);
                        cmb_Type.Items.Add(model);
                    }
                }
                string type = cmb_Type.Text;

                con.Close();
                cmd.Dispose();
            }
            catch(Exception)
            {

            }
        }
        private void getTypeID()
        {
            try
            {
                if (cmb_Manufactured_Company.SelectedIndex != -1)
                {
                    con.Open();
                    cmd = new SqlCommand("select * from model where MCID = '" + txt_error_Manufactured_Company.Text + "' and ModelName = '" + txt_error_Type.Text + "'", con);
                    SqlDataReader drtype;
                    drtype = cmd.ExecuteReader();
                    while (drtype.Read())
                    {
                        if (!drtype.IsDBNull(0))
                        {
                            typeID = (drtype.GetInt32(0)).ToString();
                        }
                    }
                    con.Close();
                }

            }
            catch (Exception) { }
        }

        private void getqrcode(string data)
        {
            QRCodeGenerator qrcodegen = new QRCodeGenerator();
            QRCodeData qrdata = qrcodegen.CreateQrCode(data, QRCodeGenerator.ECCLevel.Q);
            QRCode qrcode = new QRCode(qrdata);
            Bitmap qrcodeimage = qrcode.GetGraphic(20);

            qr_bi = qrcodeimage.ToBitmapImage();
            Image_QR.Source = qr_bi;

            string directorypath = System.IO.Path.Combine(@"C:\", "KINGCars(Pvt)Ltd(DataFile)\\", "CarQRImages");
            try
            {
                System.IO.Directory.CreateDirectory(directorypath);

                string qrID = txt_Vehical_ID.Text;
                qrlocation = (@"C:\\KINGCars(Pvt)Ltd(DataFile)\\CarQRImages\\" + qrID + ".bmp");
                //txt_image.Content = filelocation;

                BitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(qr_bi));

                using (var fileStream = new System.IO.FileStream(qrlocation, System.IO.FileMode.Create))
                {
                    encoder.Save(fileStream);
                    MessageBox.Show(this, "Image Saved Successfully");
                    saveimg = 1;
                }
            }
            catch (Exception)
            {

            }
        }
        private void getbarcode(string data)
        {
            BarcodeWriter barcode = new BarcodeWriter() { Format = BarcodeFormat.CODE_128 };
            Bitmap bar_image = barcode.Write(data);

            bar_bi = bar_image.ToBitmapImage();
            Img_Bar_Code.Source = bar_bi;

            string directorypath = System.IO.Path.Combine(@"C:\", "KINGCars(Pvt)Ltd(DataFile)\\", "CarBARImages");
            try
            {
                System.IO.Directory.CreateDirectory(directorypath);

                string qrID = txt_Vehical_ID.Text;
                barlocation = (@"C:\\KINGCars(Pvt)Ltd(DataFile)\\CarBARImages\\" + qrID + ".bmp");
                //txt_image.Content = filelocation;

                BitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bar_bi));

                using (var fileStream = new System.IO.FileStream(barlocation, System.IO.FileMode.Create))
                {
                    encoder.Save(fileStream);
                    MessageBox.Show(this, "Image Saved Successfully");
                    saveimg = 1;
                }
            }
            catch (Exception)
            {

            }
        }

        private void cmb_Servise_Provider_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void image_save_btn_Click(object sender, RoutedEventArgs e)
        {
            saveimage();
        }

        private void saveimage()
        {
            string directorypath = System.IO.Path.Combine(@"C:\", "KINGCars(Pvt)Ltd(DataFile)\\", "CarImages");
            try
            {
                System.IO.Directory.CreateDirectory(directorypath);

                string carID = txt_Vehical_ID.Text;
                photolocation = (@"C:\\KINGCars(Pvt)Ltd(DataFile)\\CarImages\\" + carID + ".bmp");
                //txt_image.Content = filelocation;

                BitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(car_image));

                using (var fileStream = new System.IO.FileStream(photolocation, System.IO.FileMode.Create))
                {
                    encoder.Save(fileStream);
                    MessageBox.Show(this, "Image Saved Successfully");
                    saveimg = 1;
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }


        private void showerroTexts()
        {
            txt_error_Manufactured_Company.Visibility = Visibility.Visible;
            txt_error_Body_Color.Visibility = Visibility.Visible;
            txt_error_Chassis_No.Visibility = Visibility.Visible;
            txt_error_Engine_Capacity.Visibility = Visibility.Visible;
            txt_error_Engine_No.Visibility = Visibility.Visible;
            txt_error_Fuel_Type.Visibility = Visibility.Visible;
            txt_error_Interior_Color.Visibility = Visibility.Visible;
            txt_error_Milage.Visibility = Visibility.Visible;
            txt_error_Price.Visibility = Visibility.Visible;
            txt_error_Service_Provider.Visibility = Visibility.Visible;
            txt_error_Status.Visibility = Visibility.Visible;
            txt_error_Supplier_ID.Visibility = Visibility.Visible;
            txt_error_Supplier_Name.Visibility = Visibility.Visible;
            txt_error_Transmission_type.Visibility = Visibility.Visible;
            txt_error_Type.Visibility = Visibility.Visible;
            txt_error_Vehical_ID.Visibility = Visibility.Visible;
            txt_error_YOM.Visibility = Visibility.Visible;
            txt_error_YOR.Visibility = Visibility.Visible;
        }
        private void hideerroTexts()
        {
            txt_error_Manufactured_Company.Visibility = Visibility.Hidden;
            txt_error_Body_Color.Visibility = Visibility.Hidden;
            txt_error_Chassis_No.Visibility = Visibility.Hidden;
            txt_error_Engine_Capacity.Visibility = Visibility.Hidden;
            txt_error_Engine_No.Visibility = Visibility.Hidden;
            txt_error_Fuel_Type.Visibility = Visibility.Hidden;
            txt_error_Interior_Color.Visibility = Visibility.Hidden;
            txt_error_Milage.Visibility = Visibility.Hidden;
            txt_error_Price.Visibility = Visibility.Hidden;
            txt_error_Service_Provider.Visibility = Visibility.Hidden;
            txt_error_Status.Visibility = Visibility.Hidden;
            txt_error_Supplier_ID.Visibility = Visibility.Hidden;
            txt_error_Supplier_Name.Visibility = Visibility.Hidden;
            txt_error_Transmission_type.Visibility = Visibility.Hidden;
            txt_error_Type.Visibility = Visibility.Hidden;
            txt_error_Vehical_ID.Visibility = Visibility.Hidden;
            txt_error_YOM.Visibility = Visibility.Hidden;
            txt_error_YOR.Visibility = Visibility.Hidden;
        }
        private void supplierFill()
        {
            con.Open();
            cmd = new SqlCommand("select * from Supplier", con);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                if (!dr.IsDBNull(1))
                {
                    string supName = dr.GetString(1);
                    cmb_Supplier_Name.Items.Add(supName);
                }
            }
            con.Close();
            cmd.Dispose();
        }
        private void cmb_Supplier_Name_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}