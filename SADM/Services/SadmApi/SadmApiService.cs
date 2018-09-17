using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Refit;
using SADM.Helpers;
using SADM.Models;
using SADM.Models.Requests;
using SADM.Models.Responses;

namespace SADM.Services
{
    public class SadmApiService : ISadmApiService
    {
        protected const string REPORT_LIST_WHERE = "Reporte_de_Incidencias.Usuario_que_Registra='{0}'";

        protected ISettingsService SettingsService { get; private set; }
        protected IHudService HudService { get; private set; }
        protected ISadmApi SadmApi { get; private set; }

        public SadmApiService(ISettingsService settingsService, IHudService hudService)
        {
            SettingsService = settingsService;
            HudService = hudService;
            SadmApi = RestService.For<ISadmApi>(new HttpClient(new AuthenticationHelper(this, SettingsService)) 
            { 
                BaseAddress = new Uri(Settings.AppConfiguration.Values.BaseUrl)
            });
        }

        public async Task<V> CallServiceAsync<U, V>(U request) where U : RequestBase where V : ResponseBase, new()
        {
             var response = new V();
            try
            {
                switch(request.GetType().Name)
                {
                    case nameof(GetAppTokenRequest):
                        response = await SadmApi.GetAppToken(request as GetAppTokenRequest) as V;
                        break;
                    case nameof(SignUpRequest):
                        response = new SignUpResponse { Token = (await SadmApi.SignUp(request as SignUpRequest)).Replace("\"", string.Empty) } as V;
                        break;
                    case nameof(RecoverPasswordRequest):
                        response = new RecoverPasswordResponse { Message = (await SadmApi.RecoverPassword(request as RecoverPasswordRequest)) } as V;
                        break;
                    case nameof(UpdateUserRequest):
                        response = new UpdateUserResponse { Message = (await SadmApi.UpdateUser(request as UpdateUserRequest)).Replace("\"", string.Empty) } as V;
                        break;
                    case nameof(LoginRequest):
                        response = await ProcessCallAsync(request as LoginRequest) as V;
                        break;
                    case nameof(PasswordRecoveryRequest):
                        response = await SimulateSuccessfulAsync<PasswordRecoveryResponse>() as V;
                        break;
                    case nameof(AddContractRequest):
                        response = new AddContractResponse { ContractId = (await SadmApi.AddNis(request as AddContractRequest)).Replace("\"", string.Empty) } as V;
                        break;
                   case nameof(RemoveContractRequest):
                        response = new RemoveContractResponse { ContractId = (await SadmApi.RemoveNis(request as RemoveContractRequest)).Replace("\"", string.Empty) } as V;
                        break;
                    case nameof(GetContractListRequest):
                        var tmp = await SadmApi.GetBalanceList(request as GetContractListRequest);
                        tmp = tmp.Substring(1, tmp.Length - 2);
                        tmp = tmp.Replace("\\", string.Empty);
                        var contractList = Newtonsoft.Json.JsonConvert.DeserializeObject<IList<Balance>>(tmp);
                        response = new GetBalanceListResponse { BalanceList = contractList } as V;
                        break;
                    case nameof(AddReportRequest):
                        response = new AddReportResponse { ReportId = (await SadmApi.AddReport((request as AddReportRequest).Report)).Replace("\"", string.Empty) } as V;
                        break;
                    case nameof(GetReportListRequest):
                        response = await SadmApi.GetReportList((request as GetReportListRequest).UserId) as V;
                        break;
                    case nameof(UpdateMyDataRequest):
                        var updateMyDataRequest = await SadmApi.Update(request as UpdateMyDataRequest);
                        response = new UpdateMyDataResponse() as V;
                        break;
                    case nameof(GetBillListRequest):
                        var responseAux = await SadmApi.GetBillList(request as GetBillListRequest);
                        var billList = Newtonsoft.Json.JsonConvert.DeserializeObject<IList<Bill>>(responseAux);
                        response = new GetBillListResponse{ BillList = billList } as V;
                        break;
                    case nameof(UpdateBillDeliveryConfigurationRequest):
                        var respAux = await SadmApi.UpdateBillDeliveryConfiguration(request as UpdateBillDeliveryConfigurationRequest);
                        var success = respAux == "\"1\"";
                        response = new UpdateBillDeliveryConfigurationResponse{ Result = success } as V;
                        break;
                    default:
                        throw new NotImplementedException();
                }
            }
            catch
            {
                response.AddError("Ocurrió un error, vuelve a intentarlo más tarde.");
            }
            return response;
        }

        public async Task<ResponseBase> ProcessCallAsync(LoginRequest request)
        {
            var response = await SadmApi.LogIn(request);
            if(string.IsNullOrEmpty(response.Name))
            {
                response.AddError("Revisa tus credenciales y vuelve a intentarlo.");
            }
            return response;
        }

        public async Task<T> SimulateSuccessfulAsync<T>() where T : new()
        {
            await Task.Delay(3000);
            return new T();
        }
    }
}
