namespace Samples.SOLID.AbstracFactory
{
   public  interface IWommanFactory
    {
        IAbstracDancer CreateDancer();
        IAbstracTeacher CreateTeacher();
    }
}
