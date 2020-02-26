using System;
using System.Globalization;

namespace JiraTransformer
{
    public class DataConverter
    {
        public string ConvertClickupToJiraTimeFormat(string clickupTimeFormat)
        {
            //11/6/2017, 5:09:41 PM GMT+1
            var result = DateTime.ParseExact(clickupTimeFormat, "M/d/yyyy, h:mm:ss tt \"GMT\"z", CultureInfo.InvariantCulture);
            return result.ToString("dd/MMM/yy h:mm tt");
        }

        public string SelectJiraIssueType(ClickupFormat clickupFormat)
        {

            if (clickupFormat.ParentId != "null")
                return "sub-task";

            var tags = SeparateClickupTags(clickupFormat.Tags);

            foreach (var tag in tags)
            {
                if (tag.ToLower() == "enhancement")
                    return "story";

                if (tag.ToLower() == "issue")
                    return "bug";

                if (tag.ToLower() == "maintenance")
                    return "task";
            }

            return "task";
        }

        public string[] SeparateClickupTags(string tagText)
        {
            return (tagText.Replace("[", String.Empty).Replace("]", String.Empty)).Split(',');
        }

        public string NewLineConverter(string input)
        {
            return input.Replace("\\n", @"\\ ");
        }

    }
}
