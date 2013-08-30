using System.Collections.Generic;
using Eksponent.CropUp;
using BI.Repository;
using BI.Repository.Entities;
using System;
using System.Web;
using Umbraco.Core;
using umbraco.NodeFactory;
using BI.Umbraco.helpers;
using umbraco.interfaces;
using Umbraco.Web;


namespace BI.Umbraco.masterpages
{
    public partial class Master : System.Web.UI.MasterPage
    {
        #region Nested type: ViewModel

        public class ViewModel
        {
            public string Title { get; set; }
            public string Description { get; set; }
            public string Keywords { get; set; }
            public string HeaderStyle { get; set; }
        }

        #endregion
        public ViewModel PageData { get; set; }
        private readonly Node _currentNode = Node.GetCurrent();
        private StandardValueItem _standardValueItem;

        protected StandardValueItem StandardValueItem { get { return _standardValueItem; } }


        public Master()
        {
            Init += PageInit;
        }
        private void PageInit(object sender, EventArgs e)
        {

            string title = _currentNode.GetPropertyRecursiveAsString("metaTitle");
            if (string.IsNullOrWhiteSpace(title))
                title = _currentNode.GetPropertyRecursiveAsString("header");

            string description = _currentNode.GetPropertyRecursiveAsString("metaDescription");
            string keywords = _currentNode.GetPropertyRecursiveAsString("metaKeywords");

            //INode node = NodeRepository.GetWebsiteFrontpageNode();
            //litFooter.Text = node.GetProperty<string>("bodyText1");
            PageData = new ViewModel
            {
                Title = title,
                Description = description,
                Keywords = keywords,
            };
            litTitle.Text = PageData.Title;

            litMetaTags.Text = string.Empty
                    + " <meta name=\"description\" content=\"" + PageData.Description + "\">"
                    + " <meta name=\"author\" content=\"" + PageData.Keywords + "\">";

            //_standardValueItem = new StandardValueItem(_currentNode);


        }

        private void Redirect()
        {
            if (_standardValueItem != null)
            {
                if (_standardValueItem.Link != null && !string.IsNullOrEmpty(_standardValueItem.Link))
                {
                    Response.Redirect(_standardValueItem.Link);
                }
            
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        

            PageData = new ViewModel();

            //Redirect ved f.eks eksterne indgående links udefra
            Redirect();

     

        }

      
        public string GetFullRootUrl()
        {
            HttpRequest request = HttpContext.Current.Request;
            return request.Url.AbsoluteUri.Replace(request.Url.AbsolutePath, String.Empty);
        }
        protected void lnkSearch_Click(object sender, EventArgs e)
        {
            //Response.Redirect("/soeg.aspx?key=" + txtSearchTerm.Text);
        }
        public void SetFormPostBackUrl(string url)
        {
            //Form1.Action = url;
        }
       
    }
}