namespace Catalog.API.Controllers.ExerciseDependencyInjection
{
    public class ClassB
    {
        public IClassA _classA;
        public ClassB(IClassA classA)
        {
            _classA = classA;
        }

        public string actionB() => _classA.actionA();
    }
}
