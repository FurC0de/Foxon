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

    public class UIMessage
    {
        public Canvas Self;

        public double Bottom = 0;

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
            border.HorizontalAlignment = HorizontalAlignment.Right;

            //adding pointer (polygon)
            Polygon signer = new Polygon();
            System.Windows.Point Point1 = new System.Windows.Point(0,  0);
            System.Windows.Point Point2 = new System.Windows.Point(10, 0);
            System.Windows.Point Point3 = new System.Windows.Point(20, 10);
            System.Windows.Point Point4 = new System.Windows.Point(0,  10);

            PointCollection myPointCollection = new PointCollection();
            myPointCollection.Add(Point1);
            myPointCollection.Add(Point2);
            myPointCollection.Add(Point3);
            myPointCollection.Add(Point4);
            signer.Points = myPointCollection;
            signer.Fill = new SolidColorBrush(Color.FromArgb(0xff, 0x9e, 0xbd, 0xe2));
            Canvas.SetLeft(signer, 130);
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
            textfield.SelectionBrush = null;
            textfield.IsHitTestVisible = true;
            textfield.AllowDrop = true;
            textfield.MinWidth = 30;
            textfield.Margin = new Thickness(10, 0, 10, -5);
            textfield.VerticalContentAlignment = VerticalAlignment.Bottom;
            textfield.TextChanged += changeSizeAuto;
            Block.SetLineHeight(textfield, 1);

            border.Child = textfield;

            Self.Children.Add(border);
            Self.Children.Add(signer);
            return Self;
        }

        private void changeSizeAuto(object sender, EventArgs e)
        {
            // to show that you'll get an enumerable of rectangles.
            IEnumerable<Border> blocks = Self.Children.OfType<Border>();
            Border blck = blocks.Last();

            RichTextBox textbox = (RichTextBox)blck.Child;

            textbox.Height = textbox.Document.GetFormattedText().Height * 1.051 + 13;
            textbox.Width = Math.Min(textbox.Document.GetFormattedText().WidthIncludingTrailingWhitespace + 30, 140);    
            blck.Height = textbox.Document.GetFormattedText().Height * 1.051 + 13;
            Self.Margin = new Thickness(10, Bottom-blck.Height+3, 0, 0);

            IEnumerable<Polygon> pointers = Self.Children.OfType<Polygon>();
            Polygon pointer = pointers.Last();
            Canvas.SetTop(pointer, blck.Height-9);
        }
    }

    public class UIElements
    {
        
        private Size MeasureString(string candidate, FontFamily ff, FontStyle fs , FontWeight fw, FontStretch fsr, int fsz)
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

            UIMessage mtest = new UIMessage();

            AnswerGrid.Children.Add(mtest.Make());

            Grid.SetRow(mtest.Self, 0);
            Grid.SetColumn(mtest.Self, 0);


            myRichTextBox.Focus();
            myRichTextBox.TextChanged += update_onChange;

            // Create a FlowDocument to contain content for the RichTextBox.
            FlowDocument myFlowDoc = new FlowDocument();

            // Add paragraphs to the FlowDocument.
            Paragraph nPar = new Paragraph();
            nPar.Inlines.Add(new Run("31415926\r\n33181930589333"));
            
            myFlowDoc.Blocks.Add(nPar);
            // Add initial content to the RichTextBox.
            myRichTextBox.Document = myFlowDoc;
        }

        public void update_onChange(object sender, EventArgs e)
        {
           
            myRichTextBox.Width = Math.Min(myRichTextBox.Document.GetFormattedText().WidthIncludingTrailingWhitespace + 30, 140);
            myRichTextBox.Height = myRichTextBox.Document.GetFormattedText().Height*1.051 + 13;
            messageBorder.Height = myRichTextBox.Document.GetFormattedText().Height*1.051 + 13;
            messageCanvas.Margin = new Thickness(10, 400+13+19-messageBorder.Height, 0, 0);
            Canvas.SetTop(pointer, messageBorder.Height-9);

        }
    }
}
