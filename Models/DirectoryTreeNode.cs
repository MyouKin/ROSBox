using System.Collections.ObjectModel;

namespace ROSBox.Models;

public class DirectoryTreeNode
{
    public string NodeName { get; set; }
    public string Tag { get; set; }
    public int FolderNum { get; set; } //文件夹数量
    public int FileNum { get; set; } //文件数量
    public ObservableCollection<DirectoryTreeNode> SubNodes { get; set; }
}