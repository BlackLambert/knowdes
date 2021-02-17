using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knowdes
{
    public class TestPDFMetaDataReader : MonoBehaviour
    {
        [SerializeField]
        private PdfContentEditor _editor;

        protected virtual void Start()
		{
            PDFMetaData md = new PDFMetaData(_editor.Data.Path);
            Debug.Log("toString: " + md.ToString());
            Debug.Log("getTitle: " + md.getTitleData()?.Content);
            List<Author> a = md.getAuthorList();
            foreach (Author item in a)
            {
                Debug.Log(item.ToString());
            }
            List<Tag> t = md.getTagList();
            foreach (Tag item in t)
            {
                Debug.Log(item.ToString());
            }
        }
    }
}