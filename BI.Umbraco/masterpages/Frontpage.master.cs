using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BI.Repository;
using BI.Repository.Entities;
using umbraco.NodeFactory;
using umbraco.cms.businesslogic.macro;
using umbraco.cms.businesslogic.member;

namespace BI.Umbraco.masterpages
{
    public partial class Frontpage : System.Web.UI.MasterPage
    {
        private readonly Node _currentNode = Node.GetCurrent();
        private TeaserItem _topTeaser;
        private TeaserItem _middleTeaser;

        protected TeaserItem TopTeaser { get { return _topTeaser; } }
        protected TeaserItem MiddleTeaser { get { return _middleTeaser; } }

        private StandardValueItem _standardValueItem;

        protected StandardValueItem StandardValueItem { get { return _standardValueItem; } }


        public Frontpage()
        {
            Init += PageInit;
        }

        private void PageInit(object sender, EventArgs e)
        {
            _topTeaser = new TeaserItem(_currentNode.GetProperty("topTeaser").Value);
            _middleTeaser = new TeaserItem(_currentNode.GetProperty("middleTeaser").Value);
            _standardValueItem = new StandardValueItem(_currentNode);

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //http://html.adobe.com/opensource/#footer_nav

 
        }

        protected string GetCustomModel()
        {
          

            return "1,2,3";

        }

        private void DoImport()
        {
            // umbBIEntities context = new umbBIEntities();
            //List<C_Users> users = context.C_Users.ToList().ToList();
            //ImportToUmbraco(users);
            // DeleteAllMembers();
        }
        
        private void DeleteAllMembers()
        {

            IEnumerable<Member> AverageJoes = Member.GetAllAsList();

            foreach (Member Joe in AverageJoes)
            {

                Joe.delete();

            }
        }
    }
}