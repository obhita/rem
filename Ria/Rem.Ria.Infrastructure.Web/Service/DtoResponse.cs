using Agatha.Common;
using Rem.Infrastructure.Service.DataTransferObject;

namespace Rem.Ria.Infrastructure.Web.Service
{
    public class DtoResponse<TDto> : Response, IDtoResponse where TDto : KeyedDataTransferObject
    {
        public TDto DataTransferObject { get; set; }

        #region IDtoResponse Members

        public KeyedDataTransferObject GetDto()
        {
            return DataTransferObject;
        }

        #endregion
    }
}