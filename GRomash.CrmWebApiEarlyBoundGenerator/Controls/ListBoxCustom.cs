using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;

namespace GRomash.CrmWebApiEarlyBoundGenerator.Controls
{
    /// <summary>
    /// Custom list box
    /// </summary>
    /// <seealso cref="System.Windows.Controls.ListBox" />
    public class ListBoxCustom : ListBox
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ListBoxCustom"/> class.
        /// </summary>
        public ListBoxCustom()
        {
            SelectionChanged += ListBoxCustom_SelectionChanged;
        }

        /// <summary>
        /// Handles the CollectionChanged event of the SelectedItemsList control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="NotifyCollectionChangedEventArgs"/> instance containing the event data.</param>
        public void SelectedItemsList_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            Dispatcher?.Invoke(() =>
            {
                if (e.Action == NotifyCollectionChangedAction.Reset)
                {
                    SelectedItems.Clear();
                }
                else
                {
                    if (e.NewItems?.Count > 0)
                    {
                        AddRangeToList(e.NewItems, SelectedItems);
                    }

                    if (e.OldItems?.Count > 0)
                    {
                        foreach (var oldItem in e.OldItems)
                        {
                            RemoveAllFromList(SelectedItemsList, oldItem);
                        }
                    }
                }
            });
        }

        /// <summary>
        /// Removes all from list.
        /// </summary>
        /// <param name="removeFrom">The remove from.</param>
        /// <param name="item">The item.</param>
        private void RemoveAllFromList(IList removeFrom, object item)
        {
            while (removeFrom.Contains(item))
            {
                removeFrom.Remove(item);
            }
        }

        /// <summary>
        /// Adds the range to list.
        /// </summary>
        /// <param name="add">The add.</param>
        /// <param name="addTo">The add to.</param>
        private void AddRangeToList(IList add, IList addTo)
        {
            foreach (var addItem in add)
            {
                if (!addTo.Contains(addItem))
                {
                    addTo.Add(addItem);
                }
            }
        }

        /// <summary>
        /// Handles the SelectionChanged event of the ListBoxCustom control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="SelectionChangedEventArgs"/> instance containing the event data.</param>
        private void ListBoxCustom_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Dispatcher?.Invoke(() =>
            {
                if (e.AddedItems?.Count > 0)
                {
                    AddRangeToList(e.AddedItems, SelectedItemsList);
                }

                if (e.RemovedItems?.Count > 0)
                {
                    foreach (var eRemovedItem in e.RemovedItems)
                    {
                        RemoveAllFromList(SelectedItemsList, eRemovedItem);
                    }
                }
            });
        }

        /// <summary>
        /// Gets or sets the selected items list.
        /// </summary>
        /// <value>
        /// The selected items list.
        /// </value>
        public ObservableCollection<string> SelectedItemsList
        {
            get => (ObservableCollection<string>)GetValue(SelectedItemsListProperty);
            set => SetValue(SelectedItemsListProperty, value);
        }

        /// <summary>
        /// The selected items list property
        /// </summary>
        public static readonly DependencyProperty SelectedItemsListProperty =
            DependencyProperty.Register("SelectedItemsList", typeof(ObservableCollection<string>), typeof(ListBoxCustom), new FrameworkPropertyMetadata(
                (s, e) =>
                {
                    var listBox = s as ListBoxCustom;
                    if (e.NewValue is ObservableCollection<string> newList)
                        if (listBox != null)
                            newList.CollectionChanged += listBox.SelectedItemsList_CollectionChanged;
                }));
    }
}
