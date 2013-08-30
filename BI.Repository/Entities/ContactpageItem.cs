using System;
using umbraco;
using umbraco.NodeFactory;
using umbraco.cms.businesslogic.media;
using umbraco.interfaces;


namespace BI.Repository.Entities 
{
    public class ContactpageItem: NodeItem
    {
        public ContactpageItem(string nodeId) : this(int.Parse(nodeId))
        {
        }

        public ContactpageItem(int nodeId) : this(new Node(nodeId))
        {
        }

        public ContactpageItem(INode node)
            : base(node)
        {
            SiteLinks = Node.GetProperty<string>("siteLinks");
            Address = Node.GetProperty<string>("address");
            ZipCode = Node.GetProperty<string>("zipCode");
            Phone = Node.GetProperty<string>("phone");
            WebsiteUrl = Node.GetProperty<string>("websiteUrl");
            WebsiteName = Node.GetProperty<string>("websiteName");
            TextpagePlaceholder = GetTextPlaceholder();

        }
        public string SiteLinks { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }
        public string Phone { get; set; }
        public string WebsiteUrl { get; set; }
        public string WebsiteName { get; set; }
        public UmbracoEnum.TextpagePlaceholder TextpagePlaceholder { get; set; }
        public ImageItem ImageItem { get; set; }


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
