using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Knowdes
{
    class FileManager
    {
        private String datei_verzeichnis = "KnowdesFiles";
        private String arbeitsverzeichnis = null;

        public FileManager()
        {
            arbeitsverzeichnis = getArbeitspfad();
            datei_verzeichnis = Application.persistentDataPath + "/" + datei_verzeichnis;
            if (!existVerzeichnis(datei_verzeichnis))
            {
                createVerzeichnis();
            }

        }

        public string Dateiverzeichnis { get => datei_verzeichnis; }
        public string Arbeitsverzeichnis { get => arbeitsverzeichnis; }



        private void copyDatei(String quelle, String ziel)
        {
            System.IO.File.Copy(quelle, ziel, true); 
        }

        public void DeleteFile(String datei)
        {
            if (!existDatei(@datei))
                throw new InvalidOperationException();
            try
            {
                System.IO.File.Delete(@datei);
            }
            catch (System.IO.IOException e)
            {
                Debug.Log (e.Message);
            }
        }

        public bool existDatei(String datei)
        {
            return System.IO.File.Exists(@datei);
        }

        public String SaveFile(String datei)
        {
            String ziel = datei_verzeichnis + @"\" + getDateiName(datei) + "_" + DateTime.Now.ToString("yyMMdd_HHmmss") + getExtension(datei);
            if (existDatei(ziel))
            {
                ziel = datei_verzeichnis + @"\" + getDateiName(datei) + "_" + DateTime.Now.ToString("yyMMdd_HHmmss") + getExtension(datei);
            }
            copyDatei(datei, ziel);
            return @ziel;
        }

        public bool IsManagedFile(string filePath)
		{
            return filePath.Contains(datei_verzeichnis);
		}

        public String getDateiExtention(String datei)
        {
            FileInfo fi = new FileInfo(datei);
            return fi.Extension; 
        }

        public String getDateiName(String datei) 
        {
            FileInfo fi = new FileInfo(datei);
            return fi.Name.Split('.')[0] ;
        }

        private string getExtension(string path)
		{
            FileInfo info = new FileInfo(path);
            return info.Extension;
		}

        public bool existVerzeichnis(String Verzeichnisname)
        {
            if (Directory.Exists(Verzeichnisname))
            {
                return true;
            }
            else { return false; }
        }

        private String getArbeitspfad()
        {
            return Directory.GetCurrentDirectory();
        }

        private void createVerzeichnis()
        {
            Directory.CreateDirectory(datei_verzeichnis);
        }


    }
}
