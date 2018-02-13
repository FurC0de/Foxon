using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
using WpfAnimatedGif;
using System.Numerics;
namespace Theme.WPF
{

    public class VisualHost : UIElement
    {
        public Visual Visual { get; set; }

        protected override int VisualChildrenCount
        {
            get { return Visual != null ? 1 : 0; }
        }

        protected override Visual GetVisualChild(int index)
        {
            return Visual;
        }
    }

    public class AsyncAwait
    {
       
    }

    public partial class MainWindow : Window
    {
        //public static DrawingVisual drawingVisual = new DrawingVisual();
        //public DrawingContext drawingContext = drawingVisual.RenderOpen();
        public static bool loginPassed = false;
        public static DoubleAnimation loginAnimation;
        public MainWindow()
        {
            InitializeComponent();

            /*double n = 0.0;
            for (n = 0; n < 1; n+=0.1) {
                this.Opacity = n;
                Thread.Sleep(10);
            }*/
            
            // анимация для ширины

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        /*
        public async Task DoStuff()
        {
            await Task.Run(() =>
            {
                LongRunningOperation();
            });
            
        }

        private async Task LongRunningOperation()
        {
            // Create a rectangle and draw it in the DrawingContext.
            DrawingVisual drawingVisual = new DrawingVisual();
            DrawingContext drawingContext = drawingVisual.RenderOpen();
            Rect rect = new Rect(new System.Windows.Point(10, 10), new System.Windows.Size(10, 10));
            drawingContext.DrawRectangle(System.Windows.Media.Brushes.Aqua, (System.Windows.Media.Pen)null, rect);
            Rect rect1 = new Rect(new System.Windows.Point(20, 20), new System.Windows.Size(10, 10));
            drawingContext.DrawRectangle(System.Windows.Media.Brushes.Aqua, (System.Windows.Media.Pen)null, rect1);
            Rect rect2 = new Rect(new System.Windows.Point(30, 30), new System.Windows.Size(10, 10));
            drawingContext.DrawRectangle(System.Windows.Media.Brushes.Aqua, (System.Windows.Media.Pen)null, rect2);
            loadingAnimation.Children.Add(new VisualHost { Visual = drawingVisual });
            drawingContext.Close();
        }
        */
        public static int alphaCounter = 0;

        public void drawLoading(object state)
        {
            /*
            int C = 0;
            while (C < 64)
            {
                Dispatcher.BeginInvoke(new Action(delegate ()
                {
                    System.Windows.Shapes.Rectangle rect;
                    rect = new System.Windows.Shapes.Rectangle();
                    rect.Stroke = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
                    rect.Fill = new SolidColorBrush(Color.FromArgb(Convert.ToByte(C), 80, 90, 100));
                    rect.Width = 5;
                    rect.Height = 15;
                    Canvas.SetLeft(rect, 15);
                    Canvas.SetTop(rect, 15);
                    loadingAnimation.Children.Add(rect);
                }));
                C++;
                Thread.Sleep(1);
            }

            C = 0;
            while (C < 64)
            {
                Dispatcher.BeginInvoke(new Action(delegate ()
                {
                    System.Windows.Shapes.Rectangle rect;
                    rect = new System.Windows.Shapes.Rectangle();
                    rect.Stroke = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
                    rect.Fill = new SolidColorBrush(Color.FromArgb(Convert.ToByte(C), 90, 110, 130));
                    rect.Width = 5;
                    rect.Height = 15;
                    Canvas.SetLeft(rect, 30);
                    Canvas.SetTop(rect, 15);
                    loadingAnimation.Children.Add(rect);
                }));
                C++;
                Thread.Sleep(1);
            }

            C = 0;
            while (C < 64)
            {
                Dispatcher.BeginInvoke(new Action(delegate ()
                {
                    System.Windows.Shapes.Rectangle rect;
                    rect = new System.Windows.Shapes.Rectangle();
                    rect.Stroke = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
                    rect.Fill = new SolidColorBrush(Color.FromArgb(Convert.ToByte(C), 100, 140, 180));
                    rect.Width = 5;
                    rect.Height = 15;
                    Canvas.SetLeft(rect, 45);
                    Canvas.SetTop(rect, 15);
                    loadingAnimation.Children.Add(rect);
                }));
                C++;
                Thread.Sleep(1);
            }

            C = 0;
            while (C < 64)
            {
                Dispatcher.BeginInvoke(new Action(delegate ()
                {
                    System.Windows.Shapes.Rectangle rect;
                    rect = new System.Windows.Shapes.Rectangle();
                    rect.Stroke = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
                    rect.Fill = new SolidColorBrush(Color.FromArgb(Convert.ToByte(C), 90, 110, 130));
                    rect.Width = 5;
                    rect.Height = 15;
                    Canvas.SetLeft(rect, 60);
                    Canvas.SetTop(rect, 15);
                    loadingAnimation.Children.Add(rect);
                }));
                C++;
                Thread.Sleep(1);
            }

            C = 0;
            while (C < 64)
            {
                Dispatcher.BeginInvoke(new Action(delegate ()
                {
                    System.Windows.Shapes.Rectangle rect;
                    rect = new System.Windows.Shapes.Rectangle();
                    rect.Stroke = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
                    rect.Fill = new SolidColorBrush(Color.FromArgb(Convert.ToByte(C), 80, 90, 100));
                    rect.Width = 5;
                    rect.Height = 15;
                    Canvas.SetLeft(rect, 75);
                    Canvas.SetTop(rect, 15);
                    loadingAnimation.Children.Add(rect);
                }));
                C++;
                Thread.Sleep(1);
            }

            C = 0;
            while (C < 64)
            {*/
            for (double i = 0; i <= 1; i+=0.05) { 
                Dispatcher.BeginInvoke(new Action(delegate ()
                {
                    this.Opacity = i;
                }));
                Thread.Sleep(20);
            }
            //C++;
            //Thread.Sleep(1);
            //}
            /*
            C = 0;
            while (C < 64)
            {
                Dispatcher.BeginInvoke(new Action(delegate ()
                {
                    System.Windows.Shapes.Rectangle rect;
                    rect = new System.Windows.Shapes.Rectangle();
                    rect.Stroke = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
                    rect.Fill = new SolidColorBrush(Color.FromArgb(Convert.ToByte(C), 100, 140, 180));
                    rect.Width = 5;
                    rect.Height = 15;
                    Canvas.SetLeft(rect, 105);
                    Canvas.SetTop(rect, 15);
                    loadingAnimation.Children.Add(rect);
                }));
                C++;
                Thread.Sleep(1);
            }

            C = 0;
            while (C < 64)
            {
                Dispatcher.BeginInvoke(new Action(delegate ()
                {
                    System.Windows.Shapes.Rectangle rect;
                    rect = new System.Windows.Shapes.Rectangle();
                    rect.Stroke = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
                    rect.Fill = new SolidColorBrush(Color.FromArgb(Convert.ToByte(C), 90, 110, 130));
                    rect.Width = 5;
                    rect.Height = 15;
                    Canvas.SetLeft(rect, 120);
                    Canvas.SetTop(rect, 15);
                    loadingAnimation.Children.Add(rect);
                }));
                C++;
                Thread.Sleep(1);
            }

            C = 0;
            while (C < 64)
            {
                Dispatcher.BeginInvoke(new Action(delegate ()
                {
                    System.Windows.Shapes.Rectangle rect;
                    rect = new System.Windows.Shapes.Rectangle();
                    rect.Stroke = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
                    rect.Fill = new SolidColorBrush(Color.FromArgb(Convert.ToByte(C), 80, 90, 100));
                    rect.Width = 5;
                    rect.Height = 15;
                    Canvas.SetLeft(rect, 135);
                    Canvas.SetTop(rect, 15);
                    loadingAnimation.Children.Add(rect);
                }));
                C++;
                Thread.Sleep(1);
            }
            /*

            Dispatcher.BeginInvoke(new Action(delegate ()
            {
                loadingAnimation.Children.Clear();
                DrawingVisual drawingVisual = new DrawingVisual();
                DrawingContext drawingContext = drawingVisual.RenderOpen();
                Rect rect = new Rect(new System.Windows.Point(10, 10), new System.Windows.Size(15, 15));
                drawingContext.DrawRectangle(System.Windows.Media.Brushes.Aqua, (System.Windows.Media.Pen)null, rect);
                loadingAnimation.Children.Add(new VisualHost { Visual = drawingVisual });
                drawingContext.Close();
            }));

            Thread.Sleep(300);

            Dispatcher.BeginInvoke(new Action(delegate ()
            {
                DrawingVisual drawingVisual = new DrawingVisual();
                DrawingContext drawingContext = drawingVisual.RenderOpen();
                Rect rect = new Rect(new System.Windows.Point(20, 20), new System.Windows.Size(15, 15));
                drawingContext.DrawRectangle(System.Windows.Media.Brushes.Aqua, (System.Windows.Media.Pen)null, rect);
                loadingAnimation.Children.Add(new VisualHost { Visual = drawingVisual });
                drawingContext.Close();
            }));

            Thread.Sleep(300);

            Dispatcher.BeginInvoke(new Action(delegate ()
            {
                DrawingVisual drawingVisual = new DrawingVisual();
                DrawingContext drawingContext = drawingVisual.RenderOpen();
                Rect rect = new Rect(new System.Windows.Point(30, 30), new System.Windows.Size(15, 15));
                drawingContext.DrawRectangle(System.Windows.Media.Brushes.Aqua, (System.Windows.Media.Pen)null, rect);
                loadingAnimation.Children.Add(new VisualHost { Visual = drawingVisual });
                drawingContext.Close();
            }));

            */

            //Dispatcher.BeginInvoke(new Action(delegate ()
            //{
            //loadingAnimation.Children.Add(new VisualHost { Visual = drawingVisual });
            //drawingContext.Close();
            //}));

            //Thread.Sleep(1000);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            TimerCallback tm = new TimerCallback(drawLoading);
            // создаем таймер
            Timer timer = new Timer(drawLoading, null, 0, Int32.MaxValue);

            lL.Opacity = 0;
            pL.Opacity = 0;
            G.Opacity = 0;
            LogLabel.Opacity = 0;
            loginAnimation = new DoubleAnimation();
            loginAnimation.From = 0;
            loginAnimation.To = 1;
            loginAnimation.AccelerationRatio = 0.5;
            loginAnimation.DecelerationRatio = 0.5;
            loginAnimation.Duration = TimeSpan.FromSeconds(1);
            loginAnimation.Completed += LoginAnimation_Completed;
            G.BeginAnimation(Rectangle.OpacityProperty, loginAnimation);
            lB.BeginAnimation(TextBox.OpacityProperty, loginAnimation);
            pB.BeginAnimation(PasswordBox.OpacityProperty, loginAnimation);

           // loadingGif.Opacity = 0;
           // var image = new BitmapImage();
           // image.BeginInit();
           // image.UriSource = new Uri(@"Y:\Олимпиады - Ищенко\Singularity Messanger\Theme.WPF\Resources\loading_sqares.gif");
           // image.EndInit();
           // ImageBehavior.SetAnimatedSource(loadingGif, image);
        }
        private void LoginAnimation_Completed(object sender, EventArgs e)
        {
            loginAnimation = new DoubleAnimation();
            loginAnimation.From = 0;
            loginAnimation.To = 1;
            loginAnimation.AccelerationRatio = 0.5;
            loginAnimation.DecelerationRatio = 0.5;
            loginAnimation.Duration = TimeSpan.FromSeconds(1);
            lL.BeginAnimation(Label.OpacityProperty, loginAnimation);
            pL.BeginAnimation(Label.OpacityProperty, loginAnimation);
        }

        private void lB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (pB.Password.Length < 1 || pB.Password == null)
                {
                    pB.Focus();

                } else {
                    exitFromLogin();
                   // MessageBox.Show("--> from lB_KeyDown : login");
                }
            }
        }

