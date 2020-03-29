using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using GRomash.CrmWebApiEarlyBoundGenerator.Command;
using GRomash.CrmWebApiEarlyBoundGenerator.Infrastructure.Extensions;
using GRomash.CrmWebApiEarlyBoundGenerator.Infrastructure.Model;
using GRomash.CrmWebApiEarlyBoundGenerator.ViewModels;
using MouseEventArgs = System.Windows.Input.MouseEventArgs;
using UserControl = System.Windows.Controls.UserControl;

namespace GRomash.CrmWebApiEarlyBoundGenerator.Controls
{
    /// <summary>
    /// Interaction logic for EntitiesTableControl.xaml
    /// </summary>
    public partial class EntitiesTableControl 
    {
        public static readonly DependencyProperty ItemsProperty = DependencyProperty.Register(
            "Items", typeof(ObservableCollection<EntityModel>), typeof(EntitiesTableControl), new PropertyMetadata(default(ObservableCollection<EntityModel>)));
        public static readonly DependencyProperty SelectedItemsProperty = DependencyProperty.Register(
            "SelectedItems", typeof(ObservableCollection<EntityModel>), typeof(EntitiesTableControl), new PropertyMetadata(new ObservableCollection<EntityModel>()));


        /// <summary>
        /// Initializes a new instance of the <see cref="EntitiesTableControl"/> class.
        /// </summary>
        public EntitiesTableControl()
        {
            InitializeComponent();
        }

        public ObservableCollection<EntityModel> Items
        {
            get => (ObservableCollection<EntityModel>) GetValue(ItemsProperty);
            set => SetValue(ItemsProperty, value);
        }

        public ObservableCollection<EntityModel> SelectedItems
        {
            get => (ObservableCollection<EntityModel>) GetValue(SelectedItemsProperty);
            set => SetValue(SelectedItemsProperty, value);
        }




        /// <summary>
        /// Mouses the enter handler.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Windows.Input.MouseEventArgs"/> instance containing the event data.</param>
        private void MouseEnterHandler(object sender, MouseEventArgs e)
        {
            if (e.LeftButton != MouseButtonState.Pressed || !(e.OriginalSource is DataGridRow row)) return;

            row.IsSelected = !row.IsSelected;
            e.Handled = true;
        }

        /// <summary>
        /// Previews the mouse down handler.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="MouseButtonEventArgs"/> instance containing the event data.</param>
        private void PreviewMouseDownHandler(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton != MouseButtonState.Pressed || !(e.OriginalSource is FrameworkElement element) ||
                !(element.GetVisualParentOfType<DataGridRow>() is DataGridRow row)) return;

            row.IsSelected = !row.IsSelected;
            e.Handled = true;
        }

        private void DataGrid_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                foreach (var addedItem in e.AddedItems)
                {
                    SelectedItems.Add(addedItem as EntityModel);
                }
            }

            if (e.RemovedItems.Count > 0)
            {
                foreach (var removedItem in e.RemovedItems)
                {
                    SelectedItems.Remove(removedItem as EntityModel);
                }
            }
        }
    }
}
