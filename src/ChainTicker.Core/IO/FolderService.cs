using System;
using System.Collections.Generic;
using System.IO;
using ChainTicker.Core.Interfaces;

namespace ChainTicker.Core.IO
{
    public class FolderService : IFolderService
    {
        private string _applicationBaseFolder;
        private const string APPNAME = "ChainTicker";

        private readonly Dictionary<AppFolder, string> _folders = new Dictionary<AppFolder, string>(3);

        public FolderService()
        {
            AddFolder(AppFolder.ApplicationBase);
            AddFolder(AppFolder.Cache);
            AddFolder(AppFolder.Icons);
        }

        private void AddFolder(AppFolder folder)
        {
            if (folder == AppFolder.ApplicationBase)
            {
                _applicationBaseFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), APPNAME);
                _folders.Add(AppFolder.ApplicationBase, _applicationBaseFolder);
                EnsureFolderExists(_applicationBaseFolder);
            }
            else
            {
                var folderPath = Path.Combine(_applicationBaseFolder, folder.ToString());
                _folders.Add(folder, folderPath);
                EnsureFolderExists(folderPath);
            }
        }

        private void EnsureFolderExists(string applicationBaseFolder)
        {
            if (Directory.Exists(applicationBaseFolder) == false)
                Directory.CreateDirectory(applicationBaseFolder);
        }

        public string GetFolderPath(AppFolder folder)
            => _folders[folder];

    }
}