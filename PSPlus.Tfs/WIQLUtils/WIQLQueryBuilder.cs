using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PSPlus.Tfs.WIQLUtils
{
    public class WIQLQueryBuilder
    {
        public WIQLQueryBuilder()
        {
        }

        public List<string> QueryFields { get; set; }
        public List<int> Ids { get; set; }
        public List<string> WorkItemTypes { get; set; }
        public List<string> States { get; set; }
        public List<string> AssignedTo { get; set; }
        public string Title { get; set; }
        public List<int> Priority { get; set; }
        public string AreaPath { get; set; }
        public string UnderAreaPath { get; set; }
        public string IterationPath { get; set; }
        public string UnderIterationPath { get; set; }
        public string ExtraFilters { get; set; }

        public string Build()
        {
            StringBuilder wiqlConditionsBuilder = new StringBuilder();
            if (Ids != null && Ids.Count > 0)
            {
                wiqlConditionsBuilder.AppendFormat("[{0}] In ({1})", WIQLSystemFieldNames.Id, string.Join(",", Ids));
            }

            if (WorkItemTypes != null && WorkItemTypes.Count > 0)
            {
                AppendAndConnectorIfNeeded(wiqlConditionsBuilder);
                BuildInStringListCondition(wiqlConditionsBuilder, WIQLSystemFieldNames.WorkItemType, WorkItemTypes);
            }

            if (States != null && States.Count > 0)
            {
                AppendAndConnectorIfNeeded(wiqlConditionsBuilder);
                BuildInStringListCondition(wiqlConditionsBuilder, WIQLSystemFieldNames.State, States);
            }

            if (AssignedTo != null && AssignedTo.Count > 0)
            {
                AppendAndConnectorIfNeeded(wiqlConditionsBuilder);
                BuildInStringListCondition(wiqlConditionsBuilder, WIQLSystemFieldNames.AssignedTo, AssignedTo);
            }

            if (!string.IsNullOrEmpty(Title))
            {
                AppendAndConnectorIfNeeded(wiqlConditionsBuilder);
                wiqlConditionsBuilder.AppendFormat("[{0}] CONTAINS ", WIQLSystemFieldNames.Title);
                AppendStringValue(wiqlConditionsBuilder, Title);
            }

            if (Priority != null && Priority.Count > 0)
            {
                AppendAndConnectorIfNeeded(wiqlConditionsBuilder);
                BuildInIntListCondition(wiqlConditionsBuilder, WIQLSystemFieldNames.Priority, Priority);
            }

            if (!string.IsNullOrWhiteSpace(AreaPath))
            {
                AppendAndConnectorIfNeeded(wiqlConditionsBuilder);
                wiqlConditionsBuilder.AppendFormat("[{0}] = ", WIQLSystemFieldNames.AreaPath);
                AppendStringValue(wiqlConditionsBuilder, AreaPath);
            }

            if (!string.IsNullOrWhiteSpace(UnderAreaPath))
            {
                AppendAndConnectorIfNeeded(wiqlConditionsBuilder);
                wiqlConditionsBuilder.AppendFormat("[{0}] UNDER ", WIQLSystemFieldNames.AreaPath);
                AppendStringValue(wiqlConditionsBuilder, UnderAreaPath);
            }

            if (!string.IsNullOrWhiteSpace(IterationPath))
            {
                AppendAndConnectorIfNeeded(wiqlConditionsBuilder);
                wiqlConditionsBuilder.AppendFormat("[{0}] = ", WIQLSystemFieldNames.IterationPath);
                AppendStringValue(wiqlConditionsBuilder, IterationPath);
            }

            if (!string.IsNullOrWhiteSpace(UnderIterationPath))
            {
                AppendAndConnectorIfNeeded(wiqlConditionsBuilder);
                wiqlConditionsBuilder.AppendFormat("[{0}] UNDER ", WIQLSystemFieldNames.IterationPath);
                AppendStringValue(wiqlConditionsBuilder, UnderIterationPath);
            }

            if (!string.IsNullOrWhiteSpace(ExtraFilters))
            {
                AppendAndConnectorIfNeeded(wiqlConditionsBuilder);
                wiqlConditionsBuilder.Append(ExtraFilters);
            }

            StringBuilder wiqlQueryBuilder = new StringBuilder();
            wiqlQueryBuilder.Append("SELECT ");

            if (QueryFields != null && QueryFields.Count > 0)
            {
                bool isFirstQueryField = true;
                foreach (var queryField in QueryFields)
                {
                    if (!isFirstQueryField)
                    {
                        wiqlConditionsBuilder.Append(", ");
                    }
                    else
                    {
                        isFirstQueryField = false;
                    }

                    wiqlQueryBuilder.Append(queryField);
                }
            }
            else
            {
                wiqlQueryBuilder.Append("*");
            }

            wiqlQueryBuilder.Append(" FROM workitems");

            if (wiqlConditionsBuilder.Length > 0)
            {
                wiqlQueryBuilder.Append(" WHERE ");
                wiqlQueryBuilder.Append(wiqlConditionsBuilder.ToString());
            }

            return wiqlQueryBuilder.ToString();
        }

        private static void AppendAndConnectorIfNeeded(StringBuilder s)
        {
            if (s.Length > 0)
            {
                s.Append(" AND ");
            }
        }

        private static void BuildInIntListCondition(StringBuilder s, string fieldName, IEnumerable<int> fieldValues)
        {
            BuildInStringListCondition(s, fieldName, fieldValues.Select(x => x.ToString()));
        }

        private static void BuildInStringListCondition(StringBuilder s, string fieldName, IEnumerable<string> fieldValues)
        {
            s.AppendFormat("{0} IN (", fieldName);

            bool isFirstField = true;
            foreach (var fieldValue in fieldValues)
            {
                if (isFirstField)
                {
                    isFirstField = false;
                }
                else
                {
                    s.Append(", ");
                }

                AppendStringValue(s, fieldValue);
            }

            s.Append(")");
        }

        private static void AppendStringValue(StringBuilder s, string fieldValue)
        {
            bool needQuote = true;
            if (fieldValue.Length > 0)
            {
                // "@" for variable. It is the same restriction for visual studio online webpage.
                // Only the values with @ in the beginning will be treated as expressions.
                if (fieldValue[0] == '@')
                {
                    needQuote = false;
                }
            }

            // In scenarios where we are not quoting, we can be injected, but since the visual studio online allows the same thing, it is ok to do it here.
            if (needQuote)
            {
                s.Append("'");

                foreach (char fieldValueChar in fieldValue)
                {
                    switch (fieldValueChar)
                    {
                        // Escape "'" to "''"
                        case '\'':
                            s.Append('\'');
                            break;
                    }

                    s.Append(fieldValueChar);
                }

                s.Append("'");
            }
            else
            {
                s.Append(fieldValue);
            }
        }
    }
}
