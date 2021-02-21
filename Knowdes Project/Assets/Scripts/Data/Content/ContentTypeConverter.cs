using System;

namespace Knowdes
{
    public class ContentTypeConverter 
    {
        private const string _textName = "Text";
        private const string _imageName = "Bild";
        private const string _pdfName = "PDF";
        private const string _weblinkName = "Weblink";
        private const string _unknownFileName = "UnbekannteDatei";
        private const string _unsetName = "Typ nicht gesetzt";

        public bool IsContentType(string word)
		{
			try
			{
                Convert(word);
                return true;
            }
            catch(NotImplementedException)
			{
                return false;
			}
		}

		public ContentType Convert(string word)
		{
            word = word.ToLower();
            if (_textName.ToLower() == word)
                return ContentType.Text;
            else if (_imageName.ToLower() == word)
                return ContentType.Image;
            else if (_pdfName.ToLower() == word)
                return ContentType.PDF;
            else if (_unsetName.ToLower() == word)
                return ContentType.Unset;
            else if (_weblinkName.ToLower() == word)
                return ContentType.Weblink;
            else if (_unknownFileName.ToLower() == word)
                return ContentType.File;
            else
                throw new NotImplementedException();
		}

        public string Convert(ContentType type)
		{
            switch (type)
            {
                case ContentType.Unset:
                    return _unsetName;
                case ContentType.Text:
                    return _textName;
                case ContentType.Image:
                    return _imageName;
                case ContentType.PDF:
                    return _pdfName;
                case ContentType.Weblink:
                    return _weblinkName;
                case ContentType.File:
                    return _unknownFileName;
                default:
                    throw new NotImplementedException();
            }
        }
	}
}