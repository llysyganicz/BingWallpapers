using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace BingWallpapers.Controls
{
    public partial class Numeric : UserControl
    {
        public Numeric()
        {
            InitializeComponent();
        }

        public int Value
        {
            get { return (int)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(int), typeof(Numeric), new FrameworkPropertyMetadata(1, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public int Step
        {
            get { return (int)GetValue(StepProperty); }
            set { SetValue(StepProperty, value); }
        }

        public static readonly DependencyProperty StepProperty =
            DependencyProperty.Register("Step", typeof(int), typeof(Numeric), new PropertyMetadata(1));

        public int Min
        {
            get { return (int)GetValue(MinProperty); }
            set { SetValue(MinProperty, value); }
        }

        public static readonly DependencyProperty MinProperty =
            DependencyProperty.Register("Min", typeof(int), typeof(Numeric), new PropertyMetadata(0));

        public int Max
        {
            get { return (int)GetValue(MaxProperty); }
            set { SetValue(MaxProperty, value); }
        }

        public static readonly DependencyProperty MaxProperty =
            DependencyProperty.Register("Max", typeof(int), typeof(Numeric), new PropertyMetadata(10));

        private void DecreaseClick(object sender, RoutedEventArgs e)
        {
            Value -= Step;
            if (Value < Min) Value = Min;
            valueText.Text = Value.ToString();
        }

        private void IncreaseClick(object sender, RoutedEventArgs e)
        {
            Value += Step;
            if (Value > Max) Value = Max;
            valueText.Text = Value.ToString();
        }

        private readonly Regex intExp = new Regex(@"\d");

        private void TextBoxLostFocus(object sender, RoutedEventArgs e)
        {
            if (!(sender is TextBox textBox)) return;

            var text = textBox.Text;
            if (intExp.IsMatch(text))
            {
                var value = int.Parse(text);
                if (value < Min) Value = Min;
                else if (value > Max) Value = Max;
                else Value = value;

                textBox.Text = Value.ToString();
            }
        }

        private void NumericLoaded(object sender, RoutedEventArgs e)
        {
            valueText.Text = Value.ToString();
        }

    }
}
