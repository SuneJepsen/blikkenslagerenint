using System;
using System.Collections.Generic;
using System.Web;
using umbraco;
using umbraco.NodeFactory;
using umbraco.cms.businesslogic.media;
using umbraco.interfaces;
using System.Linq;


namespace BI.Repository.Entities 
{
    public class PortfolioItem: NodeItem
    {
        public PortfolioItem(string nodeId) : this(int.Parse(nodeId))
        {
        }

        public PortfolioItem(int nodeId) : this(new Node(nodeId))
        {
        }

        public PortfolioItem(INode node)
            : base(node)
        {
            Header = Node.GetProperty<string>("header");
            Description = new HtmlString(Node.GetProperty<string>("description"));
            BodyText = new HtmlString(Node.GetProperty<string>("bodyText"));

            if (string.IsNullOrEmpty(Header))
                Header = Node.Name;

            PictureUrl = CreatePictureUrl(Node.GetProperty<string>("pageImage"));
            OwnSinglePage = Node.GetProperty<bool>("ownSinglePage");
            GetCategories();

        }

        public string Header { get; set; }
        public HtmlString Description { get; set; }
        public HtmlString BodyText { get; set; }
        public List<Category> Categories { get; set; }
        public string CategoriesString { get; set; }
        public string CategoriesStringComma { get; set; }
        public bool OwnSinglePage { get; set; }


        public string PictureUrl { get; set; }
        private string GetCategories()
        {
            Categories = NodeRepository.GetCategoryList(Node.GetProperty<string>("categories"));
            foreach (var item in Categories)
            {
                CategoriesString += item.UrlName + " ";
                CategoriesStringComma += item.NodeName + ", ";

            }
            if(!string.IsNullOrEmpty(CategoriesStringComma))
                CategoriesStringComma = CategoriesStringComma.TrimEnd(',');


            return "";
        }

        public List<ImageItem> GetMediaItems()
        {
            return uQuery.GetMediaByCsv(Node.GetProperty<string>("portfolioGallery"))
                 .Select(n => new ImageItem(n) )
                 .ToList();

        }
        private string CreatePictureUrl(string mediaId)
        {
            int id;
            if (string.IsNullOrWhiteSpace(mediaId) || !int.TryParse(mediaId, out id))
            {
                //return "/images/no-picture.jpg";
                return "";
            }
            var media = new Media(id);
            return media.GetImageUrl();
        }

       
       
    }
}
