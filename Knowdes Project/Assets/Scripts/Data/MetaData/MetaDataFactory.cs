using System;

namespace Knowdes
{
    public class MetaDataFactory
    {
        private const string _defaultTitle = "Titel";
        private const string _defaultComment = "Neuer Kommentar";
        private const string _defaultDescription = "Neue Beschreibung";

        public MetaData CreateNew(MetaDataType type)
		{
            Guid iD = Guid.NewGuid();
            switch (type)
            {
                case MetaDataType.Title:
                    return new TitleData(iD, _defaultTitle);
                case MetaDataType.Author:
                    return new AuthorData(iD);
                case MetaDataType.CreationDate:
                    return new CreationDateData(iD);
                case MetaDataType.LastChangedDate:
                    return new LastChangeDateData(iD);
                case MetaDataType.Tags:
                    return new TagsData(iD);
                case MetaDataType.Comment:
                    return new CommentData(iD, _defaultComment);
                case MetaDataType.Description:
                    return new DescriptionData(iD, _defaultDescription);
                case MetaDataType.PreviewImage:
                    return new PreviewImageData(iD, null);
                default:
                    throw new NotImplementedException();
            }
        }
    }
}