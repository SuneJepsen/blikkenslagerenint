using System;
using System.Web;
using umbraco;
using umbraco.NodeFactory;
using umbraco.cms.businesslogic.media;
using umbraco.interfaces;

namespace BI.Repository.Entities
{
    public class NewsItem : NodeItem
    {
        public NewsItem(string nodeId) : this(int.Parse(nodeId))
        {
        }

        public NewsItem(int nodeId) : this(new Node(nodeId))
        {
        }

        public NewsItem(INode node) : base(node)
        {
            Header = Node.GetProperty<string>("header");
            Description = new HtmlString(Node.GetProperty<string>("description"));
            ShortDecription = Node.GetProperty<string>("textpageShortDecription");

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
            //Link = NodeRepository.GetLink(Node.GetProperty<string>("link"));
            Image = CreateImageItem(Node.GetProperty<string>("pageImage"));
            OwnSinglePage = Node.GetProperty<bool>("ownSinglePage");



            Link = Node.GetProperty<string>("link");
            if (string.IsNullOrEmpty(Link))
                Link = base.Url; 

        }
        public ImageItem Image { get; set; }

        public string Header { get; set; }
        public HtmlString Description { get; set; }
        public string ShortDecription { get; set; }
        public string Abstract { get; set; }
        public string PictureUrl { get; set; }
        public DateTime PublishDate { get; set; }
        public string Url { get; set; }
        public bool OwnSinglePage { get; set; }

        public string BodyText1 { get; set; }
        public string BodyText2 { get; set; }
        public string Link { get; set; }

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