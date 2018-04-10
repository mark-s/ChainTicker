using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace ChainTicker.Ui.WpfAssets.UserControls
{
    public partial class MarqueeControl : UserControl
    {
        public MarqueeControl()
        {
            InitializeComponent();
            CanvasMain.Height = Height;
            CanvasMain.Width = Width;
        }


        public double ScrollDurationSeconds
        {
            get => (double)GetValue(ScrollDurationSecondsProperty);
            set => SetValue(ScrollDurationSecondsProperty, value);
        }
        public static readonly DependencyProperty ScrollDurationSecondsProperty =
            DependencyProperty.Register("ScrollDurationSeconds", typeof(double), typeof(MarqueeControl), new PropertyMetadata((double)0.0));

        public object InnerContent
        {
            get => GetValue(InnerContentProperty);
            set => SetValue(InnerContentProperty, value);
        }

        public static readonly DependencyProperty InnerContentProperty =
            DependencyProperty.Register("InnerContent", typeof(object), typeof(MarqueeControl), new PropertyMetadata(null));

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
                           LeftToRightMarquee();
        }
        
        

        private void LeftToRightMarquee()
        {
            var height = CanvasMain.ActualHeight - MainContent.ActualHeight;
            MainContent.Margin = new Thickness(0, height / 2, 0, 0);

            var doubleAnimation = new DoubleAnimation
            {
                From = -MainContent.ActualWidth,
                To = CanvasMain.ActualWidth,
                RepeatBehavior = RepeatBehavior.Forever,
                AutoReverse = true,
                Duration = new Duration(TimeSpan.FromSeconds(ScrollDurationSeconds))
            };

            MainContent.BeginAnimation(Canvas.LeftProperty, doubleAnimation);
        }


    }

}

