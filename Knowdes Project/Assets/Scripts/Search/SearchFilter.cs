using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knowdes
{
    public class SearchFilter 
    {
        public string SearchQuerry { get; private set; }
        public List<string> SearchTerms { get; } = new List<string>();
        public bool SearchTermsEmpty => SearchTerms.Count == 0;
        public List<ContentType> ContentTypeFilter { get; } = new List<ContentType>();
        public bool ContentTypeFilterEmpty => ContentTypeFilter.Count == 0;

        public SearchFilter(string searchQuerry, List<string> searchTerms, List<ContentType> contentTypeFilter)
		{
            SearchQuerry = searchQuerry;
            SearchTerms = searchTerms;
            ContentTypeFilter = contentTypeFilter;
        }
    }
}