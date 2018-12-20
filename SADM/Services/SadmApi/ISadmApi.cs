using System.Threading.Tasks;
using Refit;
using SADM.Models;
using SADM.Models.Requests;
using SADM.Models.Responses;

namespace SADM.Services
{
    public interface ISadmApi
    {
        [Post("/oauth/token")]
        Task<GetAppTokenResponse> GetAppToken([Body(BodySerializationMethod.UrlEncoded)] GetAppTokenRequest request);

        [Post("/api/Registro_de_usuarios/Post")]
        [Headers("Id_User: 1", "Authorization: Bearer")]
        Task<string> SignUp([Body(BodySerializationMethod.Json)]SignUpRequest request);

        [Post("/api/Registro_de_usuarios/UpdateUser")]
        [Headers("Id_User: 1", "Authorization: Bearer")]
        Task<string> UpdateUser([Body(BodySerializationMethod.Json)]UpdateUserRequest request);

        [Get("/api/Pagos/ObtenerUrlPagos")]
        Task<string> ObtenerUrlPagos(ObtenerUrlPagosRequest request);

        [Get("/api/Registro_de_usuarios/PasswordRecover")]
        Task<string> RecoverPassword(RecoverPasswordRequest request);

        [Post("/api/Registro_de_usuarios/Post")]
        [Headers("Id_User: 1", "Authorization: Bearer")]
        Task<string> Update([Body(BodySerializationMethod.Json)]UpdateMyDataRequest request);

        [Get("/api/Registro_de_usuarios/ListaSelAllApp")]
        [Headers("Authorization: Bearer")]
        Task<Spartan_User> LogIn(LoginRequest request);
        [Get("/api/Registro_de_usuarios/ListaSelAllApp")]
        [Headers("Authorization: Bearer")]
        Task<string> LogInStr(LoginRequest request);

        [Get("/api/SUMCON/ListaSelAll")]
        [Headers("Authorization: Bearer")]
        Task<string> GetBalanceList(GetContractListRequest request);

        [Post("/api/SUMCON/Post")]
        [Headers("Id_User: 1", "Authorization: Bearer")]
        Task<string> AddNis([Body(BodySerializationMethod.Json)]AddContractRequest request);

        [Post("/api/SUMCON/Post")]
        [Headers("Id_User: 1", "Authorization: Bearer")]
        Task<string> RemoveNis([Body(BodySerializationMethod.Json)]RemoveContractRequest request);

        [Post("/api/PAGOS/RealizarPago")]
        [Headers("Id_User: 1", "Authorization: Bearer")]
        Task<string> RealizarPago([Body(BodySerializationMethod.Json)]PAGOSRequest request);

        [Get("/api/Envio_de_Facturas/ListaSelAll")]
        [Headers("Authorization: Bearer")]
        Task<string> GetBillList(GetBillListRequest request);

        [Post("/api/Reporte_de_Incidencias/Post")]
        [Headers("Id_User: 1", "Authorization: Bearer")]
        Task<string> AddReport([Body(BodySerializationMethod.Json)]Report report);

        [Get("/api/Reporte_de_Incidencias/ListaSelAll?startRowIndex=1&maximumRows=100&where=Reporte_de_Incidencias.Usuario_que_Registra='{idUser}'")]
        [Headers("Authorization: Bearer")]
        Task<GetReportListResponse> GetReportList(long idUser);

        [Get("/api/recibos/Put")]
        [Headers("Authorization: Bearer")]
        Task<string> UpdateBillDeliveryConfiguration(UpdateBillDeliveryConfigurationRequest request);
    }
}