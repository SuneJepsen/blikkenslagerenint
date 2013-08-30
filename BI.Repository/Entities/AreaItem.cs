using System;
using umbraco;
using umbraco.NodeFactory;
using umbraco.cms.businesslogic.media;
using umbraco.interfaces;


namespace BI.Repository.Entities 
{
    public class AreaItem: NodeItem
    {
        public AreaItem(string nodeId) : this(int.Parse(nodeId))
        {
        }

        public AreaItem(int nodeId) : this(new Node(nodeId))
        {
        }

        public AreaItem(INode node)
            : base(node)
        {
            Header = Node.GetProperty<string>("header");
            HeaderSmall = Node.GetProperty<string>("headerSmall");
            HeaderCombobox = Node.GetProperty<string>("headerContactbox");


            if (string.IsNullOrWhiteSpace(HeaderSmall) || string.IsNullOrEmpty(HeaderSmall))
                HeaderSmall = Header;
            if (string.IsNullOrWhiteSpace(HeaderSmall))
                HeaderSmall = Node.Name;

            PictureUrl = CreatePictureUrl(Node.GetProperty<string>("tileImage"));
            Link = NodeRepository.GetLink(Node.GetProperty<string>("link"), Node);
            NodeTypeAlias = UmbracoEnum.GetDocType(node);

            ContactEmail = Node.GetProperty<string>("areaEmail");

        }

        public string Header { get; set; }
        public string HeaderSmall { get; set; }
        public string HeaderCombobox { get; set; }
        public string ContactEmail { get; set; }
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
