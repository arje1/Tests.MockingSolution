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
            //ARRANGE

            //შევქმნათ IValidator ინტერფეისის მოქი
            var mockValidator = new Mock<IValidator>();
            var price = 100;

            //IValidator ინტერფეისის მოქში განვსაზღვროთ რას დააბრუნებს IsValidPrice მეთოდი კონკრეტული პარამეტრისთვის
            mockValidator.Setup(x => x.IsValidPrice(price)).Returns(true);

            var sut = new ExhibitionManager(mockValidator.Object);

            var exhibition = new Exhibition();
            exhibition.Price = price;

            //ACT

            var newPrice = sut.GetExhebitionPrice(exhibition, GuestStatus.CitizenSchoolGuy);

            //ASSERT

            Assert.Equal(30, newPrice);
        }
        /// <summary>
        /// ამოწმებს რომ სტუდენტისთვის ფასდაკლება 50%-ია.
        /// </summary>
        [Fact]
        public void SaleFiftyPercentForStudent()
        {
            //ARRANGE

            //შევქმნათ IValidator ინტერფეისის მოქი
            var mockValidator = new Mock<IValidator>();
            var price = 100;

            //IValidator ინტერფეისის მოქში განვსაზღვროთ რას დააბრუნებს IsValidPrice მეთოდი პარამეტრისთვის რომელიც აკმაყოფილებს მითითებულ პირობას.
            mockValidator.Setup(x => x.IsValidPrice(It.Is<double>(pr => pr > 5))).Returns(true);

            var sut = new ExhibitionManager(mockValidator.Object);

            var exhibition = new Exhibition();
            exhibition.Price = price;

            //ACT

            var newPrice = sut.GetExhebitionPrice(exhibition, GuestStatus.CitizenStudent);

            //ASSERT

            Assert.Equal(50, newPrice);
        }

        /// <summary>
        /// ამოწმებს რომ პენსიონერისთვის გამოფენა უფასოა
        /// </summary>
        [Fact]
        public void SaleHundredPercentForPensioner()
        {
            //ARRANGE

            //შევქმნათ IValidator ინტერფეისის მოქი
            var mockValidator = new Mock<IValidator>(MockBehavior.Strict); //MockBehavior.Strict ყოველთვის ისვრის ექსეფშენს იმ შემთხვევაში თუ მოქის ყველა მეთოდს არ აქვს Setup-ი.
            double price = new Random().Next(ExhibitionManager.MinimalPrice + 1, int.MaxValue);

            //IValidator ინტერფეისის მოქში განვსაზღვროთ რას დააბრუნებს IsValidPrice მეთოდი ნებისმიერი double ტიპის პარამეტრისთვის
            mockValidator.Setup(x => x.IsValidPrice(It.IsAny<double>())).Returns(true);

            var sut = new ExhibitionManager(mockValidator.Object);

            var exhibition = new Exhibition { Price = price };

            //ACT

            var newPrice = sut.GetExhebitionPrice(exhibition, GuestStatus.CitizenPensioner);

            //ASSERT

            Assert.Equal(0, newPrice);
        }

        /// <summary>
        /// ამოწმებს რომ არა-მოქალაქისთვის არ არის ფასდაკლებები.
        /// </summary>
        [Fact]
        public void SaleZeroPercentForNonCitizen()
        {
            //ARRANGE

            var mockValidator = new Mock<IValidator>();
            double price = new Random().Next(0, 500);

            var isValid = true;
            mockValidator.Setup(x => x.IsValidPrice(It.IsAny<double>(), out isValid)); // out პარამეტრის Setup-ი.

            var sut = new ExhibitionManager(mockValidator.Object);

            var exhibition = new Exhibition { Price = price };

            //ACT

            var newPrice = sut.GetExhebitionPrice2(exhibition, GuestStatus.NonCitizen);

            //ASSERT
            Assert.Equal(price, newPrice);

        }

    }
}