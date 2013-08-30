using System.Web;
using umbraco;
using umbraco.NodeFactory;
using umbraco.cms.businesslogic.media;
using umbraco.interfaces;


namespace BI.Repository.Entities 
{
    public class ContentItem: NodeItem
    {
        public ContentItem(string nodeId) : this(int.Parse(nodeId))
        {
        }

        public ContentItem(int nodeId) : this(new Node(nodeId))
        {
        }

        public ContentItem(INode node)
            : base(node)
        {
            Header = Node.GetProperty<string>("header");
            BodyText1 = new HtmlString(Node.GetProperty<string>("bodyText1"));
            BodyText2 = new HtmlString(Node.GetProperty<string>("bodyText2"));
            BodyText3 = new HtmlString(Node.GetProperty<string>("bodyText3"));
            Manchet = new HtmlString(Node.GetProperty<string>("manchet"));
            Icon1 = new HtmlString(Node.GetProperty<string>("icon1"));
            Icon2 = new HtmlString(Node.GetProperty<string>("icon2"));
            Icon3 = new HtmlString(Node.GetProperty<string>("icon3"));
            RecipientEmail = Node.GetProperty<string>("recipientEmail");

            FirmName = Node.GetProperty<string>("firmName");
            Address = Node.GetProperty<string>("address");
            ZipCode = Node.GetProperty<string>("zipCode");
            Phone = Node.GetProperty<string>("phone");

            NodeTypeAlias = UmbracoEnum.GetDocType(node);
            if (string.IsNullOrWhiteSpace(Header) || string.IsNullOrEmpty(Header))
                Header = Node.Name;

            WebsiteUrl = Node.GetProperty<string>("websiteUrl");
            WebsiteName = Node.GetProperty<string>("websiteName");

        }
        public IHtmlString BodyText1 { get; set; }
        public IHtmlString BodyText2 { get; set; }
        public IHtmlString BodyText3 { get; set; }
        public IHtmlString Manchet { get; set; }
        public IHtmlString Icon1 { get; set; }
        public IHtmlString Icon2 { get; set; }
        public IHtmlString Icon3 { get; set; }
        public string Header { get; set; }
        public string RecipientEmail { get; set; }
        public UmbracoEnum.DocumentTypeAlias NodeTypeAlias { get; set; }

        public string Address { get; set; }
        public string ZipCode { get; set; }
        public string Phone { get; set; }
        public string FirmName { get; set; }

        public string WebsiteUrl { get; set; }
        public string WebsiteName { get; set; }

    }
}
