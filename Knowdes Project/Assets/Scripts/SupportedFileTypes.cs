using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Knowdes
{
    public class SupportedFileTypes
    {
        private readonly List<string> _image = new List<string>()
            { "png", "jpg", "jpeg" };
        public IEnumerable<string> Image => new List<string>(_image);

        public IEnumerable<string> All()
		{
            List<string> result = new List<string>();
            result.AddRange(Image);
            return result;
        }

		internal bool Contains(string extension)
		{
            return All().ToList().Contains(extension);
        }
	}
}