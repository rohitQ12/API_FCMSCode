namespace GlobalApi.Models.Master
{
    public class Dashboard
    {
        public int student_count { get; set; }
        public int corporate_count { get; set; }
        public int Institution_count { get; set; }
        public int Individual_count { get; set; }
        public int today_student_count { get; set; }
        public int today_Institutestudent_count { get; set; }
        public int today_Corporatestudent_count { get; set; }
        public int today_Institution_count { get; set; }
        public int today_corporate_count { get; set; }
        public int today_Individual_count { get; set; }
        public int total_InstitutionSub_count { get; set; }
        public int today_InstitutionSub_count { get; set; }
        public int total_CorporateSub_count { get; set; }
        public int today_CorporateSub_count { get; set; }
      //  public int total_pending_consultation_count { get; set; }

    }

    //public class Patient_Dashboard_Mobile
    //{
    //    public int portal_appt_count { get; set; }
    //    public int cons_count { get; set; }
    //    public int docu_count { get; set; }

    //}

    //public class Patient_DashAppt_Mobile
    //{
    //    public int? appt_total { get; set; }
    //    public int? appt_pending { get; set; }
    //    public int? appt_rejected { get; set; }
    //    public int? appt_approved { get; set; }
    //   // public int? doc_count { get; set; }
    //}
    //public class Patient_DashCons_Mobile
    //{
    //    public int? cons_total { get; set; }
    //    public int? cons_pending { get; set; }
    //    public int? cons_cancelled { get; set; }
    //    public int? cons_closed { get; set; }

    //}

    //public class Doctor_DashAppt_Mobile
    //{
    //    public int? appt_total { get; set; }
    //    public int? appt_pending { get; set; }
    //    public int? appt_rejected { get; set; }
    //    public int? appt_approved { get; set; }
    //}
    //public class Doctor_DashCons_Mobile
    //{
    //    public int? cons_total { get; set; }
    //    public int? cons_pending { get; set; }
    //    public int? cons_cancelled { get; set; }
    //    public int? cons_closed { get; set; }        

    //}

    //public class CD_Dashboard_Mobile
    //{
    //    public int? cd_id { get; set; }
    //    public string? cd_name { get; set; }
    //    public string? cd_logo { get; set; }

    //}

    //public class CDDoctorDetail_Dash_Mobile
    //{
    //    public int? do_id { get; set; }
    //    public string? doctor_name { get; set; }
    //    public string? photo { get; set; }
    //    public string? email { get; set; }
    //    public long? mobile_number { get; set; }
    //    public string? hospital_name { get; set; }       
    //    public string? discipline { get; set; }
    //    public string? specialization { get; set; }      
    //    public string? qualification { get; set; }        
    //    public string? language_known { get; set; }       
    //    public decimal? consultation_fee { get; set; }
    //    public string? exp_year { get; set; }
    //    public string? star_rating { get; set; }
    //    public List<CDAvailability>? scheduling_detail { get; set; } = null!;

    //}

    //public class CDAvailability
    //{
    //    public int? schd_id { get; set; }
    //    public int? do_id { get; set; }
    //    public string? available_date { get; set; }
    //    public string? available_day { get; set; }
    //    public string? from_time { get; set; }
    //    public string? to_time { get; set; }
    //    public string? doctor_status { get; set; }

    //}

    //public class NoCDdeailFound
    //{
    //    public int? status_code { get; set; }        
    //    public string? status_msg { get; set; }
    //}

    //public class Dashboard_MedicalAssistant
    //{
    //    public int? total_patient_count { get; set; }
    //    public int? today_patient_count { get; set; }
    //    public int? today_phcAppointment_count { get; set; }
    //    public int? total_phcAppointment_count { get; set; }
    //    public int? today_patient_payments_count { get; set; }
    //    public decimal? total_patient_payments_amount { get; set; }
    //    public decimal? today_patient_payments_amount { get; set; }
    //    public int? total_consultation_count { get; set; }
    //    public int? today_completed_consultation_count { get; set; }
    //    public int? today_pending_consultation_count { get; set; }
    //    public int? refund_count { get; set; }

    //}

    //public class Dashboard_Doctor
    //{

    //    public int? total_phcappointment_count { get; set; }
    //    public int? today_approved_appointment_count { get; set; }
    //    public int? today_pending_appointment_count { get; set; }
    //    public int? total_consultation_count { get; set; }
    //    public int? today_completed_consultation_count { get; set; }
    //    public int? today_pending_consultation_count { get; set; }
    //    public decimal? doctor_fee_amount { get; set; }
    //    public decimal? today_doctor_amount { get; set; }

    //}


    //public class Dashboard_Patient
    //{
    //    public int? PR_Id { get; set; }
    //    public int? total_appointment_count { get; set; }
    //    public int? today_approved_appointment_count { get; set; }
    //    public int? today_pending_appointment_count { get; set; }
    //    public int? total_consultation_count { get; set; }
    //    public int? today_completed_consultation_count { get; set; }
    //    public int? today_pending_consultation_count { get; set; }
    //    public decimal? patient_total_amount { get; set; }
    //    public decimal? patient_today_amount { get; set; }

    //}





    //public class patient_Details
    //{
    //    public int? PR_Id { get; set; }
    //    public string? Pr_Name { get; set; }
    //    //public string? PR_LastName { get; set; }
    //    public string? PR_Gender { get; set; }
    //    public string? PR_MobileNumber { get; set; }
    //    public string? PR_Address { get; set; }
    //    public DateTime? PR_RegistrationDateTime { get; set; }
    //    public string? Hos_HospitalName { get; set; }

    //}



    //public class Phc_Appoinment_Details
    //{
    //    public int? Phc_Appt_Id { get; set; }
    //    public string? Pr_Name { get; set; }
    //    public string? DO_Name { get; set; }
    //    public string? selectedFrmtime { get; set; }
    //    public string? selectedTotime { get; set; }
    //    public string? PatientCode { get; set; }
    //    public string? pa_Hospitalname { get; set; }
    //}


    //public class Payment_Details
    //{
    //    public string? order_id { get; set; }
    //    public string? Pr_Name { get; set; }
    //    public string? DO_Name { get; set; }
    //    public decimal? amount { get; set; }
    //    public string? order_status { get; set; }
    //    public string? Hos_HospitalName { get; set; }
    //    public string? PR_PatientCode { get; set; }
    //    public string? trans_date { get; set; }
    //}


    //public class Refund_Details
    //{

    //    public string? tracking_id { get; set; }
    //    public string? refund_ref_no { get; set; }
    //    public DateTime? refund_date { get; set; }
    //    public decimal? refund_amount { get; set; }
    //    public string? sts_name { get; set; }
    //    public string? Pr_Name { get; set; }
    //    public string? Hos_HospitalName { get; set; }
    //    public string? PR_PatientCode { get; set; }
    //}


}
