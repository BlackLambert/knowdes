using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Knowdes
{
	public class SearchFilterApplier : MonoBehaviour
	{
		[SerializeField]
		private EntriesList _entries;
		[SerializeField]
		private SearchInput _searchInput;
		private SearchFilter _currentFilter;

		protected virtual void Start()
		{
			_entries.OnEntryAdded += onEntryAdded;
			_searchInput.OnSubmit += onSearchSubmit;
		}

		protected virtual void OnDestroy()
		{
			_entries.OnEntryAdded -= onEntryAdded;
			_searchInput.OnSubmit -= onSearchSubmit;
		}

		private void onEntryAdded(Entry entry)
		{
			applyFilter(entry);

		}

		private void onSearchSubmit()
		{
			_currentFilter = _searchInput.ExtractFilter();
			applyFilter();
		}

		private void applyFilter()
		{
			foreach (Entry entry in _entries.EntriesCopy)
				applyFilter(entry);
		}
		private void applyFilter(Entry entry)
		{
			entry.Show(filterAppliesTo(entry));
		}

		private bool filterAppliesTo(Entry entry)
		{
			if (noFilter())
				return true;
			if (contentTypeIsExcluded(entry))
				return false;
			return searchTermsContainedIn(entry);
		}

		private bool noFilter()
		{
			return _currentFilter == null;
		}

		private bool contentTypeIsExcluded(Entry entry)
		{
			return !_currentFilter.ContentTypeFilterEmpty && !_currentFilter.ContentTypeFilter.Contains(entry.Data.Content.Type);
		}

		private bool searchTermsContainedIn(Entry entry)
		{
			if (_currentFilter.SearchTermsEmpty)
				return true;
			if (searchTermsContainedIn(entry.Data.Content))
				return true;
			if (searchTermsContainedIn(entry.Data.MetaDatasCopy.Values.ToList()))
				return true;
			return false;
		}

		private bool searchTermsContainedIn(ContentData content)
		{
			TextbasedContentData textContent = content as TextbasedContentData;
			if (textContent == null)
				return false;
			foreach (string term in _currentFilter.SearchTerms)
			{
				if (textContent.Content.ToLower().Contains(term))
					return true;
			}
			return false;
		}

		private bool searchTermsContainedIn(List<MetaData> metaDatas)
		{
			foreach(MetaData metaData in metaDatas)
			{
				TextBasedMetaData textMetaData = metaData as TextBasedMetaData;
				if (textMetaData == null)
					continue;
				foreach(string term in _currentFilter.SearchTerms)
				{
					if (textMetaData.Content.ToLower().Contains(term))
						return true;
				}
			}
			return false;
		}
	}
}