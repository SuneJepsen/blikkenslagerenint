using System;
using umbraco;
using umbraco.NodeFactory;
using umbraco.cms.businesslogic.media;
using umbraco.interfaces;


namespace BI.Repository.Entities 
{
    public class FrontpageItem: NodeItem
    {
        public FrontpageItem(string nodeId) : this(int.Parse(nodeId))
        {
        }

        public FrontpageItem(int nodeId) : this(new Node(nodeId))
        {
        }

        public FrontpageItem(INode node)
            : base(node)
        {

            
            TextpagePlaceholder = GetTextPlaceholder();
            PageImage = CreateImageItem(Node.GetProperty<string>("pageImage"));
            TileImage = CreateImageItem(Node.GetProperty<string>("tileImage"));
            BodyText1 = Node.GetProperty<string>("bodyText1");
            BodyText2 = Node.GetProperty<string>("bodyText2");
            Manchet = Node.GetProperty<string>("manchet");
            Description = Node.GetProperty<string>("description");
            LinkText = Node.GetProperty<string>("linkText");
            HeaderSmall = Node.GetProperty<string>("headerSmall");
            Header = Node.GetProperty<string>("header");
            Url = Node.NiceUrl;;
            LinkIntern = NodeRepository.GetLink(Node);
            LinkExtern = NodeRepository.GetLink(Node.GetProperty<string>("link"));

            if (string.IsNullOrWhiteSpace(HeaderSmall) || string.IsNullOrEmpty(HeaderSmall))
                HeaderSmall = Header;
            if (string.IsNullOrWhiteSpace(HeaderSmall))
                HeaderSmall = Node.Name;


            Carbrands = Node.GetProperty<string>("carBrands");
            TopText = Node.GetProperty<string>("mtopText");
            BottomText = Node.GetProperty<string>("mbottomText");
            Theme = Node.GetProperty<string>("theme");
            if (string.IsNullOrEmpty(Theme))
                Theme = "b";

            CompanyGuid = Node.GetProperty<string>("companyGuid");
            IsCompanyLevel = Node.GetProperty<bool>("isCompanyLevel");
        }
        public string BodyText1 { get; set; }
        public string BodyText2 { get; set; }
        public string Manchet { get; set; }
        public string Description { get; set; }
        public string LinkText { get; set; }
        public string HeaderSmall { get; set; }
        public string Header { get; set; }
        public string SubTilesCsv { get; set; }
        public Link LinkIntern { get; set; }
        public Link LinkExtern { get; set; }
        public string Carbrands { get; set; }
        public string Theme { get; set; }
        public string TopText { get; set; }
        public string BottomText { get; set; }
        public string CompanyGuid { get; set; }
        public bool IsCompanyLevel { get; set; }


        public UmbracoEnum.TextpagePlaceholder TextpagePlaceholder { get; set; }
        public ImageItem PageImage { get; set; }
        public ImageItem TileImage { get; set; }
        public string Url { get; set; }


        private UmbracoEnum.TextpagePlaceholder GetTextPlaceholder()
        {
            int textplaceHolder = Node.GetProperty<int>("toogleButtonSitelinks");
            if (textplaceHolder == 0)
            {
                return UmbracoEnum.TextpagePlaceholder.SecondHalf;
            }
            else
            {
                return UmbracoEnum.TextpagePlaceholder.FirstHalf;

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
