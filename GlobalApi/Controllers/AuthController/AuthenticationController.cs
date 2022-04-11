using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GlobalApi.Repository.AuthRepository;
using GlobalApi.GlobalClasses;
using Microsoft.AspNetCore.Authorization;
using GlobalApi.Models.Authentication;
using System.Net;
using GlobalApi.IRepository.AuthIRepository;
using Newtonsoft.Json;
using System.Globalization;
using Newtonsoft.Json.Linq;
using MaxMind.GeoIP2;

namespace GlobalApi.Controllers.AuthController
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;
        private readonly IConfiguration _configuration;
        public readonly IAuthenticationRepository _repository;
        private IEMailService _EMailService;
        private IHttpContextAccessor _accessor;
        public AuthenticationController(Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment,IHttpContextAccessor accessor,IConfiguration configuration, IAuthenticationRepository repository, IEMailService EMailService)
        {
            _configuration = configuration;
            _EMailService = EMailService;
            this._repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _accessor = accessor;
            _hostingEnvironment = hostingEnvironment;
        }
        [HttpPost, Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await this._repository.RegisterUserAsync(model);

                if (result.IsSuccess)
                {
                    if(result.Message =="User created successfully!")
                        return Ok(result); // Status Code: 200 
                    else
                       return BadRequest(result);
                }
                return BadRequest(result);
            }

            return BadRequest("Some properties are not valid"); // Status code: 400
        }

        [AllowAnonymous]
        [HttpPost, Route("ExternalRegister")]
        public async Task<IActionResult> EXtRegister([FromBody] RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await this._repository.ExtRegisterUserAsync(model);

                if (result.IsSuccess)
                    return Ok(result); // Status Code: 200 

                return BadRequest(result);
            }

            return BadRequest("Some properties are not valid"); // Status code: 400
        }


        [HttpGet,Route("testing")]
        [AllowAnonymous]
        public IActionResult tesing()
        {
            byte[] imgdata = System.IO.File.ReadAllBytes(("wwwroot/Images/user-1633249__340 (1).png"));
            return Ok(imgdata);
        }

        [HttpGet, Route("testing3")]
        [AllowAnonymous]
        public IActionResult tesing12()
        {
            using (var reader = new DatabaseReader(_hostingEnvironment.ContentRootPath + "\\GeoLite2-City.mmdb"))
            {
                // Determine the IP Address of the request
                var ipAddress = HttpContext.Connection.RemoteIpAddress;
                IPHostEntry heserver = Dns.GetHostEntry(Dns.GetHostName());
                var ip = heserver.AddressList[2].ToString();
                // Get the city from the IP Address
                var city = reader.City(ip);

                return Ok(city);
            }
        }

        [HttpGet, Route("testing2")]
        [AllowAnonymous]
        public IActionResult Gettesting()
        {
            //var ip = _accessor.HttpContext?.Connection?.RemoteIpAddress?.ToString();
            //return new string[] { ip, "value2" };
            IPHostEntry heserver = Dns.GetHostEntry(Dns.GetHostName());
            var ip = heserver.AddressList[2].ToString();
            //var url = "http://freegeoip.net/json/" + IP;
            //var url = "http://freegeoip.net/json/" + IP;
            string url = "http://api.ipstack.com/" + ip + "?access_key=[56bcee261acb7bb879c85e8a323b5683]";
            var request = System.Net.WebRequest.Create(url);

            using (WebResponse wrs = request.GetResponse())
            {
                using (Stream stream = wrs.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        string json = reader.ReadToEnd();
                        var obj = JObject.Parse(json);
                        string City = (string)obj["city"];
                        string Country = (string)obj["region_name"];
                        string CountryCode = (string)obj["country_code"];

                        return Ok(CountryCode + " - " + Country + "," + City);
                    }
                }
            }


            return NotFound();
        }

        [HttpPut, Route("Update")]
        public async Task<IActionResult> Update([FromBody] RegisterBindingModel model)
        {
            //string userName = User.Identity.Name.ToString();
            if (ModelState.IsValid)
            {
                var result = await this._repository.UpdateUserAsync(model, model.UserName);

                if (result != false)
                {
                    return Ok(result);
                }

                return Unauthorized(result);
            }

            return BadRequest("Some properties are not valid");
        }
        [HttpDelete, Route("Delete")]
        public async Task<IActionResult> Delete(string UserId)
        {
            //string userName = User.Identity.Name.ToString();
            if (ModelState.IsValid)
            {
                var result = await this._repository.DeleteUserAsync(UserId);

                if (result != false)
                {
                    return Ok(result);
                }

                return Unauthorized(result);
            }

            return BadRequest("Some properties are not valid");
        }

        [HttpGet, Route("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(token))
                return NotFound();

            var result = await this._repository.ConfirmEmailAsync(userId, token);

            if (result.IsSuccess)
            {
                return Redirect($"{_configuration["AppUrl"]}/ConfirmEmail.html");
            }
            return BadRequest(result);
        }
        [AllowAnonymous]
        [HttpPost, Route("ForgetPassword")]
        public async Task<IActionResult> ForgetPassword([FromBody] ResetPasswordViewModel model)
        {
            if (model.Username==null)
                return NotFound();

            var result = await this._repository.ResetPasswordAsync(model);

            if (result.IsSuccess)
                return Ok(result); // 200

            return BadRequest(result); // 400
        }
        [AllowAnonymous]
        [HttpGet, Route("Phonenumber")]
        public IActionResult Phonenumber(string phonenumber)
        {
            if (string.IsNullOrEmpty(phonenumber))
                return NotFound();

            var result = this._repository.Phonenumber(phonenumber);

            if (result!=null)
                return Ok(result); // 200

            return BadRequest(result); // 400
        }


        [HttpPost, Route("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await this._repository.ResetPasswordAsync(model);

                if (result.IsSuccess)
                    return Ok(result);

                return BadRequest(result);
            }

            return BadRequest("Some properties are not valid");
        }
        [AllowAnonymous]
        [HttpGet, Route("Google_Login")]
        public async Task<IActionResult> GoogleLogin(string accesstoken)
        {
            if (ModelState.IsValid)
            {
                var result = await this._repository.ForGoogle(accesstoken);

                if (result.IsSuccess)
                    return Ok(result);

                return BadRequest(result);
            }

            return BadRequest("Some properties are not valid");
        }
        [AllowAnonymous]
        [HttpPost, Route("Facebook_Login")]
        public async Task<IActionResult> FacebookLogin(string accesstoken)
        {
            if (ModelState.IsValid)
            {
                var result = await this._repository.ForFacebook(accesstoken);

                if (result.IsSuccess)
                    return Ok(result);

                return BadRequest(result);
            }

            return BadRequest("Some properties are not valid");
        }

        [HttpPut, Route("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromForm] ChangePassword model)
        {
            if (ModelState.IsValid)
            {
                var result = await this._repository.ChangePasswordAsync(model);

                if (result.IsSuccess)
                    return Ok(result);

                return BadRequest(result);
            }

            return BadRequest("Some properties are not valid");
        }

        [HttpPut, Route("ChangePassword_user")]
        public async Task<IActionResult> ChangePassword_user([FromBody] ChangePassword model)
        {
            if (ModelState.IsValid)
            {
                var result = await this._repository.ChangePasswordAsync(model);

                if (result.IsSuccess)
                    return Ok(result);

                return BadRequest(result);
            }

            return BadRequest("Some properties are not valid");
        }
        [HttpPut, Route("ActivateInactivate")]
        public async Task<ActionResult> ActivateInactivate(string userid)
        {
            if (userid != null)
            {
                var result = await this._repository.ActivateInactivate(userid);
                if (result!=null)
                    return Ok(result); // Status Code: 200 

                return BadRequest("The Role you have entered already exists");
            }
            return BadRequest("Some properties are not valid"); // Status code: 400
        }

    }
    public class IpInfo
    {
        [JsonProperty("ip")]
        public string Ip { get; set; }

        [JsonProperty("hostname")]
        public string Hostname { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("region")]
        public string Region { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("loc")]
        public string Loc { get; set; }

        [JsonProperty("org")]
        public string Org { get; set; }

        [JsonProperty("postal")]
        public string Postal { get; set; }
    }
}
