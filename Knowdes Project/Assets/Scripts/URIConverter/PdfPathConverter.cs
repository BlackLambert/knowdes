using System;
using System.IO;

namespace Knowdes
{
    public class PdfPathConverter : UriConverter
    {
		private const string _invalidFormatError = "Bitte geben Sie einen gültigen Pfad oder Link zu einer PDF-Datei an (z.B. C://Dateien/Datei.pdf).";
		private const string _invalidFileTypeError = "Invalider Datentyp. Unterstützt werden: {0}";

		private SupportedFileTypes _supportedFiles = new SupportedFileTypes();


		public override Result Convert(string link)
		{
			Uri result;
			if (!Uri.TryCreate(link, UriKind.Absolute, out result))
				return new Result(_invalidFormatError);
			if (!isFilePath(result))
				return new Result(_invalidFormatError);
			if (!isPdfFilePath(result))
				return new Result(createInvalidFileTypeError());
			return new Result(result);
		}


		private bool isFilePath(Uri uri)
		{
			return !string.IsNullOrEmpty(Path.GetExtension(uri.AbsoluteUri));
		}

		private bool isPdfFilePath(Uri uri)
		{
			string fileExtension = Path.GetExtension(uri.AbsoluteUri);
			string lowerCleanedFileExtension = fileExtension.ToLower();
			foreach (string extension in _supportedFiles.Pdf)
			{
				if (lowerCleanedFileExtension.Contains(extension))
					return true;
			}
			return false;
		}

		private string createInvalidFileTypeError()
		{
			string extensionsString = string.Empty;
			foreach (string extension in _supportedFiles.Pdf)
				extensionsString += $"{extension}, ";
			return string.Format(_invalidFileTypeError, extensionsString);
		}

		public override bool Convertable(string link)
		{
			Uri uri;
			return Uri.TryCreate(link, UriKind.Absolute, out uri) &&
				isFilePath(uri) &&
				isPdfFilePath(uri);
		}
	}
}