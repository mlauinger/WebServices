using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using DataContract;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Diagnostics;
using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace Worker
{
    public class WorkerRole : RoleEntryPoint
    {
        private readonly CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        private readonly ManualResetEvent runCompleteEvent = new ManualResetEvent(false);

        public override void Run()
        {
            Trace.TraceInformation("Worker is running");

            var account = CloudStorageAccount.Parse(RoleEnvironment.GetConfigurationSettingValue("DataConnectionString"));

            //Referenz zur Queue
            var queueClient = account.CreateCloudQueueClient();
            var queue = queueClient.GetQueueReference("calc2");
            queue.CreateIfNotExists();

            //Referenz zum Speicher
            var tableClient = account.CreateCloudTableClient();
            var table = tableClient.GetTableReference("calcTable2");
            table.CreateIfNotExists();

            while (true)
            {
                var message = queue.GetMessage();
                if (null != message)
                {
                    var job = CalcJobSerializer.Deserialize(message.AsString);
                    var result = job.Value1 + job.Value2;
                    var jobResult = new JobResult(job, result);

                    var insertOperation = TableOperation.Insert(jobResult);
                    table.Execute(insertOperation);

                    queue.DeleteMessage(message); //wichtig, sonst kommt die message wieder in die queue

                }
                else
                {
                    Thread.Sleep(100); // 0,0071€ pro 10000 Transaktionen
                }
            }

            this.runCompleteEvent.Set();
        }

        public override bool OnStart()
        {
            // Legen Sie die maximale Anzahl an gleichzeitigen Verbindungen fest.
            ServicePointManager.DefaultConnectionLimit = 12;

            // Informationen zum Behandeln von Konfigurationsänderungen
            // finden Sie im MSDN-Thema unter http://go.microsoft.com/fwlink/?LinkId=166357.

            bool result = base.OnStart();

            Trace.TraceInformation("Worker has been started");

            return result;
        }

        public override void OnStop()
        {
            Trace.TraceInformation("Worker is stopping");

            this.cancellationTokenSource.Cancel();
            this.runCompleteEvent.WaitOne();

            base.OnStop();

            Trace.TraceInformation("Worker has stopped");
        }

        private async Task RunAsync(CancellationToken cancellationToken)
        {
            // TODO: Ersetzen Sie Folgendes durch Ihre eigene Logik.
            while (!cancellationToken.IsCancellationRequested)
            {
                Trace.TraceInformation("Working");
                await Task.Delay(1000);
            }
        }
    }
}
