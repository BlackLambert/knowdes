using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Knowdes
{
	public class ImageFilePathConverter : UriConverter
	{
		private const string _invalidFormatError = "Bitte geben Sie einen gültigen Link ein (z.B. www.bilder.de/Bild.jpg).";
		private const string _invalidFileTypeError = "Ivalider Datentyp. Unterstützt werden: {0}";
		private const string _convertionFailedError = "Link Konvertierung fehlgeschlagen.";
		private const string _noFileError = "Bitte geben sie eine Link zu einer Bilddatei an.";

		private readonly List<string> _supportedFileExtensions = new List<string>() { "png", "jpg", "jpeg" };


		public override Result Convert(string link)
		{
			Uri result;
			if (!Uri.TryCreate(link, UriKind.Absolute, out result))
				return new Result(_invalidFormatError);
			if (!isLinkToFile(result))
				return new Result(_noFileError);
			if (!isImageFile(result))
				return new Result(createInvalidFileTypeError());
			return new Result(result);
		}

		private bool isLinkToFile(Uri uri)
		{
			return !string.IsNullOrEmpty(Path.GetExtension(uri.AbsoluteUri));
		}

		private bool isImageFile(Uri uri)
		{
			string fileExtension = Path.GetExtension(uri.AbsoluteUri);
			string lowerCleanedFileExtension = fileExtension.ToLower();
			foreach (string extension in _supportedFileExtensions)
			{
				if (lowerCleanedFileExtension.Contains(extension))
					return true;
			}
			return false;
		}

		private string createInvalidFileTypeError()
		{
			string extensionsString = string.Empty;
			foreach (string extension in _supportedFileExtensions)
				extensionsString += $"{extension}, ";
			return string.Format(_invalidFileTypeError, extensionsString);
		}
	}
}