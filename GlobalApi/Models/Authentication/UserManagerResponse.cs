namespace GlobalApi.Models.Authentication
{
    public class UserManagerResponse
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public IEnumerable<string> Errors { get; set; }
        public DateTime? ExpireDate { get; set; }
        public string token { get; set; }
        public string userid { get; set; }
    }

    public class UserCustomResponse
    {
        public string? message { get; set; }
        public bool is_success { get; set; }
        public IEnumerable<string> errors { get; set; } = new string[0];
        public string? user_id { get; set; } = string.Empty;
    }

    public class UserRoleCustomResponse
    {
        public string? username { get; set; } = string.Empty;
        public string? role_id { get; set; } = string.Empty;
        public string? role_name { get; set; } = string.Empty;
        public bool is_success { get; set; } = false;
        public string? message { get; set; } = string.Empty;

    }
}
