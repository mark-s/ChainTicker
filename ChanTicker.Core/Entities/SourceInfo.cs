﻿using ChanTicker.Core.Interfaces;

namespace ChanTicker.Core.Entities
{
    public class SourceInfo : ISourceInfo
    {
        public string Name { get; }
        public string Uri { get; }
        public string Description { get; }
        public bool IsEnabled { get; }

        public SourceInfo(string name, string uri, string description, bool isEnabled)
        {
            Name = name;
            Uri = uri;
            Description = description;
            IsEnabled = isEnabled;
        }

        
    }
}