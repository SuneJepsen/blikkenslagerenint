using System;
using umbraco;
using umbraco.NodeFactory;
using umbraco.cms.businesslogic.media;
using umbraco.interfaces;


namespace BI.Repository.Entities 
{
    public class ImageItem
    {
        public ImageItem(Media node)
        {
            AltText = node.GetProperty<string>("altText");
            PictureUrl = node.GetImageUrl();
        }
        public ImageItem()
        {
        }
        public string AltText { get; set; }
        public string PictureUrl { get; set; }
    }
}
