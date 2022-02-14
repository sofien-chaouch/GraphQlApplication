using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace GraphQlApplication.Model
{
    [Table("action")]
    public class Action
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int action_id { get; set; }
        public string name { get; set; }
        public DateTime dueDate { get; set; }
        public string status { get; set; }
        public string assignedTo { get; set; }
        public int? element_instance_id { get; set; }
        public int? session_id { get; set; }
        public int? sub_session_id { get; set; }
        public int score { get; set; }
        public DateTime iDate { get; set; } = DateTime.Now;
        public int iUser { get; set; }

    }
}
