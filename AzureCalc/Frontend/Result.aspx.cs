using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataContract;
using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.Storage;

namespace Frontend
{
    public partial class Result : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var id = Request.QueryString.Get("id");

            var account = CloudStorageAccount.Parse(
                RoleEnvironment.GetConfigurationSettingValue("DataConnectionString")
                );
            var client = account.CreateCloudTableClient();
            var table = client.GetTableReference("calcTable2");
            table.CreateIfNotExists();

            var q = from r in table.CreateQuery<JobResult>()
                    where r.PartitionKey=="x" && r.RowKey==id select r;

            var jobResult = q.FirstOrDefault();
            if (null != jobResult)
            {
                TextBox1.Text = jobResult.Result.ToString();
            }
        }
    }
}