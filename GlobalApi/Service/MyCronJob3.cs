//using GlobalApi.GlobalClasses;
//using GlobalApi.Data;
//using System.Globalization;

//namespace GlobalApi.Service
//{
//    public class MyCronJob3 : CronJobService
//    {
//        private readonly ILogger<MyCronJob3> _logger;
//        ISMSService objSMSService;
//        private readonly GlobalContext db = null!;
//        public MyCronJob3(IScheduleConfig<MyCronJob3> config, ILogger<MyCronJob3> logger)
//            : base(config.CronExpression, config.TimeZoneInfo)
//        {
//            _logger = logger;
//            this.objSMSService = new SMSService();
//            this.db = new GlobalContext();

//        }

//        public override Task StartAsync(CancellationToken cancellationToken)
//        {
//            _logger.LogInformation("CronJob 3 starts.");
//            return base.StartAsync(cancellationToken);
//        }

//        public override Task DoWork(CancellationToken cancellationToken)
//        {
//            _logger.LogInformation($"{DateTime.Now:hh:mm:ss} CronJob 3 is working.");
//            string Day = Convert.ToString(DateTime.Now.DayOfWeek);
//            string Date = DateTime.Now.ToString("yyyy-MM-dd");
//            var objDoctor = this.db.Doctor.Where(x => x.status != 5 && x.status != 6 && x.status != 7);
//            foreach(var d in objDoctor.ToList())
//            {
//                var objDoctor_Schedules = this.db.Doctor_Schedules.Where(x => x.Do_Scd_day == Day && x.DO_Id_FK == d.DO_Id);
//                if(objDoctor_Schedules.Count() > 0)
//                {
//                    foreach (var s in objDoctor_Schedules.ToList())
//                    {
//                        var objPatientAppointment = this.db.PatientAppointment.Where(x => x.Appt_DO_Id_FK == s.DO_Id_FK && x.status == 3 && x.Select_day == Date);
//                        if (objPatientAppointment.Count() > 0)
//                        {
//                            string Scheduled_Time = DateTime.Now.ToString("dd-MM-yyyy") +" "+ DateTime.ParseExact(s.Time_from, "hh:mm tt", CultureInfo.CurrentCulture).ToString("HH:mm")+":00";
//                            string RequestBody = "{"
//                                              + "\"Header\": \"" + "CnMDTL" + "\","
//                                              + "\"Target\": \"" + d.DO_MobileNumber + "\","
//                                              + "\"Is_Unicode\": \"" + "0" + "\","
//                                              + "\"Is_Flash\": \"" + "0" + "\","
//                                              + "\"Message_Type\": \"" + "SI" + "\","
//                                              + "\"Entity_Id\": \"" + "1401453890000057141" + "\","
//                                              + "\"Content_Template_Id\": \"" + "1407168188304298673" + "\","
//                                              + "\"Consent_Template_Id\": \"" + "" + "\","
//                                              + "\"Scheduled_Time\": \"" + Scheduled_Time + "\","
//                                              + "\"Template_Keys_and_Values\": " + "[{"
//                                                    + "\"Key\": \"" + "number" + "\","
//                                                    + "\"Value\": \"" + objPatientAppointment.Count() + "\""
//                                              + "}]"
//                                              + "}";
//                            bool SendMessage = this.objSMSService.SendScheduledMessage(RequestBody);
//                        }
//                    }
//                }
                
//            }
//            var data = this.db.Doctor_Schedules.Where(x => x.Do_Scd_day == DateTime.Now.DayOfWeek.ToString());
            
//            return Task.CompletedTask;
//        }

//        public override Task StopAsync(CancellationToken cancellationToken)
//        {
//            _logger.LogInformation("CronJob 3 is stopping.");
//            return base.StopAsync(cancellationToken);
//        }
//    }
//}
