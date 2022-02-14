using GraphQlApplication.Model;

namespace GraphQlApplication.DTO
{
    public class ActionDto
    {
        public int? session_id { get; set; }
        public int? sub_session_id { get; set; }
        public int element_id { get; set; }
        public int? participant_id { get; set; }
        public int? coach_id { get; set; }
        public Action action { get; set; }
    }
}
