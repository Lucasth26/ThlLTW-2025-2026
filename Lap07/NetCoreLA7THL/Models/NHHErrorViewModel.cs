// Author: NHH

namespace NetCoreLA7NHH.Models
{
    public class NHHErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
