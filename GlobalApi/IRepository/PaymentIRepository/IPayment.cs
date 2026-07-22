using GlobalApi.Models.Master;
using GlobalApi.Models.Payment;
namespace GlobalApi.IRepository.PaymentIRepository
{
    public interface IPayment
    {

        Task<MerchantOrderModels> ProcessMerchantOrder(CustomerOrderData customerOrderData, string razorpay_key, string razorpay_secret);
        Task<bool> SaveMerchantOrderData(MerchantOrderModels merchant_order);
        Task<CustomerOrderData> GetUsersOrderDetailById(string users_order_id);
        Task<int> RoundCourseAmount(string course_amount);
        Task<string> CapitalizeFirstLetter(string input);
        Task<string> CancelOrderProcess(string users_order_id);
        Task<string> CompleteOrderProcess(VerifySignature verifysignature, string razorpay_key, string razorpay_secret);

        Task<bool> SaveMerchantPayment(MerchantPaymentModels payment);
        Task<MerchantPaymentModels> GetUsersPaymentDetailById(string razorpay_payment_id,string razorpay_key, string razorpay_secret);
        Task<MerchantOrderModels> ProcessMerchantOrder_Ind(CustomerOrderData_Ind customerOrderData, string razorpay_key, string razorpay_secret);

    }
}
