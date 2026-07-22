using GlobalApi.Data;
using GlobalApi.IRepository.MasterIReopsitory;
using GlobalApi.Models.Master;
using Microsoft.EntityFrameworkCore;

namespace GlobalApi.Repository.MasterReopsitory
{
    public class feedbackRespository : Ifeedback
    {
        private readonly GlobalContext _db;

        public feedbackRespository(GlobalContext db)
        {
            _db = db;
        }
        public async Task<List<GetAllfeedback>> GetFeedback()
        {
            if (_db != null)
            {
                var query = (from a in _db.feedback
                             where a.feedback_id!=0
                             select new GetAllfeedback
                             {
                                 feedback_id=a.feedback_id,
                                 comment=a.comment,
                             }).ToListAsync();
                return await query;
            }
            return null;

        }

        public Task<feedback> updateFeedback(int lead)
        {
            throw new NotImplementedException();
        }
    }
}
