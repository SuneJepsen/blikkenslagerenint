using System.Web;
using umbraco;
using umbraco.NodeFactory;
using umbraco.cms.businesslogic.media;
using umbraco.interfaces;


namespace BI.Repository.Entities 
{
    public class StandardValueItem: NodeItem
    {
        public enum CustomType {BiltorvetProduct, AutodesktopProduct}

        public StandardValueItem(string nodeId) : this(int.Parse(nodeId))
        {
        }

        public StandardValueItem(int nodeId) : this(new Node(nodeId))
        {
        }

        public StandardValueItem(INode node)
            : base(node)
        {

            Header = Node.GetProperty<string>("header");
            Description = new HtmlString(Node.GetProperty<string>("description"));
            BodyText = new HtmlString(Node.GetProperty<string>("bodyText"));

            //PageImage = CreateImageItem(Node.GetProperty<string>("pageImage"));
            //TopTeaserImage = CreateImageItem(Node.GetProperty<string>("topTeaserImage"));
            //TopTeaserImageText = new HtmlString(Node.GetProperty<string>("topTeaserImageText"));
            //BodyText2 = Node.GetProperty<string>("bodyText2");
            //Description = new HtmlString(Node.GetProperty<string>("description"));

            //HeroTitle = Node.GetProperty<string>("heroTitle");
            //HeaderSmall = Node.GetProperty<string>("headerSmall");
      
            
            //Link = Node.GetProperty<string>("link");
            //if (string.IsNullOrEmpty(Link))
            //    Link = base.Url; 
            
            //NodeTypeAlias = UmbracoEnum.GetDocType(node);

            //OwnSinglePage = Node.GetProperty<bool>("ownSinglePage");

            //if (string.IsNullOrWhiteSpace(HeaderSmall) || string.IsNullOrEmpty(HeaderSmall))
            //    HeaderSmall = Header;
            //if (string.IsNullOrWhiteSpace(HeaderSmall))
            //    HeaderSmall = Node.Name;

            if (string.IsNullOrEmpty(Header))
                Header = Node.Name;


            //if (string.IsNullOrEmpty(HeroTitle))
            //    HeroTitle = Header;

            //SetCustomDocType();
        }


        public string Header { get; set; }
        public HtmlString Description { get; set; }
        public HtmlString BodyText { get; set; }
        public string BodyText2 { get; set; }
        public HtmlString HeroDescription { get; set; }
        public HtmlString TopTeaserImageText { get; set; }
        public CustomType CustomDocType { get; set; }
        public string HeroTitle { get; set; }
        public string HeaderSmall { get; set; }
        public string Link { get; set; }
        public bool OwnSinglePage { get; set; }


        public ImageItem PageImage { get; set; }
        public ImageItem TopTeaserImage { get; set; }
        public string Url { get; set; }
        public UmbracoEnum.DocumentTypeAlias NodeTypeAlias { get; set; }

        private void SetCustomDocType()
        {
            string custType  = Node.GetProperty<string>("customType");
            switch (custType)
            {
                case "biltorvetProduct":
                    CustomDocType = StandardValueItem.CustomType.BiltorvetProduct;
                    break;
                case "autodesktopProduct":
                    CustomDocType = StandardValueItem.CustomType.AutodesktopProduct;
                    break;
                default:
                    CustomDocType = StandardValueItem.CustomType.BiltorvetProduct;
                    break;
            }
        }

        private ImageItem CreateImageItem(string mediaId)
        {
            int id;
            if (string.IsNullOrWhiteSpace(mediaId) || !int.TryParse(mediaId, out id))
            {
                return new ImageItem();
            }
            Media  media = new Media(id);
            return new ImageItem(media);
        }
    }
}
