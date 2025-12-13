using System;
using NUnit.Framework;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace WinMemoryCleaner.Test
{
    /// <summary>
    /// Unit tests for Command classes
    /// </summary>
    [TestFixture]
    public sealed class CommandTests
    {
        #region RelayCommand Tests

        [TestFixture]
        public class RelayCommandTests
        {
            [Test]
            public void CanExecute_WithNoCanExecuteFunc_ReturnsTrue()
            {
                var command = new RelayCommand(() => { });
                var result = command.CanExecute(null);

                Assert.IsTrue(result);
            }

            [Test]
            public void CanExecute_WithTrueFunc_ReturnsTrue()
            {
                var command = new RelayCommand(() => { }, () => true);
                var result = command.CanExecute(null);

                Assert.IsTrue(result);
            }

            [Test]
            public void CanExecute_WithFalseFunc_ReturnsFalse()
            {
                var command = new RelayCommand(() => { }, () => false);
                var result = command.CanExecute(null);

                Assert.IsFalse(result);
            }

            [Test]
            public void CanExecuteChanged_AddAndRemoveHandlers_DoesNotThrow()
            {
                var command = new RelayCommand(() => { });
                EventHandler handler = (sender, e) => { };

                command.CanExecuteChanged += handler;
                command.CanExecuteChanged -= handler;
            }

            [Test]
            public void Constructor_WithAction_DoesNotThrow()
            {
                var unused = new RelayCommand(() => { });
            }

            [Test]
            public void Execute_InvokesAction()
            {
                var executed = false;
                var command = new RelayCommand(() => executed = true);

                command.Execute(null);

                Assert.IsTrue(executed);
            }

            [Test]
            public void Execute_MultipleTimes_InvokesMultipleTimes()
            {
                var count = 0;
                var command = new RelayCommand(() => count++);

                command.Execute(null);
                command.Execute(null);
                command.Execute(null);

                Assert.AreEqual(3, count);
            }
        }

        #endregion

        #region RelayCommand<T> Tests

        [TestFixture]
        public class RelayCommandGenericTests
        {
            [Test]
            public void CanExecute_WithNoCanExecuteFunc_ReturnsTrue()
            {
                var command = new RelayCommand<int>(_ => { });
                var result = command.CanExecute(42);

                Assert.IsTrue(result);
            }

            [Test]
            public void CanExecute_WithTrueFunc_ReturnsTrue()
            {
                var command = new RelayCommand<int>(_ => { }, () => true);
                var result = command.CanExecute(42);

                Assert.IsTrue(result);
            }

            [Test]
            public void CanExecute_WithFalseFunc_ReturnsFalse()
            {
                var command = new RelayCommand<int>(_ => { }, () => false);
                var result = command.CanExecute(42);

                Assert.IsFalse(result);
            }

            [Test]
            public void Constructor_WithAction_DoesNotThrow()
            {
                var unused = new RelayCommand<string>(_ => { });
            }

            [Test]
            public void Execute_InvokesActionWithParameter()
            {
                string result = null;
                var command = new RelayCommand<string>(param => result = param);

                command.Execute("test");

                Assert.AreEqual("test", result);
            }

            [Test]
            public void Execute_WithComplexType_InvokesCorrectly()
            {
                TestObject receivedObject = null;
                var command = new RelayCommand<TestObject>(obj => receivedObject = obj);
                var testObject = new TestObject { Value = 123 };

                command.Execute(testObject);

                Assert.AreSame(testObject, receivedObject);
                Assert.AreEqual(123, receivedObject.Value);
            }
        }

        #endregion

        #region Helper Classes

        private class TestObject
        {
            public int Value { get; set; }
        }

        #endregion
    }
}

#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
