using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Cat : IId
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public byte[] Image { get; set; }

        public int TypeId { get; set; }
        public CatType Type { get; set; }

    }
}
