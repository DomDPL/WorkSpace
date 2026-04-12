using DPLRef.eCommerce.Accessors.EntityFramework;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DPLref.eCommerce.Tests.IntegrationTests
{
    [TestClass]
    public abstract class IntegrationTestBase
    {
        [TestInitialize]
        public void Init() => CreateGlobalContext();

        [TestCleanup]
        public void Cleanup() => CancelGlobalTransaction();

        public static void CreateGlobalContext()
        {
            eCommerceDbContext.UnitTestContext = eCommerceDbContext.Create(false);
            _ = eCommerceDbContext.UnitTestContext.Database.BeginTransaction();
        }

        public static void CancelGlobalTransaction()
        {
            if (eCommerceDbContext.UnitTestContext != null)
            {
                eCommerceDbContext.UnitTestContext.Database.RollbackTransaction();
                eCommerceDbContext.UnitTestContext.AllowDispose = true;
                eCommerceDbContext.UnitTestContext.Dispose();
                eCommerceDbContext.UnitTestContext = null;
            }
        }
    }
}