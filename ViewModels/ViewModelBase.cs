using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ROSBox.ViewModels;

public class ViewModelBase : ObservableObject
{
    public ObservableCollection<MainWindowViewModel.Node> Nodes{ get; }

    public ViewModelBase()
    {
        Nodes = new ObservableCollection<MainWindowViewModel.Node>
        {                
            new MainWindowViewModel.Node("Animals", new ObservableCollection<MainWindowViewModel.Node>
            {
                new MainWindowViewModel.Node("Mammals", new ObservableCollection<MainWindowViewModel.Node>
                {
                    new MainWindowViewModel.Node("Lion"), new MainWindowViewModel.Node("Cat"), new MainWindowViewModel.Node("Zebra")
                })
            })
        };
    }
}