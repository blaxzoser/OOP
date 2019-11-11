namespace Samples.SOLID.AbstracFactory
{
    internal class ConcreteSalsaDancer : IAbstracDancer
    {
        public ConcreteSalsaDancer()
        {
        }

        public string DoSomething()
        {
            return "Dance Salsa";
        }
    }
}