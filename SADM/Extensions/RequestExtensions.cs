using System;
using System.Collections.Generic;
using System.Linq;
using SADM.Models.Requests;

namespace SADM.Extensions
{
    public static class RequestExtensions
    {
        public static IList<string> GetErrorList<V>(this V request) where V : RequestBase
        {
            if (request is SignUpRequest)
            {
                return GetErrorList(request as SignUpRequest);
            }
            if (request is UpdateUserRequest)
            {
                return GetErrorList(request as UpdateUserRequest);
            }

            if (request is LoginRequest)
            {
                return GetErrorList(request as LoginRequest);
            }

            if (request is RecoverPasswordRequest)
            {
                return null;
            }

            if (request is RecoverPasswordRequest)
            {
                return GetErrorList(request as RecoverPasswordRequest);
            }

            if (request is PasswordRecoveryRequest)
            {
                return GetErrorList(request as PasswordRecoveryRequest);
            }
            if (request is AddContractRequest)
            {
                return GetErrorList(request as AddContractRequest);
            }
            if (request is RemoveContractRequest)
            {
                return GetErrorList(request as RemoveContractRequest);
            }
            if (request is GetContractListRequest)
            {
                return null;
            }
            if (request is AddReportRequest)
            {
                return GetErrorList(request as AddReportRequest);
            }

            if (request is GetReportListRequest)
            {
                return null;
            }
            if (request is UpdateMyDataRequest)
            {
                return null;
            }
            if (request is GetBillListRequest)
            {
                return null;
            }
            if (request is UpdateBillDeliveryConfigurationRequest)
            {
                return null;
            }
            throw new NotImplementedException($"Validation not implemented for {request}, check RequestExtensions class.");
        }

        public static IList<string> GetErrorList(this SignUpRequest request)
        {
            var errorList = new List<string>();
            if (request.Nir.GetNirError() is String nirError)
            {
                errorList.Add(nirError);
            }
            if (request.PreviousReading.GetPreviousReadingError() is String previousReadingError)
            {
                errorList.Add(previousReadingError);
            }
            return errorList.Any() ? errorList : null;
        }

        public static IList<string> GetErrorList(this UpdateUserRequest request)
        {
            var errorList = new List<string>();

            return errorList.Any() ? errorList : null;
        }

        public static IList<string> GetErrorList(this LoginRequest request)
        {
            var errorList = new List<string>();
            if(request.Email.GetEmailError() is String emailError)
            {
                errorList.Add(emailError);
            }
            if (request.Password.GetPasswordError() is String passwordError)
            {
                errorList.Add(passwordError);
            }
            return errorList.Any() ? errorList : null;
        }

        public static IList<string> GetErrorList(this PasswordRecoveryRequest request)
        {
            var errorList = new List<string>();
            if (request.Email.GetEmailError() is String emailError)
            {
                errorList.Add(emailError);
            }
            return errorList.Any() ? errorList : null;
        }

        public static IList<string> GetErrorList(this AddContractRequest request)
        {
            var errorList = new List<string>();
            if (request.Nir.GetNirError() is String nirError)
            {
                errorList.Add(nirError);
            }
            if (request.PreviousReading.GetPreviousReadingError() is String previousReadingError)
            {
                errorList.Add(previousReadingError);
            }
            return errorList.Any() ? errorList : null;
        }

        public static IList<string> GetErrorList(this RemoveContractRequest request)
        {
            var errorList = new List<string>();
            //if (request.Nir.GetNirError() is String nirError)
            //{
            //    errorList.Add(nirError);
            //}
            //if (request.PreviousReading.GetPreviousReadingError() is String previousReadingError)
            //{
            //    errorList.Add(previousReadingError);
            //}
            return errorList.Any() ? errorList : null;
        }

        public static IList<string> GetErrorList(this AddReportRequest request)
        {
            var errorList = new List<string>();
            switch (request.Type)
            {
                case Enums.ReportType.FugueInMyAddress:
                    if (request.Nis == null)
                    {
                        errorList.Add("Debe seleccionar un Nis");
                    }
                    break;
                case Enums.ReportType.FugueInAnotherAddress:
                    if (string.IsNullOrWhiteSpace(request.Report.Street))
                    {
                        errorList.Add("La calle es requerida.");
                    }
                    if (string.IsNullOrWhiteSpace(request.Report.Number))
                    {
                        errorList.Add("El número es requerido");
                    }
                    if (string.IsNullOrWhiteSpace(request.Report.Colony))
                    {
                        errorList.Add("La colonia es requerida");
                    }
                    if (string.IsNullOrWhiteSpace(request.Report.PostalCode))
                    {
                        errorList.Add("El código postal es requerido");
                    }
                    if (string.IsNullOrWhiteSpace(request.Report.References))
                    {
                        errorList.Add("Las referencias son requeridas");
                    }
                    break;
                case Enums.ReportType.FugueInAnotherLocation:
                    if (request.Report.Latitude == null || request.Report.Longitude == null)
                    {
                        errorList.Add("Debe seleccionar un punto en el mapa");
                    }
                    break;
                default:
                    throw new ArgumentException($"unknown request type {request.Type}");
            }

            if (string.IsNullOrWhiteSpace(request.Report.Comments))
            {
                errorList.Add("Los comentarios adicionales son requeridos.");
            }
            return errorList.Any() ? errorList : null;
        }
    }
}
