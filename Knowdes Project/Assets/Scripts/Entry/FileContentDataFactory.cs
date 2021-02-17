using System;

namespace Knowdes
{
    public class FileContentDataFactory
    {
        private ContentDataFactory _contentDataFactory;

        private FilePathConverter _filePathConverter;
        private ImageFilePathConverter _imageFilePathConverter;
        private UnknownFilePathConverter _unknownFilePathConverter;
        private PdfPathConverter _pdfPathConverter;


        public FileContentDataFactory()
		{
            _contentDataFactory = new ContentDataFactory();
            _filePathConverter = new FilePathConverter();
            _imageFilePathConverter = new ImageFilePathConverter();
            _unknownFilePathConverter = new UnknownFilePathConverter();
            _pdfPathConverter = new PdfPathConverter();
        }


        public ContentData Create(string path)
		{
            if (!_filePathConverter.Convertable(path))
                throw new ArgumentException();
            if (_imageFilePathConverter.Convertable(path))
            {
                Uri uri = Convert(path, _imageFilePathConverter);
                ImageContentData result = _contentDataFactory.Create(ContentType.Image) as ImageContentData;
                result.Path = uri.AbsolutePath;
                return result;
            }
            else if(_pdfPathConverter.Convertable(path))
			{
                Uri uri = Convert(path, _pdfPathConverter);
                PdfContentData result = _contentDataFactory.Create(ContentType.PDF) as PdfContentData;
                result.Path = uri.AbsolutePath;
                return result;
            }
            else
			{
                Uri uri = Convert(path, _unknownFilePathConverter);
                UnknownFileContentData result = _contentDataFactory.Create(ContentType.File) as UnknownFileContentData;
                result.Path = uri.AbsolutePath;
                return result;
            }
        }

        private Uri Convert(string path, UriConverter converter)
		{
            UriConverter.Result result = converter.Convert(path);
            if (!result.Successful)
                throw new ArgumentException();
            return result.Uri;
        }
    }
}