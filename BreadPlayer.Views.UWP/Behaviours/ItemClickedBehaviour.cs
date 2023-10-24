﻿using BreadPlayer.Core.Common;
using BreadPlayer.Interfaces;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace BreadPlayer.Behaviours
{
    internal class ItemClickCommand
    {
        public static readonly DependencyProperty CommandProperty = DependencyProperty.RegisterAttached("Command", typeof(ICommand),
            typeof(ItemClickCommand), new PropertyMetadata(null, OnCommandPropertyChanged));

        public static void SetCommand(DependencyObject d, ICommand value)
        {
            d.SetValue(CommandProperty, value);
        }

        public static ICommand GetCommand(DependencyObject d)
        {
            return (ICommand)d.GetValue(CommandProperty);
        }

        private static void OnCommandPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is GridView control)
            {
                control.ItemClick += OnItemClick;
            }
        }

        private static void OnItemClick(object sender, ItemClickEventArgs e)
        {
            var control = sender as GridView;
            if (control.SelectedItem == e.ClickedItem)
            {
                var command = GetCommand(control);

                if (command != null && command.CanExecute(e.ClickedItem))
                {
                    command.Execute(e.ClickedItem);
                }
            }
        }
    }
}