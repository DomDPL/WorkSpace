using LeadManager;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Specialized;
using System.Threading.Tasks;

[TestClass]
public class LeadManagerTests
{
    private Mock<IContactAccess> _mockContactAccess;
    private LeadManager.LeadManager _leadManager;

    [TestInitialize]
    public void TestInitialize()
    {
        _mockContactAccess = new Mock<IContactAccess>();
        _leadManager = new LeadManager.LeadManager(_mockContactAccess.Object);
    }

    [TestMethod]
    public async Task PromoteToLeadAsync_ExistingContact_SetsIsLeadToTrue()
    {
        // Arrange
        var id = 1;
        var contact = new Contact
        {
            Id = id,
            IsLead = false
        };
        _mockContactAccess.Setup(c => c.GetContactAsync(999))
            .ReturnsAsync(contact);

        // _mockContactAccess.Setup(s => s.SaveContactAsync(It.Is<Contact>(c => c.Id == 2 && c.IsLead == true)))
        //     .Returns(Task.CompletedTask);

        // Act
        await _leadManager.PromoteToLeadAsync(999);

        // Assert
        Assert.IsTrue(contact.IsLead);
        _mockContactAccess.Verify(v => v.SaveContactAsync(It.Is<Contact>(c => c.Id == contact.Id && c.IsLead == true)), Times.Exactly(1));
       
    }

    [TestMethod]
    public async Task PromoteToLeadAsync_ContactNotFound_ThrowsException()
    {
        // Arrange
        _mockContactAccess
            .Setup(m => m.GetContactAsync(2))
            .ReturnsAsync((Contact)null!);

        // Act 
        // Assert
        await Assert.ThrowsExactlyAsync<InvalidOperationException>(
            () => _leadManager.PromoteToLeadAsync(2));
    }

    [TestMethod]
    public async Task IsLeadAsync_ReturnsCorrectValue()
    {
        // Arrange
        _mockContactAccess
        .Setup(x => x.GetContactAsync(3))
        .ReturnsAsync(new Contact { Id = 3, IsLead = false });

        // Act
        var result = await _leadManager.IsLeadAsync(3);

        // Assert
        Assert.IsFalse(result);
    }

    [TestMethod]
    public async Task PromoteToLeadAsync_VerifySaveIsCalledExactlyOnce()
    {
        // Arrange
        var contact = new Contact { Id = 100, IsLead = false };
        _mockContactAccess.Setup(x => x.GetContactAsync(100)).ReturnsAsync(contact);
        _mockContactAccess.Setup(x => x.SaveContactAsync(It.IsAny<Contact>())).Returns(Task.CompletedTask);

        // Act
        await _leadManager.PromoteToLeadAsync(100);

        // Assert
        _mockContactAccess.Verify(x => x.SaveContactAsync(It.IsAny<Contact>()), Times.Once);
    }
}