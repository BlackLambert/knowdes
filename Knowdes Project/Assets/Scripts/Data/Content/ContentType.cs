using System;

namespace Knowdes
{ 
    public enum ContentType
    {
        Unset = 0,
        Text = 10,
        Image = 11,
        PDF = 12,
        Weblink = 13,
        Data = 1000
    }

    static class ContentDataTypeMethods
    {

        public static string GetName(this ContentType type)
        {
            switch (type)
            {
                case ContentType.Unset:
                    return "Typ nicht gesetzt";
                case ContentType.Text:
                    return "Editierbarer Text";
                case ContentType.Image:
                    return "Bild";
                case ContentType.PDF:
                    return "PDF";
                case ContentType.Weblink:
                    return "Weblink";
                case ContentType.Data:
                    return "Unbekannte Datei";
                default:
                    throw new NotImplementedException();
            }
        }
    }
}