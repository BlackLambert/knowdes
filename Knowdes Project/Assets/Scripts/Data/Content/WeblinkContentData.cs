using System;
using System.Linq;

namespace Knowdes
{
    public class WeblinkContentData : ContentData, TextbasedContentData
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
		public override event Action OnChanged;

		private Uri _url = null;
        public Uri Url
		{
            get => _url;
			set
			{
                _url = value;
                OnUrlChanged();
                OnChanged?.Invoke();
            }
		}

        

        public override ContentType Type => ContentType.Weblink;

		public string Content => UrlString;

		public WeblinkContentData(Guid iD) : base(iD)
        {
            
        }

        public WeblinkContentData(Guid iD, Uri url) : base(iD)
        {
            _url = url;
        }
    }
}