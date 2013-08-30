using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Compilation;
using System.Web.UI;

namespace BI.Umbraco.helpers
{
    //http://www.delphicdigital.com/home/blog.aspx?d=854&title=Code-Expressions-to-Programmaticify-Your-Umbraco-Site
        [ExpressionPrefix("Code")]
        public class CodeExpressionBuilder : ExpressionBuilder
        {
            public override CodeExpression GetCodeExpression(BoundPropertyEntry entry,
               object parsedData, ExpressionBuilderContext context)
            {
                return new CodeSnippetExpression(entry.Expression);
            }
        }
}