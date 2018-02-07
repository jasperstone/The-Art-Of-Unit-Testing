using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LogAn.UnitTests
{
    [TestClass]
    public class LogAnalyzerTests
    {
        [TestMethod]
        public void IsValidLogFileName_BadExtension_ReturnsFalse()
        {
            var analyzer = new LogAnalyzer();

            var result = analyzer.IsValidLogFileName("filewithbadextension.foo");

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsValidLogFileName_GoodExtensionLowercase_ReturnsTrue()
        {
            var analyzer = new LogAnalyzer();

            var result = analyzer.IsValidLogFileName("filewithgoodextension.slf");

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsValidLogFileName_GoodExtensionUppercase_ReturnsTrue()
        {
            var analyzer = new LogAnalyzer();

            var result = analyzer.IsValidLogFileName("filewithgoodextension.SLF");

            Assert.IsTrue(result);
        }

        // this is a refactoring of the previous two tests
        [DataTestMethod]
        [DataRow("filewithgoodextension.SLF")]
        [DataRow("filewithgoodextension.slf")]
        public void IsValidLogFileName_ValidExtensions_ReturnsTrue(string file)
        {
            var analyzer = new LogAnalyzer();

            var result = analyzer.IsValidLogFileName(file);

            Assert.IsTrue(result);
        }

        // this is a refactoring of all the "regular" tests
        [DataTestMethod]
        [DataRow("filewithgoodextension.SLF", true)]
        [DataRow("filewithgoodextension.slf", true)]
        [DataRow("filewithbadextension.foo", false)]
        public void IsValidLogFileName_VariousExtensions_ChecksThem(string file, bool expected)
        {
            var analyzer = new LogAnalyzer();

            var result = analyzer.IsValidLogFileName(file);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "filename has to be provided")]
        public void IsValidLogFileName_EmptyFileName_ThrowsException()
        {
            var la = MakeAnalyzer();
            la.IsValidLogFileName(string.Empty);
        }

        private LogAnalyzer MakeAnalyzer()
        {
            return new LogAnalyzer();
        }

        [TestMethod]
        public void IsValidLogFileName_EmptyFileName_Throws()
        {
            var la = MakeAnalyzer();

            var ex = Assert.ThrowsException<ArgumentException>(() => la.IsValidLogFileName(""));

            StringAssert.Contains("filename has to be provided", ex.Message);
        }

        [TestMethod]
        public void IsValidLogFileName_EmptyFileName_ThrowsFluent()
        {
            var la = MakeAnalyzer();

            var ex = Assert.ThrowsException<ArgumentException>(() => la.IsValidLogFileName(""));

            StringAssert.Contains(ex.Message, "filename has to be provided");
        }

        [TestMethod]
        public void IsValidLogFileName_WhenCalled_ChangesWasLastFileNameValid()
        {
            var la = MakeAnalyzer();

            la.IsValidLogFileName("badname.foo");

            Assert.IsFalse(la.WasLastFileNameValid);
        }

        //refactored from above
        [DataTestMethod]
        [DataRow("badfile.foo", false)]
        [DataRow("goodfile.slf", true)]
        public void IsValidLogFileName_WhenCalled_ChangesWasLastFileNameValid(string file, bool expected)
        {
            var la = MakeAnalyzer();

            la.IsValidLogFileName(file);

            Assert.AreEqual(expected, la.WasLastFileNameValid);
        }
    }
}