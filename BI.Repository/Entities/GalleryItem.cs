using System;
using umbraco;
using umbraco.NodeFactory;
using umbraco.cms.businesslogic.media;
using umbraco.interfaces;
using System.Collections.Generic;


namespace BI.Repository.Entities 
{
    public class GalleryItem: NodeItem
    {
        public GalleryItem(string nodeId) : this(int.Parse(nodeId))
        {
        }

        public GalleryItem(int nodeId) : this(new Node(nodeId))
        {
        }

        public GalleryItem(INode node)
            : base(node)
        {
            //Images = NodeRepository.GetGalleryList(Node.GetProperty<string>("gallery"));
            TextpagePlaceholder = GetTextPlaceholder();

        }

        public List<ImageItem> Images { get; set; }
        public UmbracoEnum.TextpagePlaceholder TextpagePlaceholder { get; set; }

        private UmbracoEnum.TextpagePlaceholder GetTextPlaceholder()
        {
            int textplaceHolder = Node.GetProperty<int>("toggleButtonGallery");
            if (textplaceHolder == 0)
            {
                return  UmbracoEnum.TextpagePlaceholder.SecondHalf;
            }
            else
            {
                return UmbracoEnum.TextpagePlaceholder.FirstHalf;

            }
        }
       
    }
}
