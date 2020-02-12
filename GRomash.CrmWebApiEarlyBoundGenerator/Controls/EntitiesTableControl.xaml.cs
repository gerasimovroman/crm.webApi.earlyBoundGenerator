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
            "Items", typeof(IEnumerable<EntityModel>), typeof(EntitiesTableControl), new PropertyMetadata());
        public static readonly DependencyProperty SelectionChangedCommandProperty = DependencyProperty.Register(
            "SelectionChangedCommand", typeof(ICommand), typeof(EntitiesTableControl), new PropertyMetadata(default(ICommand)));
        public static readonly DependencyProperty SelectedItemsProperty = DependencyProperty.Register(
            "SelectedItems", typeof(IEnumerable<EntityModel>), typeof(EntitiesTableControl), new PropertyMetadata(SelectedItemsChanged));
      

        /// <summary>
        /// Initializes a new instance of the <see cref="EntitiesTableControl"/> class.
        /// </summary>
        public EntitiesTableControl()
        {
            InitializeComponent();
            SelectionChangedCommand = new CommandGeneric<ObservableCollection<object>>(SelectionChanged);
        }

        /// <summary>
        /// Gets or sets the selected items.
        /// </summary>
        /// <value>
        /// The selected items.
        /// </value>
        public IEnumerable<EntityModel> SelectedItems
        {
            get => (ObservableCollection<EntityModel>) GetValue(SelectedItemsProperty);
            set => SetValue(SelectedItemsProperty, value);
        }

        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        /// <value>
        /// The items.
        /// </value>
        public IEnumerable<EntityModel> Items
        {
            get => (IEnumerable<EntityModel>) GetValue(ItemsProperty);
            set => SetValue(ItemsProperty, value);
        }

        /// <summary>
        /// Gets or sets the selection changed command.
        /// </summary>
        /// <value>
        /// The selection changed command.
        /// </value>
        public ICommand SelectionChangedCommand
        {
            get => (ICommand)GetValue(SelectionChangedCommandProperty);
            set => SetValue(SelectionChangedCommandProperty, value);
        }




        /// <summary>
        /// Selecteds the items changed.
        /// </summary>
        /// <param name="o">The o.</param>
        /// <param name="args">The <see cref="DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void SelectedItemsChanged(DependencyObject o, DependencyPropertyChangedEventArgs args)
        {
            var entitiesTableControl = (EntitiesTableControl)o;

            if (entitiesTableControl != null &&
                args.NewValue is IEnumerable<EntityModel> newItems)
            {
                var dataGrid = entitiesTableControl.DataGrid;
                var dataGridSelectedItems = dataGrid.SelectedItems;
                var entityModels = newItems as EntityModel[] ?? newItems.ToArray();

                if (dataGridSelectedItems.Count != entityModels.Length &&
                    entityModels.Except(dataGridSelectedItems.Cast<EntityModel>()).Any())
                {
                    var indexes = entityModels.Select(x => dataGrid.Items.IndexOf(x)).Where(x => x >= 0).ToArray();

                    if (indexes.Any())
                    {
                        dataGrid.SelectRowByIndexes(indexes);
                    }
                }
            }
        }

        /// <summary>
        /// Selections the changed.
        /// </summary>
        /// <param name="selectedItems">The selected items.</param>
        private void SelectionChanged(ObservableCollection<object> selectedItems)
        {
            SelectedItems = selectedItems.Cast<EntityModel>().ToList();
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
    }
}
