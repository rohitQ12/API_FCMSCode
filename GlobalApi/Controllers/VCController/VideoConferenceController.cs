using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using BigBlueButtonAPI.Core;
using System.Security.Cryptography;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.Extensions.Options;
using System.Collections;
using System.IO;
using System.Text;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using System.Xml;
//using vgsl_vc_api.Models;
//using vgsl_vc_api.Repositorys;
using GlobalApi.Models.VC;
using Microsoft.AspNetCore.Server;

namespace GlobalApi.Controllers.VCController
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoConferenceController : ControllerBase
    {
        string url = "https://meditel.co.in/bigbluebutton/";
        string secret = "4GogdV1dRal3NqO8mM5KBFu8GQuI0gtQpKwL35Do";
        private readonly BigBlueButtonAPIClient client;
        public VideoConferenceController(BigBlueButtonAPIClient client)
        {
            this.client = client;
        }
        [HttpGet]
        [Route("StartMeeting")]
        public async Task<ActionResult> StartMeeting()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            GeneralErrorMessage objErr = new GeneralErrorMessage();
            //CreateMeetingSuccessMessage objSuc = new CreateMeetingSuccessMessage();
            MeetingJoinMessage objJoinReqtUrl = new MeetingJoinMessage();
            try
            {
                Random randomObj = new Random();
                string meetingId = "MT" + randomObj.Next(10000000, 100000000).ToString();

                //here pass the doctor id
                string doctorId = "DR" + randomObj.Next(10000000, 100000000).ToString();
                //string doctorId = model.doctorID;

                string attendeePw = "attendee";
                string moderatorPw = "moderator";
                //enabling recording parameters
                bool record = true;
                bool autoStartRecording = false;
                bool allowStartStopRecording = true;
                bool removeMeetingWhenEnded = false;
                int defaultMeetingExpireDuration = 300;// 300;
                int meetingDuration = 0;
                bool allowRequestsWithoutSession = true;
                string defaultWelcomeMessage = "Welcome to Meditel!";
                string defaultWelcomeMessageFooter = "the server is running";
                //string defaultWC = "defaultWelcomeMessage='Welcome Medel'" +" + "+"defaultWelcomeMessageFooter='This footer'";
                bool autoSwapLayout = true;
                bool hidePresentation = false;
                bool preUploadedPresentationOverrideDefault = false;
                string meetingLayout = "CUSTOM_LAYOUT"; //"SMART_LAYOUT"; // "VIDEO_FOCUS";
                //int meetingExpireWhenLastUserLeftInMinutes = 5;
                bool recordingmarks = true;

                //diable the sharedNotes //working
                //&disabledFeatures=sharedNotes

                //dev server               
                //string callbackUrl = "http://192.168.1.23:8088/api/VideoConference/callback?meetingID="+meetingId; //api 
                //string logoutURL = "http://192.168.1.23:4200/#/home"; //app  
                //string logoutURL = "https://vcc.medetel.in/#/callback?meetingID=" + meetingId + "&recordingmarks=" + recordingmarks; //app               

                //staging server
                string callbackUrl = "https://vccapi.medetel.in/api/VideoConference/callback?meetingID=" + meetingId; //api 
                string logoutURL = "https://vcc.medetel.in/#/home"; //app               

                //encoding process
                string encodeCallbackUrl = System.Net.WebUtility.UrlEncode(callbackUrl);
                string encodeLogoutURL = System.Net.WebUtility.UrlEncode(logoutURL);
                string encodeDefaultWelcomeMessage = System.Net.WebUtility.UrlEncode(defaultWelcomeMessage);
                string encodeDefaultWelcomeMessageFooter = System.Net.WebUtility.UrlEncode(defaultWelcomeMessageFooter);

                //string encodeDefaultWC = System.Net.WebUtility.UrlEncode(defaultWC);

                //string encodeMeetingName = System.Net.WebUtility.UrlEncode(model.meetingName);

                //meeting creation process
                var strParameters = "name=" + meetingId + "&meetingID=" + meetingId + "&attendeePW=" + attendeePw + "&moderatorPW=" + moderatorPw + "&duration=" + meetingDuration
                    + "&record=" + record + "&autoStartRecording=" + autoStartRecording
                    + "&allowStartStopRecording=" + allowStartStopRecording + "&removeMeetingWhenEnded=" + removeMeetingWhenEnded
                    + "&defaultMeetingExpireDuration=" + defaultMeetingExpireDuration
                    + "&welcome=" + encodeDefaultWelcomeMessage
                    + "&defaultWelcomeMessageFooter=" + encodeDefaultWelcomeMessageFooter
                    + "&allowRequestsWithoutSession=" + allowRequestsWithoutSession
                    + "&joinViaHtml5=true"
                    + "&userdata-bbb_hide_presentation=true"
                    + "&userdata-bbb_show_participants_on_login=false"
                    + "&learningDashboardEnabled=false"
                    + "&endCallbackUrl=" + encodeCallbackUrl + "&logoutURL=" + encodeLogoutURL;

                var strSha1CheckSum = await GetChecksum("create", strParameters);
                var reqUrl = url + "api/create?" + strParameters + "&checksum=" + strSha1CheckSum;

                string strReturnCode = string.Empty;
                string strData = string.Empty;

                HttpClient httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Accept.Clear();
                var response = httpClient.GetAsync(reqUrl).Result;

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(result);
                    strData = JsonConvert.SerializeXmlNode(doc, Newtonsoft.Json.Formatting.None, true);
                    var resultjson = JObject.Parse(strData);
                    // to read the data from the response
                    strReturnCode = Convert.ToString(resultjson["returncode"]);

                    if (strReturnCode == "FAILED")
                    {
                        JsonSerializer serializer = new JsonSerializer();
                        objErr = (GeneralErrorMessage)serializer.Deserialize(new JTokenReader(resultjson), typeof(GeneralErrorMessage));
                    }
                    else
                    {

                        //string moderatorPw = "moderator";
                        //string userID = "DR" + randomObj.Next(10000000, 100000000).ToString();
                        //string joineeNameEncode = System.Net.WebUtility.UrlEncode(model.joineeName);
                        var strParametersModerator = "fullName=" + doctorId + "&meetingID=" + meetingId
                            + "&password=" + moderatorPw
                            + "&redirect=true"
                            + "&joinViaHtml5=true"
                            + "&userdata-bbb_hide_presentation=true"
                            + "&userdata-bbb_show_participants_on_login=false"
                            + "&userID=" + doctorId;
                        var strSha1CheckSumModerator = await GetChecksum("join", strParametersModerator);
                        var strMeetingJoinUrl = url + "api/join?" + strParametersModerator + "&checksum=" + strSha1CheckSumModerator;

                        /*
                        var meetingJoinReqtUrl = new MeetingJoinMessage
                        {
                            meetingJoinUrl = strMeetingJoinUrl
                        };
                        */
                        objJoinReqtUrl.meetingJoinUrl = strMeetingJoinUrl;

                        /*
                        objSuc.returncode = Convert.ToString(resultjson["returncode"]);
                        objSuc.meetingID = Convert.ToString(resultjson["meetingID"]);
                        objSuc.meetingName = meetingId;
                        objSuc.doctorID = doctorId;
                        objSuc.internalMeetingID = Convert.ToString(resultjson["internalMeetingID"]);
                        objSuc.parentMeetingID = Convert.ToString(resultjson["parentMeetingID"]);
                        objSuc.attendeePW = Convert.ToString(resultjson["attendeePW"]);
                        objSuc.moderatorPW = Convert.ToString(resultjson["moderatorPW"]);
                        objSuc.createTime = Convert.ToString(resultjson["createTime"]);
                        objSuc.voiceBridge = Convert.ToInt32(resultjson["voiceBridge"]);
                        objSuc.dialNumber = Convert.ToString(resultjson["dialNumber"]);
                        objSuc.createDate = Convert.ToString(resultjson["createDate"]);
                        objSuc.hasUserJoined = Convert.ToBoolean(resultjson["hasUserJoined"]);
                        objSuc.duration = Convert.ToInt32(resultjson["duration"]);
                        objSuc.hasBeenForciblyEnded = Convert.ToBoolean(resultjson["hasBeenForciblyEnded"]);
                        objSuc.messageKey = Convert.ToString(resultjson["messageKey"]);
                        objSuc.message = Convert.ToString(resultjson["message"]);
                        */
                    }

                }



                if (strReturnCode == "FAILED")
                {
                    return BadRequest(objErr);
                }
                else
                {
                    return Ok(objJoinReqtUrl);
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }
        [HttpPost]
        [Route("JoinMeetingAsAttendee")]
        public async Task<ActionResult> JoinMeetingAttendee([FromBody] JoinModel model)
        {
            //pass meeting id , patient id, patient name // will get the after the login to the application

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            Random randomObj = new Random();
            try
            {
                string attendeePw = "attendee";
                string userID = "PT" + randomObj.Next(10000000, 100000000).ToString();
                string joineeNameEncode = System.Net.WebUtility.UrlEncode(model.joineeName);
                var strParameters = "fullName=" + joineeNameEncode + "&meetingID=" + model.meetingID
                    + "&password=" + attendeePw
                    + "&joinViaHtml5=true"
                    + "&userdata-bbb_hide_presentation=true"
                    + "&userdata-bbb_show_participants_on_login=false"
                    + "&redirect=true" + "&userID=" + userID;

                var strSha1CheckSum = await GetChecksum("join", strParameters);
                var strMeetingJoinUrl = url + "api/join?" + strParameters + "&checksum=" + strSha1CheckSum;

                var meetingJoinReqtUrl = new MeetingJoinMessage
                {
                    meetingJoinUrl = strMeetingJoinUrl
                };

                return Ok(meetingJoinReqtUrl);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }
        private async Task<string> GetChecksum(string method, string parameters)
        {

            if (parameters == null) parameters = string.Empty;
            return await Sha1Hash(method + parameters, secret);
        }

        public static Task<string> Sha1Hash(string str, string salt = null)
        {
            if (!string.IsNullOrEmpty(salt)) str = str + salt;

            byte[] data = Encoding.UTF8.GetBytes(str);

            var sha1 = SHA1.Create();
            var result = sha1.ComputeHash(data);
            StringBuilder EnText = new StringBuilder();
            foreach (byte iByte in result)
            {
                EnText.AppendFormat("{0:x2}", iByte);
            }
            return Task.FromResult(EnText.ToString());
        }
    }
}
