using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Knowdes
{
    public class WeblinkConverter : UriConverter
    {
        private const string _invalidFormat = "Bitte geben Sie einen gültigen Link ein (z.B. www.google.de).";
        private const string _convertionFailed = "Link Konvertierung fehlgeschlagen.";
        private const string _fileError = "Dieser Link zeigt auf eine Datei. Bitte geben Sie einen Link zu einer Website an.";

        public override Result Convert(string link)
        {
            //if (!Regex.IsMatch(link, _regexPattern))
            if (!Uri.IsWellFormedUriString(link, UriKind.Absolute))
                return new Result(_invalidFormat);
            Uri result;
            if (!Uri.TryCreate(link, UriKind.Absolute, out result))
                return new Result(_convertionFailed);
            if(isLinkToFile(result))
                return new Result(_fileError);
            return new Result(result);
        }

        private bool isLinkToFile(Uri uri)
        {
            return !string.IsNullOrEmpty(Path.GetExtension(uri.AbsoluteUri));
        }
    }
}