        private void pB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) {
                //MessageBox.Show("--> from pB_KeyDown : password");
                exitFromLogin();
            }
        }
        private void exitFromLogin()
        {
            loginAnimation = new DoubleAnimation();
            loginAnimation.From = 1;
            loginAnimation.To = 0;
            loginAnimation.AccelerationRatio = 0.5;
            loginAnimation.DecelerationRatio = 0.5;
            loginAnimation.Completed += loadingTextFadein;
            loginAnimation.Duration = TimeSpan.FromSeconds(0.5);
            lL.BeginAnimation(OpacityProperty, loginAnimation);
            pL.BeginAnimation(OpacityProperty, loginAnimation);
            G.BeginAnimation(OpacityProperty, loginAnimation);
            lB.BeginAnimation(OpacityProperty, loginAnimation);
            pB.BeginAnimation(OpacityProperty, loginAnimation);


            //object value = null;
            Backbone table = new Backbone(":::\\encrypted_sd.fish");

            var _tabth = new Thread(table.Initialize);
            _tabth.Start();
            //_tabth.Join();

            if (table.Status == 0x02) {
                var mainPage = new Workpage();
                mainPage.Show();
            } else {
                MessageBox.Show("Login unsuccessful!");
            }
            //loginAnimation = new DoubleAnimation();
            //loginAnimation.From = 0;
            //loginAnimation.To = 1;
            //loginAnimation.AccelerationRatio = 0.5;
            //loginAnimation.DecelerationRatio = 0.5;
            //loginAnimation.Completed += loadingTextFadein;
            //loginAnimation.Duration = TimeSpan.FromSeconds(1.5);
            //loadingGif.BeginAnimation(OpacityProperty, loginAnimation);

            //Storyboard sbr = this.FindResource("RotateAnim") as Storyboard;
            //Storyboard.SetTarget(sbr, this.loadingGif);
            //sbr.Begin();

            //< DoubleAnimation
            //            Storyboard.TargetProperty = "(Rectangle.RenderTransform).(RotateTransform.Angle)"
            //            To = "-360" Duration = "0:0:6" RepeatBehavior = "Forever" />
        }

        public void loadingTextFadein(object sender, EventArgs e)
        {

            Storyboard sbo = this.FindResource("OpacityAnim") as Storyboard;
            Storyboard.SetTarget(sbo, this.LogLabel);
            sbo.Begin();
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (lB.Text.Length < 1 || lB.Text == null)
            {
                lB.Focus();

            }
            else if (pB.Password.Length < 1 || pB.Password == null)
            {
                pB.Focus();
            }
            else
            {
                exitFromLogin();
            }
        }
    }
}
