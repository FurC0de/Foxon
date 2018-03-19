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
using System.Globalization;

namespace Theme.WPF
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    /// 
    

    public class mPositioner
    {
        public mPositioner(Grid parent)
        {
            mainParent = parent;
        }

        public mPositioner()
        {

        }

        Grid mainParent;

        public double messageCoordinatesCounter = 0;

        public void Next (UIMessage m)
        {
            messageCoordinatesCounter += m.Size.Height + 10;
        }

        public void addToList(UIMessage m)
        {
            Next(m);
            if (m.isUserMessage)
            {
                m.Position = new Point(10, messageCoordinatesCounter);
            }
            else
            {
                m.Position = new Point(mainParent.Width - 160, messageCoordinatesCounter);
            }
            mainParent.Height = messageCoordinatesCounter;
        }
    }

    public class UIMessage
    {
        public UIMessage(bool isSelfUser)
        {
            this.isUserMessage = isSelfUser;
            this.Self = Make();
        }

        public UIMessage(double x, double y, bool isSelfUser)
        {
            this.isUserMessage = isSelfUser;
            this._position = new Point(x, y);
            this.Self = Make();
        }

        public UIMessage(Point pos, bool isSelfUser)
        {
            this.isUserMessage = isSelfUser;
            this._position = pos;
            this.Self = Make();
        }

        public Canvas Self;

        public bool isUserMessage = true;

        private Point _position;

        public Point Position
        {
            get { return _position; }
            set { _position = value; changeSizeAuto(this, null);  }
        }

        /*
        public Canvas Make(Point Position)
        {
            this.Position = Position;
            return Make();
        }

        public Canvas Make(double x, double y)
        {
            this.Position = new Point(x, y);
            return Make();
        }
        */
        public Canvas Make()
        {
            //create new canvas
            Self = new Canvas();
            Self.HorizontalAlignment = HorizontalAlignment.Left;
            Self.Height = 33;
            Self.Width = 150;
            Self.Margin = new Thickness(200, 300, 0, 0);
            Self.VerticalAlignment = VerticalAlignment.Top;
            Grid.SetColumn(Self, 0);
            Grid.SetRow(Self, 0);

            //create new border and place it in a canvas
            Border border = new Border();
            border.BorderThickness = new Thickness(0);
            border.CornerRadius = new CornerRadius(8);
            border.Width = 140;
            border.Height = 33;
            border.Background = new SolidColorBrush(Color.FromArgb(0xff, 0x9e, 0xbd, 0xe2));
            Canvas.SetTop(border, 1);
            Panel.SetZIndex(border, 1);
            border.VerticalAlignment = VerticalAlignment.Bottom;


            if (!isUserMessage)
            {
                border.HorizontalAlignment = HorizontalAlignment.Left;
            }
            else
            {
                border.HorizontalAlignment = HorizontalAlignment.Right;
            }
            //adding pointer (polygon)
            System.Windows.Point Point1;
            System.Windows.Point Point2;
            System.Windows.Point Point3;
            System.Windows.Point Point4;
            Polygon signer = new Polygon();
            if (isUserMessage)
            {
                Point2 = new System.Windows.Point(10, 0);
                Point3 = new System.Windows.Point(20, 0);
                Point4 = new System.Windows.Point(20, 10);
                Point1 = new System.Windows.Point(0, 10);
            }
            else
            {
                Point1 = new System.Windows.Point(0, 0);
                Point2 = new System.Windows.Point(10, 0);
                Point3 = new System.Windows.Point(20, 10);
                Point4 = new System.Windows.Point(0, 10);
            }

            PointCollection myPointCollection = new PointCollection();
            myPointCollection.Add(Point1);
            myPointCollection.Add(Point2);
            myPointCollection.Add(Point3);
            myPointCollection.Add(Point4);
            signer.Points = myPointCollection;
            signer.Fill = new SolidColorBrush(Color.FromArgb(0xff, 0x9e, 0xbd, 0xe2));

            if (!isUserMessage)
            {
                Canvas.SetLeft(signer, 130);
            }
            else
            {
                Canvas.SetLeft(signer, 10);
            }
            
            Canvas.SetTop(signer, 21);
            signer.RenderTransformOrigin = new Point(0, 0);

            //adding richtext field
            RichTextBox textfield = new RichTextBox();
            textfield.FontFamily = new FontFamily("Roboto");
            textfield.FontSize = 14;
            textfield.Foreground = new SolidColorBrush(Color.FromArgb(0xff, 0x4f, 0x4a, 0x4a));
            textfield.Padding = new Thickness(5);
            textfield.VerticalAlignment = VerticalAlignment.Top;
            textfield.HorizontalAlignment = HorizontalAlignment.Center;
            textfield.Height = 35;
            textfield.Width = 120;
            textfield.Background = null;
            textfield.BorderBrush = null;
            textfield.SelectionBrush = new SolidColorBrush(Color.FromArgb(0x44, 0x21, 0x21, 0x21));
            textfield.IsHitTestVisible = true;
            textfield.AllowDrop = true;
            textfield.MinWidth = 30;
            textfield.IsReadOnly = true;
            textfield.Margin = new Thickness(10, 0, 10, -5);
            textfield.VerticalContentAlignment = VerticalAlignment.Bottom;
            textfield.TextChanged += changeSizeAuto;
            Block.SetLineHeight(textfield, 1);
            border.Child = textfield;

            //adding time
            TextBlock time = new TextBlock();
            time.Width = 30;
            time.Text = DateTime.Now.ToString("t");
            time.Foreground = new SolidColorBrush(Color.FromArgb(0xbb, 0x4f, 0x4f, 0x4f));
            time.FontFamily = new FontFamily("Roboto");
            time.FontSize = 11;
            time.Margin = new Thickness(10,10,10,10);

            Self.Children.Add(time);
            Canvas.SetZIndex(time, 9999);
            Self.Children.Add(border);
            Self.Children.Add(signer);
            return Self;
        }

        public void setText(string text)
        {
            IEnumerable<Border> blocks = Self.Children.OfType<Border>();
            Border blck = blocks.Last();
            RichTextBox textbox = (RichTextBox)blck.Child;

            FlowDocument myFlowDoc = new FlowDocument();
            // Add paragraphs to the FlowDocument.
            Paragraph Par = new Paragraph();
            string[] Runs = text.Split(new string[] { "\r\n", "\r" }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < Runs.Length; i++)
            {
                if (i == Runs.Length - 1)
                {
                    Par.Inlines.Add((new Run(Runs[i])));
                }
                else
                {
                    Par.Inlines.Add((new Run(Runs[i] + Environment.NewLine)));
                }
                //myFlowDoc.Blocks.Add(new Paragraph(new Run(a)));
            }

            myFlowDoc.Blocks.Add(Par);
            // Add initial content to the RichTextBox.
            textbox.Document = myFlowDoc;
        }

        public Size Size;

        private void changeSizeAuto(object sender, EventArgs e)
        {
            // to show that you'll get an enumerable of rectangles.
            IEnumerable<Border> blocks = Self.Children.OfType<Border>();
            Border blck = blocks.Last();

            RichTextBox textbox = (RichTextBox)blck.Child;

            textbox.Height = textbox.Document.GetFormattedText().Height * 1.051 + 13;
            textbox.Width = Math.Min(textbox.Document.GetFormattedText().WidthIncludingTrailingWhitespace + 30, 300);
            blck.Height = textbox.Document.GetFormattedText().Height * 1.051 + 13;
            blck.Width = Math.Min(textbox.Document.GetFormattedText().WidthIncludingTrailingWhitespace + 30, 300);
            if (isUserMessage)
            {
                Canvas.SetLeft(blck, 20);
            }
            else
            {
                Canvas.SetRight(blck, 10);
            }
            
            Self.Margin = new Thickness(Position.X, Position.Y - blck.Height + 3, 0, 0);

            IEnumerable<Polygon> pointers = Self.Children.OfType<Polygon>();
            Polygon pointer = pointers.Last();
            Canvas.SetTop(pointer, blck.Height - 9);

            IEnumerable<TextBlock> textblocks = Self.Children.OfType<TextBlock>();
            TextBlock txtblk = textblocks.Last();
            Canvas.SetTop(txtblk, blck.Height - 24);

            if (isUserMessage)
            {
                Canvas.SetLeft(txtblk, blck.Width);
            }
            else
            {
                Canvas.SetRight(txtblk, blck.Width);
                
            }

            this.Size = new Size(blck.Width, blck.Height);
        }
    }

    public class UIElements
    {

        private Size MeasureString(string candidate, FontFamily ff, FontStyle fs, FontWeight fw, FontStretch fsr, int fsz)
        {
            var formattedText = new FormattedText(
                candidate,
                CultureInfo.CurrentCulture,
                FlowDirection.LeftToRight,
                new Typeface(ff, fs, fw, fsr),
                fsz,
                Brushes.Black,
                new NumberSubstitution(),
                TextFormattingMode.Ideal);

            return new Size(formattedText.Width, formattedText.Height);
        }


    }

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

            UIMessage mtest1 = new UIMessage(AnswerGrid.Width - 160, 160, false);
            mainGrid.Children.Add(mtest1.Self);
            Grid.SetRow(mtest1.Self, 0);
            Grid.SetColumn(mtest1.Self, 0);
            mtest1.setText("Помню\r\nНе зря\r\nПятый день ноября\r\nИ заговор\r\nПороховой\r\n since \r\n 1487\r\n  487\r\n   87\r\n    7");

            UIMessage mtest2 = new UIMessage(AnswerGrid.Width - 160, 160, true);
            mainGrid.Children.Add(mtest2.Self);
            Grid.SetRow(mtest2.Self, 0);
            Grid.SetColumn(mtest2.Self, 0);
            mtest2.setText("3141\r\n5926\r\n3318\r\n1930\r\n5893\r\n3223");

            UIMessage mtest3 = new UIMessage(AnswerGrid.Width - 160, 160, false);
            mainGrid.Children.Add(mtest3.Self);
            Grid.SetRow(mtest3.Self, 0);
            Grid.SetColumn(mtest3.Self, 0);
            mtest3.setText("3141\r\n5926\r\n3318\r\n1930\r\n5893\r\n3223");

            mPositioner p = new mPositioner(mainGrid);
            p.addToList(mtest1);
            p.addToList(mtest2);
            p.addToList(mtest3);
            mainGrid.Height = p.messageCoordinatesCounter;
            /*
            UIMessage mtest = new UIMessage();
            mainGrid.Children.Add(mtest.Make(AnswerGrid.Width-160, 160));
            Grid.SetRow(mtest.Self, 0);
            Grid.SetColumn(mtest.Self, 0);
            mtest.setText("3141\r\n5926\r\n3318\r\n1930\r\n5893\r\n3223");

            UIMessage mtest2 = new UIMessage();
            mainGrid.Children.Add(mtest2.Make(AnswerGrid.Width - 160, AnswerGrid.ActualHeight));
            Grid.SetRow(mtest2.Self, 0);
            Grid.SetColumn(mtest2.Self, 0);
            mtest2.setText("answernumber2\r\n fuckitfuckifuckitfuckitfuckit\r\n fuckit\r\n fuckit\r\n fuckit\r\n fuckit\r\n fuckit\r\n fuckit\r\n fuckit\r\n fuckit\r\n fuckit\r\n fuckit\r\n fuckit\r\n fuckit\r\n fuckit");

            UIMessage mtest3 = new UIMessage();
            mainGrid.Children.Add(mtest3.Make(AnswerGrid.Width - 160, AnswerGrid.ActualHeight+400));
            Grid.SetRow(mtest3.Self, 0);
            Grid.SetColumn(mtest3.Self, 0);
            mtest3.setText("answernumber3\r\n fuckitfuckifuckitfuckitfuckit\r\n fuckit\r\n fuckit\r\n fuckit\r\n fuckit\r\n fuckit\r\n fuckit\r\n fuckit\r\n fuckit\r\n fuckit\r\n fuckit\r\n fuckit\r\n fuckit\r\n fuckit");
            mainGrid.Height = 1500;
            */
        }
    }
}
