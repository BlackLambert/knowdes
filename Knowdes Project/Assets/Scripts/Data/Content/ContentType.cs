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
        File = 1000
    }

    static class ContentDataTypeMethods
    {

        public static string GetName(this ContentType type)
        {
            ContentTypeConverter converter = new ContentTypeConverter();
            return converter.Convert(type);
        }
    }
}