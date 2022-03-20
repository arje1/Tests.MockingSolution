using ArtGallery.Model;

namespace ArtGallery.Implementations
{
    public class Validator : IValidator
    {
        public string? Status { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public ValidationStatus? ValidationStatus { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public bool IsValidPrice(double price)
        {
            throw new NotImplementedException();
        }

        public void IsValidPrice(double price, out bool isValid)
        {
            throw new NotImplementedException();
        }
    }
}
