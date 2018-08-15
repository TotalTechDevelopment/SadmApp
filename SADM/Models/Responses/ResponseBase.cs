using System.Collections.Generic;

namespace SADM.Models.Responses
{
    public class ResponseBase
    {
        public bool Success => ErrorList == null || ErrorList.Count == default(int);
        public IList<string> ErrorList { get; set; }
        public string Error { get; set; }

        public void AddError(string error)
        {
            if(error != null)
            {
                if (ErrorList is null)
                {
                    ErrorList = new List<string>();
                }
                ErrorList.Add(error);
            }
        }
    }
}
