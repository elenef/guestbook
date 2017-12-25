using GuestBook.TestCore;
using Ninject;
using System;

namespace GuestBook.WebApi.Tests.Facts
{
    /// <summary>
    /// Base class for all unit tests
    /// </summary>
    public abstract class AFacts : IDisposable
    {
        protected IKernel _kernel;
        protected TestModule _testModule;

        public AFacts()
        {
            _testModule = new TestModule();
            _kernel = new StandardKernel(_testModule);
        }

        public void Dispose()
        {
            // Order is important
            _testModule.Dispose();
            _kernel.Dispose();
        }

        public T GetFixture<T>()
        {
            return _kernel.Get<T>();
        }
    }
}
