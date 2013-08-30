using System;
using umbraco;
using umbraco.NodeFactory;
using umbraco.cms.businesslogic.media;
using umbraco.interfaces;


namespace BI.Repository.Entities 
{
    public class DepartmentItem : NodeItem
    {
        public DepartmentItem()
        {
      
        }

        public DepartmentItem(string nodeId) : this(int.Parse(nodeId))
        {
        }

        public DepartmentItem(int nodeId) : this(new Node(nodeId))
        {
        }

        public DepartmentItem(INode node)
            : base(node)
        {
            Department = Node.GetProperty<string>("name");
            Address = Node.GetProperty<string>("address");
            Other = Node.GetProperty<string>("other");


            OpeningHours = Node.GetProperty<string>("openingHours");
            GeoCoords = Node.GetProperty<string>("geoInfo");

            if (!string.IsNullOrEmpty(Node.GetProperty<string>("employeeImage")))
                Employee = NodeRepository.GetEmployee(Node.GetProperty<string>("employeeImage"));

            DepartmentImage = CreatePictureUrl(Node.GetProperty<string>("departmentImage"));
            
            if (!string.IsNullOrEmpty(Node.GetProperty<string>("link")))
                Link = NodeRepository.GetLink(Node.GetProperty<string>("link"));
            Image = CreateImageItem(Node.GetProperty<string>("image"));

            EmailBookTryCar = Node.GetProperty<string>("emailBookTryCar");
            EmailServicebooking = Node.GetProperty<string>("emailServiceBooking");
        }

        public string Department { get; set; }
        public string EmailBookTryCar { get; set; }
        public string EmailServicebooking { get; set; }
        public string DepartmentImage { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }
        public string OpeningHours { get; set; }
        public string Other { get; set; }
        public string GeoCoords { get; set; }
        public Employee Employee { get; set; }
        public Link Link { get; set; }
        public ImageItem Image { get; set; }


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
