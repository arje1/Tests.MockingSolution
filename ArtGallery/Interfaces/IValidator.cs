namespace ArtGallery
{
    public interface IValidator
    {
        public string? Status { get; set; }

        public bool IsValidPrice(double price);

        public void IsValidPrice(double price, out bool isValid);

    }
}
