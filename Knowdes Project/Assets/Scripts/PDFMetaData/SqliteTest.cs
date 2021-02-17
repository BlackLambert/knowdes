using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.IO;
using System.Data;
using UnityEngine.UI;
using Knowdes;
using System;



public class SqliteTest : MonoBehaviour
{
    

    // Start is called before the first frame update
    void Start()
    {
        PDFMetaData md = new PDFMetaData(@"C:\Temp\hello.pdf");
        Debug.Log("toString: " + md.ToString());
        Debug.Log("getTitle: " + md.getTitleData().Content );
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

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
