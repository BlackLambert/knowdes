using System;
using System.IO;

namespace Knowdes
{
	public class ImageFilePathConverter : UriConverter
	{
		private const string _invalidFormatError = "Bitte geben Sie einen gültigen Pfad oder Link zu einer Bilddatei an (z.B. www.bilder.de/Bild.jpg).";
		private const string _invalidFileTypeError = "Ivalider Datentyp. Unterstützt werden: {0}";

		private SupportedFileTypes _supportedFiles = new SupportedFileTypes();


		public override Result Convert(string link)
		{
			Uri result;
			if (!Uri.TryCreate(link, UriKind.Absolute, out result))
				return new Result(_invalidFormatError);
			if (!isFilePath(result))
				return new Result(_invalidFormatError);
			if (!isImageFilePath(result))
				return new Result(createInvalidFileTypeError());
			return new Result(result);
		}


		private bool isFilePath(Uri uri)
		{
			return !string.IsNullOrEmpty(Path.GetExtension(uri.AbsoluteUri));
		}

		private bool isImageFilePath(Uri uri)
		{
			string fileExtension = Path.GetExtension(uri.AbsoluteUri);
			string lowerCleanedFileExtension = fileExtension.ToLower();
			foreach (string extension in _supportedFiles.Image)
			{
				if (lowerCleanedFileExtension.Contains(extension))
					return true;
			}
			return false;
		}

		private string createInvalidFileTypeError()
		{
			string extensionsString = string.Empty;
			foreach (string extension in _supportedFiles.Image)
				extensionsString += $"{extension}, ";
			return string.Format(_invalidFileTypeError, extensionsString);
		}

		public override bool Convertable(string link)
		{
			Uri uri;
			return Uri.TryCreate(link, UriKind.Absolute, out uri) &&
				isFilePath(uri) &&
				isImageFilePath(uri);
		}
	}
}