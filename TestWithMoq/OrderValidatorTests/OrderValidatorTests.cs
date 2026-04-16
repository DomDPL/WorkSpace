using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrderValidation;

[TestClass]
public class OrderValidatorTests
{
    private readonly OrderValidator _validator;

    public OrderValidatorTests()
    {
        _validator = new OrderValidator();
    }

    [TestMethod]
    public void IsValidOrder_NegativeAmount_ReturnsFalse()
    {
        bool result = _validator.IsValidOrder(-10m, 2, true, false);
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void IsValidOrder_ZeroAmount_ReturnsFalse()
    {
        bool result = _validator.IsValidOrder(0m, 5, true, false);
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void IsValidOrder_ZeroItems_ReturnsFalse()
    {
        bool result = _validator.IsValidOrder(50m, 0, true, false);
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void IsValidOrder_NonMember_BelowMinimum_ReturnsFalse()
    {
        bool result = _validator.IsValidOrder(20m, 3, false, false);
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void IsValidOrder_NonMember_MeetsMinimum_ReturnsTrue()
    {
        bool result = _validator.IsValidOrder(30m, 2, false, false);
        Assert.IsTrue(result);
    }

    [TestMethod]
    public void IsValidOrder_MemberWithCoupon_BelowMinimum_ReturnsFalse()
    {
        bool result = _validator.IsValidOrder(10m, 1, true, true);
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void IsValidOrder_MemberWithCoupon_MeetsMinimum_ReturnsTrue()
    {
        bool result = _validator.IsValidOrder(18m, 3, true, true);
        Assert.IsTrue(result);
    }

    [TestMethod]
    public void IsValidOrder_RegularMember_BelowMinimum_ReturnsFalse()
    {
        bool result = _validator.IsValidOrder(18m, 2, true, false);
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void IsValidOrder_RegularMember_MeetsMinimum_ReturnsTrue()
    {
        bool result = _validator.IsValidOrder(25m, 4, true, false);
        Assert.IsTrue(result);
    }

    [TestMethod]
    public void IsValidOrder_MemberWithCoupon_HighAmount_ReturnsTrue()
    {
        bool result = _validator.IsValidOrder(150m, 5, true, true);
        Assert.IsTrue(result);
    }
}