using System;
using System.Management.Automation;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.WorkItemTracking.Client;
using Microsoft.VisualStudio.Services.Client;
using Microsoft.VisualStudio.Services.Common;
using PSPlus.Core.Powershell.Cmdlets;
using PSPlus.Tfs;

namespace PSPlus.Modules.Tfs.Connection
{
    [Cmdlet(VerbsCommunications.Connect, "TfsTeamProjectCollection")]
    [OutputType(typeof(TfsTeamProjectCollection))]
    public class ConnectTfsTeamProjectCollectionCmdlet : CmdletBase
    {
        [Parameter(Position = 0, ValueFromPipeline = true, Mandatory = true, HelpMessage = "Team project collection URL.")]
        public string URL { get; set; }

        [Parameter(Mandatory = false, HelpMessage = "Return the project object.")]
        public SwitchParameter PassThru { get; set; }

        protected override void ProcessRecordInEH()
        {
            if (string.IsNullOrWhiteSpace(URL))
            {
                throw new PSArgumentException("URL cannot be null or empty.");
            }

            VssCredentials creds = new VssClientCredentials();
            creds.Storage = new VssClientCredentialStorage();

            Uri teamCollectionURI = new Uri(URL);
            TfsTeamProjectCollection collection = TfsTeamProjectCollectionFactory.GetTeamProjectCollection(teamCollectionURI, creds);
            collection.Authenticate();

            CmdletContext.Collection = collection;
            CmdletContext.WorkItemStore = new WorkItemStore(collection);

            if (PassThru.IsPresent)
            {
                WriteObject(collection);
            }
        }
    }
}
