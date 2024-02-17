using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace webAssistPill
{
    public partial class masterpage : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public HtmlButton HomeButton1
        {
            get { return homeButton1; }
        }

        public HtmlButton HomeButton2
        {
            get { return homeButton2; }
        }

        public HtmlButton HomeButton3
        {
            get { return homeButton3; }
        }

        public HtmlButton HomeButton4
        {
            get { return homeButton4; }
        }
    }
}