using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
using System.Collections;
using System.Text.RegularExpressions;

namespace Barcode_Reader
{
    /// <summary>
    /// Interaction logic for mainWindow.xaml
    /// </summary>
    public partial class mainWindow : Window
    {
        double ratioW;
        double ratioH;

        static string okutulanBarkod;

        static Image tlIconImage= new Image();
        static Image euroIconImage= new Image();
        static Image dollarIconImage= new Image();
        static Image poundIconImage= new Image();

        SqlConnection sqlConnection = new SqlConnection();
        ArrayList callProductArrayList = new ArrayList();
        ArrayList kurlar = new ArrayList();
        
        ArrayList footerItemsArrayList = new ArrayList();
        ArrayList midItemsArrayList = new ArrayList();


        //Classlar
        sqlString sqlStringClass = new sqlString();
        callProductFromDatabase classCallProduct = new callProductFromDatabase();

        bool justOneCreateTab = true;
        bool innerItemsBackground = true;

        int acikOlanPencere = 0;
        int acikOlanPencereSayisi = 1;
        StackPanel stackPanelForUserTabs = new StackPanel();



        BitmapImage backgroundBlackBitmap = new BitmapImage();
        BitmapImage newUserBitmap = new BitmapImage();
        BitmapImage findProductBitmap = new BitmapImage();
        BitmapImage iadeBitmap = new BitmapImage();
        BitmapImage settingToolsBitmap = new BitmapImage();
        BitmapImage logoutBlackBitmap = new BitmapImage();
        BitmapImage logoutWhiteBitmap = new BitmapImage();
        BitmapImage buttonBlackBitmap = new BitmapImage();
        BitmapImage buttonWhiteBitmap = new BitmapImage();
        BitmapImage exitBlackBitmap = new BitmapImage();
        BitmapImage exitWhiteBitmap = new BitmapImage();
        BitmapImage toggleBlackBitmap = new BitmapImage();
        BitmapImage toggleWhiteBitmap = new BitmapImage();
        BitmapImage userTabBackgroundBitmap = new BitmapImage();
        BitmapImage userIconBitmap = new BitmapImage();


        Image backgroundBlackImage = new Image();
        Image newuserImage = new Image();
        Image findProdutImage = new Image();
        Image iadeImage = new Image();
        Image settingToolsImage = new Image();
        Image logoutBlackImage = new Image();
        Image buttonImage = new Image();
        Image exitImage = new Image();
        Image toggleImage = new Image();
        Image userIconImage = new Image();
        ImageBrush userTabBrush = new ImageBrush();
        public mainWindow()
        {
            InitializeComponent();
            this.WindowState = WindowState.Maximized;
            this.ResizeMode = ResizeMode.NoResize;
            this.SizeChanged += mainWindow_SizeChanged;
            this.WindowStyle = WindowStyle.None;
            this.Loaded += mainWindow_Loaded;
            this.ContentRendered += mainWindow_ContentRendered;


            backgroundBlackBitmap.BeginInit();
            backgroundBlackBitmap.UriSource = new Uri("pack://application:,,,/Barcode Reader;component/image/mainForm/backgroundBlack.png");
            backgroundBlackBitmap.EndInit();

            newUserBitmap.BeginInit();
            newUserBitmap.UriSource = new Uri("pack://application:,,,/Barcode Reader;component/image/mainForm/header/yeniMusteri.png");
            newUserBitmap.EndInit();

            findProductBitmap.BeginInit();
            findProductBitmap.UriSource = new Uri("pack://application:,,,/Barcode Reader;component/image/mainForm/header/urunBul.png");
            findProductBitmap.EndInit();

            iadeBitmap.BeginInit();
            iadeBitmap.UriSource = new Uri("pack://application:,,,/Barcode Reader;component/image/mainForm/header/iade.png");
            iadeBitmap.EndInit();

            settingToolsBitmap.BeginInit();
            settingToolsBitmap.UriSource = new Uri("pack://application:,,,/Barcode Reader;component/image/mainForm/header/yonetimselAraclar.png");
            settingToolsBitmap.EndInit();

            logoutBlackBitmap.BeginInit();
            logoutBlackBitmap.UriSource = new Uri("pack://application:,,,/Barcode Reader;component/image/mainForm/header/cikisYapBlack.png");
            logoutBlackBitmap.EndInit();

            logoutWhiteBitmap.BeginInit();
            logoutWhiteBitmap.UriSource = new Uri("pack://application:,,,/Barcode Reader;component/image/mainForm/header/cikisYapWhite.png");
            logoutWhiteBitmap.EndInit();

            buttonBlackBitmap.BeginInit();
            buttonBlackBitmap.UriSource = new Uri("pack://application:,,,/Barcode Reader;component/image/mainForm/header/closebuttonBlack.png");
            buttonBlackBitmap.EndInit();

            buttonWhiteBitmap.BeginInit();
            buttonWhiteBitmap.UriSource = new Uri("pack://application:,,,/Barcode Reader;component/image/mainForm/header/closebuttonWhite.png");
            buttonWhiteBitmap.EndInit();

            exitBlackBitmap.BeginInit();
            exitBlackBitmap.UriSource = new Uri("pack://application:,,,/Barcode Reader;component/image/mainForm/header/closeTextBlack.png");
            exitBlackBitmap.EndInit();

            exitWhiteBitmap.BeginInit();
            exitWhiteBitmap.UriSource = new Uri("pack://application:,,,/Barcode Reader;component/image/mainForm/header/closeTextWhite.png");
            exitWhiteBitmap.EndInit();

            toggleBlackBitmap.BeginInit();
            toggleBlackBitmap.UriSource = new Uri("pack://application:,,,/Barcode Reader;component/image/mainForm/header/toggleBlack.png");
            toggleBlackBitmap.EndInit();

            toggleWhiteBitmap.BeginInit();
            toggleWhiteBitmap.UriSource = new Uri("pack://application:,,,/Barcode Reader;component/image/mainForm/header/toggleWhite.png");
            toggleWhiteBitmap.EndInit();

            userTabBackgroundBitmap.BeginInit();
            userTabBackgroundBitmap.UriSource = new Uri("pack://application:,,,/Barcode Reader;component/image/mainForm/musteri/musteriTabsBackground.png");
            userTabBackgroundBitmap.EndInit();

            userIconBitmap.BeginInit();
            userIconBitmap.UriSource = new Uri("pack://application:,,,/Barcode Reader;component/image/mainForm/musteri/Icon.png");
            userIconBitmap.EndInit();

            backgroundBlackImage.Source = backgroundBlackBitmap;
            newuserImage.Source = newUserBitmap;
            findProdutImage.Source = findProductBitmap;
            iadeImage.Source = iadeBitmap;
            settingToolsImage.Source = settingToolsBitmap;
            logoutBlackImage.Source = logoutBlackBitmap;
            buttonImage.Source = buttonBlackBitmap;
            exitImage.Source = exitBlackBitmap;
            toggleImage.Source = toggleBlackBitmap;
            userTabBrush.ImageSource = userTabBackgroundBitmap;
            userIconImage.Source = userIconBitmap;


            newuserImage.MouseUp += newuserImage_MouseUp;

           



        }

