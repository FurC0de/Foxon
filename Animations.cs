using System;
using System.Windows.Media.Animation;

namespace Theme.WPF
{
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
            set { }
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
            set { }
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
}
