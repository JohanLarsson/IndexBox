using System.Windows;
using System.Windows.Controls;

namespace IndexBox
{
    public static class Index
    {
        public static readonly DependencyPropertyKey OfPropertyKey = DependencyProperty.RegisterAttachedReadOnly(
            "Of",
            typeof(int),
            typeof(Index),
            new PropertyMetadata(-1));

        public static readonly DependencyProperty OfProperty = OfPropertyKey.DependencyProperty;

        public static readonly DependencyProperty InProperty = DependencyProperty.RegisterAttached(
            "In",
            typeof(ItemsControl),
            typeof(Index),
            new PropertyMetadata(default(ItemsControl), OnInChanged));

        public static void SetOf(this DependencyObject element, int value)
        {
            element.SetValue(OfPropertyKey, value);
        }

        public static int GetOf(this DependencyObject element)
        {
            return (int)element.GetValue(OfProperty);
        }

        public static void SetIn(this DependencyObject element, ItemsControl value)
        {
            element.SetValue(InProperty, value);
        }

        public static ItemsControl GetIn(this DependencyObject element)
        {
            return (ItemsControl)element.GetValue(InProperty);
        }

        private static void OnInChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var itemsControl = e.NewValue as ItemsControl;
            if (itemsControl == null)
            {
                return;
            }
            var itemContainerGenerator = itemsControl.ItemContainerGenerator;
            var indexFromContainer = itemContainerGenerator.IndexFromContainer(d);
            d.SetOf(indexFromContainer);
        }
    }
}