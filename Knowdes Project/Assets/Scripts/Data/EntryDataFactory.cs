using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knowdes
{
    public class EntryDataFactory
    {
        public EntryData CreateNew(ContentData content)
		{
            Guid iD = Guid.NewGuid();
            EntryData result = new EntryData(iD, content);
            return result;

        }
    }
}