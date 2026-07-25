
using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace GlobalApi.GlobalClasses
{
    public interface ISMSService
    {
        bool SendMessage(string RequestBody);
        bool SendScheduledMessage(string RequestBody);
    }
    public class SMSService: ISMSService
    {
        HttpClient client;
        private readonly IConfigurationRoot configurationRoot = null!;
        public SMSService()
        {
            this.client = new HttpClient();
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder().SetBasePath(Environment.CurrentDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            configurationRoot = configurationBuilder.Build();
        }
        public bool SendMessage(string RequestBody)
        {
            try
            {
                string URL = "https://bulksms.bsnl.in:5010/api/Send_SMS";
                this.client.DefaultRequestHeaders.ConnectionClose = true;
                HttpClientHandler handler = new HttpClientHandler() { UseDefaultCredentials = true };
                //HttpClient client = new HttpClient(handler);
                if(this.client.BaseAddress == null)
                {
                    this.client.BaseAddress = new Uri(URL);
                }
                
                this.client.DefaultRequestHeaders.Accept.Clear();
                this.client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                this.client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjEwMzI4IDEiLCJuYmYiOjE3MTI4Mzc3NTAsImV4cCI6MTc0NDM3Mzc1MCwiaWF0IjoxNzEyODM3NzUwLCJpc3MiOiJodHRwczovL2J1bGtzbXMuYnNubC5pbjo1MDEwIiwiYXVkIjoiMTAzMjggMSJ9.fQnqoELIv-rMvuAg4veAS4HTUeVpK1MHZ5uz0TvTjcY");


                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, Uri.EscapeUriString(this.client.BaseAddress.ToString()));
                request.Content = new StringContent(RequestBody, Encoding.UTF8, "application/json");
                request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                HttpResponseMessage tokenResponse = this.client.PostAsync(Uri.EscapeUriString(this.client.BaseAddress.ToString()), request.Content).Result;
                if (tokenResponse.IsSuccessStatusCode)
                {
                    this.client.DefaultRequestHeaders.ConnectionClose = true;
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
              throw new Exception(ex.Message);
            }
            finally
            {

            }
            
        }

        public bool SendScheduledMessage(string RequestBody)
        {
            try
            {
                string URL = "https://bulksms.bsnl.in:5010/api/Schedule_SMS";
                this.client.DefaultRequestHeaders.ConnectionClose = true;
                HttpClientHandler handler = new HttpClientHandler() { UseDefaultCredentials = true };
                //HttpClient client = new HttpClient(handler);
                if (this.client.BaseAddress == null)
                {
                    this.client.BaseAddress = new Uri(URL);
                }

                this.client.DefaultRequestHeaders.Accept.Clear();
                this.client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                this.client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjEwMzI4IDEiLCJuYmYiOjE2ODAyNDUxMDQsImV4cCI6MTcxMTc4MTEwNCwiaWF0IjoxNjgwMjQ1MTA0LCJpc3MiOiJodHRwczovL2J1bGtzbXMuYnNubC5pbjo1MDEwIiwiYXVkIjoiMTAzMjggMSJ9.TjREsEyjibM5AXfWmI4tNctPqtvS2o9yamoIDO4XRIk");


                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, Uri.EscapeUriString(this.client.BaseAddress.ToString()));
                request.Content = new StringContent(RequestBody, Encoding.UTF8, "application/json");
                request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                HttpResponseMessage tokenResponse = this.client.PostAsync(Uri.EscapeUriString(this.client.BaseAddress.ToString()), request.Content).Result;
                if (tokenResponse.IsSuccessStatusCode)
                {
                    this.client.DefaultRequestHeaders.ConnectionClose = true;
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {

            }

        }
    }
}
