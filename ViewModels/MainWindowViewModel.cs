using System.Collections.ObjectModel;
using System.IO;
using ROSBox.Models;
namespace ROSBox.ViewModels;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Reactive;
using ReactiveUI;

public partial class MainWindowViewModel : ViewModelBase
{
    public string Greeting { get; } = "Welcome to Avalonia!";
    
    private ObservableCollection<DirectoryTreeNode> _folderItems;

    public MainWindowViewModel()
    {
        SearchFolder();
    }

    public ObservableCollection<DirectoryTreeNode> FolderItems 
    { 
        get { return _folderItems; }
        set => this.RaiseAndSetIfChanged(ref _folderItems, value);
    }

    private void SearchFolder()
    {
        FolderItems.Clear();
        FolderItems = GetFolderNodes("/home/myoukin");
    }

    private ObservableCollection<DirectoryTreeNode> GetFolderNodes(string path)
    {
        ObservableCollection<DirectoryTreeNode> folderNodes = new ObservableCollection<DirectoryTreeNode>();
        if (!Directory.Exists(path))
        {
            return folderNodes;
        }
        DirectoryInfo dirInfo = new DirectoryInfo(path);
        DirectoryInfo[] dirs = dirInfo.GetDirectories();
        foreach (DirectoryInfo dir in dirs)
        {
            DirectoryTreeNode node = new DirectoryTreeNode();
            node.NodeName=dir.Name;
            node.Tag = dir.FullName;
            node.SubNodes=GetFolderNodes(dir.FullName);
            node.FolderNum = node.SubNodes.Count;
            FileInfo[] files = dirInfo.GetFiles();
            node.FileNum = files.Length;
            folderNodes.Add(node);
        }
        return folderNodes;
    }
    
    
}