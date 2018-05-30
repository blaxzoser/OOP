using System;
using System.Collections.Generic;
using System.Text;

namespace Samples.Generics
{
    public class Dancer
    {
        public Dancer(string s, int i) => (Name, ID) = (s, i);
        public string Name { get; set; }
        public int ID { get; set; }
    }
}
