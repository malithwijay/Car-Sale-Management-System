using Microsoft.Win32;
using QRCoder;
using System;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using ZXing;
using static System.Char;
using static System.String;

namespace Car_Sale
{
    /// <summary>
    /// Interaction logic for Addcar.xaml
    /// </summary>
    public partial class Addcar : Window
    {
        public Addcar()
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
            manufacture_fill();
            vehicleID_increment();
            supplierFill();

            rdb_New_Car.IsChecked = true;
            showerroTexts();

            date_Imported.DisplayDateEnd = DateTime.Now.Date;
        }

        BitmapImage bar_bi = new BitmapImage();
        BitmapImage qr_bi = new BitmapImage();
        BitmapImage car_image;

        int saveimg = 0;

        string qrlocation = "", barlocation = "", photolocation = "", typeID = "", condition = "";
        private void Btn_Browse_Click(object sender, RoutedEventArgs e)
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


        private void Btn_Get_QR_Click(object sender, RoutedEventArgs e)
        {
            string cardata = "Car Data";
            if (rdb_New_Car.IsChecked == true)
            {
                cardata = "" + cmb_Manufactured_Company.Text + " " + cmb_Type.Text + "(Unregistered)\n" +
                "Warrenty Period :- " + txt_Period.Text + "\n Best Service Provider :- " + cmb_Servise_Provider.Text + "" +
                "\nImported Date :- " + date_Imported.SelectedDate + "\nManufactured Year :- " + txt_YOM.Text + "\n" +
                "Feather Details,\n Fuel Type :- " + txt_Fuel_Type.Text + "" +
                "\nPrice :- " + txt_Price.Text + "";
            }
            else if (rdb_Used_Car.IsChecked == true)
            {
                cardata = "" + cmb_Manufactured_Company.Text + " " + cmb_Type.Text + "(Registered)\n" +
                "Warrenty Period :- " + txt_Period.Text + "\n Best Service Provider :- " + cmb_Servise_Provider.Text + "" +
                "\nImported Date :- " + date_Imported.SelectedDate + "\nManufactured Year :- " + txt_YOM.Text + "\n" +
                "Feather Details,\n Fuel Type :- " + txt_Fuel_Type.Text + "" +
                "\nMilage :- " + txt_Milage.Text + "\n" +
                "Year Registered :- " + txt_YOR.Text + "\nPrice :- " + txt_Price.Text + "";
            }

            getqrcode(cardata);
            getbarcode(txt_Vehical_ID.Text);
        }

