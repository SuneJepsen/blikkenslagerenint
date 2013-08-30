using System;
using System.Collections.Generic;
using umbraco;
using umbraco.NodeFactory;
using umbraco.cms.businesslogic.media;
using umbraco.interfaces;


namespace BI.Repository.Entities 
{
    public class EmployeeFolder: NodeItem
    {
        public EmployeeFolder(string nodeId) : this(int.Parse(nodeId))
        {
        }

        public EmployeeFolder(int nodeId) : this(new Node(nodeId))
        {
        }

        public EmployeeFolder(INode node)
            : base(node)
        {
            Header = Node.GetProperty<string>("header");
            if (string.IsNullOrWhiteSpace(Header) || string.IsNullOrEmpty(Header))
                Header = Node.Name;

            Employees = NodeRepository.GetEmployeeItems(node);
        }

        public IEnumerable<Employee> Employees { get; set; }
        public string Header { get; set; }

    }
}
