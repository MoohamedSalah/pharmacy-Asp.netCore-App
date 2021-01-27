namespace WebApplication2Core.ViewModel
{
    public class ProductUpdateViewModel : ProductCreateViewModel
    {
        public int Id { get; set; }
        public string ExistingPhotoPath { get; set; }
    }
}
