using System;
using umbraco;
using umbraco.NodeFactory;
using umbraco.cms.businesslogic.media;
using umbraco.interfaces;


namespace BI.Repository.Entities 
{
    public class ShareholderItem: NodeItem
    {
        public ShareholderItem(string nodeId) : this(int.Parse(nodeId))
        {
        }

        public ShareholderItem(int nodeId) : this(new Node(nodeId))
        {
        }

        public ShareholderItem(INode node)
            : base(node)
        {
            BodyText1 = Node.GetProperty<string>("bodyText1");
            BodyText2 = Node.GetProperty<string>("bodyText2");
            Manchet = Node.GetProperty<string>("manchet");


        }
        public string BodyText1 { get; set; }
        public string BodyText2 { get; set; }
        public string Manchet { get; set; }
    }
}
