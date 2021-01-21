using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Knowdes
{
    public class URLConverter
    {
        private const string _invalidFormat = "Bitte geben Sie einen gültigen Link ein (z.B. www.google.de).";
        private const string _convertionFailed = "Link Konvertierung fehlgeschlagen.";

        // Source: https://stackoverflow.com/questions/9289007/regular-expression-for-url-validation
        private const string _regexPattern = @"^((http|ftp|https|www)://)?([\w+?\.\w+])+([a-zA-Z0-9\~\!\@\#\$\%\^\&\*\(\)_\-\=\+\\\/\?\.\:\;\'\,]*)?$";

        public Result Convert(string link)
		{
            //if (!Regex.IsMatch(link, _regexPattern))
            if(!Uri.IsWellFormedUriString(link, UriKind.Absolute))
                return new Result(null, false, _invalidFormat);
            Uri result;
            if (!Uri.TryCreate(link, UriKind.Absolute, out result))
                return new Result(null, false, _convertionFailed);
            return new Result(result, true, null);
        }

        public class Result
		{
            public Uri Url { get; }
            public bool Successful { get; }
            public string Error { get; }

            public Result(Uri url, bool successful, string error)
			{
                Url = url;
                Successful = successful;
                Error = error;
            }
        }
    }
}