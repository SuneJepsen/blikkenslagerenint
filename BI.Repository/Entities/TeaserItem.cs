using System;
using System.Web;
using umbraco;
using umbraco.NodeFactory;
using umbraco.cms.businesslogic.media;
using umbraco.interfaces;


namespace BI.Repository.Entities 
{
    public class TeaserItem: NodeItem
    {
        public TeaserItem(string nodeId) : this(int.Parse(nodeId))
        {
        }

        public TeaserItem(int nodeId) : this(new Node(nodeId))
        {
        }

        public TeaserItem(INode node)
            : base(node)
        {
            Title = Node.GetProperty<string>("title");
            TitleLong = Node.GetProperty<string>("titleLong");
            Link = Node.GetProperty<string>("link");
            Manchet = new HtmlString(Node.GetProperty<string>("manchet"));
            Description = new HtmlString(Node.GetProperty<string>("description"));
            Icon = new HtmlString(Node.GetProperty<string>("icon"));

        }
        public string Title { get; set; }
        public string TitleLong { get; set; }
        public HtmlString Manchet { get; set; }
        public HtmlString Description { get; set; }
        public HtmlString Icon { get; set; }
        public string Link { get; set; }
    }
}
