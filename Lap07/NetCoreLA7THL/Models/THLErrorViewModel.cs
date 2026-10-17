// Author: THL

namespace NetCoreLA7THL.Models
{
    public class THLErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
