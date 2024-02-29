using ClientTagihan.Models;
using ClientTagihan.Repositories.Interface;
using ClientTagihan.ViewModels;
using Newtonsoft.Json;
using System.Text;

namespace ClientTagihan.Repositories.Data;

public class BillRepository : GeneralRepository<Bill, Guid>, IBillRepository
{
    private readonly HttpClient httpClient;
    private readonly string request;
    public BillRepository(string request = "Bills/") : base(request)
    {
        httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:7236/api/")
        };
        this.request = request;
    }

    public async Task<ResponseMessageVM> PayBills(PayBill entity)
    {
        ResponseMessageVM entityVM = null;
        StringContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
        using (var response = httpClient.PostAsync(request + "Pay", content).Result)
        {
            string apiResponse = await response.Content.ReadAsStringAsync();
            entityVM = JsonConvert.DeserializeObject<ResponseMessageVM>(apiResponse);
        }
        return entityVM;
    }

    public async Task<ResponseListVM<Penalty>> Penalties(DateTime dateNow)
    {
        ResponseListVM<Penalty> entityVM = null;
        using (var response = httpClient.GetAsync(request + "Penalties/" + dateNow).Result)
        {
            string apiResponse = await response.Content.ReadAsStringAsync();
            entityVM = JsonConvert.DeserializeObject<ResponseListVM<Penalty>>(apiResponse);
        }
        return entityVM;
    }

}
