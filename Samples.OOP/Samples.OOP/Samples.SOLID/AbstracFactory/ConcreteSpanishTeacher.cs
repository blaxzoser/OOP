namespace Samples.SOLID.AbstracFactory
{
    internal class ConcreteSpanishTeacher : IAbstracTeacher
    {
        public ConcreteSpanishTeacher()
        {
        }

        public string DoSomething()
        {
            return "Teach Spanish";
        }
    }
}