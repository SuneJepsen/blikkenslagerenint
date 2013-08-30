using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Bi.Umbraco.usercontrols
{
    public partial class OpenLayers : System.Web.UI.UserControl
    {
        public string Latitude { get; set; }
        public string Longitude { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            hdfLatitude.Value = Latitude;
            hdfLongitude.Value = Longitude; 
        }

       
      
    }


}