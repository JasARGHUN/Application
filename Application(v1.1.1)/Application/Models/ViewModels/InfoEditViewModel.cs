namespace Application.Models.ViewModels
{
    public class InfoEditViewModel : InfoCreateViewModel
    {
        public int Id { get; set; }
        public string ExistingImagePath { get; set; }
        public string ExistingSocialImagePath { get; set; }
        public string ExistingBackgroundImagePath { get; set; }
    }
}
