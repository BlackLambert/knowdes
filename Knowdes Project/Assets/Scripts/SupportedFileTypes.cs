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
        private readonly List<string> _pdf = new List<string>() {"pdf" };
        public IEnumerable<string> Image => new List<string>(_image);
        public IEnumerable<string> Pdf => new List<string>(_pdf);

        public IEnumerable<string> All()
		{
            List<string> result = new List<string>();
            result.AddRange(Image);
            result.AddRange(Pdf);
            return result;
        }

		internal bool Contains(string extension)
		{
            return All().ToList().Contains(extension);
        }
	}
}