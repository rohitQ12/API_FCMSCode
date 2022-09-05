using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GlobalApi.Models.VC
{
    public class MeetingRequest
    {

        public string AttendeePW { get; set; }
        public string ModeratorPW { get; set; }
        public string UserId { get; set; }

    }


    public class JoinModel
    {
        public string meetingID { get; set; }
        public string joineeName { get; set; }

    }

    public class StartModel
    {
        public string doctorID { get; set; }

    }

    public class CreateMeetingModel
    {
        [Required]
        public string meetingName { get; set; }
        public int meetingDuration { get; set; } = 0;
    }


    public class MeetingJoinMessage
    {
        public string meetingJoinUrl { get; set; }

    }

    public class CreateMeetingSuccessMessage
    {
        public string returncode { get; set; }
        public string meetingID { get; set; }
        public string meetingName { get; set; }
        public string doctorID { get; set; }
        public string internalMeetingID { get; set; }
        public string parentMeetingID { get; set; }
        public string attendeePW { get; set; }
        public string moderatorPW { get; set; }
        public string createTime { get; set; }
        public int? voiceBridge { get; set; }
        public string dialNumber { get; set; }
        public string createDate { get; set; }
        public bool? hasUserJoined { get; set; }
        public int? duration { get; set; }
        public bool? hasBeenForciblyEnded { get; set; }
        public string messageKey { get; set; }
        public string message { get; set; }

    }

    public class GeneralErrorMessage
    {
        public string returncode { get; set; }
        public string messagekey { get; set; }
        public string message { get; set; }

    }


    public class IsMeetingRunningRequestMessage
    {
        public string returncode { get; set; }
        public bool running { get; set; }

    }

    public class SingleMeetingInfo
    {
        public string returncode { get; set; }
        public string meetingName { get; set; }
        public string meetingID { get; set; }
        public string internalMeetingID { get; set; }
        public string createTime { get; set; }
        public string createDate { get; set; }
        public int? voiceBridge { get; set; }
        public string dialNumber { get; set; }
        public string attendeePW { get; set; }
        public string moderatorPW { get; set; }
        public bool? running { get; set; }
        public int? duration { get; set; }
        public bool? hasUserJoined { get; set; }
        public bool? recording { get; set; }
        public bool? hasBeenForciblyEnded { get; set; }
        public string startTime { get; set; }
        public int? endTime { get; set; }
        public int? participantCount { get; set; }
        public int? listenerCount { get; set; }
        public int? voiceParticipantCount { get; set; }
        public int? videoCount { get; set; }
        public int? maxUsers { get; set; }
        public int? moderatorCount { get; set; }
        public SingleAttendees attendees { get; set; }
        public bool? isBreakout { get; set; }
    }

    public class SingleAttendees
    {
        public Attendee attendee { get; set; }

    }

    public class MultipleMeetingInfo
    {
        public string returncode { get; set; }
        public string meetingName { get; set; }
        public string meetingID { get; set; }
        public string internalMeetingID { get; set; }
        public string createTime { get; set; }
        public string createDate { get; set; }
        public int? voiceBridge { get; set; }
        public string dialNumber { get; set; }
        public string attendeePW { get; set; }
        public string moderatorPW { get; set; }
        public bool? running { get; set; }
        public int? duration { get; set; }
        public bool? hasUserJoined { get; set; }
        public bool? recording { get; set; }
        public bool? hasBeenForciblyEnded { get; set; }
        public string startTime { get; set; }
        public int? endTime { get; set; }
        public int? participantCount { get; set; }
        public int? listenerCount { get; set; }
        public int? voiceParticipantCount { get; set; }
        public int? videoCount { get; set; }
        public int? maxUsers { get; set; }
        public int? moderatorCount { get; set; }
        public Attendees attendees { get; set; }
        public bool? isBreakout { get; set; }
    }

    public class Attendees
    {
        public Attendee[] attendee { get; set; }

    }

    //below seq used for get meetings - all
    public class SingleMeetingOneJoinee
    {
        public string returncode { get; set; }
        public SMeetings meetings { get; set; }

    }
    public class SMeetings
    {
        public SMeeting meeting { get; set; }

    }

    public class SMeeting
    {

        public string meetingName { get; set; }
        public string meetingID { get; set; }
        public string internalMeetingID { get; set; }
        public string createTime { get; set; }
        public string createDate { get; set; }
        public int? voiceBridge { get; set; }
        public string dialNumber { get; set; }
        public string attendeePW { get; set; }
        public string moderatorPW { get; set; }
        public bool? running { get; set; }
        public int? duration { get; set; }
        public bool? hasUserJoined { get; set; }
        public bool? recording { get; set; }
        public bool? hasBeenForciblyEnded { get; set; }
        public string startTime { get; set; }
        public int? endTime { get; set; }
        public int? participantCount { get; set; }
        public int? listenerCount { get; set; }
        public int? voiceParticipantCount { get; set; }
        public int? videoCount { get; set; }
        public int? maxUsers { get; set; }
        public int? moderatorCount { get; set; }
        public AttendeeSingle attendees { get; set; }
        public bool? isBreakout { get; set; }
    }

    public class AttendeeSingle
    {
        public Attendee attendee { get; set; }

    }

    public class AttendeeMultiple
    {
        public Attendee[] attendee { get; set; }

    }

    public class Attendee
    {
        public string userID { get; set; }
        public string fullName { get; set; }
        public string role { get; set; }
        public bool? isPresenter { get; set; }
        public bool? isListeningOnly { get; set; }
        public bool? hasJoinedVoice { get; set; }
        public bool? hasVideo { get; set; }
        public string clientType { get; set; }

    }


    public class SingleMeetingMultipleJoinee
    {
        public string returncode { get; set; }
        public MMeetings meetings { get; set; }

    }

    public class MMeetings
    {
        public MMeeting meeting { get; set; }

    }

    public class MMeeting
    {

        public string meetingName { get; set; }
        public string meetingID { get; set; }
        public string internalMeetingID { get; set; }
        public string createTime { get; set; }
        public string createDate { get; set; }
        public int? voiceBridge { get; set; }
        public string dialNumber { get; set; }
        public string attendeePW { get; set; }
        public string moderatorPW { get; set; }
        public bool? running { get; set; }
        public int? duration { get; set; }
        public bool? hasUserJoined { get; set; }
        public bool? recording { get; set; }
        public bool? hasBeenForciblyEnded { get; set; }
        public string startTime { get; set; }
        public int? endTime { get; set; }
        public int? participantCount { get; set; }
        public int? listenerCount { get; set; }
        public int? voiceParticipantCount { get; set; }
        public int? videoCount { get; set; }
        public int? maxUsers { get; set; }
        public int? moderatorCount { get; set; }
        public AttendeeMultiple attendees { get; set; }
        public bool? isBreakout { get; set; }
    }

    public class EndMeetingMessage
    {
        public string message_key { get; set; }
        public string message { get; set; }
        public string return_code { get; set; }

    }

}
