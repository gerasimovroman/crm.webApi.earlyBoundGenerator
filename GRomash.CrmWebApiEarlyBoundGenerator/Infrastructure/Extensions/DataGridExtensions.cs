using System;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace GRomash.CrmWebApiEarlyBoundGenerator.Infrastructure.Extensions
{
    /// <summary>
    /// Extensions for DataGrid
    /// </summary>
    public static class DataGridExtensions
    {
        /// <summary>
        /// Selects the row by indexes.
        /// </summary>
        /// <param name="dataGrid">The data grid.</param>
        /// <param name="rowIndexes">The row indexes.</param>
        /// <exception cref="ArgumentException">
        /// The SelectionUnit of the DataGrid must be set to FullRow.
        /// or
        /// The SelectionMode of the DataGrid must be set to Extended.
        /// or
        /// Invalid number of indexes.
        /// or
        /// </exception>
        public static void SelectRowByIndexes(this DataGrid dataGrid, params int[] rowIndexes)
        {
            if (!dataGrid.SelectionUnit.Equals(DataGridSelectionUnit.FullRow))
                throw new ArgumentException("The SelectionUnit of the DataGrid must be set to FullRow.");

            if (!dataGrid.SelectionMode.Equals(DataGridSelectionMode.Extended))
                throw new ArgumentException("The SelectionMode of the DataGrid must be set to Extended.");

            if (rowIndexes.Length.Equals(0) || rowIndexes.Length > dataGrid.Items.Count)
                throw new ArgumentException("Invalid number of indexes.");

            dataGrid.SelectedItems.Clear();
            foreach (int rowIndex in rowIndexes)
            {
                if (rowIndex < 0 || rowIndex > (dataGrid.Items.Count - 1))
                    throw new ArgumentException(string.Format("{0} is an invalid row index.", rowIndex));

                object item = dataGrid.Items[rowIndex]; //=Product X
                dataGrid.SelectedItems.Add(item);

                DataGridRow row = dataGrid.ItemContainerGenerator.ContainerFromIndex(rowIndex) as DataGridRow;
                if (row == null)
                {
                    dataGrid.ScrollIntoView(item);
                    row = dataGrid.ItemContainerGenerator.ContainerFromIndex(rowIndex) as DataGridRow;
                }

                if (row != null)
                {
                    System.Windows.Controls.DataGridCell cell = GetCell(dataGrid, row, 0);
                    if (cell != null)
                        cell.Focus();
                }
            }
        }


        /// <summary>
        /// Gets the cell.
        /// </summary>
        /// <param name="dataGrid">The data grid.</param>
        /// <param name="rowContainer">The row container.</param>
        /// <param name="column">The column.</param>
        /// <returns></returns>
        public static DataGridCell GetCell(this DataGrid dataGrid, DataGridRow rowContainer, int column)
        {
            if (rowContainer != null)
            {
                DataGridCellsPresenter presenter = rowContainer.FindVisualChild<DataGridCellsPresenter>();
                if (presenter == null)
                {
                    /* if the row has been virtualized away, call its ApplyTemplate() method
                     * to build its visual tree in order for the DataGridCellsPresenter
                     * and the DataGridCells to be created */
                    rowContainer.ApplyTemplate();
                    presenter = rowContainer.FindVisualChild<DataGridCellsPresenter>();
                }
                if (presenter != null)
                {
                    DataGridCell cell = presenter.ItemContainerGenerator.ContainerFromIndex(column) as DataGridCell;
                    if (cell == null)
                    {
                        /* bring the column into view
                         * in case it has been virtualized away */
                        dataGrid.ScrollIntoView(rowContainer, dataGrid.Columns[column]);
                        cell = presenter.ItemContainerGenerator.ContainerFromIndex(column) as DataGridCell;
                    }
                    return cell;
                }
            }
            return null;
        }
    }
}
