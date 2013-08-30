using BI.Repository.Entities;
using Eksponent.CropUp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using umbraco.interfaces;
using umbraco.NodeFactory;

namespace Bi.Umbraco.masterpages
{
    public partial class Contactpage : System.Web.UI.MasterPage
    {
        private readonly Node _currentNode = Node.GetCurrent();
        private TextItem _textItem;
        private TextpageItem _textpageItem;

        protected TextItem TextItem { get { return _textItem; } }
        protected TextpageItem TextpageItem { get { return _textpageItem; } }
        private StandardValueItem _standardValueItem;
        protected string ProductTypeClass;

        protected StandardValueItem StandardValueItem { get { return _standardValueItem; } }


        public Contactpage()
        {
            Init += PageInit;
        }
        private void PageInit(object sender, EventArgs e)
        {
            _standardValueItem = new StandardValueItem(_currentNode);

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            ucOpenLayers.Latitude = "55.38189";
            ucOpenLayers.Longitude = "10.390059";
            SetProductClass();

        }

        

        protected void SetProductClass()
        {
            switch (StandardValueItem.CustomDocType)
            {
                case StandardValueItem.CustomType.BiltorvetProduct:
                    ProductTypeClass = "biltorvetProduct";
                    break;
                case StandardValueItem.CustomType.AutodesktopProduct:
                    ProductTypeClass = "autodesktopProduct";
                    break;
                default:
                    ProductTypeClass = "biltorvetProduct";
                    break;
            }
        }




       

        private string CreateGalleryList(GalleryItem galleryItem)
        {
            if (galleryItem.Images.Count > 0)
            {
                string galleryListHTML = string.Empty
                + "<div id='textpageSlider'>"
                + "<div id='slider' class='flexslider'>"
                + "<ul class='slides'>";

                for (int i = 0; i < galleryItem.Images.Count; i++)
                {
                    galleryListHTML += "<li>"
                                    + RenderImage(galleryItem.Images[i], "galleryimage")
                                    + "</li>";
                }
                galleryListHTML += " </ul></div>  <div id='carousel' class='flexslider'> <ul class='slides'>";

                for (int i = 0; i < galleryItem.Images.Count; i++)
                {
                    galleryListHTML += "<li>"
                                    + RenderImage(galleryItem.Images[i], "gallerythumb")
                                    + "</li>";
                }
                galleryListHTML += " </ul></div> </div>";

                return galleryListHTML;
            }
            return "";

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