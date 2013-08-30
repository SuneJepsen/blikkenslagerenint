using System.Collections.Generic;
using System.Linq;
using umbraco;
using umbraco.NodeFactory;
using umbraco.cms.businesslogic.media;
using umbraco.interfaces;

namespace BI.Repository.Entities
{
    public class NavigationItem : NodeItem
    {
        private readonly int _currentId;

        public NavigationItem() : base()
        {
        }

        public NavigationItem(int id, int currentId) : this(new Node(id), currentId)
        {
        }

        public NavigationItem(INode node, int currentId) : base(node)
        {
            _currentId = currentId;
            switch (NodeType)
            {
                default:
                    NavText = Node.GetProperty<string>("naviText");

                    if (string.IsNullOrWhiteSpace(NavText) || string.IsNullOrEmpty(NavText))
                        NavText = Node.Name;
                    if (string.IsNullOrWhiteSpace(NavText))
                        NavText = Node.GetProperty<string>("header");
                    Description = Node.GetProperty<string>("description");
                    var mediaIdString = Node.GetProperty<string>("pagePicture");
                    int mediaId; 
                    if (int.TryParse(mediaIdString, out mediaId) && mediaId != 0)
                    {
                        var media = new Media(mediaId);
                        PictureUrl = media.GetImageUrl();
                    }
                    Class = Node.Id == _currentId ? "active" : "";

                    Link = Node.GetProperty<string>("link");
                    if (string.IsNullOrEmpty(Link))
                        Link = base.Url;


                    break;
            }
        }

        public string NavText { get; set; }
        public string Description { get; set; }
        public string PictureUrl { get; set; }
        public string Class { get; set; }
        public bool IsFirst { get; set; }
        public bool IsLast { get; set; }
        public string Link { get; set; }

        public bool HasSubmenu
        {
            get { return Node.ChildrenAsList.Any(n => ((Node) n).GetProperty<bool>("umbracoNaviHide") != true); }
        }
   


    }
}