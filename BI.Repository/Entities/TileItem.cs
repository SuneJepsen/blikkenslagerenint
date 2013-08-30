using System;
using umbraco;
using umbraco.NodeFactory;
using umbraco.cms.businesslogic.media;
using umbraco.interfaces;


namespace BI.Repository.Entities 
{
    public class TileItem: NodeItem
    {
        public TileItem()
        {
        }
        public TileItem(string nodeId) : this(int.Parse(nodeId))
        {
        }

        public TileItem(int nodeId) : this(new Node(nodeId))
        {
        }

        public TileItem(INode node)
            : base(node)
        {
            Header = Node.GetProperty<string>("header");
            Description = Node.GetProperty<string>("description");
            BodyText1 = Node.GetProperty<string>("bodyText1");
            BodyText2 = Node.GetProperty<string>("bodyText2");

            int mediaId = Node.GetProperty<int>("pageImage");
            if (mediaId != 0)
            {
                PictureUrl = new Media(mediaId).GetImageUrl();
            }

            PublishDate = Node.GetProperty<DateTime>("publishDate");
            if (PublishDate == DateTime.MinValue)
                PublishDate = Node.CreateDate;

            Url = library.NiceUrl(Node.Id);
            Link = NodeRepository.GetLink(Node.GetProperty<string>("link"));
            Image = CreateImageItem(Node.GetProperty<string>("pageImage"));

        
        }
        public ImageItem Image { get; set; }

        public string Header { get; set; }
        public string Description { get; set; }
        public string Abstract { get; set; }
        public string PictureUrl { get; set; }
        public DateTime PublishDate { get; set; }
        public string Url { get; set; }

        public string BodyText1 { get; set; }
        public string BodyText2 { get; set; }
        public Link Link { get; set; }

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
