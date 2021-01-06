using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knowdes
{
    public class Tag 
    {
        public Guid ID { get; }

        private string _name;
        public event Action OnNameChanged;
        public string Name
		{
            get => _name;
			set
			{
                _name = value;
                OnNameChanged();
			}
		}


        public Tag(Guid iD, string name)
		{
            ID = iD;
            _name = name;
		}
    }
}