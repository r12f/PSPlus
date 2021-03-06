﻿using System;
using System.Management.Automation;
using Microsoft.TeamFoundation.Client;
using Microsoft.VisualStudio.Services.Client;
using Microsoft.VisualStudio.Services.Common;
using PSPlus.Core.Powershell.Cmdlets;

namespace PSPlus.Modules.Tfs.TeamProjectCollection
{
    [Cmdlet(VerbsCommon.Get, "TfsTeamProjectCollection")]
    [OutputType(typeof(TfsTeamProjectCollection))]
    public class GetTfsTeamProjectCollectionCmdlet : CmdletBase
    {
        [Parameter(Position = 0, ValueFromPipeline = true, Mandatory = true, HelpMessage = "Team project collection URL.")]
        public string URL { get; set; }

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

            WriteObject(collection);
        }
    }
}
