
using creditoauto.Domain.Interfaces;
using creditoauto.Entity.Models;
using creditoauto.Test.FackeClass;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace creditoauto.Test.Repository
{
    public class RepositoryTest
    {
        [Test]
        public void CreateEntityAsync_TestClassObjectPassed_ReturnEntity()
        {
            //Arrange
            var testObject = new TestClass();
            var repositoryMock = new Mock<IRepository<TestClass>>();
           

            repositoryMock.Setup(r => r.CreateEntityAsync(testObject)).ReturnsAsync(new TestClass { });
        }
    }
}
