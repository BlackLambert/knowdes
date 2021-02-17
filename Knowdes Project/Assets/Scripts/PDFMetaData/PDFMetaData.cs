using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using iText.Kernel.Pdf;
using System;

namespace Knowdes
{

    public class PDFMetaData
    {
        private string _author = string.Empty;
        private string _keywords = string.Empty;
        private string _title = string.Empty;
        private string _subject = string.Empty;
        private string _file = string.Empty;

        public string Author { get => _author; }
        public string Keywords { get => _keywords;}
        public string Title { get => _title;}
        public string Subject { get => _subject;}
        public string File { get => _file;}

        public PDFMetaData(string _datei)
        {
            PdfReader reader = new PdfReader(_datei);
            PdfDocument pdfDocument = new PdfDocument(reader);
            PdfDocumentInfo info = pdfDocument.GetDocumentInfo();
            _file = _datei;
            _author = _author + info.GetAuthor();
            _keywords = _keywords + info.GetKeywords();
            _title = _title + info.GetTitle();
            _subject = _subject + info.GetSubject();
        }

        public TitleData getTitleData()
        {
            if (string.IsNullOrEmpty(_title))
                return null;
            else
                return new TitleData(Guid.NewGuid(), _title);
        }

        public List<Author> getAuthorList()
        {
            if (string.IsNullOrEmpty(_author))
                return new List<Author>();
            char seperator = getSeperator(_author);
            if (seperator == Char.MinValue)
                return new List<Author>() { new Author(Guid.NewGuid(), _author) };
            else
            {
                List<Author> a = new List<Author>();
                string[] authorenliste = _author.Split(seperator);
                foreach (string item in authorenliste)
                {
                    a.Add(new Author(Guid.NewGuid(), item));
                }
                return a;
            }
        }

        public List<Tag> getTagList() 
        {
            List<Tag> tags = new List<Tag>();
            char seperator = getSeperator(_keywords);
            if (string.IsNullOrEmpty(_keywords))
            {
                tags = new List<Tag>();
            }
            else if (seperator == Char.MinValue)
            {
                tags.Add(new Tag(Guid.NewGuid(), _keywords));
            }
            else
            {
                string[] tagliste = _keywords.Split(seperator);
                foreach (string item in tagliste)
                {
                    tags.Add(new Tag(Guid.NewGuid(), item));
                }
            }
            return tags;
        }

        private char getSeperator(String data)
        {
            // die Empfehlung ist semikolon, komma aber auch möglich
            char semikolon = ';';
            char komma = ',';
            if (data.Contains(semikolon.ToString()))
            {
                return semikolon;
            }
            else if(data.Contains(komma.ToString()))
            {
                return komma;
            } 
            else
            {
                return Char.MinValue;
            }

        }

        public override string ToString()
        {
            return _file + " | " + _author + " | " + _keywords + " | " + _title + " | " + _subject;
        }

    }
}
