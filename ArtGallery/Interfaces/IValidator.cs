namespace ArtGallery
{
    public interface IValidator
    {
        
        public bool IsValidPrice(double price);

        public void IsValidPrice(double price, out bool isValid);

    }
}
