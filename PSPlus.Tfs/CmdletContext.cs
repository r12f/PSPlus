using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.WorkItemTracking.Client;

namespace PSPlus.Tfs
{
    public static class CmdletContext
    {
        public static TfsTeamProjectCollection Collection { get; set; }
        public static WorkItemStore WorkItemStore { get; set; }
        public static Project Project { get; set; }
    }
}
