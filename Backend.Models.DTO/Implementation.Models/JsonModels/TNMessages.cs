using System;
using System.Collections.Generic;

namespace Backend.Models.Implementation.JsonModels
{
    public class TNMessages
    {
        public class Preview
        {
            public string id { get; set; }
            public string username { get; set; }
            public int direction { get; set; }
            public string contact_value { get; set; }
            public int contact_type { get; set; }
            public string contact_name { get; set; }
            public int message_type { get; set; }
            public string message { get; set; }
            public bool read { get; set; }
            public bool deleted { get; set; }
            public string date { get; set; }
        }

        public class Ringtones
        {
            public string ios { get; set; }
        }

        public class Avatar
        {
            public string background_colour { get; set; }
            public string picture { get; set; }
            public string initials { get; set; }
        }

        public class Member
        {
            public string global_id { get; set; }
            public string contact_value { get; set; }
            public string name { get; set; }
            public string contact_proxy_number { get; set; }
            public Ringtones ringtones { get; set; }
            public string updated_at { get; set; }
            public Avatar avatar { get; set; }
            public int contact_type { get; set; }
        }

        public class Result
        {
            public string username { get; set; }
            public string contact_value { get; set; }
            public string is_blocked { get; set; }
            public bool has_read { get; set; }
            public string last_deleted_Id { get; set; }
            public string archived_message_id { get; set; }
            public string archived_message_time { get; set; }
            public string created_at { get; set; }
            public string updated_at { get; set; }
            public Preview preview { get; set; }
            public List<Member> members { get; set; }
            public string name { get; set; }
            public string avatar { get; set; }
        }

        public class JsonMessages
        {
            public List<Result> result { get; set; }
            public string error_code { get; set; }
        }
    }
}
