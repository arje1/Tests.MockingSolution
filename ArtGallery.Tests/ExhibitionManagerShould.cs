using ArtGallery.Managers;
using ArtGallery.Model;
using Moq;
using Xunit;

namespace ArtGallery.Tests
{
    public class ExhibitionManagerShould
    {
        [Fact]
        public void SaleSeventyPercentForSchoolGuy()
        {
            var mockValidator = new Mock<IValidator>();
            var price = 100;

            mockValidator.Setup(x => x.IsValidPrice(price)).Returns(true);

            var sut = new ExhibitionManager(mockValidator.Object);

            var exhibition = new Exhibition();
            exhibition.Price = price;

            var newPrice = sut.GetExhebitionPrice(exhibition, GuestStatus.CitizenSchoolGuy);

            Assert.Equal(30, newPrice);
        }

        [Fact]
        public void SaleFiftyPercentForStudent()
        {
            var mockValidator = new Mock<IValidator>();
            var price = 100;

            mockValidator.Setup(x => x.IsValidPrice(price)).Returns(true);

            var sut = new ExhibitionManager(mockValidator.Object);

            var exhibition = new Exhibition();
            exhibition.Price = price;

            var newPrice = sut.GetExhebitionPrice(exhibition, GuestStatus.CitizenStudent);

            Assert.Equal(50, newPrice);
        }
    }
}