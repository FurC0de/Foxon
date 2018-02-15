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
using System.Windows.Threading;
using System.Threading;
using System.Numerics;

namespace Theme.WPF
{
    public class Singulum
    {

    }

    public class SingulumData
    {
        public bool isNew = false;
    }

    public class Backbone
    {
        public Backbone(string storage)
        {
            DataStorage = storage;
        }

        public void Initialize()
        {
            //Thread.Sleep(5000);
            //SingulumData _RES = new SingulumData();

            //this.Status = 0x01; // 0x01 = init started
            //MainWindow.main.loginPassed = true;

            MainWindow.main.loginPassed = true;


            //if ( _RES.isNew == false )
            //{
            //    MainWindow.main.loginPassed = false;
            //    //this.Status = 0xff; //data is new. nready for work
            //}
            //else
            //{
            //    MainWindow.main.loginPassed = true;
            //    //this.Status = 0x02; //ready for work (data loaded successfully)
            //}

        } 
        public byte Status { get; internal set; } = 0x00;
        public string DataStorage { get; internal set; }
        public Singulum protocol = new Singulum();
        /*public SingulumData tryLoad(string path)
        {
            Thread.Sleep(5000);
            return new SingulumData();
        }*/
    }
}
