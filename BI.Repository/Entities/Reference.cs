using System;
using System.Web;
using umbraco;
using umbraco.NodeFactory;
using umbraco.cms.businesslogic.media;
using umbraco.interfaces;


namespace BI.Repository.Entities 
{
    public class Reference: NodeItem
    {
        public Reference(string nodeId) : this(int.Parse(nodeId))
        {
        }

        public Reference(int nodeId) : this(new Node(nodeId))
        {
        }

        public Reference(INode node)
            : base(node)
        {
            Name = Node.GetProperty<string>("name");

            if (string.IsNullOrWhiteSpace(Name) || string.IsNullOrEmpty(Name))
                Name = Node.Name;


            Title = Node.GetProperty<string>("title");
            BodyText1 = new HtmlString(Node.GetProperty<string>("bodyText1"));
            Link = Node.GetProperty<string>("link");
            NodeTypeAlias = UmbracoEnum.GetDocType(node);
            Image = CreateImageItem(Node.GetProperty<string>("image"));

        }

        public string Name { get; set; } 
        public string Title { get; set; }
        public HtmlString BodyText1 { get; set; }
        public string Link { get; set; }
        public UmbracoEnum.DocumentTypeAlias NodeTypeAlias { get; set; }
        public ImageItem Image { get; set; }



        private string CreatePictureUrl(string mediaId)
        {
            int id;
            if (string.IsNullOrWhiteSpace(mediaId) || !int.TryParse(mediaId, out id))
            {
                return "/images/persona/no-image.png";
            }
            var media = new Media(id);
            return media.GetImageUrl();
        }
        private ImageItem CreateImageItem(string mediaId)
        {
            int id;
            if (string.IsNullOrWhiteSpace(mediaId) || !int.TryParse(mediaId, out id))
            {
                return new ImageItem();
            }
            Media media = new Media(id);
            return new ImageItem(media);
        }
       
       
    }
}
