using System;
using System.Collections.Generic;
using System.Web;
using umbraco;
using umbraco.NodeFactory;
using umbraco.cms.businesslogic.media;
using umbraco.interfaces;
using System.Linq;


namespace BI.Repository.Entities 
{
    public class FileItem: NodeItem
    {
        public FileItem(string nodeId) : this(int.Parse(nodeId))
        {
        }

        public FileItem(int nodeId) : this(new Node(nodeId))
        {
        }

        public FileItem(INode node)
            : base(node)
        {
            FileText = Node.GetProperty<string>("fileText");


        }

        public string FileText { get; set; }



        public string GetFile()
        {
     

            string nodeId = Node.GetProperty<string>("file"); // get this from wherever!

            if (!string.IsNullOrEmpty(nodeId))
            {
                var media = new Media(int.Parse(nodeId));
                var file = media.getProperty("umbracoFile");
                return file.Value.ToString();
            }
            return "";


        }


       
       
    }
}
