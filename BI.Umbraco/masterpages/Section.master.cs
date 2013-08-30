using BI.Repository.Entities;
using Eksponent.CropUp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using umbraco.NodeFactory;

namespace BI.Umbraco.masterpages
{
    public partial class Section : System.Web.UI.MasterPage
    {
        private readonly Node _currentNode = Node.GetCurrent();
        private StandardValueItem _standardValueItem;

        protected StandardValueItem StandardValueItem { get { return _standardValueItem; } }

        public Section()
        {
           Init += PageInit;
        }
        private void PageInit(object sender, EventArgs e)
        {
            _standardValueItem = new StandardValueItem(_currentNode);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
        }
        private void LoadTopImage()
        {
        }
        protected string RenderImage(ImageItem imageItem, string cropUpAlias)
        {
            string img = @"<img src=""{0}"" alt=""{1}"" />";
            string picUrl;
            if (!string.IsNullOrEmpty(imageItem.PictureUrl))
            {
                picUrl = CropUp.GetUrl(imageItem.PictureUrl, new ImageSizeArguments { CropAlias = cropUpAlias });
                return string.Format(img, picUrl, imageItem.AltText);
            }
            return string.Empty;
        }
    }
}