using System;

namespace Knowdes
{
    public abstract class UriConverter
    {
        public abstract Result Convert(string link);

        public struct Result
		{
            public Uri Uri { get; }
            public bool Successful { get => Uri != null && String.IsNullOrEmpty(Error); }
            public string Error { get; }

            public Result(Uri url)
			{
                Uri = url;
                Error = string.Empty;
            }

            public Result(string error)
            {
                Uri = null;
                Error = error;
            }
        }
    }
}