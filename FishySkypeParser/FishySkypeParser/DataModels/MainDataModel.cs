using Fishy.Utils;
using FishySkypeParser.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FishySkypeParser.DataModels
{
    public class MainDataModel : NotifyPropertyChangedHandler
    {
        public FSPFile theFile { get; set; } = null;

        private string jsonFilePath = null;
        public string JsonFilePath
        {
            get { return jsonFilePath; }
            set
            {
                jsonFilePath = value;
                NotifyPropertyChanged("JsonFilePath");
            }
        }

        private IEnumerable<string> peers = null;
        public IEnumerable<string> Peers
        {
            get { return peers; }
            set
            {
                peers = value;
                NotifyPropertyChanged("Peers");
                SelectedPeer = null;
            }
        }

        private string selectedPeer = null;
        public string SelectedPeer
        {
            get { return selectedPeer; }
            set
            {
                selectedPeer = value;
                NotifyPropertyChanged("SelectedPeer");
                NotifyPropertyChanged("MessagesToDisplay");
            }
        }

        private DateTime? filterStartDate = null;
        public DateTime? FilterStartDate
        {
            get { return filterStartDate; }
            set
            {
                filterStartDate = value;
                NotifyPropertyChanged("FilterStartDate");
                NotifyPropertyChanged("MessagesToDisplay");
            }
        }

        private DateTime? filterEndDate = null;
        public DateTime? FilterEndDate
        {
            get { return filterEndDate; }
            set
            {
                filterEndDate = value;
                NotifyPropertyChanged("FilterEndDate");
                NotifyPropertyChanged("MessagesToDisplay");
            }
        }

        private string myself = null;
        public string Myself
        {
            get { return myself; }
            set
            {
                myself = value;
                NotifyPropertyChanged("Myself");
            }
        }

        private bool hasDateFiltering = false;
        public bool HasDateFiltering
        {
            get { return hasDateFiltering; }
            set
            {
                hasDateFiltering = value;
                NotifyPropertyChanged("HasDateFiltering");
                NotifyPropertyChanged("MessagesToDisplay");
            }
        }

        private string searchString = null;
        public string SearchString
        {
            get { return searchString; }
            set
            {
                searchString = value;
                NotifyPropertyChanged("SearchString");
                NotifyPropertyChanged("MessagesToDisplay");
            }
        }

        public IEnumerable<FSPMessage> MessagesToDisplay
        {
            get
            {
                var data = theFile?.conversations?
                    .Where(c => (c.displayName ?? c.id) == SelectedPeer)
                    .SelectMany(c => c.MessageList)
                    .Where(m => !string.IsNullOrEmpty(m.content))
                    .OrderBy(m => m.originalarrivaltime ?? DateTime.MinValue);

                if (!HasDateFiltering)
                {
                    return data;
                }
                else
                {
                    DateTime s = FilterStartDate ?? DateTime.MinValue;
                    DateTime e = FilterEndDate ?? DateTime.MaxValue;

                    return data.Where(m => (m.originalarrivaltime ?? DateTime.MinValue) >= s)
                        .Where(m => (m.originalarrivaltime ?? DateTime.MinValue) <= e);
                }
            }
        }
    }
}
