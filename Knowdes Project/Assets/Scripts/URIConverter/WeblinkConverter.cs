using System;

namespace Knowdes
{
    public class WeblinkConverter : UriConverter
    {
        private const string _invalidFormat = "Bitte geben Sie einen gültigen Link ein (z.B. www.google.de).";
        private const string _fileError = "Dieser Link zeigt auf eine Datei. Bitte geben Sie einen Link zu einer Website an.";

        public override Result Convert(string link)
        {
            Uri result;
            if (!Uri.TryCreate(link, UriKind.Absolute, out result))
                return new Result(_invalidFormat);
            if(isLinkToFile(result))
                return new Result(_fileError);
            return new Result(result);
        }

        private bool isLinkToFile(Uri uri)
        {
            return uri.IsFile;
        }
    }
}