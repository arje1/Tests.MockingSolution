using ArtGallery.Managers;
using ArtGallery.Model;
using Moq;
using System;
using Xunit;

namespace ArtGallery.Tests
{
    public class ExhibitionManagerShould
    {
        /// <summary>
        /// ამოწმებს რომ მოსწავლისთვის ფასდაკლება 70%-ია.
        /// </summary>
        [Fact]
        public void SaleSeventyPercentForSchoolGuy()
        {
            //შევქმნათ IValidator ინტერფეისის მოქი
            var mockValidator = new Mock<IValidator>();
            var price = 100;

            //IValidator ინტერფეისის მოქში განვსაზღვროთ რას დააბრუნებს IsValidPrice მეთოდი კონკრეტული პარამეტრისთვის
            mockValidator.Setup(x => x.IsValidPrice(price)).Returns(true);

            var sut = new ExhibitionManager(mockValidator.Object);

            var exhibition = new Exhibition();
            exhibition.Price = price;

            var newPrice = sut.GetExhebitionPrice(exhibition, GuestStatus.CitizenSchoolGuy);

            Assert.Equal(30, newPrice);
        }
        /// <summary>
        /// ამოწმებს რომ სტუდენტისთვის ფასდაკლება 50%-ია.
        /// </summary>
        [Fact]
        public void SaleFiftyPercentForStudent()
        {
            //შევქმნათ IValidator ინტერფეისის მოქი
            var mockValidator = new Mock<IValidator>();
            var price = 100;

            //IValidator ინტერფეისის მოქში განვსაზღვროთ რას დააბრუნებს IsValidPrice მეთოდი კონკრეტული პარამეტრისთვის
            mockValidator.Setup(x => x.IsValidPrice(price)).Returns(true);

            var sut = new ExhibitionManager(mockValidator.Object);

            var exhibition = new Exhibition();
            exhibition.Price = price;

            var newPrice = sut.GetExhebitionPrice(exhibition, GuestStatus.CitizenStudent);

            Assert.Equal(50, newPrice);
        }

        /// <summary>
        /// ამოწმებს რომ პენსიონერისთვის გამოფენა უფასოა
        /// </summary>
        [Fact]
        public void SaleHundredPercentForPensioner()
        {
            //შევქმნათ IValidator ინტერფეისის მოქი
            var mockValidator = new Mock<IValidator>();
            double price = new Random().Next(ExhibitionManager.MinimalPrice + 1, int.MaxValue);

            //IValidator ინტერფეისის მოქში განვსაზღვროთ რას დააბრუნებს IsValidPrice მეთოდი ნებისმიერი double ტიპის პარამეტრისთვის
            mockValidator.Setup(x => x.IsValidPrice(It.IsAny<double>())).Returns(true);

            var sut = new ExhibitionManager(mockValidator.Object);

            var exhibition = new Exhibition { Price = price };

            var newPrice = sut.GetExhebitionPrice(exhibition, GuestStatus.CitizenPensioner);

            Assert.Equal(0, newPrice);
        }
    }
}