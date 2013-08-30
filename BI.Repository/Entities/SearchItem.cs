using System;
using umbraco;
using umbraco.NodeFactory;
using umbraco.cms.businesslogic.media;
using umbraco.interfaces;

namespace BI.Repository.Entities
{
    public class SearchItem : NodeItem
    {
        public SearchItem(string nodeId) : this(int.Parse(nodeId))
        {
        }

        public SearchItem(int nodeId) : this(new Node(nodeId))
        {
        }

        public SearchItem(INode node)
            : base(node)
        {
            Header = Node.GetProperty<string>("header");
            HeaderSmall = Node.GetProperty<string>("headerSmall");
            Description = Node.GetProperty<string>("description");
            PictureUrl = CreatePictureUrl(Node.GetProperty<string>("tileImage"));
            LinkIntern = NodeRepository.GetLink(Node);
            LinkExtern = NodeRepository.GetLink(Node.GetProperty<string>("link"));

            NodeTypeAlias = UmbracoEnum.GetDocType(node);
        }

        public string Header { get; set; }
        public string HeaderSmall { get; set; }
        public string Description { get; set; }
        public Link LinkIntern { get; set; }
        public Link LinkExtern { get; set; }
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