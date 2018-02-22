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
using System.Windows.Shapes;
using System.Threading;

namespace Theme.WPF
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Workpage : Window
    {
        public Workpage()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //TimerCallback tm = new TimerCallback(drawLoading);
            // создаем таймер
            //Timer timer = new Timer(drawLoading, null, 0, Int32.MaxValue);
            Aero.AutoApply(this);
            var sAnim = Animations.logChangeSize;
            sAnim.To = 1000;
            this.Width = 0;
            this.BeginAnimation(Window.WidthProperty, sAnim);
            this.BeginAnimation(Window.OpacityProperty, Animations.sFadeinAnimation);

            Grid DynamicGrid = new Grid();

            DynamicGrid.Width = 400;

            DynamicGrid.HorizontalAlignment = HorizontalAlignment.Left;

            DynamicGrid.VerticalAlignment = VerticalAlignment.Top;

            DynamicGrid.ShowGridLines = true;

            DynamicGrid.Background = new SolidColorBrush(Colors.LightSteelBlue);

            ( DynamicGrid);
        }
    }
}
