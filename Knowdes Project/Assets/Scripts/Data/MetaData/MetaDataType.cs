using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knowdes
{
    public enum MetaDataType
    {
        Unset = 0,
        Title = 10,
        Tags = 11,
        Author = 12,
        CreationDate = 13,
        LastChangedDate = 14,
        Comment = 15,
        Description = 16,
        PreviewImage = 17
    }

    static class MetaDataTypeMethods
    {

        public static string GetName(this MetaDataType type)
        {
            switch (type)
            {
                case MetaDataType.Unset:
                    return "Metadatum hinzufügen";
                case MetaDataType.Title:
                    return "Titel";
                case MetaDataType.Author:
                    return "Autoren";
                case MetaDataType.CreationDate:
                    return "Erstellungsdatum";
                case MetaDataType.LastChangedDate:
                    return "Änderungsdatum";
                case MetaDataType.Tags:
                    return "Schlagwörter";
                case MetaDataType.Comment:
                    return "Kommentar";
                case MetaDataType.Description:
                    return "Beschreibung";
                case MetaDataType.PreviewImage:
                    return "Vorschaubild";
                default:
                    throw new NotImplementedException();
            }
        }
    }
}