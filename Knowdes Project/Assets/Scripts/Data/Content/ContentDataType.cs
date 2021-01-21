using System;

namespace Knowdes
{ 
    public enum ContentDataType
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

        public static string GetName(this ContentDataType type)
        {
            switch (type)
            {
                case ContentDataType.Unset:
                    return "Typ nicht gesetzt";
                case ContentDataType.Text:
                    return "Editierbarer Text";
                case ContentDataType.Image:
                    return "Bild";
                case ContentDataType.PDF:
                    return "PDF";
                case ContentDataType.Weblink:
                    return "Weblink";
                case ContentDataType.Data:
                    return "Unbekannte Datei";
                default:
                    throw new NotImplementedException();
            }
        }
    }
}