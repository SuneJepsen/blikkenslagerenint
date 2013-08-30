using umbraco.NodeFactory;
using umbraco.interfaces;
using System;

namespace BI.Repository.Entities
{
    public abstract class NodeItem
    {
        protected NodeItem()
        {
        }

        protected NodeItem(INode node)
        {
            Id = node.Id;
            NodeName = node.Name;
            NodeType = node.NodeTypeAlias;
            Url = node.Url;
            UrlName = node.UrlName;
            CreatedDate = node.CreateDate;
            Node = (Node) node;
            
        }

        public int Id { get; set; }
        public string NodeName { get; set; }
        public string NodeType { get; set; }
        public string Url { get; set; }
        public string UrlName { get; set; }
        public DateTime CreatedDate { get; set; }
        public Node Node { get; set; }
    }
}