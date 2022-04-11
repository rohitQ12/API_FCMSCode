namespace GlobalApi.Models.Authentication
{
    public class FacebookUserInfoResult
    {
        public string? first_name { get; set; }
        public string? last_name { get; set; }
        public FacebookPicture? picture { get; set; }
        public string? email { get; set; }
        public string? id { get; set; }
    }
    public class FacebookPicture
    {
        public Data? data { get; set; }
    }
    public class Data
    {
        public int? height { get; set; }
        public bool? is_silhouette { get; set; }
        public string? url { get; set; }
        public int width { get; set; }
    }
}
