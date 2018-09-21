//using System.Collections.Generic;
//using System.Collections.ObjectModel;
//using System.Windows;
//using ChainTicker.App.Models;
//using Xceed.Wpf.Toolkit;

//namespace ChainTicker.App
//{
//    public class CheckListBoxExtension : DependencyObject
//    {

//        public static ObservableCollection<MarketModel> GetSelectedItems(DependencyObject obj)
//        {
//            return (ObservableCollection<MarketModel>)obj.GetValue(SelectedItemsProperty);
//        }

//        public static void SetSelectedItems(DependencyObject obj, ObservableCollection<MarketModel> value)
//        {
//            obj.SetValue(SelectedItemsProperty, value);
//        }

//        // Using a DependencyProperty as the backing store for SelectedItems.  This enables animation, styling, binding, etc...
//        public static readonly DependencyProperty SelectedItemsProperty =
//            DependencyProperty.RegisterAttached("SelectedItems", typeof(ObservableCollection<MarketModel>), typeof(CheckListBoxExtension), new PropertyMetadata(null, new PropertyChangedCallback(OnSelectedItemsChanged)));
//        static bool isInternal = false;
//        private static void OnSelectedItemsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
//        {

//            if (d is CheckListBox)
//            {
//                isInternal = true;
//                foreach (var item in (d as CheckListBox).Items)
//                {
//                    if ((d as CheckListBox).ItemContainerGenerator.ContainerFromItem(item) is CheckListBox childItem)
//                        childItem.IsSelected = false;
//                }
//                if (d.GetValue(CheckListBoxExtension.SelectedItemsProperty) is List<MarketModel> selectedItems)
//                {
//                    foreach (var item in selectedItems)
//                    {
//                        if ((d as CheckListBox).ItemContainerGenerator.ContainerFromItem(item) is CheckListBoxItem childItem)
//                            childItem.IsSelected = true;
//                    }
//                }
//                isInternal = false;
//            }
//        }


//        public static bool GetCanHaveSelectedItems(DependencyObject obj)
//        {
//            return (bool)obj.GetValue(CanHaveSelectedItemsProperty);
//        }

//        public static void SetCanHaveSelectedItems(DependencyObject obj, bool value)
//        {
//            obj.SetValue(CanHaveSelectedItemsProperty, value);
//        }

//        // Using a DependencyProperty as the backing store for CanHaveSelectedItems.  This enables animation, styling, binding, etc...
//        public static readonly DependencyProperty CanHaveSelectedItemsProperty =
//            DependencyProperty.RegisterAttached("CanHaveSelectedItems", typeof(bool), typeof(CheckListBoxExtension), new PropertyMetadata(false, new PropertyChangedCallback(OnCanHaveSelectedItemsChanged)));

//        private static void OnCanHaveSelectedItemsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
//        {
//            if ((bool)e.NewValue)
//            {
//                if (d is CheckListBox)
//                {
//                    (d as CheckListBox).SelectionChanged += CheckListBoxExtension_SelectionChanged;
//                    (d as CheckListBox).Loaded += CheckListBoxExtension_Loaded;
//                }
//            }
//        }

//        static void CheckListBoxExtension_Loaded(object d, RoutedEventArgs e)
//        {
//            if (d is CheckListBox)
//            {
//                isInternal = true;
//                foreach (var item in (d as CheckListBox).Items)
//                {
//                    var childItem = (d as CheckListBox).ItemContainerGenerator.ContainerFromItem(item) as CheckListBoxItem;
//                    if (childItem != null)
//                        childItem.IsSelected = false;
//                }
//                var selectedItems = (d as CheckListBox).GetValue(CheckListBoxExtension.SelectedItemsProperty) as ObservableCollection<MarketModel>;
//                if (selectedItems != null)
//                {
//                    foreach (var item in selectedItems)
//                    {
//                        var childItem = (d as CheckListBox).ItemContainerGenerator.ContainerFromItem(item) as CheckListBoxItem;
//                        if (childItem != null)
//                            childItem.IsSelected = true;
//                    }
//                }
//                isInternal = false;
//            }
//        }

//        static void CheckListBoxExtension_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
//        {
//            if (!isInternal)
//            {
//                var selectedItems = (sender as CheckListBox).GetValue(CheckListBoxExtension.SelectedItemsProperty) as ObservableCollection<MarketModel>;
//                if (selectedItems != null)
//                {
//                    if (e.AddedItems.Count > 0)
//                    {
//                        foreach (var item in e.AddedItems)
//                        {
//                            selectedItems.Add((MarketModel)item);
//                        }
//                    }
//                    if (e.RemovedItems.Count > 0)
//                    {
//                        foreach (var item in e.RemovedItems)
//                        {
//                            selectedItems.Remove((MarketModel)item);
//                        }
//                    }
//                }
//            }
//        }



//    }
//}
