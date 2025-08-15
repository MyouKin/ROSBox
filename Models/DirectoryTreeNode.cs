using System.Collections.ObjectModel;

namespace ROSBox.Models
{
    public class DirectoryTreeNode
    {
        public string NodeName { get; set; } = string.Empty;
        public string Tag { get; set; } = string.Empty;
        public DirectoryTreeNode? Parent { get; set; }
        public int FolderNum { get; set; }
        public int FileNum { get; set; }
        public ObservableCollection<DirectoryTreeNode> Children { get; set; } = new();
        public bool IsFolder { get; init; } = true;
    }
}