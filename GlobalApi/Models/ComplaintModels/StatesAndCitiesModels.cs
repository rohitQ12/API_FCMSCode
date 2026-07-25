namespace GlobalApi.Models.ComplaintModels
{
    public class StatesModels
    {
        public int state_id { get; set; }
        public string state_name { get; set; }
        public string state_code { get; set; }
        public string category { get; set; }
             
    }

    public class CitiesModels
    {
        public int city_id { get; set; }
        public string city_name { get; set; }
        public int state_id { get; set; }

    }

}
