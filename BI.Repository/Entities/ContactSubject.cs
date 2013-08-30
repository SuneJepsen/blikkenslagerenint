using System;
using umbraco;
using umbraco.NodeFactory;
using umbraco.cms.businesslogic.media;
using umbraco.interfaces;


namespace BI.Repository.Entities 
{
    public class ContactSubject: NodeItem
    {
        public ContactSubject(string nodeId) : this(int.Parse(nodeId))
        {
        }

        public ContactSubject(int nodeId) : this(new Node(nodeId))
        {
        }

        public ContactSubject(INode node)
            : base(node)
        {
            Header = Node.GetProperty<string>("header");
            if (string.IsNullOrWhiteSpace(Header) || string.IsNullOrEmpty(Header))
                Header = Node.Name; ;
  
        }

        public string Header { get; set; }
        public UmbracoEnum.DocumentTypeAlias NodeTypeAlias { get; set; }


       
       
    }
}
