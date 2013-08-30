using System;
using umbraco;
using umbraco.NodeFactory;
using umbraco.cms.businesslogic.media;
using umbraco.interfaces;


namespace BI.Repository.Entities
{
	public class CarBrandItem : NodeItem
	{
		public CarBrandItem(string nodeId)
			: this(int.Parse(nodeId))
		{
		}

		public CarBrandItem(int nodeId)
			: this(new Node(nodeId))
		{
		}

		public CarBrandItem(INode node)
			: base(node)
		{
			ImageItem = CreateImageItem(Node.GetProperty<string>("image"));
			Link = Node.GetProperty<string>("link");
		}
		public string Link { get; set; }



		public ImageItem ImageItem { get; set; }


		private ImageItem CreateImageItem(string mediaId)
		{
			int id;
			if (string.IsNullOrWhiteSpace(mediaId) || !int.TryParse(mediaId, out id))
			{
				return new ImageItem();
			}
			Media media = new Media(id);
			return new ImageItem(media);
		}
	}
}
