using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using static View_Spot_of_City.Language.Language.LanguageDictionaryHelper;

namespace View_Spot_of_City.ViewModel
{
    public enum OverlayerType
    {
        SpotQuery,
        SpotRecommend,
        Visualization,
        Share,
    }

    public class OverlayerItemViewModel : INotifyPropertyChanged
    {
        private string _iconPath;
        private string _title;
        private object _content;
        private string _titleKey;

        public OverlayerItemViewModel(string iconPath, string titleKey, object content)
        {
            _iconPath = iconPath;
            _titleKey = titleKey;
            _title = GetString(_titleKey) as string;
            _content = content;
            VAlignType = VerticalAlignment.Stretch;
            HAlignType = HorizontalAlignment.Left;
            OverlayerMargin = new Thickness(20, 20, 0, 53);
        }

        public string IconPath
        {
            get
            {
                return _iconPath;
            }
            set
            {
                _iconPath = value;
                OnNotifyPropertyChanged();
            }
        }

        public string TitleKey
        {
            get => _titleKey;
            set
            {
                Title = GetString(_titleKey) as string;
            }
        }

        public string Title
        {
            get { return _title; }
            set { _title = value; OnNotifyPropertyChanged(); }
        }

        public object Content
        {
            get { return _content; }
            set { _content = value; OnNotifyPropertyChanged(); }
        }

        public VerticalAlignment VAlignType { set; get; }
        public HorizontalAlignment HAlignType { set; get; }

        public OverlayerType OverlayerIndicator { set; get; }

        public Thickness OverlayerMargin { set; get; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnNotifyPropertyChanged([CallerMemberName] string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