        void mainWindow_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key.ToString() == "Return")
            {
                callProductArrayList.Clear();

                sqlConnection = sqlStringClass.sqlConnectionOpen();
                callProductArrayList = classCallProduct.callSelectedItem(okutulanBarkod,sqlConnection);

                kurlar = classCallProduct.kurlar(sqlConnection);


                Border containerBorder = (Border)midItemsArrayList[acikOlanPencere];
                Canvas mainBorderCanvas = (Canvas)containerBorder.Child;
                Canvas footerCanvas = (Canvas)footerItemsArrayList[acikOlanPencere];

                ScrollViewer scrollViewer = (ScrollViewer)mainBorderCanvas.Children[24];
                TextBox amountTB = (TextBox)mainBorderCanvas.Children[21];
                Label amountType = (Label)mainBorderCanvas.Children[22];

                StackPanel urunDetailsStackPanel = (StackPanel)scrollViewer.Content;

                Label tlText = (Label)mainBorderCanvas.Children[5];
                Label poundText = (Label)mainBorderCanvas.Children[7];
                Label euroText = (Label)mainBorderCanvas.Children[6];
                Label dolarText = (Label)mainBorderCanvas.Children[8];

                if (callProductArrayList.Count>0)
                {
                    StackPanel innerStackPanel = new StackPanel();
                    urunDetailsStackPanel.Children.Add(innerStackPanel);
                    innerStackPanel.Orientation = Orientation.Horizontal;
                    double total = 0;

                    Label urunText = new Label();
                    Label kdvText = new Label();
                    Label miktarText = new Label();
                    Label olcuBirimiText = new Label();
                    Label birimFiyatText = new Label();
                    Label toplamFiyatText = new Label();

                    if(innerItemsBackground == true)
                    {
                        innerStackPanel.Background = new SolidColorBrush(Color.FromArgb(125, 55, 55, 55));
                        innerItemsBackground = false;
                    }
                    else
                    {
                        innerStackPanel.Background = new SolidColorBrush(Color.FromArgb(255, 55, 55, 55));
                        innerItemsBackground = true;
                    }

                    total = Convert.ToDouble(amountTB.Text.ToString()) * Convert.ToDouble(callProductArrayList[0].ToString());

                    tlText.Content = Convert.ToString(Convert.ToDouble(tlText.Content.ToString()) + total);
                    poundText.Content = Convert.ToString(Convert.ToDouble(poundText.Content.ToString()) + (total / Convert.ToDouble(kurlar[2])));
                    euroText.Content = Convert.ToString(Convert.ToDouble(euroText.Content.ToString()) + (total / Convert.ToDouble(kurlar[1])));
                    dolarText.Content = Convert.ToString(Convert.ToDouble(dolarText.Content.ToString()) + (total / Convert.ToDouble(kurlar[0])));


                    euroText.Content = String.Format("{0:F}", Convert.ToDouble(euroText.Content.ToString()));
                    tlText.Content = String.Format("{0:F}", Convert.ToDouble(tlText.Content.ToString()));
                    poundText.Content = String.Format("{0:F}", Convert.ToDouble(poundText.Content.ToString()));
                    dolarText.Content = String.Format("{0:F}", Convert.ToDouble(dolarText.Content.ToString()));


                    urunText.Content = callProductArrayList[2].ToString() + " " + callProductArrayList[3].ToString();
                    urunText.FontSize = 20 * ratioW;
                    urunText.HorizontalContentAlignment = HorizontalAlignment.Left;
                    urunText.Foreground = Brushes.White;

                    kdvText.Content = "%" + callProductArrayList[4].ToString();
                    kdvText.FontSize = 20 * ratioW;
                    kdvText.HorizontalContentAlignment = HorizontalAlignment.Center;
                    kdvText.Foreground = Brushes.White;

                    miktarText.Content = amountTB.Text.ToString() + " " + amountType.Content.ToString();
                    miktarText.FontSize = 20 * ratioW;
                    miktarText.HorizontalContentAlignment = HorizontalAlignment.Center;
                    miktarText.Foreground = Brushes.White;

                    birimFiyatText.Content = callProductArrayList[0].ToString();
                    birimFiyatText.FontSize = 20 * ratioW;
                    birimFiyatText.HorizontalContentAlignment = HorizontalAlignment.Center;
                    birimFiyatText.Foreground = Brushes.White;

                    toplamFiyatText.Content = total.ToString();
                    toplamFiyatText.FontSize = 20 * ratioW;
                    toplamFiyatText.HorizontalContentAlignment = HorizontalAlignment.Center;
                    toplamFiyatText.Foreground = Brushes.White;


                    urunText.MouseUp += innerItems_MouseUp;
                    kdvText.MouseUp += innerItems_MouseUp;
                    miktarText.MouseUp += innerItems_MouseUp;
                    birimFiyatText.MouseUp += innerItems_MouseUp;
                    toplamFiyatText.MouseUp += innerItems_MouseUp;


                    setWidthAndHeight(urunText, 750, 50);
                    setWidthAndHeight(kdvText, 100, 50);
                    setWidthAndHeight(miktarText, 125, 50);
                    setWidthAndHeight(birimFiyatText, 160, 50);
                    setWidthAndHeight(toplamFiyatText, 125, 50);

                    setTopLeftPosition(urunText, 42, 0);
                    innerStackPanel.Children.Add(urunText);
                    innerStackPanel.Children.Add(kdvText);
                    innerStackPanel.Children.Add(miktarText);
                    innerStackPanel.Children.Add(birimFiyatText);
                    innerStackPanel.Children.Add(toplamFiyatText);
                }
                okutulanBarkod = "";

            }
            else if (e.Key.ToString() == "Escape")
            {

            }
            else
            {
                okutulanBarkod += e.Key.ToString().Replace("D","");
            }
        }

        private void innerItems_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Label label = (Label)sender;
            StackPanel innerStackPanel = (StackPanel)label.Parent;
            StackPanel containerStackPanel = (StackPanel)innerStackPanel.Parent;
            double total = 0;

            containerStackPanel.Children.Remove(innerStackPanel);
            

            Border containerBorder = (Border)midItemsArrayList[acikOlanPencere];
            Canvas mainBorderCanvas = (Canvas)containerBorder.Child;

            Label totalText = (Label)innerStackPanel.Children[4];
            Label tlText = (Label)mainBorderCanvas.Children[5];
            Label poundText = (Label)mainBorderCanvas.Children[7];
            Label euroText = (Label)mainBorderCanvas.Children[6];
            Label dolarText = (Label)mainBorderCanvas.Children[8];

            total = Convert.ToDouble(totalText.Content.ToString());

            tlText.Content = Convert.ToString(Convert.ToDouble(tlText.Content.ToString()) - total);
            poundText.Content = Convert.ToString(Convert.ToDouble(poundText.Content.ToString()) - (total / Convert.ToDouble(kurlar[2])));
            euroText.Content = Convert.ToString(Convert.ToDouble(euroText.Content.ToString()) - (total / Convert.ToDouble(kurlar[1])));
            dolarText.Content = Convert.ToString(Convert.ToDouble(dolarText.Content.ToString()) - (total / Convert.ToDouble(kurlar[0])));


            euroText.Content = String.Format("{0:F}", Convert.ToDouble(euroText.Content.ToString()));
            tlText.Content = String.Format("{0:F}", Convert.ToDouble(tlText.Content.ToString()));
            poundText.Content = String.Format("{0:F}", Convert.ToDouble(poundText.Content.ToString()));
            dolarText.Content = String.Format("{0:F}", Convert.ToDouble(dolarText.Content.ToString()));
            
        }

        void mainWindow_ContentRendered(object sender, EventArgs e)
        {
            this.KeyUp += mainWindow_KeyUp;


            newuserImage.VerticalAlignment = VerticalAlignment.Top;
            newuserImage.HorizontalAlignment = HorizontalAlignment.Left;
            findProdutImage.VerticalAlignment = VerticalAlignment.Top;
            findProdutImage.HorizontalAlignment = HorizontalAlignment.Left;
            iadeImage.VerticalAlignment = VerticalAlignment.Top;
            iadeImage.HorizontalAlignment = HorizontalAlignment.Left;
            settingToolsImage.VerticalAlignment = VerticalAlignment.Top;
            settingToolsImage.HorizontalAlignment = HorizontalAlignment.Left;
            logoutBlackImage.VerticalAlignment = VerticalAlignment.Top;
            logoutBlackImage.HorizontalAlignment = HorizontalAlignment.Left;
            buttonImage.VerticalAlignment = VerticalAlignment.Top;
            buttonImage.HorizontalAlignment = HorizontalAlignment.Left;
            exitImage.VerticalAlignment = VerticalAlignment.Top;
            exitImage.HorizontalAlignment = HorizontalAlignment.Left;
            toggleImage.VerticalAlignment = VerticalAlignment.Top;
            toggleImage.HorizontalAlignment = HorizontalAlignment.Left;
            stackPanelForUserTabs.VerticalAlignment = VerticalAlignment.Top;
            stackPanelForUserTabs.HorizontalAlignment = HorizontalAlignment.Left;
            userIconImage.VerticalAlignment = VerticalAlignment.Top;
            userIconImage.HorizontalAlignment = HorizontalAlignment.Left;

            baseGrid.Children.Add(backgroundBlackImage);
            baseGrid.Children.Add(newuserImage);
            baseGrid.Children.Add(findProdutImage);
            baseGrid.Children.Add(iadeImage);
            baseGrid.Children.Add(settingToolsImage);
            baseGrid.Children.Add(logoutBlackImage);
            baseGrid.Children.Add(buttonImage);
            baseGrid.Children.Add(exitImage);
            baseGrid.Children.Add(toggleImage);
            baseGrid.Children.Add(stackPanelForUserTabs);

            stackPanelForUserTabs.Orientation = Orientation.Horizontal;
            stackPanelForUserTabs.Children.Clear();
            stackPanelForUserTabs.Children.Add(userIconImage);
            stackPanelForUserTabs.Background = userTabBrush;

            setWidthAndHeight(newuserImage, 226, 99);
            setWidthAndHeight(findProdutImage, 413, 99);
            setWidthAndHeight(iadeImage, 340, 99);
            setWidthAndHeight(settingToolsImage, 533, 99);
            setWidthAndHeight(logoutBlackImage, 188, 99);
            setWidthAndHeight(buttonImage, 39, 99);
            setWidthAndHeight(exitImage, 148, 99);
            setWidthAndHeight(toggleImage, 63, 99);
            setWidthAndHeight(stackPanelForUserTabs, 1920, 57);
            setWidthAndHeight(userIconImage, 29, 29);

            setTopLeftPosition(newuserImage, 0, 0);
            setTopLeftPosition(findProdutImage, 227, 0);
            setTopLeftPosition(iadeImage, 640, 0);
            setTopLeftPosition(settingToolsImage, 980, 0);
            setTopLeftPosition(logoutBlackImage, 1513, 0);
            setTopLeftPosition(buttonImage, 1681, 0);
            setTopLeftPosition(exitImage, 1701, 0);
            setTopLeftPosition(toggleImage, 1849, 0);
            setTopLeftPosition(stackPanelForUserTabs, 0, 101);
            setTopLeftPosition(userIconImage, 40, 15);

            createAllItems();
        }

        void newuserImage_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Label tab = new Label();
            stackPanelForUserTabs.Children.Add(tab);
            tab.Content = "MÜŞTERİ" + (acikOlanPencereSayisi).ToString();
            tab.FontSize = 20 * ratioW;
            tab.Name = "m" + acikOlanPencereSayisi.ToString();
            tab.Foreground = new SolidColorBrush(Color.FromArgb(255,4,140,238));
            tab.MouseUp += tab_MouseUp;
            tab.MouseDoubleClick += tab_MouseDoubleClick;
            setTopLeftPosition(tab, 15, 9);
            int tabLocation = Convert.ToInt32(tab.Name.Replace("m", "")) - 1;

            createAllItems();

            int i = 0;
            for (i = 0; i < acikOlanPencereSayisi; i++)
            {
                Border containerBorder = (Border)midItemsArrayList[i];
                Canvas footerCanvas = (Canvas)footerItemsArrayList[i];
                containerBorder.Visibility = Visibility.Hidden;
                footerCanvas.Visibility = Visibility.Hidden;
            }

            Border border = (Border)midItemsArrayList[i-1];
            Canvas canvas = (Canvas)footerItemsArrayList[i-1];
            border.Visibility = Visibility.Visible;
            canvas.Visibility = Visibility.Visible;
            acikOlanPencereSayisi++;

            acikOlanPencere = tabLocation;
            int j = 0;
            for (j = 1; j < stackPanelForUserTabs.Children.Count; j++)
            {
                Label label = (Label)stackPanelForUserTabs.Children[j];
                label.Foreground = new SolidColorBrush(Color.FromArgb(255, 4, 140, 238));
                label.FontWeight = FontWeights.Regular;
            }
            Label onLabel = (Label)stackPanelForUserTabs.Children[acikOlanPencere + 1];
            onLabel.FontWeight = FontWeights.Bold;
            onLabel.Foreground = new SolidColorBrush(Color.FromArgb(255, 145, 176, 255));

        }

        void tab_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Label label = (Label)sender;
            label.Visibility = Visibility.Hidden;

            setWidthAndHeight(label, 0, 0);
            setTopLeftPosition(label, -15, -15);



            Border containerBorder = (Border)midItemsArrayList[acikOlanPencere];
            Canvas footerCanvas = (Canvas)footerItemsArrayList[acikOlanPencere];

            containerBorder.Visibility = Visibility.Hidden;
            footerCanvas.Visibility = Visibility.Hidden;
        }

        void tab_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Label sLabel = (Label)sender;

            int tabLocation =Convert.ToInt32(sLabel.Name.Replace("m", ""))-1;
            
            for (int i = 0; i < acikOlanPencereSayisi - 1; i++)
            {
                Border containerBorder = (Border)midItemsArrayList[i];
                Canvas footerCanvas = (Canvas)footerItemsArrayList[i];

                containerBorder.Visibility = Visibility.Hidden;
                footerCanvas.Visibility = Visibility.Hidden;
            }

            Border border = (Border)midItemsArrayList[tabLocation];
            Canvas canvas = (Canvas)footerItemsArrayList[tabLocation];
            border.Visibility = Visibility.Visible;
            canvas.Visibility = Visibility.Visible;

            acikOlanPencere = tabLocation;

            int j = 0;
            for (j = 1; j < stackPanelForUserTabs.Children.Count; j++)
            {
                Label label = (Label)stackPanelForUserTabs.Children[j];
                label.Foreground = new SolidColorBrush(Color.FromArgb(255, 4, 140, 238));
                label.FontWeight = FontWeights.Regular;
            }
            Label onLabel = (Label)stackPanelForUserTabs.Children[acikOlanPencere + 1];
            onLabel.FontWeight = FontWeights.Bold;
            onLabel.Foreground = new SolidColorBrush(Color.FromArgb(255, 145, 176, 255));
        }

        void mainWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ratioW = this.ActualWidth / 1920;
            ratioH = this.ActualHeight / 1080;

        }

        void mainWindow_Loaded(object sender, RoutedEventArgs e)
        {

        }

        void createAllItems()
        {
            double totalPrice = 0 ;


            Image totalBackgroundImage = new Image();
            Image tlImage = new Image();
            Image poundImage = new Image();
            Image dollarImage = new Image();
            Image euroImage = new Image();
            Image paymentCashImage = new Image();
            Image paymentCardImage = new Image();
            Image paymentVeresiyeImage = new Image();
            Image barcodeImage = new Image();
            Image urunImage = new Image();
            Image amountAreaImage = new Image();
            Image amountIconImage = new Image();
            Image minusImage = new Image();
            Image linesImage = new Image();
            Image plusImage = new Image();
            Image handImage = new Image();
            Image urunDetailsImage = new Image();
            Image footerBannerImage = new Image();
            Image footerButtonImage = new Image();

            Label footerLineLabel = new Label();
            Label totalText = new Label();
            Label totalTextPrice = new Label();
            Label totalTextEuro = new Label();
            Label totalTextPound = new Label();
            Label totalTextDollar = new Label();
            Label amountType = new Label();

            Canvas footerFooterCanvas = new Canvas();
            Canvas footerCanvas = new Canvas();
            StackPanel urunDetailsPanel = new StackPanel();
            Canvas allItemsPanel = new Canvas();

            TextBox urunAmountTB = new TextBox();

            ScrollViewer urunDetailsScroll = new ScrollViewer();

            Border borderForAllItems = new Border();

            BitmapImage totalBackgroundBitmap = new BitmapImage();
            BitmapImage tlBitmap = new BitmapImage();
            BitmapImage poundBitmap = new BitmapImage();
            BitmapImage dollarBitmap = new BitmapImage();
            BitmapImage euroBitmap = new BitmapImage();
            BitmapImage paymentCashBitmap = new BitmapImage();
            BitmapImage paymentCardBitmap = new BitmapImage();
            BitmapImage paymentVeresiyeBitmap = new BitmapImage();
            BitmapImage barcodeBitmap = new BitmapImage();
            BitmapImage urunBitmap = new BitmapImage();
            BitmapImage amountAreaBitmap = new BitmapImage();
            BitmapImage amountIconBitmap = new BitmapImage();
            BitmapImage minusBitmap = new BitmapImage();
            BitmapImage linesBitmap = new BitmapImage();
            BitmapImage plusBitmap = new BitmapImage();
            BitmapImage handBitmap = new BitmapImage();
            BitmapImage urunDetailsBitmap = new BitmapImage();
            BitmapImage footerBannerBitmap = new BitmapImage();
            BitmapImage footerButtonBitmap = new BitmapImage();

            baseGrid.Children.Add(footerCanvas);
            baseGrid.Children.Add(borderForAllItems);
            borderForAllItems.Child = allItemsPanel;

            midItemsArrayList.Add(borderForAllItems);
            footerItemsArrayList.Add(footerCanvas);

            if (justOneCreateTab == true)
            {
                Label tab = new Label();
                stackPanelForUserTabs.Children.Add(tab);
                tab.Content = "MÜŞTERİ " + (acikOlanPencereSayisi).ToString();
                tab.Name = "m" + acikOlanPencereSayisi.ToString();
                tab.MouseUp +=tab_MouseUp;
                tab.MouseDoubleClick +=tab_MouseDoubleClick;
                tab.FontSize = 20 * ratioW;
                tab.Foreground = new SolidColorBrush(Color.FromArgb(255, 4, 140, 238));
                setTopLeftPosition(tab, 15, 9);
                acikOlanPencereSayisi++;
            }

            justOneCreateTab = false;


            totalBackgroundBitmap.BeginInit();
            totalBackgroundBitmap.UriSource = new Uri("pack://application:,,,/Barcode Reader;component/image/mainForm/total/background.png");
            totalBackgroundBitmap.EndInit();
            tlBitmap.BeginInit();
            tlBitmap.UriSource = new Uri("pack://application:,,,/Barcode Reader;component/image/mainForm/total/tlIcon.png");
            tlBitmap.EndInit();
            poundBitmap.BeginInit();
            poundBitmap.UriSource = new Uri("pack://application:,,,/Barcode Reader;component/image/mainForm/total/poundIcon.png");
            poundBitmap.EndInit();
            dollarBitmap.BeginInit();
            dollarBitmap.UriSource = new Uri("pack://application:,,,/Barcode Reader;component/image/mainForm/total/dollarIcon.png");
            dollarBitmap.EndInit();
            euroBitmap.BeginInit();
            euroBitmap.UriSource = new Uri("pack://application:,,,/Barcode Reader;component/image/mainForm/total/euroIcon.png");
            euroBitmap.EndInit();
            paymentCashBitmap.BeginInit();
            paymentCashBitmap.UriSource = new Uri("pack://application:,,,/Barcode Reader;component/image/mainForm/odeme/cash.png");
            paymentCashBitmap.EndInit();
            paymentCardBitmap.BeginInit();
            paymentCardBitmap.UriSource = new Uri("pack://application:,,,/Barcode Reader;component/image/mainForm/odeme/card.png");
            paymentCardBitmap.EndInit();
            paymentVeresiyeBitmap.BeginInit();
            paymentVeresiyeBitmap.UriSource = new Uri("pack://application:,,,/Barcode Reader;component/image/mainForm/odeme/veresiye.png");
            paymentVeresiyeBitmap.EndInit();
            barcodeBitmap.BeginInit();
            barcodeBitmap.UriSource = new Uri("pack://application:,,,/Barcode Reader;component/image/mainForm/barcode/barcodeArea.png");
            barcodeBitmap.EndInit();
            urunBitmap.BeginInit();
            urunBitmap.UriSource = new Uri("pack://application:,,,/Barcode Reader;component/image/mainForm/Urun/Urun.png");
            urunBitmap.EndInit();
            amountAreaBitmap.BeginInit();
            amountAreaBitmap.UriSource = new Uri("pack://application:,,,/Barcode Reader;component/image/mainForm/Urun/amountArea.png");
            amountAreaBitmap.EndInit();
            amountIconBitmap.BeginInit();
            amountIconBitmap.UriSource = new Uri("pack://application:,,,/Barcode Reader;component/image/mainForm/Urun/amountIconText.png");
            amountIconBitmap.EndInit();
            minusBitmap.BeginInit();
            minusBitmap.UriSource = new Uri("pack://application:,,,/Barcode Reader;component/image/mainForm/Urun/minus.png");
            minusBitmap.EndInit();
            plusBitmap.BeginInit();
            plusBitmap.UriSource = new Uri("pack://application:,,,/Barcode Reader;component/image/mainForm/Urun/plus.png");
            plusBitmap.EndInit();
            handBitmap.BeginInit();
            handBitmap.UriSource = new Uri("pack://application:,,,/Barcode Reader;component/image/mainForm/Urun/handIcon.png");
            handBitmap.EndInit();
            urunDetailsBitmap.BeginInit();
            urunDetailsBitmap.UriSource = new Uri("pack://application:,,,/Barcode Reader;component/image/mainForm/urunList/urunListArea.png");
            urunDetailsBitmap.EndInit();
            footerBannerBitmap.BeginInit();
            footerBannerBitmap.UriSource = new Uri("pack://application:,,,/Barcode Reader;component/image/mainForm/footer/tabsArea.png");
            footerBannerBitmap.EndInit();
            footerButtonBitmap.BeginInit();
            footerButtonBitmap.UriSource = new Uri("pack://application:,,,/Barcode Reader;component/image/mainForm/footer/button.png");
            footerButtonBitmap.EndInit();

            totalBackgroundImage.Source = totalBackgroundBitmap;
            tlImage.Source = tlBitmap;
            poundImage.Source = poundBitmap;
            euroImage.Source = euroBitmap;
            dollarImage.Source = dollarBitmap;
            paymentCashImage.Source = paymentCashBitmap;
            paymentCardImage.Source = paymentCardBitmap;
            paymentVeresiyeImage.Source = paymentVeresiyeBitmap;
            barcodeImage.Source = barcodeBitmap;
            urunImage.Source = urunBitmap;
            amountAreaImage.Source = amountAreaBitmap;
            amountIconImage.Source = amountIconBitmap;
            minusImage.Source = minusBitmap;
            linesImage.Source = linesBitmap;
            plusImage.Source = plusBitmap;
            handImage.Source = handBitmap;
            urunDetailsImage.Source = urunDetailsBitmap;
            footerBannerImage.Source = footerBannerBitmap;
            footerButtonImage.Source = footerButtonBitmap;

            footerFooterCanvas.Background = new SolidColorBrush(Color.FromArgb(15, 239, 87, 56));
            footerLineLabel.Background = new SolidColorBrush(Color.FromRgb(239, 87, 56));

            paymentCashImage.MouseUp += paymentCashImage_MouseUp;


            allItemsPanel.Children.Add(totalBackgroundImage);
            allItemsPanel.Children.Add(tlImage);
            allItemsPanel.Children.Add(poundImage);
            allItemsPanel.Children.Add(euroImage);
            allItemsPanel.Children.Add(totalText);
            allItemsPanel.Children.Add(totalTextPrice);
            allItemsPanel.Children.Add(totalTextEuro);
            allItemsPanel.Children.Add(totalTextPound);
            allItemsPanel.Children.Add(totalTextDollar);
            allItemsPanel.Children.Add(dollarImage);
            allItemsPanel.Children.Add(paymentCashImage);
            allItemsPanel.Children.Add(paymentCardImage);
            allItemsPanel.Children.Add(paymentVeresiyeImage);
            allItemsPanel.Children.Add(barcodeImage);
            allItemsPanel.Children.Add(urunImage);
            allItemsPanel.Children.Add(amountAreaImage);
            allItemsPanel.Children.Add(amountIconImage);
            allItemsPanel.Children.Add(minusImage);
            allItemsPanel.Children.Add(linesImage);
            allItemsPanel.Children.Add(plusImage);
            allItemsPanel.Children.Add(handImage);
            allItemsPanel.Children.Add(urunAmountTB);
            allItemsPanel.Children.Add(amountType);
            allItemsPanel.Children.Add(urunDetailsImage);
            allItemsPanel.Children.Add(urunDetailsScroll);


            urunDetailsScroll.Content = urunDetailsPanel;
            urunDetailsScroll.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;

            urunDetailsPanel.Name = "panel";
            allItemsPanel.Name = "canvas";

            footerCanvas.Children.Add(footerFooterCanvas);
            footerCanvas.Children.Add(footerLineLabel);
            footerCanvas.Children.Add(footerButtonImage);
            footerCanvas.Children.Add(footerBannerImage);

            borderForAllItems.VerticalAlignment = VerticalAlignment.Top;
            borderForAllItems.HorizontalAlignment = HorizontalAlignment.Left;


            barcodeImage.Stretch = Stretch.Fill;
            handImage.Stretch = Stretch.Fill;
            footerBannerImage.Stretch = Stretch.Fill;
            footerButtonImage.Stretch = Stretch.Fill;

            totalText.Content = "TOPLAM TUTAR";
            totalText.FontSize = 30 * ratioW;
            totalText.Foreground = new SolidColorBrush(Color.FromRgb(4, 144, 243));

            urunAmountTB.Text = "1";
            urunAmountTB.FontSize = 28 * ratioW;
            urunAmountTB.Foreground = new SolidColorBrush(Color.FromRgb(4, 144, 243));
            urunAmountTB.HorizontalContentAlignment = HorizontalAlignment.Right;
            urunAmountTB.VerticalContentAlignment = VerticalAlignment.Top;
            urunAmountTB.Background = new SolidColorBrush(Color.FromArgb(1, 25, 25, 25));
            urunAmountTB.BorderThickness = new Thickness(0);
            urunAmountTB.PreviewTextInput += urunAmountTB_PreviewTextInput;
            urunAmountTB.MouseEnter += urunAmountTB_MouseEnter;
            urunAmountTB.MouseLeave += urunAmountTB_MouseLeave;
            urunAmountTB.KeyUp += urunAmountTB_KeyUp;

            amountType.Content = "ADET";
            amountType.FontSize = 28 * ratioW;
            amountType.Foreground = new SolidColorBrush(Color.FromRgb(223, 113, 93));

            totalTextEuro.Content = "0";
            totalTextEuro.FontSize = 21 * ratioW;
            totalTextEuro.FontWeight = FontWeights.Bold;
            totalTextEuro.Foreground = new SolidColorBrush(Color.FromRgb(4, 144, 243));
            totalTextEuro.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Right;
            
            totalTextDollar.Content = "0";
            totalTextDollar.FontSize = 21 * ratioW;
            totalTextDollar.FontWeight = FontWeights.Bold;
            totalTextDollar.Foreground = new SolidColorBrush(Color.FromRgb(4, 144, 243));
            totalTextDollar.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Right;
            
            totalTextPound.Content = "0";
            totalTextPound.FontSize = 21 * ratioW;
            totalTextPound.FontWeight = FontWeights.Bold;
            totalTextPound.Foreground = new SolidColorBrush(Color.FromRgb(4, 144, 243));
            totalTextPound.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Right;



            totalTextPrice.Content = totalPrice.ToString();
            totalTextPrice.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Right;
            totalTextPrice.FontSize = 52 * ratioW;
            totalTextPrice.FontWeight = FontWeights.Bold;
            totalTextPrice.Foreground = new SolidColorBrush(Color.FromRgb(4, 144, 243));



            borderForAllItems.BorderThickness = new Thickness(13, 0, 0, 1);
            borderForAllItems.BorderBrush = new SolidColorBrush(Color.FromRgb(4, 144, 243));


            setWidthAndHeight(borderForAllItems, 1920, 560);
            setWidthAndHeight(allItemsPanel, 1920, 560);
            setWidthAndHeight(totalBackgroundImage, 596, 155);
            setWidthAndHeight(tlImage, 30, 30);
            setWidthAndHeight(euroImage, 25, 25);
            setWidthAndHeight(poundImage, 25, 25);
            setWidthAndHeight(dollarImage, 25, 25);
            setWidthAndHeight(paymentCashImage, 184, 97);
            setWidthAndHeight(totalBackgroundImage, 583, 155);
            setWidthAndHeight(paymentCardImage, 192, 197);
            setWidthAndHeight(paymentVeresiyeImage, 192, 197);
            setWidthAndHeight(barcodeImage, 578, 82);
            setWidthAndHeight(urunImage, 125, 62);
            setWidthAndHeight(amountAreaImage, 583, 158);
            setWidthAndHeight(amountIconImage, 144, 31);
            setWidthAndHeight(minusImage, 25, 2);
            setWidthAndHeight(linesImage, 265, 54);
            setWidthAndHeight(plusImage, 25, 27);
            setWidthAndHeight(handImage, 45, 63);
            setWidthAndHeight(urunDetailsImage, 1305, 134);
            setWidthAndHeight(totalTextPrice, 245, 100);
            setWidthAndHeight(totalTextPound, 180, 100);
            setWidthAndHeight(totalTextEuro, 200, 100);
            setWidthAndHeight(totalTextDollar, 200, 100);
            setWidthAndHeight(footerBannerImage, 1920, 57);
            setWidthAndHeight(footerFooterCanvas, 1920, 273);
            setWidthAndHeight(footerButtonImage, 51, 160);
            setWidthAndHeight(footerLineLabel, 10, 273);
            setWidthAndHeight(urunDetailsScroll, 1305, 399);
            setWidthAndHeight(urunAmountTB, 150, 35);


            setTopLeftPosition(borderForAllItems, 0, 160);
            setTopLeftPosition(totalBackgroundImage, 0, 0);
            setTopLeftPosition(totalText, 35, 35);
            setTopLeftPosition(totalTextPrice, 280, 19);
            setTopLeftPosition(totalTextEuro, 150, 108);
            setTopLeftPosition(totalTextPound, 0, 108);
            setTopLeftPosition(totalTextDollar, 325, 108);
            setTopLeftPosition(tlImage, 535, 48);
            setTopLeftPosition(poundImage, 190, 117);
            setTopLeftPosition(euroImage,370, 117);
            setTopLeftPosition(dollarImage, 535, 117);
            setTopLeftPosition(paymentCashImage, 0, 155);
            setTopLeftPosition(paymentCardImage, 189, 105);
            setTopLeftPosition(paymentVeresiyeImage, 386, 105);
            setTopLeftPosition(barcodeImage, 0, 250);
            setTopLeftPosition(urunImage, 20, 340);
            setTopLeftPosition(amountAreaImage, 0, 402);
            setTopLeftPosition(amountIconImage, 20, 460);
            setTopLeftPosition(minusImage, 200, 460);
            setTopLeftPosition(linesImage, 240, 435);
            setTopLeftPosition(handImage, 350, 450);
            setTopLeftPosition(plusImage, 520, 448);
            setTopLeftPosition(urunAmountTB, 300, 510);
            setTopLeftPosition(amountType, 490, 505);
            setTopLeftPosition(urunDetailsImage, 600, 25);
            setTopLeftPosition(urunDetailsScroll, 600, 160);
            setTopLeftPosition(footerBannerImage, 0, 750);
            setTopLeftPosition(footerFooterCanvas, 0, 807);
            setTopLeftPosition(footerButtonImage, 0, 870);
            setTopLeftPosition(footerLineLabel, 0, 807);
            addFooterHeaderItems(footerCanvas);
            addFooterItems(footerFooterCanvas);

            tlIconImage = tlImage;
            euroIconImage = euroImage;
            dollarIconImage = dollarImage;
            poundIconImage = poundImage;


        }

        void paymentCashImage_MouseUp(object sender, MouseButtonEventArgs e)
        {
            SqlConnection sqlconn = new SqlConnection();
            sqlconn = sqlStringClass.sqlConnectionOpen();
            Border border = (Border)midItemsArrayList[acikOlanPencere];
            Canvas canvas = (Canvas)border.Child;
            Canvas footerBorder = (Canvas)footerItemsArrayList[acikOlanPencere];


            Label thisWindowTab = (Label)stackPanelForUserTabs.Children[acikOlanPencereSayisi-1];


            Label totalPriceTLLabel = (Label)canvas.Children[5];
            Label totalPriceEuroLabel = (Label)canvas.Children[6];
            Label totalPricePoundLabel = (Label)canvas.Children[7];
            Label totalPriceDollarLabel = (Label)canvas.Children[8];

            DateTime nowDate = nowDateTime();


            double tlAmount = Convert.ToDouble(totalPriceTLLabel.Content);

            Canvas showPopupWindow = createPopUpWindow(canvas);

            


            if(totalPriceTLLabel.Content.ToString() !="0" )
            {
                try
                {
                    /*classAddSales.addSale(sqlconn, username, nowDate, tlAmount, 2, 2);
                    baseGrid.Children.Remove(border);
                    baseGrid.Children.Remove(footerBorder);
                    stackPanelForUserTabs.Children.Remove(thisWindowTab);
                    acikOlanPencereSayisi--;*/

                }
                catch (Exception x)
                {
                    MessageBox.Show(x.StackTrace.ToString());

                }
            }
                
            else
            {
               // MessageBox.Show("!!??!?!?");
            }



        }

        void urunAmountTB_KeyUp(object sender, KeyEventArgs e)
        {
            TextBox sTB = (TextBox)sender;
            sTB.Text = sTB.Text.Replace(" ", "");

            if(e.Key.ToString() == "Return")
            {
                this.Focus();
            }
        }

        void urunAmountTB_MouseLeave(object sender, MouseEventArgs e)
        {
            TextBox senderTB = (TextBox)sender;
            if(senderTB.Text == "")
            {
                senderTB.Text = "1";
            }
        }

        void urunAmountTB_MouseEnter(object sender, MouseEventArgs e)
        {
            TextBox senderTB = (TextBox)sender;
            if(senderTB.Text =="1")
            {
                senderTB.Text = "";
            }
            senderTB.BorderThickness = new Thickness(0);
        }

        void urunAmountTB_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private static bool IsTextAllowed(string text)
        {
            Regex regex = new Regex("[^0-9.-]+"); //regex that matches disallowed text
            return !regex.IsMatch(text);
        }

        void addFooterHeaderItems(Canvas canvas)
        {
            Canvas footerCanvas = (Canvas)canvas;
            
            int locationY = 0;
            for (int i= 0;i<10;i++)
            {
                Label menuItem = new Label();
                menuItem.Content = "Deneme" + i.ToString();
                menuItem.FontSize = 17;
                menuItem.Background = new SolidColorBrush(Color.FromRgb(224,82,53));
                menuItem.Padding = new Thickness(20 * ratioW, 12 * ratioW, 0, 0);
                menuItem.Foreground = Brushes.GhostWhite;
                menuItem.HorizontalAlignment = HorizontalAlignment.Center;
                footerCanvas.Children.Add(menuItem);
                setWidthAndHeight(menuItem, 124, 50);
                setTopLeftPosition(menuItem, locationY, 755);
                locationY+=125;
            }
        }
        void addFooterItems(Canvas canvasFooterFooter)
        {
            Canvas footerFooterCanvas = (Canvas)canvasFooterFooter;
            BitmapImage background = new BitmapImage();
            background.BeginInit();
            background.UriSource = new Uri("pack://application:,,,/Barcode Reader;component/image/mainForm/footer/itemsbackground.png");
            background.EndInit();
            int locationX = 0;
            int locationY = 0;
            for (int i = 1 ;i<13;i++)
            {
                
                Image backgroundImage = new Image();
                backgroundImage.Source = background;
                backgroundImage.Stretch = Stretch.Fill;
                footerFooterCanvas.Children.Add(backgroundImage);
                setWidthAndHeight(backgroundImage, 150, 90);
                setTopLeftPosition(backgroundImage, locationX, locationY);
                locationY += 90;
                if (i % 3 == 0)
                {
                    locationX += 150;
                    locationY = 0;
                }
                

            }
            
        }

        void setTopLeftPosition(object sender, double x, double y)
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
                double paddingTop = 0 * ratioH;
                double paddingLeft = 0 * ratioW;
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
            else if (sender.GetType().Name == "StackPanel")
            {
                StackPanel passwordBox = (StackPanel)sender;
                passwordBox.Margin = new Thickness(x, y, 0, 0);
            }
            else if (sender.GetType().Name == "Border")
            {
                Border passwordBox = (Border)sender;
                passwordBox.Margin = new Thickness(x, y, 0, 0);
            }
            else if (sender.GetType().Name == "ScrollViewer")
            {
                ScrollViewer passwordBox = (ScrollViewer)sender;
                passwordBox.Margin = new Thickness(x, y, 0, 0);
            }
            else
            {
                MessageBox.Show("KayÄ±tlÄ± ÃœrÃ¼n DeÄŸil");
            }

        }
        void setWidthAndHeight(Object sender, int width, int height)
        {
            if (sender.GetType().Name == "Canvas")
            {

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
            else if (sender.GetType().Name == "Label")
            {
                Label textbox = new Label();
                textbox = (Label)sender;
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
            else if (sender.GetType().Name == "StackPanel")
            {
                StackPanel passwordBox = new StackPanel();
                passwordBox = (StackPanel)sender;
                passwordBox.Width = width * ratioW;
                passwordBox.Height = height * ratioH;
            }
            else if (sender.GetType().Name == "Grid")
            {
                Grid passwordBox = new Grid();
                passwordBox = (Grid)sender;
                passwordBox.Width = width * ratioW;
                passwordBox.Height = height * ratioH;
            }
            else if (sender.GetType().Name == "Border")
            {
                Border passwordBox = new Border();
                passwordBox = (Border)sender;
                passwordBox.Width = width * ratioW;
                passwordBox.Height = height * ratioH;
            }
            else if (sender.GetType().Name == "ScrollViewer")
            {
                ScrollViewer passwordBox = new ScrollViewer();
                passwordBox = (ScrollViewer)sender;
                passwordBox.Width = width * ratioW;
                passwordBox.Height = height * ratioH;
            }
            else
            {
                MessageBox.Show("KayÄ±tlÄ± ÃœrÃ¼n DeÄŸil");
            }
        }
        public DateTime nowDateTime()
        {
            DateTime nowDate = DateTime.Now.Date;
            nowDate += DateTime.Now.TimeOfDay;
            return nowDate;
        }
        public Canvas createPopUpWindow(Canvas canvas)
        {
            Canvas containerCanvas = new Canvas();



            Canvas centerCanvas = new Canvas();
            Canvas backgroundCanvas = new Canvas();
            Canvas mainCanvas = (Canvas)canvas;

            Border totalBorder = new Border();


            //Bitmaps
            BitmapImage backgroundBitmap = new BitmapImage();
            BitmapImage headerIconBitmap = new BitmapImage();


            //Images
            Image backgroundImage = new Image();
            Image headerIconImage = new Image();
            Image tlIcon = new Image();
            Image euroIcon = new Image();
            Image dollarIcon = new Image();
            Image poundIcon = new Image();

            //Label
            Label headerText = new Label();
            Label totalTextHeader = new Label();
            Label totalTextTL = new Label();



            backgroundBitmap.BeginInit();
            backgroundBitmap.UriSource = new Uri("pack://application:,,,/Barcode Reader;component/image/mainForm/popupWindow/bg-pop-up.png");
            backgroundBitmap.EndInit();

            headerIconBitmap.BeginInit();
            headerIconBitmap.UriSource = new Uri("pack://application:,,,/Barcode Reader;component/image/mainForm/popupWindow/icon-little-cash.png");
            headerIconBitmap.EndInit();

            backgroundImage.Source = backgroundBitmap;
            headerIconImage.Source = headerIconBitmap;

            backgroundCanvas.Background = new SolidColorBrush(Color.FromArgb(225, 25, 25, 25));


            baseGrid.Children.Add(containerCanvas);
            containerCanvas.Children.Add(backgroundCanvas);
            containerCanvas.Children.Add(centerCanvas);
            

            backgroundImage.Stretch = Stretch.Fill;


             tlIcon.Source = tlIconImage.Source;
             euroIcon.Source = euroIconImage.Source;
             dollarIcon.Source = dollarIconImage.Source;
             poundIcon.Source = poundIconImage.Source;

            headerText.Content = "NAKİT ÖDEME";
            headerText.Foreground = new SolidColorBrush(Color.FromArgb(255, 104, 183, 255));
            headerText.FontWeight = FontWeights.SemiBold;
            headerText.FontSize = 28 * ratioW;


            totalTextHeader.Content = "TOPLAM TUTAR";
            totalTextHeader.Foreground = new SolidColorBrush(Color.FromArgb(255, 104, 183, 255));
            totalTextHeader.FontWeight = FontWeights.SemiBold;
            totalTextHeader.FontSize = 21 * ratioW;

            totalTextTL.Content = "30,87";
            totalTextTL.Foreground = new SolidColorBrush(Color.FromArgb(255, 104, 183, 255));
            totalTextTL.FontWeight = FontWeights.Bold;
            totalTextTL.FontSize = 28 * ratioW;
            totalTextTL.HorizontalContentAlignment = HorizontalAlignment.Right;

            centerCanvas.Children.Add(backgroundImage);
            centerCanvas.Children.Add(headerIconImage);
            centerCanvas.Children.Add(headerText);
            centerCanvas.Children.Add(totalBorder);
            centerCanvas.Children.Add(totalTextHeader);
            centerCanvas.Children.Add(totalTextTL);
            centerCanvas.Children.Add(tlIcon);
            centerCanvas.Children.Add(euroIcon);
            centerCanvas.Children.Add(dollarIcon);
            centerCanvas.Children.Add(poundIcon);




            setWidthAndHeight(centerCanvas, 870, 490);
            setWidthAndHeight(headerIconImage, 35, 35);
            setWidthAndHeight(containerCanvas, 1920, 1080);
            setWidthAndHeight(backgroundImage, 870, 490);
            setWidthAndHeight(totalTextTL, 200, 50);
            setWidthAndHeight(tlIcon, 35, 35);
            setWidthAndHeight(euroIcon, 35, 35);
            setWidthAndHeight(dollarIcon, 35, 35);
            setWidthAndHeight(poundIcon, 35, 35);
            setWidthAndHeight(backgroundCanvas, 1920, 1080);

            setTopLeftPosition(centerCanvas, (1920-870)/2, (1080 - 500) / 2);
            setTopLeftPosition(headerIconImage, 40, 30);
            setTopLeftPosition(backgroundImage, 0, 0);
            setTopLeftPosition(headerText, 150, 25);
            setTopLeftPosition(totalTextHeader, 50, 110);
            setTopLeftPosition(totalTextTL, 200, 105);
            setTopLeftPosition(tlIcon, 410, 105);

            backgroundCanvas.MouseUp += backGroundCanvas_MouseUp;


            return containerCanvas;
        }
        public Canvas headerMenuClick()
        {
            Canvas mainCanvas = new Canvas();
            Canvas backGroundCanvas = new Canvas();
            Canvas containerCanvas = new Canvas();

            baseGrid.Children.Add(containerCanvas);
            containerCanvas.Children.Add(backGroundCanvas);
            containerCanvas.Children.Add(mainCanvas);

            backGroundCanvas.Background = new SolidColorBrush(Color.FromArgb(225, 25, 25, 25));

            mainCanvas.Background = Brushes.Red;

            setWidthAndHeight(mainCanvas, 1366, 768);
            setWidthAndHeight(backGroundCanvas, 1920, 1080);
            setTopLeftPosition(containerCanvas, 0, 0);
            setTopLeftPosition(mainCanvas, (1920-1366) /2, (1080 - 768) / 2);
            setTopLeftPosition(backGroundCanvas, 0, 0);

            backGroundCanvas.MouseUp += backGroundCanvas_MouseUp;

            return containerCanvas;
        }

        void backGroundCanvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Canvas canvas = (Canvas)sender;
            Canvas containerCanvas = (Canvas)canvas.Parent;
            baseGrid.Children.Remove(containerCanvas);
        }


    
    }
}


