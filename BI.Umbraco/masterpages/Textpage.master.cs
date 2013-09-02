using BI.Repository;
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

namespace BI.Umbraco.masterpages
{
    public partial class Textpage : System.Web.UI.MasterPage
    {
         private readonly Node _currentNode = Node.GetCurrent();
        private StandardValueItem _standardValueItem;

        protected StandardValueItem StandardValueItem { get { return _standardValueItem; } }
        private FileItem _fileItem;

        protected FileItem FileItem { get { return _fileItem; } }

        public Textpage()
        {
           Init += PageInit;
           PreRender += PagePreRender;
            
        }

        private void PagePreRender(object sender, EventArgs e)
        {
            //Tabspane.MacroAttributes.Add("pagevalue", "textpage");
            //Tabspane.Attributes.Add("pagevalue", "textpage");
            //Tabspane.Attributes["pagevalue"] = "textpage";

        }
        private void PageInit(object sender, EventArgs e)
        {
            _standardValueItem = new StandardValueItem(_currentNode);
            _fileItem = new FileItem(_currentNode);

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            //Tabspane.MacroAttributes.Add("pagevalue", "textpage");
            //Tabspane.Attributes.Add("pagevalue", "textpage");
            //Tabspane.Attributes["pagevalue"] = "textpage";

       
        }

      

      

      
        protected string RenderImage()
        {
            string img = @"<figure><img src=""{0}"" alt=""{1}"" /></figure>";
            string picUrl;
            if (!string.IsNullOrEmpty(_standardValueItem.PageImage.PictureUrl))
            {
                picUrl = CropUp.GetUrl(_standardValueItem.PageImage.PictureUrl, new ImageSizeArguments { CropAlias = "news" });
                return string.Format(img, picUrl, _standardValueItem.PageImage.AltText);
            }
            return string.Empty;
        }

        protected string GetFile()
        {
            string filePath = FileItem.GetFile();
            if (!string.IsNullOrEmpty(filePath))
            {
                return "<h6> <a href=" + filePath + " class='btn btn-very-subtle'>" + FileItem.FileText + "</a> </h6>";
            }
            return "";
        }

    
    }
}