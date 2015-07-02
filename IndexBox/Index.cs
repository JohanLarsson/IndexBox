using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace IndexBox
{
    public static class Index
    {
        public static readonly DependencyPropertyKey OfPropertyKey = DependencyProperty.RegisterAttachedReadOnly(
            "Of",
            typeof(int),
            typeof(Index),
            new PropertyMetadata(default(int)));

        public static readonly DependencyProperty OfProperty = OfPropertyKey.DependencyProperty;

        public static readonly System.Windows.DependencyProperty InProperty = System.Windows.DependencyProperty.RegisterAttached(
            "In",
            typeof(ListBox),
            typeof(Index),
            new PropertyMetadata(default(ListBox), OnInChanged));

        public static void SetOf(this DependencyObject element, int value)
        {
            element.SetValue(OfPropertyKey, value);
        }

        public static int GetOf(this DependencyObject element)
        {
            return (int)element.GetValue(OfProperty);
        }

        public static void SetIn(this DependencyObject element, ListBox value)
        {
            element.SetValue(InProperty, value);
        }

        public static ListBox GetIn(this DependencyObject element)
        {
            return (ListBox)element.GetValue(InProperty);
        }

        private static void OnInChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue == null)
            {
                d.SetOf(-1);
                return;
            }
            var item = d.Ancestors().OfType<ListBoxItem>().First();
            var listBox = (ListBox) e.NewValue;
            d.SetOf(listBox.ItemContainerGenerator.IndexFromContainer(item));
        }

        public static IEnumerable<DependencyObject> Ancestors(this DependencyObject o)
        {
            DependencyObject parent = VisualTreeHelper.GetParent(o);
            while (parent != null)
            {
                yield return parent;
                parent = VisualTreeHelper.GetParent(parent);
            }
        }
    }
}