using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using CsvHelper;
using Newtonsoft.Json;

namespace JiraTransformer
{
    class Program
    {
        static int maxTags = 0;
        static int maxAttachments = 0;

        static void Main(string[] args)
        {
            var dataConverter = new DataConverter();

            var jiraExport = new List<dynamic>();

            using (var reader = new StreamReader("7579562FbZKDPs.csv"))
            {
                using (var csv = new CsvReader(reader))
                {
                    var records = csv.GetRecords<ClickupFormat>();

                    foreach (var record in records)
                    {
                        if (record.ListName.ToLower() != "technical writing")
                            //if (record.SpaceName.ToLower() != "iterations"
                            //    || record.ProjectName.ToLower() != "engineering"
                            //    || record.ListName.ToLower() != "team kata"
                            //    || ExcludeIssue(record.Tags))
                            continue;

                        dynamic jiraObject = new ExpandoObject();
                        jiraObject.IssueType = dataConverter.SelectJiraIssueType(record);
                        jiraObject.ParentId = record.ParentId == "null" ? String.Empty : record.ParentId;
                        jiraObject.TaskId = SetTaskId(record);
                        jiraObject.TaskName = record.TaskName;
                        jiraObject.TaskContent = dataConverter.NewLineConverter(record.TaskContent);
                        jiraObject.Status = record.Status;
                        jiraObject.DateCreatedText = dataConverter.ConvertClickupToJiraTimeFormat(record.DateCreatedText);
                        jiraObject.DueDateText = record.DueDateText;
                        jiraObject.StartDateText = record.StartDateText;
                        jiraObject.Assignees = String.Empty;
                        jiraObject.Priority = record.Priority;
                        jiraObject.ListName = record.ListName;
                        jiraObject.ProjectName = record.ProjectName;
                        jiraObject.SpaceName = record.SpaceName;
                        jiraObject.TimeEstimated = SetTimeEstimated(record.TimeEstimated);

                        SetTags(record.Tags, jiraObject);
                        SetAttachments(record.Attachments, jiraObject);
                        SetComments(record.Comments, jiraObject);

                        jiraExport.Add(jiraObject);
                    }
                }

                Console.WriteLine("Max tags: " + maxTags);
                Console.WriteLine("Max attachments: " + maxAttachments);

                using (var writer = new StreamWriter("JiraImport5.csv"))
                using (var csv = new CsvWriter(writer))
                {
                    csv.WriteRecords(jiraExport);
                }
            }

            string SetTaskId(dynamic record)
            {
                if (record.ParentId == "null")
                    return record.TaskId;

                return String.Empty;
            }

            string SetTimeEstimated(string timeEstimatedText)
            {
                if (String.IsNullOrEmpty(timeEstimatedText) || String.IsNullOrWhiteSpace(timeEstimatedText))
                    return String.Empty;

                return timeEstimatedText.Remove(timeEstimatedText.Length - 3);
            }

            bool ExcludeIssue(string tags)
            {
                var tagList = dataConverter.SeparateClickupTags(tags);

                bool isTeamLinda = false;
                foreach (var tag in tagList)
                {
                    if (tag == "team-linda")
                        isTeamLinda = true;
                }

                return isTeamLinda;
            }

            void SetTags(string tags, ExpandoObject jiraObject)
            {
                var expandoDict = jiraObject as IDictionary<string, object>;
                var count = 1;
                var tagList = dataConverter.SeparateClickupTags(tags);

                foreach (var tag in tagList)
                {
                    expandoDict.Add("tags" + count, tag.Replace(' ', '_'));
                    count++;
                }

                for (int i = count; i < 6; i++)
                {
                    expandoDict.Add("tags" + i, String.Empty);
                }

                if (count > maxTags)
                maxTags = count;

            }

            void SetAttachments(string clickupAttachment, ExpandoObject jiraObject)
            {
                var expandoDict = jiraObject as IDictionary<string, object>;
                var count = 1;

                var attachments = JsonConvert.DeserializeObject<List<ClickupAttachmentFormat>>(clickupAttachment);

                foreach (var attachment in attachments)
                {
                    expandoDict.Add("attachment" + count, attachment.GenerateJiraAttachmentFormat());
                    count++;
                }

                for (int i = count; i < 4; i++)
                {
                    expandoDict.Add("attachment" + i, String.Empty);
                }

                if (count > maxAttachments)
                    maxAttachments = count;
            }

            void SetComments(string commentJson, ExpandoObject jiraObject)
            {
                var expandoDict = jiraObject as IDictionary<string, object>;
                var count = 1;

                var commentList = JsonConvert.DeserializeObject<List<ClickupCommentFormat>>(commentJson);

                foreach (var comment in commentList)
                {
                    expandoDict.Add("comment" + count, comment.GenerateJiraCommentFormat());
                    count++;
                }
            }
        }
    }
        

}

