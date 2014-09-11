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
using System.Data.Sql;
using System.Data.SqlClient;

namespace Barcode_Reader
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        double ratioW;
        double ratioH;

        Image backgroundLoginImage = new Image();
        Image contentLoginImage = new Image();
        Image contentIDImage = new Image();
        Image contentPasswordImage = new Image();
        Image contentSubmitButtonImage = new Image();
        Image contentExitButtonImage = new Image();


        Label contentLoginHeaderFirstText = new Label();
        Label contentLoginHeaderSecondText = new Label();
        Label contentIdText = new Label();
        Label contentPasswordText = new Label();
        Label contentSubmitButtonText = new Label();
        Label contentExitButtonText = new Label();


        TextBox idTB = new TextBox();
        PasswordBox passwordTB = new PasswordBox();

        Canvas contentLoginCanvas = new Canvas();



        BitmapImage backgroundLoginBitmapImage = new BitmapImage();
        BitmapImage contentLoginBitmapImage = new BitmapImage();
        BitmapImage contentIDBitmapImage = new BitmapImage();
        BitmapImage contentPasswordBitmapImage = new BitmapImage();
        BitmapImage contentSubmitButtonBitmapImage = new BitmapImage();
        BitmapImage contentExitButtonBitmapImage = new BitmapImage();

        SqlConnection sql = new SqlConnection();

        //Class tanımları
        sqlString sqlStringClass = new sqlString();
        login loginClass = new login();

        public MainWindow()
        {
            InitializeComponent();


            this.SizeChanged += MainWindow_SizeChanged;
            this.ResizeMode = ResizeMode.NoResize;
            this.WindowState = WindowState.Maximized;
            this.WindowStyle = WindowStyle.None;
            this.Loaded += MainWindow_Loaded;
            this.KeyDown += MainWindow_KeyDown;

            ratioW = this.Width / 1920;
            ratioH = this.Height / 1080;


            backgroundLoginBitmapImage.BeginInit();
            backgroundLoginBitmapImage.UriSource = new Uri("pack://application:,,,/Barcode Reader;component/image/login/backgroundlogin.png");
            backgroundLoginBitmapImage.EndInit();

            contentLoginBitmapImage.BeginInit();
            contentLoginBitmapImage.UriSource = new Uri("pack://application:,,,/Barcode Reader;component/image/login/formBackground.png");
            contentLoginBitmapImage.EndInit();

            contentIDBitmapImage.BeginInit();
            contentIDBitmapImage.UriSource = new Uri("pack://application:,,,/Barcode Reader;component/image/login/id.png");
            contentIDBitmapImage.EndInit();

            contentPasswordBitmapImage.BeginInit();
            contentPasswordBitmapImage.UriSource = new Uri("pack://application:,,,/Barcode Reader;component/image/login/password.png");
            contentPasswordBitmapImage.EndInit();

            contentSubmitButtonBitmapImage.BeginInit();
            contentSubmitButtonBitmapImage.UriSource = new Uri("pack://application:,,,/Barcode Reader;component/image/login/confirm-button.png");
            contentSubmitButtonBitmapImage.EndInit();

            contentExitButtonBitmapImage.BeginInit();
            contentExitButtonBitmapImage.UriSource = new Uri("pack://application:,,,/Barcode Reader;component/image/login/exit-button.png");
            contentExitButtonBitmapImage.EndInit();

            backgroundLoginImage.Source = backgroundLoginBitmapImage;
            contentLoginImage.Source = contentLoginBitmapImage;

            contentIDImage.Source = contentIDBitmapImage;
            contentPasswordImage.Source = contentPasswordBitmapImage;
            contentSubmitButtonImage.Source = contentSubmitButtonBitmapImage;
            contentExitButtonImage.Source = contentExitButtonBitmapImage;

            backgroundLoginImage.Stretch = Stretch.Fill;
            contentLoginImage.Stretch = Stretch.Fill;



            baseGrid.Children.Add(backgroundLoginImage);
            baseGrid.Children.Add(contentLoginCanvas);


            contentLoginCanvas.Children.Add(contentLoginImage);
            contentLoginCanvas.Children.Add(contentLoginHeaderFirstText);
            contentLoginCanvas.Children.Add(contentLoginHeaderSecondText);
            contentLoginCanvas.Children.Add(contentIDImage);
            contentLoginCanvas.Children.Add(contentPasswordImage);
            contentLoginCanvas.Children.Add(contentIdText);
            contentLoginCanvas.Children.Add(contentPasswordText);
            contentLoginCanvas.Children.Add(contentSubmitButtonImage);
            contentLoginCanvas.Children.Add(contentExitButtonImage);
            contentLoginCanvas.Children.Add(contentSubmitButtonText);
            contentLoginCanvas.Children.Add(contentExitButtonText);
            contentLoginCanvas.Children.Add(idTB);
            contentLoginCanvas.Children.Add(passwordTB);


            contentSubmitButtonImage.MouseEnter += handOver;
            contentSubmitButtonImage.MouseLeave += handOut;
            contentSubmitButtonText.MouseEnter += handOver;
            contentSubmitButtonText.MouseLeave += handOut;
            contentExitButtonImage.MouseEnter += handOver;
            contentExitButtonImage.MouseLeave += handOut;
            contentExitButtonText.MouseEnter += handOver;
            contentExitButtonText.MouseLeave += handOut;

            
            contentSubmitButtonImage.MouseUp += contentSubmitButtonImage_MouseUp;
            contentExitButtonImage.MouseUp += contentExitButtonImage_MouseUp;
            contentSubmitButtonText.MouseUp += contentSubmitButtonImage_MouseUp;
            contentExitButtonText.MouseUp += contentExitButtonImage_MouseUp;




            contentLoginHeaderFirstText.Content = "Kullancı Girişi";
            contentLoginHeaderFirstText.Foreground = new SolidColorBrush(Color.FromArgb(255, 65, 169, 211));
            contentLoginHeaderFirstText.FontFamily = new FontFamily("Arial");
            contentLoginHeaderFirstText.FontWeight = FontWeights.SemiBold;


            contentLoginHeaderSecondText.Content = "Kullanıcı adınızı ve şifrenizi girin";
            contentLoginHeaderSecondText.Foreground = new SolidColorBrush(Color.FromArgb(255, 65, 169, 211));
            contentLoginHeaderSecondText.FontFamily = new FontFamily("Arial");
            contentLoginHeaderSecondText.FontWeight = FontWeights.Thin;



            contentIdText.Content = "Kullanıcı Adı";
            contentIdText.Foreground = new SolidColorBrush(Color.FromArgb(255, 65, 169, 211));
            contentIdText.FontFamily = new FontFamily("Arial");
            contentIdText.FontWeight = FontWeights.Thin;

            idTB.BorderThickness = new Thickness(0, 0, 0, 0);

            contentPasswordText.Content = "Parola";
            contentPasswordText.Foreground = new SolidColorBrush(Color.FromArgb(255, 65, 169, 211));
            contentPasswordText.FontFamily = new FontFamily("Arial");
            contentPasswordText.FontWeight = FontWeights.Thin;

            passwordTB.BorderThickness = new Thickness(0, 0, 0, 0);

            contentSubmitButtonText.Content = "GİRİŞ YAP";
            contentSubmitButtonText.Foreground = Brushes.White;
            contentSubmitButtonText.FontFamily = new FontFamily("Arial");
            contentSubmitButtonText.FontWeight = FontWeights.Thin;

            contentExitButtonText.Content = "ÇIKIŞ";
            contentExitButtonText.Foreground = Brushes.White;
            contentExitButtonText.FontFamily = new FontFamily("Arial");
            contentExitButtonText.FontWeight = FontWeights.Thin;


            sql = sqlStringClass.sqlConnectionOpen();
            if (sql.State.ToString() == "Closed")
            {
                this.Close();
            }


        }

        void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key.ToString() == "Return")
            {
                contentSubmitButtonImage_MouseUp(contentSubmitButtonImage);
            }
        }

        void handOut(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.Arrow;
        }

        void handOver(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        void contentExitButtonImage_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        void contentSubmitButtonImage_MouseUp(object sender, MouseButtonEventArgs e=null)
        {
            string username = idTB.Text;
            string password = passwordTB.Password;
            username = username.Replace(" ", "");
            password = password.Replace(" ", "");

            bool loginState = true;


            /*if (username == "" || password == "")
            {
                MessageBox.Show("Kullanıcı adı veya şifre bölümü boş bırakılamaz.");
                idTB.Text = "";
                passwordTB.Password = "";
            }
            else
            {*/
                //loginState = loginClass.returnLoginState(sql, username, password);
                
                if (loginState == true)
                {
                    Window deneme = new mainWindow();
                    deneme.Show();
                    this.Close();
                }
                else if (loginState == false)
                {
                    MessageBox.Show("Kullanıcı adı veya şifre hatalı !!");
                }
            //}

        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {

            //Set fontsizes
            contentLoginHeaderFirstText.FontSize = 25 * ratioW;
            contentLoginHeaderSecondText.FontSize = 17 * ratioW;
            contentIdText.FontSize = 25 * ratioW;
            contentPasswordText.FontSize = 25 * ratioW;
            contentSubmitButtonText.FontSize = 24 * ratioW;
            contentExitButtonText.FontSize = 24 * ratioW;
            idTB.FontSize = 25 * ratioW;
            passwordTB.FontSize = 25 * ratioW;

            //Set Width and height
            setWidthAndHeight(contentLoginImage, 976, 556);
            setWidthAndHeight(contentLoginCanvas, 976, 556);
            setWidthAndHeight(contentIDImage, 649, 77);
            setWidthAndHeight(contentPasswordImage, 576, 77);
            setWidthAndHeight(contentSubmitButtonImage, 275, 110);
            setWidthAndHeight(contentExitButtonImage, 275, 110);
            setWidthAndHeight(idTB, 350, 50);
            setWidthAndHeight(passwordTB, 350, 50);

            //Set Position for canvas,label,and image
            setTopLeftPosition(contentLoginCanvas, 0, 0);
            setTopLeftPosition(contentLoginHeaderFirstText, 250, 30);
            setTopLeftPosition(contentLoginHeaderSecondText, 250, 60);
            setTopLeftPosition(contentIDImage, 80, 200);
            setTopLeftPosition(contentPasswordImage, 153, 320);
            setTopLeftPosition(contentIdText, 80, 220);
            setTopLeftPosition(contentPasswordText, 140, 340);
            setTopLeftPosition(contentSubmitButtonImage, 205, 490);
            setTopLeftPosition(contentExitButtonImage, 495, 490);
            setTopLeftPosition(contentSubmitButtonText, 300, 528);
            setTopLeftPosition(contentExitButtonText, 570, 528);
            setTopLeftPosition(idTB, 360, 215);
            setTopLeftPosition(passwordTB, 360, 335);



        }

        void MainWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {

            baseGrid.Height = this.Height;
            baseGrid.Width = this.Width;

            ratioW = this.Width / 1920;
            ratioH = this.Height / 1080;

        }

        void setTopLeftPosition(object sender,double x , double y)
        {
                x = x * ratioW;
                y = y * ratioH;
            if (sender.GetType().Name == "Canvas")
            { 
                Canvas canvas = (Canvas)sender;
                canvas.Margin = new Thickness(x, y, 0, 0);
            }
            else if (sender.GetType().Name == "Image")
            {
                Image image = (Image)sender;
                image.Margin = new Thickness(x, y, 0, 0);
            }
            else if (sender.GetType().Name == "Label")
            {
                Label label = (Label)sender;
                label.Margin = new Thickness(x, y, 0, 0);
            }
            else if (sender.GetType().Name == "TextBox")
            {
                double paddingTop = 7 * ratioH;
                double paddingLeft = 7 * ratioW;
                TextBox textbox = (TextBox)sender;
                textbox.Margin = new Thickness(x, y, 0, 0);
                textbox.Padding = new Thickness(paddingLeft, paddingTop, 0, 0);
            }
            else if (sender.GetType().Name == "PasswordBox")
            {
                double paddingTop = 7 * ratioH;
                double paddingLeft = 7 * ratioW;
                PasswordBox passwordBox = (PasswordBox)sender;
                passwordBox.Margin = new Thickness(x, y, 0, 0);
                passwordBox.Padding = new Thickness(paddingLeft, paddingTop, 0, 0);
            }
            else
            {
                MessageBox.Show("Kayıtlı Ürün Değil");
            }
            
        }
        void setWidthAndHeight(Object sender, int width, int height)
        {
            if (sender.GetType().Name == "Canvas") {

                Canvas canvas = new Canvas();
                canvas = (Canvas)sender;
                canvas.Width = width * ratioW;
                canvas.Height = height * ratioH;

            }
            else if (sender.GetType().Name == "Image")
            {
                Image image = new Image();
                image = (Image)sender;
                image.Width = width * ratioW;
                image.Height = height * ratioH;
            }
            else if (sender.GetType().Name == "TextBox")
            {
                TextBox textbox = new TextBox();
                textbox = (TextBox)sender;
                textbox.Width = width * ratioW;
                textbox.Height = height * ratioH;
            }
            else if (sender.GetType().Name == "PasswordBox")
            {
                PasswordBox passwordBox = new PasswordBox();
                passwordBox = (PasswordBox)sender;
                passwordBox.Width = width * ratioW;
                passwordBox.Height = height * ratioH;
            }
            else
            {
                MessageBox.Show("Kayıtlı Ürün Değil");
            }
        }
    }
}
