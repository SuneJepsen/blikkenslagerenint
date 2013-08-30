using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using umbraco;
using umbraco.NodeFactory;
using umbraco.cms.businesslogic.media;
using umbraco.interfaces;

namespace BI.Repository.Entities
{
    public class Link
    {
        public Link(XElement linkElement)
        {
            if (linkElement.Attribute("mode").Value == "Content")
            {
                string nodeid = linkElement.Element("node-id").Value;
                if(!string.IsNullOrEmpty(nodeid))
                    Url = library.NiceUrl(int.Parse(linkElement.Element("node-id").Value));
            }
            else
            {
                Url = linkElement.Element("url").Value;
            }
            Caption = linkElement.Element("link-title").Value;
            NewWindow = linkElement.Element("new-window").Value == "False" ? false : true;

        }
        public Link(XElement linkElement, Node node)
        {
            if (linkElement.Attribute("mode").Value == "Content")
            {
                Url = library.NiceUrl(int.Parse(linkElement.Element("node-id").Value));
            }
            else
            {
                Url = linkElement.Element("url").Value;
            }
            Caption = linkElement.Element("link-title").Value;
            NewWindow = linkElement.Element("new-window").Value == "False" ? false : true;
            LinkText = node.GetProperty<string>("linkText");

        }
        public Link(Node node)
        {
            Url = library.NiceUrl(node.Id);
            Caption = !string.IsNullOrEmpty(node.GetProperty<string>("header")) ? node.GetProperty<string>("headerSmall") : node.GetProperty<string>("header");
            LinkText= node.GetProperty<string>("linkText");

        }
        

        public string Url { get; set; }
        public string Caption { get; set; }
        public string LinkText { get; set; }
        public bool NewWindow { get; set; }

    }
}
