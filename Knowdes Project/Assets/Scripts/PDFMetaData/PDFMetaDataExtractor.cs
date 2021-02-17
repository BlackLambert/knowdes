using iText.Kernel.Pdf;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Knowdes
{
    public class PDFMetaDataExtractor
    {
        private const char _dotSeparator = '.';
        private const char _semicolonSeparator = ';';

        private string _path;
        private MetaDataFactory _metaDataFactory;

        public PDFMetaDataExtractor(string pdfPath)
		{
            _path = pdfPath;
            _metaDataFactory = new MetaDataFactory();
        }

        public Result Extract()
		{
            PdfReader reader = new PdfReader(_path);
            PdfDocument pdfDocument = new PdfDocument(reader);
            PdfDocumentInfo info = pdfDocument.GetDocumentInfo();

            TitleData title = extractTitle(info);
            AuthorData authors = extractAuthors(info);
            TagsData tags = extractTags(info);

            return new Result(title, authors, tags);
        }

		private TitleData extractTitle(PdfDocumentInfo info)
		{
            string title = info.GetTitle();
            if (string.IsNullOrEmpty(title))
                return null;
            TitleData result = _metaDataFactory.CreateNew(MetaDataType.Title) as TitleData;
            result.Content = title;
            return result;
        }

        private TagsData extractTags(PdfDocumentInfo info)
        {
            string[] tags = separate(info.GetKeywords());
            if (tags.Length == 0)
                return null;
            TagsData result = _metaDataFactory.CreateNew(MetaDataType.Tags) as TagsData;
            foreach (string tag in tags)
                result.AddTag(new Tag(Guid.NewGuid(), tag));
            return result;
        }

		private string[] separate(string value)
		{
            if (string.IsNullOrEmpty(value))
                return new string[0];
            if (value.Contains(_dotSeparator))
                return value.Split(_dotSeparator);
            if (value.Contains(_semicolonSeparator))
                return value.Split(_semicolonSeparator);
            return new string[1] { value };
        }

		private AuthorData extractAuthors(PdfDocumentInfo info)
        {
            string[] authors = separate(info.GetAuthor());
            if (authors.Length == 0)
                return null;
            AuthorData result = _metaDataFactory.CreateNew(MetaDataType.Author) as AuthorData;
            foreach (string author in authors)
                result.Add(new Author(Guid.NewGuid(), author));
            return result;
        }

        public class Result
		{
            public TitleData Title { get; }
            public AuthorData Authors { get; }
            public TagsData Tags { get; }

            public bool IsEmpty => Title is null && Authors is null && Tags is null;

            public Result(TitleData title, AuthorData authors, TagsData tags)
			{
                Title = title;
                Authors = authors;
                Tags = tags;
            }

            public List<MetaData> GetExisting()
			{
                List<MetaData> result = new List<MetaData>();
                if (Title != null)
                    result.Add(Title);
                if (Authors != null)
                    result.Add(Authors);
                if (Tags != null)
                    result.Add(Tags);
                return result;
            }

            public List<MetaData> All()
			{
                List<MetaData> result = new List<MetaData>();
                result.Add(Title);
                result.Add(Authors);
                result.Add(Tags);
                return result;
            }
        }

        public class InvalidSeparatorException : NotImplementedException { }
    }
}