using System.Collections.Generic;

namespace ImageRename.Ca
{
    public class ReportModel
    {
        public ReportModel()
        {
            Images = new List<Image>();
        }

        public string FolderName { get; set; }
        public List<Image> Images { get; set; }
    }

    public class Image
    {
        public string BeforeName { get; set; }
        public string AfterName { get; set; }
    }
}
