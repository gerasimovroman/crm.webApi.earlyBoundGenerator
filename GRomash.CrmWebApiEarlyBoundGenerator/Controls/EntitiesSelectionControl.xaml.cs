using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using GRomash.CrmWebApiEarlyBoundGenerator.Command;
using GRomash.CrmWebApiEarlyBoundGenerator.Infrastructure.Model;
using NuGet;

namespace GRomash.CrmWebApiEarlyBoundGenerator.Controls
{
    /// <summary>
    /// Interaction logic for EntitiesSelectionControl.xaml
    /// </summary>
    public partial class EntitiesSelectionControl 
    {
        public static readonly DependencyProperty SelectedEntitiesProperty = DependencyProperty.Register(
            "SelectedEntities", typeof(ObservableCollection<EntityModel>), typeof(EntitiesSelectionControl), new PropertyMetadata(default(ObservableCollection<EntityModel>)));
        public static readonly DependencyProperty EntitiesListProperty = DependencyProperty.Register(
            "EntitiesList", typeof(ObservableCollection<EntityModel>), typeof(EntitiesSelectionControl), new PropertyMetadata(default(ObservableCollection<EntityModel>)));
        public static readonly DependencyProperty UnselectEntitiesCommandProperty = DependencyProperty.Register(
            "UnselectEntitiesCommand", typeof(ICommand), typeof(EntitiesSelectionControl), new PropertyMetadata(default(ICommand)));
        public static readonly DependencyProperty SelectEntitiesCommandProperty = DependencyProperty.Register(
            "SelectEntitiesCommand", typeof(ICommand), typeof(EntitiesSelectionControl), new PropertyMetadata(default(ICommand)));
        public static readonly DependencyProperty SearchTextProperty = DependencyProperty.Register(
            "SearchText", typeof(string), typeof(EntitiesSelectionControl), new PropertyMetadata(default(string)));


     

        public EntitiesSelectionControl()
        {
            InitializeComponent();
            SelectEntitiesCommand = new CommandGeneric<ObservableCollection<EntityModel>>(SelectEntities);
            UnselectEntitiesCommand = new CommandGeneric<ObservableCollection<EntityModel>>(UnselectEntities);
        }



        public ObservableCollection<EntityModel> EntitiesList
        {
            get => (ObservableCollection<EntityModel>) GetValue(EntitiesListProperty);
            set => SetValue(EntitiesListProperty, value);
        }

        public ObservableCollection<EntityModel> SelectedEntities
        {
            get => (ObservableCollection<EntityModel>)GetValue(SelectedEntitiesProperty);
            set => SetValue(SelectedEntitiesProperty, value);
        }

        public string SearchText
        {
            get => (string) GetValue(SearchTextProperty);
            set => SetValue(SearchTextProperty, value);
        }

        public ICommand UnselectEntitiesCommand
        {
            get => (ICommand) GetValue(UnselectEntitiesCommandProperty);
            set => SetValue(UnselectEntitiesCommandProperty, value);
        }
      
        public ICommand SelectEntitiesCommand
        {
            get => (ICommand) GetValue(SelectEntitiesCommandProperty);
            set => SetValue(SelectEntitiesCommandProperty, value);
        }

        private void UnselectEntities(ObservableCollection<EntityModel> entityModels)
        {
            foreach (var entityModel in entityModels.ToArray())
            {
                SelectedEntities.Remove(entityModel);
                EntitiesList.Add(entityModel);
            }
        }

        private void SelectEntities(ObservableCollection<EntityModel> entityModels)
        {
            foreach (var entityModel in entityModels.ToArray())
            {
                EntitiesList.Remove(entityModel);
                SelectedEntities.Add(entityModel);
            }
        }
    }
}
