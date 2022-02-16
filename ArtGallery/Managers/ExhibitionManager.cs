using ArtGallery.Model;

namespace ArtGallery.Managers
{
    public class ExhibitionManager
    {
        private const double MinimalPrice = 5;
        private const double TwentyPercent = 0.8;
        private const double FiftyPercent = 0.5;
        private const double SeventyPercent = 0.3;
        private const double ZeroPercent = 1;
        private const double HundredPercent = 0;
        private IValidator _validator { get; }

        public ExhibitionManager(IValidator validator)
        {
            _validator = validator;
        }


        /// <summary>
        /// აბრუნებს გამოფენის ფასს ფასდაკლების გათვალისწინებით
        /// </summary>
        /// <param name="exhibition">გამოფენა</param>
        /// <param name="guestStatus">სტუმრის სტატუსი</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public double GetExhebitionPrice(Exhibition exhibition, GuestStatus guestStatus)
        {
            if (!_validator.IsValidPrice(exhibition.Price))
            {
                throw new ArgumentException("Provided exhibition price is not correct!");
            }

            if (exhibition.Price <= MinimalPrice)
            {
                return exhibition.Price;
            }
            return exhibition.Price * GetSalePercent(guestStatus);
        }

        /// <summary>
        /// აბრუნებს ფასდაკლების პროცენტს სტუმრის სტატუსის მიხედვით
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        private double GetSalePercent(GuestStatus status) =>
             status switch
             {
                 GuestStatus.CitizenSchoolGuy => SeventyPercent,
                 GuestStatus.CitizenStudent => FiftyPercent,
                 GuestStatus.CitizenAdult => TwentyPercent,
                 GuestStatus.CitizenPensioner => HundredPercent,
                 GuestStatus.NonCitizen => ZeroPercent,
                 _ => ZeroPercent,
             };




    }



}
