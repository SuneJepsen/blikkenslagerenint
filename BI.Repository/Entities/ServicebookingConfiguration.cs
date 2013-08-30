using System;
using umbraco;
using umbraco.NodeFactory;
using umbraco.cms.businesslogic.media;
using umbraco.interfaces;


namespace BI.Repository.Entities 
{
    public class ServicebookingConfiguration: NodeItem
    {
        public ServicebookingConfiguration(string nodeId) : this(int.Parse(nodeId))
        {
        }

        public ServicebookingConfiguration(int nodeId) : this(new Node(nodeId))
        {
            ServicebookingEmail = Node.GetProperty<string>("servicebookingEmail");
            ServicebookingEmailCc = Node.GetProperty<string>("servicebookingEmailCc");
            Signature = Node.GetProperty<string>("signature");
            MailPrefix = Node.GetProperty<string>("mailPrefix");
            if (string.IsNullOrEmpty(MailPrefix))
                MailPrefix = "autoit";

        }

        public ServicebookingConfiguration(INode node)
            : base(node)
        {
        }

        public string ServicebookingEmail { get; set; }
        public string ServicebookingEmailCc { get; set; }
        public string Signature { get; set; }
        public string MailPrefix { get; set; }

       
       
    }
}
