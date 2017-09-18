namespace VinCLAPP.Model
{
    public class SelectListItem
    {
        public string Text { get; set; }
        public string Value { get; set; }
        public bool Selected { get; set; }
    }

    public class SelectDetailListItem : SelectListItem
    {
        public string Description { get; set; }
    }

    public interface ISelectedTrimItem
    {
        string SelectedTrimItem { get; set; }
    }

    public class PostClDriveTrain
    {
        public string Name { get; set; }

        public string TextValue { get; set; }

        public string ShortValue { get; set; }
    }
}