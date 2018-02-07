using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LogAn.UnitTests
{
    //PRODUCTION CODE
    public class ClassUnderTest
    {
        public static void ReturnAfter500Ms(int value, Action<int> result)
        {
            TaskFacility.Factory.StartNew(() =>
            {
                Thread.Sleep(500);
                result(value);
            });
        }
    }


    public class TaskFacility
    {
        private static TaskFactory _factory;

        public static TaskFactory Factory
        {
            get => _factory ?? Task.Factory;
            set => _factory = value;
        }

        public static void Reset()
        {
            Factory = Task.Factory;
        }
    }


    // TEST CODE
    [TestClass]
    public class AsyncTests
    {
        [TestMethod]
        public void TestAsync()
        {
            var tasks = new TaskFactory(new CancellationTokenSource().Token, TaskCreationOptions.AttachedToParent,
                TaskContinuationOptions.ExecuteSynchronously, TaskScheduler.Default);
            TaskFacility.Factory = tasks;

            ClassUnderTest.ReturnAfter500Ms(3, Console.WriteLine);
        }
    }
}