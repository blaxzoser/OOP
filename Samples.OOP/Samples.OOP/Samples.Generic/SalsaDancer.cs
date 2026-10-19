using System;
using System.Collections.Generic;
using System.Text;

namespace Samples.Generics
{
    public class SalsaDancer : Dancer
    {
        public SalsaDancer(string name, int id, int level = 1) : base(name, id) => Level = level;
        public int Level { get; set; }
    }
}
