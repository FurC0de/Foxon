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

    public partial class MainWindow : Window
    {

        private void _PASSERCHK()
        {
            //Must be called in a new thread

            var BB = new Backbone("::");
            BB.Initialize(); //May work as long as you need

            if (BB.Status == 0x02) //If login successful
            {
                Dispatcher.BeginInvoke(new Action(delegate ()
                {
                    _loginPassed = true;
                }));
            }
        }

        public static DoubleAnimation loginAnimation;
        public MainWindow()
        {
            
            InitializeComponent();
            main = this;

        }

        public static MainWindow main {
            get;
            internal set;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
            this.Width = 0;
            this.BeginAnimation(Window.WidthProperty, Animations.logChangeSize );
            this.BeginAnimation(Window.OpacityProperty, Animations.sFadeinAnimation);

            Aero.AutoApply(this);

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
        public bool _loginPassed;

        public  bool loginPassed
        {
            get { return _loginPassed; }
            set { Dispatcher.BeginInvoke(new Action(() => { _loginPassed = value; })); }
        }

        public void checkStabLog(object state)
        {
            while (true)
            {
                Dispatcher.BeginInvoke(new Action(delegate ()
                {
                    this.WindowLable.Content = _loginPassed.ToString();
                }));

                if (_loginPassed == false)
                {

                }
                else
                {
                    _loginPassed = false;
                    Dispatcher.BeginInvoke(new Action(delegate ()
                    {
                        this.WindowLable.Content = "LOGGED IN!";
                        DoubleAnimation outAnim = Animations.logChangeSizeBack;
                        outAnim.Completed += exitOnSlip;
                        this.BeginAnimation(Window.WidthProperty, outAnim);
                        this.BeginAnimation(Window.OpacityProperty, Animations.sFadeoutAnimation);
                    }));
                }
                Thread.Sleep(200);
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

            var _tabthch = new Thread(this.checkStabLog);
            _tabthch.Start();

            var _tabth = new Thread(this._PASSERCHK);
            _tabth.Start();
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
