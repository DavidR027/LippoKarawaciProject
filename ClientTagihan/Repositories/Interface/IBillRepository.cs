using ClientTagihan.Models;
using ClientTagihan.ViewModels;

namespace ClientTagihan.Repositories.Interface
{
    public interface IBillRepository : IGeneralRepository<Bill, Guid>
    {
        public Task<ResponseMessageVM> PayBills(PayBill entity);
        public Task<ResponseListVM<Penalty>> Penalties(DateTime dateNow);
    }
}
