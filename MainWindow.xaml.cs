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
using System.Numerics;
using System.Runtime.InteropServices;
using System.Windows.Interop;
using System.Windows.Threading;

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

    public static class Animations
    {
        public static DoubleAnimation logChangeSize
        {
            get
            { 
                var t = new DoubleAnimation(); //0, 440, TimeSpan.FromSeconds(1)
                t.From = 0;
                t.To = 440;
                t.Duration = TimeSpan.FromSeconds(1);
                t.DecelerationRatio = 0.5;
                t.AccelerationRatio = 0.5;
                return t;
            }
            set{}
        }

        public static DoubleAnimation sFadeinAnimation
        {
            get
            { 
                var t = new DoubleAnimation(); //0, 1, TimeSpan.FromSeconds(1)
                t.From = 0;
                t.To = 1;
                t.Duration = TimeSpan.FromSeconds(1);
                t.AccelerationRatio = 0.5;
                t.DecelerationRatio = 0.5;
                return t;
            }
            set{}
        }

        public static DoubleAnimation logChangeSizeBack
        {
            get
            {
                var t = new DoubleAnimation(); //0, 440, TimeSpan.FromSeconds(1)
                t.From = 440;
                t.To = 0;
                //t.Completed += ;
                t.Duration = TimeSpan.FromSeconds(1);
                t.DecelerationRatio = 0.5;
                t.AccelerationRatio = 0.5;
                return t;
            }
            set { }
        }

        public static DoubleAnimation sFadeoutAnimation
        {
            get
            {
                var t = new DoubleAnimation(); //0, 1, TimeSpan.FromSeconds(1)
                t.From = 1;
                t.To = 0;
                t.Duration = TimeSpan.FromSeconds(1);
                t.AccelerationRatio = 0.5;
                t.DecelerationRatio = 0.5;
                return t;
            }
            set { }
        }

    }

    public partial class MainWindow : Window
    {

        public static DoubleAnimation loginAnimation;
        public MainWindow()
        {
            
            InitializeComponent();
            main = this;
            /*double n = 0.0;
            for (n = 0; n < 1; n+=0.1) {
                this.Opacity = n;
                Thread.Sleep(10);
            }*/

            // анимация для ширины

        }

        public static MainWindow main {
            get;
            internal set;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
        }
        public static int alphaCounter = 0;

        public void drawLoading(object state)
        {
            /*
            int C = 0;
            while (C < 64)
            {
                .BeginInvoke(new Action(delegate ()
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
            
            for (double i = 0; i <= 1; i+=0.05) { 
                Dispatcher.BeginInvoke(new Action(delegate ()
                {
                    this.Opacity = i;
                }));
                Thread.Sleep(20);
            }*/
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
            this.Width = 0;
            this.BeginAnimation(Window.WidthProperty, Animations.logChangeSize );
            this.BeginAnimation(Window.OpacityProperty, Animations.sFadeinAnimation);

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

        }
        private void LoginAnimation_Completed(object sender, EventArgs e)
        {
            //GlassEffectHelper.EnableGlassEffect(this, true);
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

        private void exitOnSlip(object sender, EventArgs e)
        {
            var mainPage = new Workpage();
            mainPage.Show();
            this.Close();
        }

        private void pB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) {
                //MessageBox.Show("--> from pB_KeyDown : password");
                exitFromLogin();
            }
        }
        private bool _loginPassed;

        public bool loginPassed
        {
            get { return _loginPassed; }
            set { Dispatcher.BeginInvoke(new Action(() => { _loginPassed = value; })); }
        }

        private void checkStabLog(object state)
        {
            if (!loginPassed)
            {
                Dispatcher.BeginInvoke(new Action(delegate ()
                {
                    this.WindowLable.Content = "NOT LOGGED IN!";
                }));
                
            }
            else
            {
                loginPassed = false;
                Dispatcher.BeginInvoke(new Action(delegate ()
                {
                    this.WindowLable.Content = "LOGGED IN!";
                    DoubleAnimation outAnim = Animations.logChangeSizeBack;
                    outAnim.Completed += exitOnSlip;
                    this.BeginAnimation(Window.WidthProperty, outAnim);
                    this.BeginAnimation(Window.OpacityProperty, Animations.sFadeoutAnimation);
                }));
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
            loginButton.BeginAnimation(OpacityProperty, loginAnimation);

            var fadeinAnimation = new DoubleAnimation();
            fadeinAnimation.From = 0;
            fadeinAnimation.To = 1;
            fadeinAnimation.AccelerationRatio = 0.5;
            fadeinAnimation.DecelerationRatio = 0.5;
            fadeinAnimation.Duration = TimeSpan.FromSeconds(1);
            //this.BeginAnimation(Backgrou, fadeinAnimation);

            Backbone table = new Backbone(":::\\encrypted_sd.fish");

            var _tabth = new Thread(table.Initialize);
            _tabth.Start();

            //TimerCallback tm = new TimerCallback(checkStabLog);
            // создаем таймер
            Timer timer = new Timer(checkStabLog, null, 0, 500);
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
