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
            "Items", typeof(ObservableCollection<EntityModel>), typeof(EntitiesTableControl), new PropertyMetadata(default(ObservableCollection<EntityModel>), ItemsPropChanged));

        private static void ItemsPropChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is EntitiesTableControl self)
            {
                if (e.NewValue is ObservableCollection<EntityModel> items)
                {
                    items.CollectionChanged += self.Items_CollectionChanged;
                }
            }
        }

        public static readonly DependencyProperty SelectedItemsProperty = DependencyProperty.Register(
            "SelectedItems", typeof(ObservableCollection<EntityModel>), typeof(EntitiesTableControl), new PropertyMetadata(new ObservableCollection<EntityModel>()));


        /// <summary>
        /// Initializes a new instance of the <see cref="EntitiesTableControl"/> class.
        /// </summary>
        public EntitiesTableControl()
        {
            InitializeComponent();
            if (Items != null) Items.CollectionChanged += Items_CollectionChanged;
        }

        private void Items_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems != null)
            {
                foreach (var entityModel in e.OldItems.Cast<EntityModel>())
                {
                    if (SelectedItems.Contains(entityModel))
                    {
                        SelectedItems.Remove(entityModel);
                    }
                }

                SetHasSelectedItems();
            }

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

        public static readonly DependencyProperty HasSelectedItemsProperty = DependencyProperty.Register(
            "HasSelectedItems", typeof(bool), typeof(EntitiesTableControl), new PropertyMetadata(default(bool)));

        public bool HasSelectedItems
        {
            get => (bool) GetValue(HasSelectedItemsProperty);
            set => SetValue(HasSelectedItemsProperty, value);
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

            SetHasSelectedItems();
        }

        private void SetHasSelectedItems()
        {
            HasSelectedItems = SelectedItems.Any();
        }
    }
}
