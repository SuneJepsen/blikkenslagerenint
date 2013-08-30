using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using umbraco.interfaces;
using umbraco.NodeFactory;

namespace BI.Repository.Entities
{
    public class UmbracoEnum
    {
        public enum DocumentTypeAlias { NotImplemented = 0, OneColumn = 1, TwoColumn = 2, ThreeColumn = 3, 
            ContactFormularBiltorvet,
            EmployeeFolder,
            ReferenceFolder,
            BiltorvetProduct,
            ContactFormularBiltovetAS,
            AutodesktopNews,
            BiltorvetNews,
            TestimonialFolder
        };
        public enum TextpagePlaceholder { FirstHalf = 0, SecondHalf = 1};

        public static DocumentTypeAlias GetDocType(INode node)
        {
            DocumentTypeAlias returnValue;
            switch (node.NodeTypeAlias)
            {
                case "OneColumn":
                    returnValue = DocumentTypeAlias.OneColumn;
                    break;
                case "TwoColumn":
                    returnValue = DocumentTypeAlias.TwoColumn;
                    break;
                case "ThreeColumns":
                    returnValue = DocumentTypeAlias.ThreeColumn;
                    break;
         
                case "ContactFormularBiltorvet":
                    returnValue = DocumentTypeAlias.ContactFormularBiltorvet;
                    break;
                case "ContactFormularBiltorvetAS":
                    returnValue = DocumentTypeAlias.ContactFormularBiltovetAS;
                    break;
                case "EmployeeFolder":
                    returnValue = DocumentTypeAlias.EmployeeFolder;
                    break;

                case "ReferenceFolder":
                    returnValue = DocumentTypeAlias.ReferenceFolder;
                    break;
                case "BiltorvetProduct":
                    returnValue = DocumentTypeAlias.BiltorvetProduct;
                    break;
                case "AutodesktopNews":
                    returnValue = DocumentTypeAlias.AutodesktopNews;
                    break;
                case "BiltorvetNews":
                    returnValue = DocumentTypeAlias.BiltorvetNews;
                    break;
                case "TestimonialFolder":
                    returnValue = DocumentTypeAlias.TestimonialFolder;
                    break;
                default:
                    returnValue = DocumentTypeAlias.NotImplemented;
                    break;
            }

            return returnValue;

        }
    }
}
