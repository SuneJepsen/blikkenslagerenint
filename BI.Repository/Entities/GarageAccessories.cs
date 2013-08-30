using System;
using umbraco;
using umbraco.NodeFactory;
using umbraco.cms.businesslogic.media;
using umbraco.interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;


namespace BI.Repository.Entities 
{
    public class GarageAccessories: NodeItem
    {
        public GarageAccessories(string nodeId) : this(int.Parse(nodeId))
        {
        }

        public GarageAccessories(int nodeId) : this(new Node(nodeId))
        {
        }

        public GarageAccessories(INode node)
            : base(node)
        {
            Link = NodeRepository.GetLink(Node.GetProperty<string>("link"));
            string idCsv = Node.GetProperty<string>("pageLink");
            if (string.IsNullOrWhiteSpace(idCsv))
            {
            }
            else
            {
                TileItem =  uQuery.GetNodesByCsv(idCsv).Select(n => new TileItem(n)).ToList()[0];
            }

        }

        public Link Link { get; set; }
        public TileItem TileItem { get; set; }



       
       
    }
}
