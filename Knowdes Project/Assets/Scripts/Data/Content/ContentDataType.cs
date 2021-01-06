using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knowdes
{ 
    public enum ContentDataType
    {
        Unset = 0,
        Text = 10,
        Image = 11,
        PDF = 12,
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
                case ContentDataType.Data:
                    return "Unbekannte Datei";
                default:
                    throw new NotImplementedException();
            }
        }
    }
}