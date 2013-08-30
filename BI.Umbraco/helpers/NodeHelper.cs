using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using umbraco;
using umbraco.NodeFactory;

namespace BI.Umbraco.helpers
{
    public static class NodeHelper
    {
        public static string GetPropertyRecursiveAsString(this Node node, string propertyAlias)
        {
            string result = string.Empty;
            if (node.HasProperty(propertyAlias))
                result = node.GetProperty<string>(propertyAlias);
            if (!string.IsNullOrWhiteSpace(result))
                return result;
            var ancestors = node.GetAncestorNodes();
            foreach (var ancestor in ancestors)
            {
                if (!ancestor.HasProperty(propertyAlias)) continue;
                result = ancestor.GetProperty<string>(propertyAlias);
                if (!string.IsNullOrWhiteSpace(result))
                    return result;
            }
            return string.Empty;
        }
    }
}