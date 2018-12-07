using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SADM.Views
{ 
	public partial class PaymentBanamex : PageBase
    {
		public PaymentBanamex ()
		{
			InitializeComponent ();
            wvBanamex.Source = "https://banamex.dialectpayments.com/vpcpay?vpc_Amount=102800&vpc_Version=1&vpc_OrderInfo=50000008&vpc_Command=pay&vpc_Currency=MXN&vpc_Merchant=TEST1008073&vpc_ReturnAuthResponseData=N&vpc_ReturnURL=http%3A%2F%2Flocalhost%3A8080%2FaydB%2FpaymentController&vpc_SecureHash=DE45F717A8C5A4BB6C7AA205BF4CEC9054D6627CD757C535C54AA78B7E290F23&vpc_SecureHashType=SHA256&vpc_AccessCode=B3B33E35&vpc_MerchTxnRef=67ff3b50%3A16659de4b9d%3A-8000&vpc_Locale=es_MX";
		}
	}
}