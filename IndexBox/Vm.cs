using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

    public class Vm
    {
        private readonly ObservableCollection<Item> _items;

        public Vm()
        {
            _items = new ObservableCollection<Item>
            {
                new Item(1),
                new Item(2),
                new Item(3),
                new Item(4),
            };
        }
        public ObservableCollection<Item> Items
        {
            get { return _items; }
        }
    }

    public class Item
    {
        public Item(int value)
        {
            Value = value;
        }
        public int Value { get; private set; }
    }
}
