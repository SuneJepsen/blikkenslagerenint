using System;
using umbraco;
using umbraco.NodeFactory;
using umbraco.cms.businesslogic.media;
using umbraco.interfaces;


namespace BI.Repository.Entities 
{
    public class Employee: NodeItem
    {
        public Employee(string nodeId) : this(int.Parse(nodeId))
        {
        }

        public Employee(int nodeId) : this(new Node(nodeId))
        {
        }

        public Employee(INode node)
            : base(node)
        {
            Name = Node.GetProperty<string>("name");

            if (string.IsNullOrWhiteSpace(Name) || string.IsNullOrEmpty(Name))
                Name = Node.Name;


            Title = Node.GetProperty<string>("title");
            Street = Node.GetProperty<string>("street");
            ZipCode = Node.GetProperty<string>("zipCode");
            City = Node.GetProperty<string>("city");
            Email = Node.GetProperty<string>("email");
            Phone = Node.GetProperty<string>("phone");
            Mobile = Node.GetProperty<string>("mobile");
            Motto1 = Node.GetProperty<string>("topText1");
            Motto2 = Node.GetProperty<string>("topText2");
            PictureUrl = CreatePictureUrl(Node.GetProperty<string>("image"));


            NodeTypeAlias = UmbracoEnum.GetDocType(node);

            Image = CreateImageItem(Node.GetProperty<string>("image"));

        }

        public string Name { get; set; } 
        public string Title { get; set; }
        public string Street { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Motto1 { get; set; }
        public string Motto2 { get; set; }
        public string Mobile { get; set; }
        public UmbracoEnum.DocumentTypeAlias NodeTypeAlias { get; set; }
        public Link Link { get; set; }
        public ImageItem Image { get; set; }


        public string PictureUrl { get; set; }

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
