using System;
using System.IO;

namespace Knowdes
{
	public class FilePathConverter : UriConverter
	{
		private const string _invalidFormatError = "Bitte geben Sie einen gültigen Dateipfad an.";

		public override Result Convert(string link)
		{
			Uri result;
			if (!Uri.TryCreate(link, UriKind.Absolute, out result))
				return new Result(_invalidFormatError);
			if (!hasExtension(result))
				return new Result(_invalidFormatError);
			return new Result(result);
		}

		public override bool Convertable(string link)
		{
			Uri uri;
			return Uri.TryCreate(link, UriKind.Absolute, out uri) &&
				hasExtension(uri);
		}

		private bool hasExtension(Uri uri)
		{
			string fileExtension = Path.GetExtension(uri.AbsoluteUri);
			return !string.IsNullOrEmpty(fileExtension);
		}
	}
}