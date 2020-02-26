using CsvHelper.Configuration.Attributes;

namespace JiraTransformer
{
    public class ClickupFormat
    {
        [Index(0)]
        public string TaskId { get; set; }

        [Index(1)]
        public string TaskName { get; set; }

        [Index(2)]
        public string TaskContent { get; set; }

        [Index(3)]
        public string Status { get; set; }

        [Index(4)]
        public string DateCreated { get; set; }

        [Index(5)]
        public string DateCreatedText { get; set; }

        [Index(6)]
        public string DueDate { get; set; }

        [Index(7)]
        public string DueDateText { get; set; }

        [Index(8)]
        public string StartDate { get; set; }

        [Index(9)]
        public string StartDateText { get; set; }

        [Index(10)]
        public string ParentId { get; set; }

        [Index(11)]
        public string Attachments { get; set; }

        [Index(12)]
        public string Assignees { get; set; }

        [Index(13)]
        public string Tags { get; set; }

        [Index(14)]
        public string Priority { get; set; }

        [Index(15)]
        public string ListName { get; set; }

        [Index(16)]
        public string ProjectName { get; set; }

        [Index(17)]
        public string SpaceName { get; set; }

        [Index(18)]
        public string TimeEstimated { get; set; }

        [Index(19)]
        public string TimeEstimatedText { get; set; }

        [Index(20)]
        public string Checklists { get; set; }

        [Index(21)]
        public string Comments { get; set; }
    }
}