using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Samples.SOLID.AbstracFactory
{
    class ConcreateFolckoreDancer : IAbstracDancer
    {
        public string DoSomething()
        {
            return "Dance Folckore";
           
        }
    }
}
