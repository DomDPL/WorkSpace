using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace DPLRef.eCommerce.Tests.AccessorTests
{
    public static class DataValidation
    {
        public static void AreEqual(object expected, object actual, params string[] excludeProps)
        {
            var expectedType = expected.GetType();
            var actualType = actual.GetType();
            Assert.AreEqual(expectedType.FullName, actualType.FullName);

            foreach (var p in actualType.GetProperties())
            {
                if (!excludeProps.Contains(p.Name))
                {
                    var expectedValue = p.GetValue(expected);
                    var actualValue = p.GetValue(actual);

                    Assert.AreEqual(expectedValue, actualValue, $"{p.Name} does not match");
                }
            }
        }
    }
}