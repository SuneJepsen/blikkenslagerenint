using System;
using System.Web;
using umbraco;
using umbraco.NodeFactory;
using umbraco.cms.businesslogic.media;
using umbraco.interfaces;


namespace BI.Repository.Entities 
{
    public class Category: NodeItem
    {
        public Category(string nodeId) : this(int.Parse(nodeId))
        {
        }

        public Category(int nodeId) : this(new Node(nodeId))
        {
        }

        public Category(INode node)
            : base(node)
        {
            Header = Node.GetProperty<string>("header");

            if (string.IsNullOrEmpty(Header))
                Header = Node.Name;



        }

        public string Header { get; set; }


       
       
    }
}
