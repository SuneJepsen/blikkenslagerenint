﻿using System;
using System.Web;
using umbraco;
using umbraco.NodeFactory;
using umbraco.cms.businesslogic.media;
using umbraco.interfaces;


namespace BI.Repository.Entities 
{
    public class Testimonial: NodeItem
    {
        public Testimonial(string nodeId) : this(int.Parse(nodeId))
        {
        }

        public Testimonial(int nodeId) : this(new Node(nodeId))
        {
        }

        public Testimonial(INode node)
            : base(node)
        {



            Description = new HtmlString(Node.GetProperty<string>("testimonialText"));
            TestimonialFrom = Node.GetProperty<string>("fromTestimonial");
            string id = Node.GetProperty<string>("link");

            if (!string.IsNullOrEmpty(id))
                Link = NodeRepository.GetLink(id, Node);

        }

        public string TestimonialFrom { get; set; }
        public HtmlString Description { get; set; }

        public Link Link { get; set; }


  
       
       
    }
}
