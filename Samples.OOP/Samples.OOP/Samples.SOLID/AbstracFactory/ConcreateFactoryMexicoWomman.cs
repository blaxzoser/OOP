using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Samples.SOLID.AbstracFactory
{
   public  class ConcreateFactoryMexicoWomman : IWommanFactory
    {
        public IAbstracDancer CreateDancer()
        {
           return  new ConcreteSalsaDancer();
        }

        public IAbstracTeacher CreateTeacher()
        {
            return new ConcreteSpanishTeacher();
        }
    }
}
