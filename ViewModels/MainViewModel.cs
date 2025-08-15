using System;
using System.Collections.ObjectModel;
using System.IO;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Platform.Storage;
using ReactiveUI;
using ROSBox.Models;

namespace ROSBox.ViewModels
{
    public class MainViewModel : ReactiveObject
    {
        private ObservableCollection<DirectoryTreeNode> _folderItems = new();
        public ObservableCollection<DirectoryTreeNode> FolderItems
        {
            get => _folderItems;
            set => this.RaiseAndSetIfChanged(ref _folderItems, value);
        }

        /* 供按钮命令调用：弹出文件夹选择框，然后加载树 */
        public async void SearchFolder()
        {
            if (Avalonia.Application.Current?.ApplicationLifetime
                is not IClassicDesktopStyleApplicationLifetime desktop) return;

            var top = desktop.MainWindow;
            if (top is null) return;

            var folders = await top.StorageProvider.OpenFolderPickerAsync(
                new FolderPickerOpenOptions { AllowMultiple = false });

            if (folders.Count == 0) return;

            FolderItems = GetFolderNodes(folders[0].Path.LocalPath);
        }

        /* 与文章完全一致的递归函数 */
        private static ObservableCollection<DirectoryTreeNode> GetFolderNodes(string path)
        {
            var nodes = new ObservableCollection<DirectoryTreeNode>();

            if (!Directory.Exists(path))
                return nodes;

            try
            {
                var dirInfo = new DirectoryInfo(path);

                // 1. 子目录
                foreach (var dir in dirInfo.GetDirectories())
                {
                    try
                    {
                        var child = new DirectoryTreeNode
                        {
                            NodeName = dir.Name,
                            Tag      = dir.FullName,
                            IsFolder = true
                        };

                        foreach (var sub in GetFolderNodes(dir.FullName))
                            child.Children.Add(sub);

                        nodes.Add(child);
                    }
                    catch (UnauthorizedAccessException) { /* 跳过 */ }
                    catch (PathTooLongException)        { /* 跳过 */ }
                }

                // 2. 当前目录的文件
                foreach (var file in dirInfo.GetFiles())
                {
                    nodes.Add(new DirectoryTreeNode
                    {
                        NodeName = file.Name,
                        Tag      = file.FullName,
                        IsFolder = false
                    });
                }
            }
            catch (UnauthorizedAccessException) { /* 根目录无权限 */ }

            return nodes;
        }

    }
}