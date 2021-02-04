using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knowdes.Prototype
{
    public class MetaContentEditorFactory : MonoBehaviour
    {
        [SerializeField]
        private MetaContentEditorShell _shellPrefab;
        [SerializeField]
        private TitleEditor _titleEditorPrefab;
        [SerializeField]
        private TagsEditor _tagsEditorPrefab;
        [SerializeField]
        private CreationDateEditor _creationDateEditor;
        [SerializeField]
        private LastChangeDateEditor _lastChangeDateEditor;
        [SerializeField]
        private CommentEditor _commentEditor;
        [SerializeField]
        private DescriptionEditor _descriptionEditor;
        [SerializeField]
        private PreviewImageEditor _previewImageEditor;

        public MetaContentEditorShell Create(MetaData data)
		{
            MetaContentEditorShell result = Instantiate(_shellPrefab);
            MetaContentEditor editor = createEditor(data, result);
            result.SetEditor(editor);
            return result;
        }

        private MetaContentEditor createEditor(MetaData data, MetaContentEditorShell shell)
		{
            switch (data.Type)
            {
                case MetaDataType.Title:
                    shell.ViewToggle.Toggle(true);
                    TitleEditor result = Instantiate(_titleEditorPrefab);
                    result.Data = data as TitleData;
                    return result;
                case MetaDataType.Tags:
                    shell.ViewToggle.Toggle(true);
                    TagsEditor tagsEditor = Instantiate(_tagsEditorPrefab);
                    tagsEditor.Data = data as TagsData;
                    return tagsEditor;
                case MetaDataType.CreationDate:
                    shell.ViewToggle.Toggle(false);
                    CreationDateEditor creationDateEditor = Instantiate(_creationDateEditor);
                    creationDateEditor.Data = data as CreationDateData;
                    return creationDateEditor;
                case MetaDataType.LastChangedDate:
                    shell.ViewToggle.Toggle(false);
                    LastChangeDateEditor lastChangeDate = Instantiate(_lastChangeDateEditor);
                    lastChangeDate.Data = data as LastChangeDateData;
                    return lastChangeDate;
                case MetaDataType.Comment:
                    shell.ViewToggle.Toggle(true);
                    CommentEditor commentEditor = Instantiate(_commentEditor);
                    commentEditor.Data = data as CommentData;
                    return commentEditor;
                case MetaDataType.Description:
                    shell.ViewToggle.Toggle(true);
                    DescriptionEditor descriptionEditor = Instantiate(_descriptionEditor);
                    descriptionEditor.Data = data as DescriptionData;
                    return descriptionEditor;
                case MetaDataType.PreviewImage:
                    shell.ViewToggle.Toggle(true);
                    PreviewImageEditor previewImageEditor = Instantiate(_previewImageEditor);
                    previewImageEditor.Data = data as PreviewImageData;
                    return previewImageEditor;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}