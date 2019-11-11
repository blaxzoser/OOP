using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Samples.SOLID.AbstracFactory
{
    public class ConcreateFactoryGermanyWomman : IWommanFactory
    {
        public IAbstracDancer CreateDancer()
        {
          return   new ConcreateFolckoreDancer();
        }

        public IAbstracTeacher CreateTeacher()
        {
            return new ConcreateGermanyTeacher();
        }
    }
}
