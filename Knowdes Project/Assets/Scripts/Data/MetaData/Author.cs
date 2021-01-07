using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knowdes
{
    public class Author 
    {
		private string _name;
		public event Action OnNameChanged;
		public string Name
		{
			get => _name;
			set
			{
				_name = value;
				OnNameChanged?.Invoke();
			}
		}

		public Author(string name)
		{
			_name = name;
		}
	}
}