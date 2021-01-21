using System;
using System.Linq;

namespace Knowdes
{
    public class WeblinkContentData : ContentData
    {
        public bool Empty
		{
            get => _url == null;
        }

        public string UrlString => Empty ? string.Empty : Url.AbsoluteUri;
        public string Host => _url.Host;
        public string Protocol => _url.Scheme;
        public int Port => _url.Port;
        public string TopLevelDomain => Host.Split('.').Last();

        public event Action OnUrlChanged;

        private Uri _url = null;
        public Uri Url
		{
            get => _url;
			set
			{
                _url = value;
                OnUrlChanged();
            }
		}

        

        public override ContentDataType Type => ContentDataType.Weblink;

        public WeblinkContentData(Guid iD) : base(iD)
        {
            
        }

        public WeblinkContentData(Guid iD, Uri url) : base(iD)
        {
            _url = url;
        }
    }
}