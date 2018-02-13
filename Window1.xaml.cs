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

        public void drawLoading(object state)
        {
            
            for (double i = 0; i <= 2; i += 0.05)
            {
                Dispatcher.BeginInvoke(new Action(delegate ()
                {
                    this.Opacity = i;
                }));
                Thread.Sleep(10);
            }
 
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            TimerCallback tm = new TimerCallback(drawLoading);
            // создаем таймер
            Timer timer = new Timer(drawLoading, null, 0, Int32.MaxValue);

        }
    }
}
