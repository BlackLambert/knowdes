using System;
using System.IO;

namespace Knowdes
{
    public class UnknownFilePathConverter : UriConverter
    {
		private const string _invalidFormatError = "Bitte geben Sie einen gültigen Pfad oder Link zu einer Datei an.";
		private const string _knownFileTypeError = "{0} ist ein unterstützter .";

		private SupportedFileTypes _supportedFiles = new SupportedFileTypes();


		public override Result Convert(string link)
		{
			Uri result;
			if (!Uri.TryCreate(link, UriKind.Absolute, out result))
				return new Result(_invalidFormatError);
			if (!isFilePath(result))
				return new Result(_invalidFormatError);
			if (!isUnknownFilePath(result))
				return new Result(createKnownFileTypeError(result));
			return new Result(result);
		}

		public override bool Convertable(string link)
		{
			Uri uri;
			return Uri.TryCreate(link, UriKind.Absolute, out uri) &&
				isFilePath(uri) &&
				isUnknownFilePath(uri);
		}

		private bool isFilePath(Uri uri)
		{
			return !string.IsNullOrEmpty(Path.GetExtension(uri.AbsoluteUri));
		}

		private bool isUnknownFilePath(Uri uri)
		{
			string extension = Path.GetExtension(uri.AbsoluteUri);
			if (string.IsNullOrEmpty(extension))
				return false;
			string[] split = extension.Split('.');
			extension = split[split.Length - 1];
			return !_supportedFiles.Contains(extension);
		}

		private string createKnownFileTypeError(Uri uri)
		{
			return string.Format(_knownFileTypeError);
		}
	}
}