        private void Btn_Print_QR_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Btn_Save_Click(object sender, RoutedEventArgs e)
        {
            showerroTexts();


            if (cmb_Manufactured_Company.SelectedIndex < 0)
            {
                ClearError();
                txt_error_Manufactured_Company.Text = "You missed Manufactured Company! Please... Select Manufactured Company";
                cmb_Manufactured_Company.Focus();
            }
            else if (cmb_Type.SelectedIndex < 0)
            {
                ClearError();
                txt_error_Type.Text = "You missed Vehicle type! Please... Select Vehicle type";
                cmb_Type.Focus();
            }
            else if (cmb_Supplier_Name.SelectedIndex < 0)
            {
                ClearError();
                txt_error_Supplier_Name.Text = "You missed Supplier Name!Please... Supplier Name";
                cmb_Supplier_Name.Focus();
            }
            //else if (IsNullOrEmpty(txt_Supplier_ID.Text) || IsNullOrWhiteSpace(txt_Supplier_ID.Text) || txt_Supplier_ID.Text.Any(IsSymbol))
            //{
            //    txt_error_Supplier_ID.Text = "Invalid Supplier ID";
            //    txt_Supplier_ID.Focus();
            //}
            else if (cmb_Servise_Provider.SelectedIndex < 0)
            {
                ClearError();
                txt_error_Service_Provider.Text = "You missed Service Provider! Please... Select Service Provider type";
                cmb_Servise_Provider.Focus();
            }
            else if (IsNullOrEmpty(txt_Period.Text) || IsNullOrWhiteSpace(txt_Period.Text))
            {
                ClearError();
                txt_error_Period.Text = "Period cannot be blank!";
                txt_Period.Focus();
            }
            else if (txt_Period.Text.Any(IsLetter) || txt_Period.Text.Any(IsSymbol))
            {
                ClearError();
                txt_error_Period.Text = "Invalid Period!";
                txt_Period.Focus();
            }
            else if (IsNullOrEmpty(txt_Status.Text) || IsNullOrWhiteSpace(txt_Status.Text) || txt_Status.Text.Any(IsSymbol))
            {
                ClearError();
                txt_error_Status.Text = "Warranty Status can not be blank and symbols! Please... type valid Warranty Status!";
                txt_Status.Focus();
            }
            else if (IsNullOrEmpty(txt_YOM.Text) || IsNullOrWhiteSpace(txt_YOM.Text) || txt_YOM.Text.Any(IsLetter) || txt_YOM.Text.Any(IsSymbol))
            {
                ClearError();
                txt_error_YOM.Text = "Please!... Enter valid Year!";
                txt_YOM.Focus();
            }
            else if (Convert.ToInt32(txt_YOM.Text) < 1940)
            {
                ClearError();
                txt_error_YOM.Text = "Oldest vehicles before 19,40 can not be insert!";
                txt_YOM.Focus();
            }
            else if (txt_Chassis_No.Text.Any(IsSymbol) || (txt_Chassis_No.Text.Length != 10))
            {
                ClearError();
                txt_error_Chassis_No.Text = "Please!... Enter valid Chassis Number!";
                txt_Chassis_No.Focus();
            }
            else if (txt_Engine_No.Text.Any(IsSymbol) || (txt_Engine_No.Text.Length != 10))
            {
                ClearError();
                txt_error_Engine_No.Text = "Engine_Capacity cannot be blank!";
                txt_Engine_No.Focus();
            }
            else if (txt_Engine_Capacity.Text.Any(IsSymbol))
            {
                ClearError();
                txt_error_Engine_Capacity.Text = "Engine_Capacity cannot be symbols!";
                txt_Engine_Capacity.Focus();
            }
            else if (txt_Engine_Capacity.Text.Length > 7)
            {
                ClearError();
                txt_error_Engine_Capacity.Text = "Please!... Enter valid Engine Capacity!";
                txt_Engine_Capacity.Focus();
            }
            else if (IsNullOrEmpty(txt_Interior_Color.Text) || IsNullOrWhiteSpace(txt_Interior_Color.Text))
            {
                ClearError();
                txt_error_Interior_Color.Text = "Interior Color cannob blank!";
                txt_Interior_Color.Focus();
            }
            else if (txt_Interior_Color.Text.Any(IsSymbol) || txt_Interior_Color.Text.Any(IsNumber))
            {
                ClearError();
                txt_error_Interior_Color.Text = "Please!... Enter valid Interior Color!";
                txt_Interior_Color.Focus();
            }
            else if (IsNullOrEmpty(txt_Body_Color.Text) || IsNullOrWhiteSpace(txt_Body_Color.Text))
            {
                ClearError();
                txt_error_Body_Color.Text = "Body Color cannot be blank!";
                txt_Body_Color.Focus();
            }
            else if (txt_Body_Color.Text.Any(IsSymbol) || txt_Body_Color.Text.Any(IsNumber))
            {
                ClearError();
                txt_error_Body_Color.Text = "Please!... Enter valid Body Color!";
                txt_Body_Color.Focus();
            }
            else if (IsNullOrEmpty(txt_Price.Text) || IsNullOrWhiteSpace(txt_Price.Text) || txt_Price.Text.Any(IsLetter) || txt_Price.Text.Any(IsSymbol))
            {
                ClearError();
                txt_error_Price.Text = "Price cannot be symbol, letter or blank!";
                txt_Price.Focus();
            }
            else if (Convert.ToDouble(txt_Price.Text) < 0.00)
            {
                ClearError();
                txt_error_Price.Text = "Please!... Enter valid Price!";
                txt_Price.Focus();
            }
            else
            {
                ClearError();
                try
                {
                    con.Open();
                    cmd = new SqlCommand("insert into Vehicle(VehicleID, MCID ,SupplierID ,YearOfMade ,ChassisNumber ,EngineNumber  ,EngineCapacity ,FuelType ,Transmissiontype ,Condition ,BarCode ,QRCode ,Vehiclehphoto ," +
                                    "InColor, OutColor, ImportedDate, YearOfRegistered, Mileage, PlateNumber, VehicleStatus, Price)values" +
                                    "('" + txt_Vehical_ID.Text + "', " + typeID + ", '" + txt_Supplier_ID.Text + "', '" + txt_YOM.Text + "', '" + txt_Chassis_No.Text + "', '" + txt_Engine_No.Text + "', '" + txt_Engine_Capacity.Text + "', '" + txt_Fuel_Type.Text + "', '" + cmb_Transmission_type.Text + "'," +
                                    " '" + condition + "', '" + barlocation + "', '" + qrlocation + "', '" + photolocation + "', '" + txt_Interior_Color.Text + "', '" + txt_Body_Color.Text + "', '" + date_Imported.Text + "'," +
                                    " '" + txt_YOR.Text + "', '" + txt_Milage.Text + "', '" + txt_plate_No.Text + "', 'Available', '" + txt_Price.Text + "')", con);

                    int i = cmd.ExecuteNonQuery();
                    con.Close();
                    cmd.Dispose();

                    if (i == 1)
                    {
                        MessageBox.Show(this, "Data Saved Successfully", "Done !", MessageBoxButton.OK, MessageBoxImage.Information);


                        con.Open();
                        cmd = new SqlCommand("insert into warranty (serviceprovider,period,WarrantyStatus,VehicleID) values('" + cmb_Servise_Provider.Text + "','" + txt_Period.Text + "','" + txt_Status.Text + "','" + txt_Vehical_ID.Text + "')", con);
                        int i1 = cmd.ExecuteNonQuery();
                        if (i1 == 1)
                        {
                            MessageBox.Show(this, "New Warranty Has Added");
                        }
                        else
                        {
                            MessageBox.Show("Warranty Updating Failed");
                        }
                        con.Close();
                    }
                    else
                    {
                        MessageBox.Show(this, "Data Not Saved", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                catch (SqlException)
                {
                    MessageBox.Show(this, "Something is wrong in Database!", "Database Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (TimeoutException)
                {
                    MessageBox.Show(this, "Your Connection Time Out! Please sure your connection is real!", "Time Out!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (InvalidTimeZoneException)
                {
                    MessageBox.Show("Your current TimeZone is useless!... Please! setup real Time Zone!", "Invalid Time Zone!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (TimeZoneNotFoundException)
                {
                    MessageBox.Show(this, "Can't find your Time Zone! Make sure you are in valid Time Zone or check BIOS!", "Time Zone Not Found!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (InvalidProgramException)
                {
                    MessageBox.Show(this, "You have invalid Microsoft Intermediate Language or missing it! Please!... install MSIL and related metadata! ", "MSIL Not Found!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (Exception)
                {
                    MessageBox.Show(this, "Something is going wrong! Please check what you trying to insert!", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }

        }

        private void Btn_Clear_Click(object sender, RoutedEventArgs e)
        {
            ClearError();
            ClearTextBoxs();
            hideerroTexts();

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
            txt_error_Type.Text = null;
            if (cmb_Manufactured_Company.SelectedIndex == -1)
            {
                txt_error_Manufactured_Company.Visibility = Visibility.Visible;
                txt_error_Manufactured_Company.Text = "Select Manufacturing Company";
            }
            else
            {
                cmb_Type.Items.Clear();
                string manu_com = cmb_Manufactured_Company.SelectedItem.ToString();
                txt_error_Manufactured_Company.Visibility = Visibility.Hidden;
                txt_error_Manufactured_Company.Text = manu_com;
                model_fill(manu_com);
                if (cmb_Type.SelectedIndex == -1)
                {
                    txt_error_Type.Visibility = Visibility.Visible;
                    txt_error_Type.Text = "Select model";
                }
            }
        }
        private void cmb_Type_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txt_error_Type.Visibility = Visibility.Hidden;
            if (cmb_Type.SelectedIndex == -1)
            {
                txt_error_Type.Text = "Select model";
            }
            else
            {
                txt_error_Type.Text = cmb_Type.SelectedItem.ToString();

            }
            getTypeID();
        }
        private void rdb_New_Car_Checked(object sender, RoutedEventArgs e)
        {
            rdb_Used_Car.IsChecked = false;
            txt_YOR.IsReadOnly = true;
            txt_Milage.IsReadOnly = true;
            txt_plate_No.IsReadOnly = true;

            txt_YOR.Text = "0000";

            txt_Milage.Text = "0";
            txt_plate_No.Text = "XXXXXXXXX";
            date_Imported.IsEnabled = true;
            condition = "New Car";
        }

        private void rdb_Used_Car_Checked(object sender, RoutedEventArgs e)
        {
            rdb_New_Car.IsChecked = false;
            date_Imported.IsEnabled = false;

            txt_YOR.IsReadOnly = false;
            txt_Milage.IsReadOnly = false;
            txt_plate_No.IsReadOnly = false;

            condition = "Used Car";
            date_Imported.SelectedDate = DateTime.Now.Date;
            txt_Milage.Text = "";
            txt_plate_No.Text = "";
            txt_YOR.Text = "";
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
            catch (Exception)
            {
                MessageBox.Show("Can't fill manufacture. Something is going wrong!", "Oops!...", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void model_fill(string mod_type)
        {
            try
            {
                cmb_Type.Items.Clear();
                con.Open();
                cmd = new SqlCommand("select * from model where MCID = @a", con);
                cmd.Parameters.AddWithValue("@a", mod_type);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    if (!dr.IsDBNull(2))
                    {
                        string model = dr.GetString(2);
                        cmb_Type.Items.Add(model);
                    }
                }
                string type = cmb_Type.Text;

                con.Close();
                cmd.Dispose();
            }
            catch (Exception)
            {
                MessageBox.Show("Can't fill model. Something is going wrong!", "Oops!...", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void getTypeID()
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

        private void vehicleID_increment()
        {
            string maxID = "VEH-100000";
            con.Open();
            cmd = new SqlCommand("select max(VehicleID) from Vehicle", con);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                if (!dr.IsDBNull(0))
                {
                    maxID = dr.GetValue(0).ToString();
                }
            }
            con.Close();
            cmd.Dispose();

            string[] id = maxID.Split('-');
            var id_no = Convert.ToInt32(id[1]);
            id_no = id_no + 5;
            txt_Vehical_ID.Text = id[0] + "-" + id_no.ToString();
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

        private void cmb_Servise_Provider_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void image_save_btn_Click(object sender, RoutedEventArgs e)
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

        private void Btn_searchClick(object sender, RoutedEventArgs e)
        {

        }

        private void Btn_Update_Click(object sender, RoutedEventArgs e)
        {
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



        void ClearTextBoxs()
        {
            txt_Body_Color.Text = null;
            txt_Chassis_No.Text = null;
            txt_Engine_Capacity.Text = null;
            txt_Engine_No.Text = null;
            txt_Fuel_Type.Text = null;

            txt_Interior_Color.Text = null;
            txt_Milage.Text = null;
            txt_Period.Text = null;
            txt_plate_No.Text = null;
            txt_Price.Text = null;
            txt_Status.Text = null;
            txt_Supplier_ID.Text = null;
            txt_Vehical_ID.Text = null;
            txt_YOM.Text = null;
            txt_YOR.Text = null;
        }

        void ClearError()
        {
            txt_error_Manufactured_Company.Text = null;
            txt_error_Type.Text = null;
            txt_error_Status.Text = null;
            txt_error_Body_Color.Text = null;
            txt_error_Chassis_No.Text = null;
            txt_error_Engine_Capacity.Text = null;
            txt_error_Engine_No.Text = null;
            txt_error_Fuel_Type.Text = null;
            txt_error_imported_date.Text = null;
            txt_error_Interior_Color.Text = null;
            txt_error_Milage.Text = null;
            txt_error_Period.Text = null;
            txt_error_plate_No.Text = null;
            txt_error_Price.Text = null;
            txt_error_radiobtn.Text = null;
            txt_error_Service_Provider.Text = null;
            txt_error_Status.Text = null;
            txt_error_Supplier_ID.Text = null;
            txt_error_Supplier_Name.Text = null;
            txt_error_Transmission_type.Text = null;
            txt_error_Type.Text = null;
            txt_error_Vehical_ID.Text = null;
            txt_error_YOM.Text = null;
            txt_error_YOR.Text = null;
        }
        private void supplierFill()
        {
            try
            {
                con.Open();
                cmd = new SqlCommand("select * from Supplier", con);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    if (!dr.IsDBNull(1))
                    {
                        string supName = dr.GetString(1);
                        supName = supName +" "+ dr.GetString(2);
                        cmb_Supplier_Name.Items.Add(supName);
                    }
                }
                con.Close();
                cmd.Dispose();
            }
            catch (SqlException)
            {
                MessageBox.Show("Can't fill Suppliers!", "Database Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception)
            {
                MessageBox.Show("Something is going wrong!", "Oops!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void cmb_Supplier_Name_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {               
                txt_Supplier_ID.Items.Clear();
                if (cmb_Supplier_Name.Text != null)
                {
                    con.Open();
                    //txt_error_Supplier_Name.Text = cmb_Supplier_Name.SelectedItem.ToString();
                    string[] supname = cmb_Supplier_Name.SelectedItem.ToString().Split(' ');

                    cmd = new SqlCommand("select * from Supplier where FirstName = '" + supname[0] + "' and LastName='"+supname[1]+"'", con);
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        if (!dr.IsDBNull(0))
                        {
                            string supID = dr.GetString(0);
                            txt_Supplier_ID.Items.Add(supID);
                        }
                    }
                    con.Close();
                }                
            }
            catch (SqlException)
            {
                MessageBox.Show("Can't find Suppliers!", "Database Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show("Something is going wrong!", "Oops!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
