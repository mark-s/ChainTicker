using System;
using System.Collections.Generic;
using System.IO;
using ChanTicker.Core.Interfaces;

namespace ChanTicker.Core.IO
{
    public class FolderService : IFolderService
    {
        private string _applicationBaseFolder;
        private const string APPNAME = "ChainTicker";

        private readonly Dictionary<ChainTickerFolder, string> _folders = new Dictionary<ChainTickerFolder, string>(3);

        public FolderService()
        {
            AddFolder(ChainTickerFolder.ApplicationBase);
            AddFolder(ChainTickerFolder.Cache);
            AddFolder(ChainTickerFolder.Icons);
        }

        private void AddFolder(ChainTickerFolder folder)
        {
            if (folder == ChainTickerFolder.ApplicationBase)
            {
                _applicationBaseFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), APPNAME);
                _folders.Add(ChainTickerFolder.ApplicationBase, _applicationBaseFolder);
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

        public string GetFolderPath(ChainTickerFolder folder)
            => _folders[folder];

    }
}