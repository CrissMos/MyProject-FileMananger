using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyProject.Models
{
    public class Enums
    {
        public enum AssetCategory{
            Image=1,
            Video=2
        }

        public enum VariantType
        {
            Initial=1,
            Thumbnail=2,
            Small=3,
            Big=4,
            Instagram=5
        }
    }
}
