using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Specialized;
using System.Deployment.Application;
using System.Web;

namespace Banagrario.Scanning.Utils
{
    public class ClickOnceUtil
    {
        public static NameValueCollection GetQueryStringParameters()
        {
            var NameValueTable = new NameValueCollection();

            if (ApplicationDeployment.IsNetworkDeployed)
            {
                if (ApplicationDeployment.CurrentDeployment.ActivationUri != null)
                {
                    var QueryString = ApplicationDeployment.CurrentDeployment.ActivationUri.Query;
                    NameValueTable = HttpUtility.ParseQueryString(QueryString);
                }
            }

            return NameValueTable;
        }
    }
}
