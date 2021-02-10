using System;
using System.IO;

namespace Knowdes
{
	public class ImageFilePathConverter : UriConverter
	{
		private const string _invalidFormatError = "Bitte geben Sie einen gültigen Link ein (z.B. www.bilder.de/Bild.jpg).";
		private const string _invalidFileTypeError = "Ivalider Datentyp. Unterstützt werden: {0}";
		private const string _noFileError = "Bitte geben sie eine Link zu einer Bilddatei an.";

		private SupportedFileTypes _supportedFiles = new SupportedFileTypes();


		public override Result Convert(string link)
		{
			Uri result;
			if (!Uri.TryCreate(link, UriKind.Absolute, out result))
				return new Result(_invalidFormatError);
			if (!isFilePath(result))
				return new Result(_noFileError);
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
	}
}