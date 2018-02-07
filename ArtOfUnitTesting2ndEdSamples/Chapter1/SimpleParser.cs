using System;
using System.Reflection;

namespace Chapter1
{
    public class SimpleParser
    {
        public int ParseAndSum(string numbers)
        {
            if (numbers.Length == 0)
                return 0;
            if (!numbers.Contains(","))
                return int.Parse(numbers);
            throw new InvalidOperationException(
                "I can only handle 0 or 1 numbers for now!");
        }
    }

    public class SimpleParserTests
    {
        public static void TestReturnsZeroWhenEmptyString()
        {
            try
            {
                var p = new SimpleParser();
                var result = p.ParseAndSum(string.Empty);
                if (result != 0)
                    Console.WriteLine(
                        @"***SimpleParserTests.TestReturnsZeroWhenEmptyString: 
-------
Parse and sum should have returned 0 on an empty string");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }

    public class TestUtil
    {
        public static void ShowProblem(string test, string message)
        {
            var msg = $@"
---{test}---
       {message}
--------------------
";
            Console.WriteLine(msg);
        }
    }

    public class SimpleParserTestsWithTestUtil
    {
        public static void TestReturnsZeroWhenEmptyString()
        {
            var testName = MethodBase.GetCurrentMethod().Name;
            try
            {
                var p = new SimpleParser();
                var result = p.ParseAndSum(string.Empty);
                if (result != 0)
                    TestUtil.ShowProblem(testName,
                        "Parse and sum should have returned 0 on an empty string");
            }
            catch (Exception e)
            {
                TestUtil.ShowProblem(testName, e.ToString());
            }
        }
    }
}