using System;
using System.Collections.Generic;

namespace Knowdes
{
    public class SearchFilterExtractor
    {
        private const char _contentTypeIndicator = '#';
        private const char _termSeparator = ' ';
        private ContentTypeConverter _contentTypeConverter;

        public SearchFilterExtractor()
		{
            _contentTypeConverter = new ContentTypeConverter();

        }

        public SearchFilter Extract(string input)
		{
            input = input.Trim();
            string lowerInput = input.ToLower();
            if (string.IsNullOrEmpty(input))
                return null;
            List<string> terms = new List<string>();
            List<ContentType> filteredTypes = new List<ContentType>();
            string[] words = lowerInput.Split(_termSeparator);
            foreach(string word in words)
            {
                if (isContentTypeFilter(word))
                    filteredTypes.Add(convertTo(word));
                else
                    terms.Add(word);
            }
            return new SearchFilter(input, terms, filteredTypes);
        }

		private ContentType convertTo(string word)
		{
            return _contentTypeConverter.Convert(word.Remove(0, 1));
		}

		private bool isContentTypeFilter(string word)
		{
            return word[0] == _contentTypeIndicator &&
                _contentTypeConverter.IsContentType(word.Remove(0, 1));
        }
	}
}