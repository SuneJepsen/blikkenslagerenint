using System;
using System.Web;
using umbraco;
using umbraco.NodeFactory;
using umbraco.cms.businesslogic.media;
using umbraco.interfaces;


namespace BI.Repository.Entities 
{
    public class SliderItem: NodeItem
    {

        public SliderItem(string nodeId) : this(int.Parse(nodeId))
        {
        }

        public SliderItem(int nodeId) : this(new Node(nodeId))
        {
        }

        public SliderItem(INode node)
            : base(node)
        {
            Header = Node.GetProperty<string>("header");
            Description = new HtmlString(Node.GetProperty<string>("description")); 
            Link = Node.GetProperty<string>("link");
            Image = CreateImageItem(Node.GetProperty<string>("image"));

            
        }

        public string Header { get; set; }
        public string Link { get; set; }
        public HtmlString Description { get; set; }

        public ImageItem Image { get; set; }

        private string CreatePictureUrl(string mediaId)
        {
            int id;
            if (string.IsNullOrWhiteSpace(mediaId) || !int.TryParse(mediaId, out id))
            {
                return "/images/no-picture.jpg";
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
