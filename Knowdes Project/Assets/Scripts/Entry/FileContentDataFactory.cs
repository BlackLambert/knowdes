using System;

namespace Knowdes
{
    public class FileContentDataFactory
    {
        private ContentDataFactory _contentDataFactory;

        private FilePathConverter _filePathConverter;
        private ImageFilePathConverter _imageFilePathConverter;
        private UnknownFilePathConverter _unknownFilePathConverter;


        public FileContentDataFactory()
		{
            _contentDataFactory = new ContentDataFactory();
            _filePathConverter = new FilePathConverter();
            _imageFilePathConverter = new ImageFilePathConverter();
            _unknownFilePathConverter = new UnknownFilePathConverter();
        }


        public ContentData Create(string path)
		{
            if (!_filePathConverter.Convertable(path))
                throw new ArgumentException();
            if (_imageFilePathConverter.Convertable(path))
            {
                Uri uri = Convert(path, _imageFilePathConverter);
                ImageContentData result = _contentDataFactory.Create(ContentType.Image) as ImageContentData;
                result.Path = uri.AbsoluteUri;
                return result;
            }
            else
			{
                Uri uri = Convert(path, _unknownFilePathConverter);
                UnknownFileContentData result = _contentDataFactory.Create(ContentType.File) as UnknownFileContentData;
                result.Path = uri.AbsoluteUri;
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