using AForge.Video;
using AForge.Video.DirectShow;
using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows;
using System.Windows.Media.Imaging;
using static System.Char;
using static System.String;


namespace Car_Sale
{
    /// <summary>
    /// Interaction logic for Addsalesman.xaml
    /// </summary>
    public partial class Addsalesman : Window, INotifyPropertyChanged
    {
        public ObservableCollection<FilterInfo> VideoDevices { get; set; }

        public FilterInfo CurrentDevice
        {
            get { return _currentDevice; }
            set { _currentDevice = value; this.OnPropertyChanged("CurrentDevice"); }
        }


        private FilterInfo _currentDevice;

        private IVideoSource _videoSource;
        public Addsalesman()
        {
            InitializeComponent();
            this.DataContext = this;
            GetVideoDevices();
            this.Closing += Window_Closing;
        }


        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;

        BitmapImage bi;

        int saveimg = 0;


        private void Btn_Save_Click(object sender, RoutedEventArgs e)
        {
            string gender = txt_gender.Text;
            if (rdb_male.IsChecked == true)
            {
                gender = "Male";
            }
            else if (rdb_female.IsChecked == true)
            {
                gender = "Female";
            }

            if (IsNullOrEmpty(txt_Salesman_NIC.Text) || IsNullOrWhiteSpace(txt_Salesman_NIC.Text))
            {
                ClearErrors();
                txt_error_Salesman_NIC.Text = "NIC cannot be blank!";
                txt_Salesman_NIC.Focus();
            }
            else if (!Regex.IsMatch(txt_Salesman_NIC.Text, @"^([0-9]{9}[x|X|v|V]|[0-9]{12})$"))
            {
                ClearErrors();
                txt_error_Salesman_NIC.Text = "Invalid NIC!";
                txt_Salesman_NIC.Focus();
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
            else if (IsNullOrEmpty(txt_Salary.Text) || IsNullOrWhiteSpace(txt_Salary.Text))
            {
                ClearErrors();
                txt_error_Salary.Text = "Salary cannot be blank!";
                txt_Salary.Focus();
            }
            else if ((txt_Salary.Text.Any(IsLetter)) || (txt_Salary.Text.Any(IsSymbol)))
            {
                ClearErrors();
                txt_error_Salary.Text = "Salary cannot be letter , Symbol! or less than zero! Please! add real salary!";
                txt_Salary.Focus();
            }
            else
            {
                ClearErrors();
                try
                {
                    con.Open();

                    cmd = new SqlCommand("insert into Salesman values" +
                    "('" + txt_Salesman_ID.Text + "','" + txt_Salesman_NIC.Text + "','" + txt_First_Name.Text + "','" + txt_Last_Name.Text + "','" + gender + "','" + txt_Telephone.Text + "','" + txt_Email.Text + "'," +
                    "'" + txt_Join_Date.Text + "','" + txt_Salary.Text + "', '" + txt_image.Content + "' )", con);
                    int i = cmd.ExecuteNonQuery();
                    if (i == 1)
                    {
                        MessageBox.Show("Data Saved Successfully", "All Done!", MessageBoxButton.OK, MessageBoxImage.Information);
                        //clear_menu();
                    }
                    else
                    {
                        MessageBox.Show("Data Saved Unsuccessfully", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                    con.Close();
                }

                catch (FormatException)
                {
                    MessageBox.Show(this, "You entered something wrong! Please!... check again!", "Invalid Format!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (SqlException)
                {
                    MessageBox.Show(this, "Something is wrong in Database! or You entered something wrong! Please!... check again!", "Database Error or Invalid Format!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (TimeoutException)
                {
                    MessageBox.Show(this, "Your Connection Time Out! Please sure your connection is real!", "Time Out!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (InvalidTimeZoneException)
                {
                    MessageBox.Show(this, "Your current TimeZone is useless!... Please! setup real Time Zone!", "Invalid Time Zone!", MessageBoxButton.OK, MessageBoxImage.Error);
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
            clear_menu();
        }

        private void Btn_Browse_Click(object sender, RoutedEventArgs e)
        {
            StopCamera();

            try
            {
                Image_Salesman.Source = null;
                Camera_panel.Visibility = Visibility.Hidden;

                OpenFileDialog picture = new OpenFileDialog();
                if (picture.ShowDialog() == true)
                {
                    Uri extention = new Uri(picture.FileName);
                    bi = new BitmapImage(extention);
                    Image_Salesman.Source = bi;

                    JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(bi));
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

        private void rdb_male_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

            rdb_female.IsChecked = false;

        }

        private void rdb_female_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            rdb_male.IsChecked = false;

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //con = new SqlConnection("Data Source=DESKTOP-0UIQMQB;Initial Catalog=CarSaleNew;Integrated Security=True");
            //con = new SqlConnection("Data Source=Sajana;Initial Catalog=DB_VehicleSale;Integrated Security=True");
            con = new SqlConnection("Data Source=NOTEBOOK-MALITH;Initial Catalog=CarSaleNew;Integrated Security=True");

            rdb_male.IsChecked = true;

            txt_Join_Date.DisplayDateEnd = DateTime.Now.Date;

            autoincreaseid();


        }

        private void Camera_Click(object sender, RoutedEventArgs e)
        {
            Image_Salesman.Source = null;
            Camera_panel.Visibility = Visibility.Visible;
            GetVideoDevices();
            this.Closing += Window_Closing;

        }

        private void cam_open_btn_Click(object sender, RoutedEventArgs e)
        {

            StartCamera();
        }

        private void cam_capture_btn_Click(object sender, RoutedEventArgs e)
        {
            StopCamera();
            cam_open_btn.Content = "Capture Again";
        }

        private void video_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            try
            {

                using (var bitmap = (Bitmap)eventArgs.Frame.Clone())
                {
                    bi = bitmap.ToBitmapImage();
                }
                bi.Freeze();
                Dispatcher.BeginInvoke(new ThreadStart(delegate { Image_Salesman.Source = bi; }));
                JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bi));
                using (MemoryStream ms = new MemoryStream())
                {
                    encoder.Save(ms);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Video Camera Not Found", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                StopCamera();
            }

        }
        private void GetVideoDevices()
        {

            VideoDevices = new ObservableCollection<FilterInfo>();
            foreach (FilterInfo filterInfo in new FilterInfoCollection(FilterCategory.VideoInputDevice))
            {
                VideoDevices.Add(filterInfo);
            }
            if (VideoDevices.Any())
            {
                CurrentDevice = VideoDevices[0];
            }
            else
            {
                MessageBox.Show("No video sources found", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void StartCamera()
        {
            if (CurrentDevice != null)
            {
                _videoSource = new VideoCaptureDevice(CurrentDevice.MonikerString);
                _videoSource.NewFrame += video_NewFrame;
                _videoSource.Start();
            }
        }

        private void StopCamera()
        {
            if (_videoSource != null && _videoSource.IsRunning)
            {
                _videoSource.SignalToStop();
                _videoSource.NewFrame -= new NewFrameEventHandler(video_NewFrame);
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                handler(this, e);
            }
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {

        }
        private void clear_menu()
        {
            // txt_Salesman_ID.Clear();
            txt_Salesman_NIC.Clear();
            txt_Last_Name.Clear();
            txt_First_Name.Clear();
            txt_Telephone.Clear();
            txt_Email.Clear();
            txt_Join_Date.Text = null;
            txt_Salary.Clear();
            Image_Salesman.Source = null;
        }

        void ClearErrors()
        {
            txt_error_Email.Text = null;
            txt_error_First_Name.Text = null;
            txt_error_Gender.Text = null;
            txt_error_Join_Date.Text = null;
            txt_error_Last_Name.Text = null;
            txt_error_Salary.Text = null;

            txt_error_Salesman_NIC.Text = null;
            txt_error_Telephone.Text = null;
            txt_error_First_Name.Text = null;


        }
        public void autoincreaseid()
        {
            string smID = "SM-100005";
            con.Open();
            cmd = new SqlCommand("select max(SalesmanID) from Salesman", con);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                if (!dr.IsDBNull(0))
                {
                    smID = dr.GetValue(0).ToString();
                }
            }
            con.Close();
            cmd.Dispose();

            string[] id = smID.Split('-');
            int id_no = Convert.ToInt32(id[1]);
            id_no = id_no + 5;
            txt_Salesman_ID.Text = id[0] + "-" + id_no.ToString();
        }

        private void save_image_Click(object sender, RoutedEventArgs e)
        {
            string directorypath = System.IO.Path.Combine(@"C:\", "KINGCars(Pvt)Ltd(DataFile)\\", "SalesPersonImages");
            try
            {
                System.IO.Directory.CreateDirectory(directorypath);

                string salesmanid = txt_Salesman_ID.Text;
                string filelocation = (@"C:\\KINGCars(Pvt)Ltd(DataFile)\\SalesPersonImages\\" + salesmanid + ".bmp");
                txt_image.Content = filelocation;



                BitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bi));

                using (var fileStream = new System.IO.FileStream(filelocation, System.IO.FileMode.Create))
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
    }
}
