using System;
using umbraco;
using umbraco.NodeFactory;
using umbraco.cms.businesslogic.media;
using umbraco.interfaces;


namespace BI.Repository.Entities 
{
    public class TextpageItem: NodeItem
    {
        public TextpageItem(string nodeId) : this(int.Parse(nodeId))
        {
        }

        public TextpageItem(int nodeId) : this(new Node(nodeId))
        {
        }

        public TextpageItem(INode node)
            : base(node)
        {
            SiteLinks = Node.GetProperty<string>("siteLinks");
            FullWidthColumn = Node.GetProperty<bool>("hideSubmenu");
            TextpagePlaceholder = GetTextPlaceholder();
            ImageItem = CreateImageItem(Node.GetProperty<string>("pageImage"));

        }
        public string SiteLinks { get; set; }
        public UmbracoEnum.TextpagePlaceholder TextpagePlaceholder { get; set; }
        public ImageItem ImageItem { get; set; }
        public bool FullWidthColumn { get; set; }


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
