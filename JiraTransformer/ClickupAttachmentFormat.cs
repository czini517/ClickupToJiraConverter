
using System;

namespace JiraTransformer
{
    public class ClickupAttachmentFormat
    {
        DataConverter dataConverter;

        public string Title { get; set; }

        public string Url { get; set; }

        public ClickupAttachmentFormat()
        {
            dataConverter = new DataConverter();
        }

        public string GenerateJiraAttachmentFormat()
        {
            //assign,subject,attachment
            //"01/01/2012 13:10;Admin;image.png;file://image-name.png"
            //var jiraAttachmentFormat = "\"J.Doe\",\"" + Title + "\"," + Url;
            var jiraAttachmentFormat = DateTime.Now.ToString("dd/MMM/yy h:mm tt") + ";Admin;" + Url;
            return jiraAttachmentFormat;
        }
    }
}
