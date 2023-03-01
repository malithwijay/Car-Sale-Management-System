using AForge.Video;
using AForge.Video.DirectShow;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Car_Sale
{
    /// <summary>
    /// Interaction logic for UserRegistration.xaml
    /// </summary>
    public partial class UserRegistration : Window
    {
        public ObservableCollection<FilterInfo> VideoDevices { get; set; }

        public FilterInfo CurrentDevice
        {
            get { return _currentDevice; }
            set { _currentDevice = value; this.OnPropertyChanged("ThisDevice"); }
        }

        private FilterInfo _currentDevice;

        private IVideoSource _videoSource;
        public UserRegistration()
        {
            InitializeComponent();
            this.DataContext = this;
            GetVideoDevices();
            this.Closing += Window_Closing;
        }

        BitmapImage bi;
        SqlConnection con;
        SqlCommand cmd;
        int imagesaveCondition = 0;

        private void Btn_Browse_Click(object sender, RoutedEventArgs e)
        {
            saveimage();
        }

        private void Camera_Click(object sender, RoutedEventArgs e)
        {
            img_user.Source = null;
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
        }

        private void Btn_Save_Click(object sender, RoutedEventArgs e)
        {
            if (txt_User_name.Text.Length < 4)
            {
                txt_error_user_name.Text = "Invalid Username!";
                txt_User_name.Focus();
            }
            else if (txt_Password.Text.Length < 4)
            {
                txt_error_password.Text = "Password must have letters,symbols and numbers more than 5 characters!";
                txt_Password.Focus();
            }
            else if (txt_Password.Text != txt_Retype_Password.Text)
            {
                txt_error_retype_password.Text = "Password did not match!";
                txt_Retype_Password.Focus();
            }
            else if (cmb_Type.SelectedIndex < 0)
            {
                cmb_Type.Focus();
            }
            else if (img_user.Source == null)
            {
                MessageBox.Show("Please Capture user image", "User cannot add", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (imagesaveCondition == 1)
                {
                    try
                    {
                        con.Open(); //(UserName, UserPassword, UserType)
                        cmd = new SqlCommand("Insert into VehicleSaleUser values ('" + txt_User_name.Text + "','" + txt_Password.Text + "','" + (cmb_Type.SelectedIndex).ToString() + "','" + txt_image_loc.Text + "')", con);
                        int i = cmd.ExecuteNonQuery();
                        if (i == 1)
                        {
                            MessageBox.Show("Data Saved Successfully");
                        }
                        con.Close();

                    }
                    catch (NullReferenceException)
                    {
                        MessageBox.Show(this, "Object reference not set! ", "Null Reference Found!", MessageBoxButton.OK, MessageBoxImage.Error);
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
                else if (imagesaveCondition == 0)
                {
                    if (MessageBox.Show("Image Not Saved yet do you want to save the image ?", "Cannot add user...!", MessageBoxButton.YesNo, MessageBoxImage.Error) == MessageBoxResult.Yes)
                    {
                        saveimage();
                    }
                }
            }

        }

        private void Btn_Clear_Click(object sender, RoutedEventArgs e)
        {
            txt_error_retype_password.Text = null;
            txt_error_password.Text = null;
            txt_error_type.Text = null;
            txt_error_user_name.Text = null;
            txt_Password.Text = null;
            txt_Retype_Password.Text = null;
            txt_User_name.Text = null;

        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //con = new SqlConnection("Data Source=DESKTOP-0UIQMQB;Initial Catalog=CarSaleNew;Integrated Security=True");
            con = new SqlConnection("Data Source=NOTEBOOK-MALITH;Initial Catalog=CarSaleNew;Integrated Security=True");
            //con = new SqlConnection("Data Source=Sajana;Initial Catalog=DB_VehicleSale;Integrated Security=True");

        }

        private void cmb_Type_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmb_Type.SelectedIndex == 0)
            {
                admin_des.Visibility = Visibility.Visible;
                manager_des.Visibility = Visibility.Hidden;
                user_des.Visibility = Visibility.Hidden;
            }
            else if (cmb_Type.SelectedIndex == 1)
            {
                admin_des.Visibility = Visibility.Hidden;
                manager_des.Visibility = Visibility.Visible;
                user_des.Visibility = Visibility.Hidden;
            }
            else if (cmb_Type.SelectedIndex == 2)
            {
                admin_des.Visibility = Visibility.Hidden;
                manager_des.Visibility = Visibility.Hidden;
                user_des.Visibility = Visibility.Visible;
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

        private void video_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            try
            {
                using (var bitmap = (Bitmap)eventArgs.Frame.Clone())
                {
                    bi = bitmap.ToBitmapImage();
                }
                bi.Freeze();
                Dispatcher.BeginInvoke(new ThreadStart(delegate { img_user.Source = bi; }));
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
            StopCamera();
        }

        private void saveimage()
        {
            string directorypath = System.IO.Path.Combine(@"C:\", "KINGCars(Pvt)Ltd(DataFile)\\", "SystemUserImages");

            try
            {
                System.IO.Directory.CreateDirectory(directorypath);

                string user = txt_User_name.Text;
                string filelocation = (@"C:\\KINGCars(Pvt)Ltd(DataFile)\\SystemUserImages\\" + user + ".bmp");
                txt_image_loc.Text = filelocation;



                BitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bi));

                using (var fileStream = new System.IO.FileStream(filelocation, System.IO.FileMode.Create))
                {
                    encoder.Save(fileStream);
                    MessageBox.Show(this, "Image Saved Successfully", "Done...!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    imagesaveCondition = 1;
                }
            }
            catch (Exception)
            {

            }
        }
    }
}
