using System;
using System.Collections.Generic;
using System.IO;
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
                switch (request.GetType().Name)
                {
                    case nameof(ObtenerUrlPagosRequest):
                        response = await ProcessCallPagosAsync(request as ObtenerUrlPagosRequest) as V; 
                        break;
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
                        response = new GetBillListResponse { BillList = billList } as V;
                        break;
                    case nameof(UpdateBillDeliveryConfigurationRequest):
                        var respAux = await SadmApi.UpdateBillDeliveryConfiguration(request as UpdateBillDeliveryConfigurationRequest);
                        var success = respAux == "\"1\"";
                        response = new UpdateBillDeliveryConfigurationResponse { Result = success } as V;
                        break;
                    default:
                        throw new NotImplementedException();
                }
            }
            catch (Exception ex)
            {
                response.AddError("Ocurrió un error, vuelve a intentarlo más tarde.");
            }
            return response;
        }
        public async Task<ResponseBase> ProcessCallPagosAsync(ObtenerUrlPagosRequest request)
        {
            var temp = await SadmApi.ObtenerUrlPagos(request);
            var resultado = Newtonsoft.Json.JsonConvert.DeserializeObject<ObtenerUrlPagosResponse>(temp);

            var response = new ObtenerUrlPagosResponse();
            if (String.IsNullOrEmpty(temp))
                response.AddError("Correo / Contraseña no validos. Revise sus datos y vuelva a intentarlo.");
            else
            {
                response.Url = resultado.Url;
            }
            // else if(string.IsNullOrEmpty(resultado.Registro_de_Usuarioss)
            return response;

        }
        public async Task<ResponseBase> ProcessCallAsync(LoginRequest request)
        {
            var temp = await SadmApi.LogInStr(request);
            var resultado = Newtonsoft.Json.JsonConvert.DeserializeObject<Registro_de_UsuariosPagingModel>(temp);

            var response = new LoginResponse();
            if (resultado.Registro_de_Usuarioss == null)
                response.AddError("Correo / Contraseña no válidos. Revise sus datos y vuelva a intentarlo.");
            else
            {
                foreach (var r in resultado.Registro_de_Usuarioss)
                {
                    response.Activo = r.Activo;
                    response.Apellido_Materno = r.Apellido_Materno;
                    response.Apellido_Paterno = r.Apellido_Materno;
                    response.Nombre = r.Nombre;
                    response.Correo = r.Correo;
                    response.Calle = r.Calle;
                    response.Ciudad = r.Ciudad;
                    response.Clave_de_acceso = r.Clave_de_acceso;
                    response.Codigo_Postal = r.Codigo_Postal;
                    response.Colonia = r.Colonia;
                    response.Contrasena = r.Contrasena;
                    response.Correo = r.Correo;
                    response.Estado = r.Estado;
                    response.Fecha_de_Registro = r.Fecha_de_Registro;
                    response.Folio = r.Folio;
                    response.Hora_de_Registro = r.Hora_de_Registro;
                    response.lastReading = r.lastReading;
                    response.Lec = r.Lec;
                    response.Numero = r.Numero;
                    response.Pregunta_de_seguridad = r.Pregunta_de_seguridad;
                    response.Respuesta_de_seguridad = r.Respuesta_de_seguridad;
                    response.Rol = r.Rol;
                    response.Usuario_que_Registra = r.IdSpartanUser;
                }
            }
            // else if(string.IsNullOrEmpty(resultado.Registro_de_Usuarioss)
            return response;

        }

        public async Task<T> SimulateSuccessfulAsync<T>() where T : new()
        {
            await Task.Delay(300);
            return new T();
        }
    }
}
