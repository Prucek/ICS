using System;

namespace ICSproj.App.Factories
{
    // malo by sa používať pri vytváraní nového View Modelu ak som to pochopil správne
    public class Factory<T> : IFactory<T>
    {
        private readonly Func<T> _initFunc;

        public Factory(Func<T> initFunc) => _initFunc = initFunc;

        public T Create() => _initFunc();
    }
}
