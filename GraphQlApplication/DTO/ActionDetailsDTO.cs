using System;


namespace GraphQlApplication.DTO
{
    public class ActionDetailsDTO
    {
        public int? action_id { get; set; }
        public string? type { get; set; }
        public Object? value { get; set; }
    }
}
