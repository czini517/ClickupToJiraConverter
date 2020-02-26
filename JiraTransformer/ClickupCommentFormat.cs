namespace JiraTransformer
{
    internal class ClickupCommentFormat
    {
        private DataConverter dataConverterHelper;

        public string Text { get; set; }

        public string By { get; set; }

        public string Assigned { get; set; }

        public string Date { get; set; }

        public string Resolved { get; set; }

        public ClickupCommentFormat()
        {
            dataConverterHelper = new DataConverter();
        }

        public string GenerateJiraCommentFormat()
        {
            //"01/01/2012 10:10;Admin; This comment works"
            var jiraDateFormat = dataConverterHelper.ConvertClickupToJiraTimeFormat(Date);
            return jiraDateFormat + ";" + By + ";" + Text;
        }
    }
}