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

        public MetaContentEditorShell Create(MetaData data)
		{
            MetaContentEditorShell result = Instantiate(_shellPrefab);
            MetaContentEditor editor = createEditor(data);
            result.SetEditor(editor);
            return result;
        }

        private MetaContentEditor createEditor(MetaData data)
		{
            switch (data.Type)
            {
                case MetaDataType.Title:
                    TitleEditor result = Instantiate(_titleEditorPrefab);
                    result.Data = data as TitleData;
                    return result;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}