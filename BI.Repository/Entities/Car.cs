using System;
using umbraco;
using umbraco.NodeFactory;
using umbraco.cms.businesslogic.media;
using umbraco.interfaces;


namespace BI.Repository.Entities 
{
    public class Car: NodeItem
    {
        public Car(string nodeId) : this(int.Parse(nodeId))
        {
        }

        public Car(int nodeId) : this(new Node(nodeId))
        {
        }

        public Car(INode node)
            : base(node)
        {
            Header = Node.GetProperty<string>("header");
            Description = Node.GetProperty<string>("price");
            PictureUrl = CreatePictureUrl(Node.GetProperty<string>("tileImage"));
            Link = NodeRepository.GetLink(Node.GetProperty<string>("link"), Node);
            NodeTypeAlias = UmbracoEnum.GetDocType(node);

        }

        public string Header { get; set; }
        public string Description { get; set; }
        public Link Link { get; set; }
        public UmbracoEnum.DocumentTypeAlias NodeTypeAlias { get; set; }

        public string PictureUrl { get; set; }

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
