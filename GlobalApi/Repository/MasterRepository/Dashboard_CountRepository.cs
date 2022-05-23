using GlobalApi.Data;
using GlobalApi.IRepository.MasterIRepository;
using GlobalApi.Models.Master;

namespace GlobalApi.Repository.MasterRepository
{
    public class Dashboard_CountRepository : IDashboard_Count
    {
        private readonly GlobalContext db;
        public Dashboard_CountRepository()
        {
            db = new GlobalContext();

        }

        public int GetPatient_Count()
        {
            try
            {
                if (db != null)
                {
                    var query = (from a in db.Patient
                                 where a.status !=6
                                 select 1).Count();
                    return query;
                }
                return 0;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
        public int GetNetworkHospital_Count()
        {
            try
            {
                if (db != null)
                {
                    var query = (from a in db.Network
                                 where a.status != 6
                                 select 1).Count();
                    return query;
                }
                return 0;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
        public int GetHospital_Count()
        {
            try
            {
                if (db != null)
                {
                    var query = (from a in db.Hospital
                                 where a.status != 6
                                 select 1).Count();
                    return query;
                }
                return 0;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
        public int GetPharmacy_Count()
        {
            try
            {
                if (db != null)
                {
                    var query = (from a in db.Pharmacy
                                 where a.status != 6
                                 select 1).Count();
                    return query;
                }
                return 0;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
        public int GetDiagnostic_Count()
        {
            try
            {
                if (db != null)
                {
                    var query = (from a in db.DiagnosticCenters
                                 where a.status != 6
                                 select 1).Count();
                    return query;
                }
                return 0;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
        public int GetTotalAppointment_Count()
        {
            try
            {
                if (db != null)
                {
                    var query = (from a in db.PatientAppointment
                                 where a.status != 6
                                 select 1).Count();
                    return query;
                }
                return 0;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
        public int GetTodayAppointment_Count()
        {
            try
            {
                if (db != null)
                {
                    var query = (from a in db.PatientAppointment
                                 where a.status != 6
                                 select 1).Count();
                    return query;
                }
                return 0;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
        public int GetTodayConsultation_Count()
        {
            try
            {
                if (db != null)
                {
                    var query = (from a in db.Consultation
                                 where a.status != 6
                                 select 1).Count();
                    return query;
                }
                return 0;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
        public int GetTotalConsultation_Count()
        {
            try
            {
                if (db != null)
                {
                    var query = (from a in db.Consultation
                                 where a.status != 6
                                 select 1).Count();
                    return query;
                }
                return 0;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
        public int Getreferal_Count()
        {
            try
            {
                if (db != null)
                {
                    var query = (from a in db.SHReferrals
                                 where a.status != 6
                                 select 1).Count();
                    return query;
                }
                return 0;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
    }
}
