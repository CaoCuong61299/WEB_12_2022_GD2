using AMISAPP_MISA.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMISAPP_MISA.Common.Entities.DTO
{
    public class ErrorResult
    {
        #region property

        /// <summary>
        /// Mã lỗi
        /// </summary>
        public ErrorCode ErrorCode { get; set; }

        /// <summary>
        /// Message lỗi cho dev
        /// </summary>
        public string DevMsg { get; set; }

        /// <summary>
        /// Message lỗi cho người dùng
        /// </summary>
        public string UserMsg { get; set; }

        /// <summary>
        /// Thông tin về lỗi
        /// </summary>
        public object MoreInfo { get; set; }

        /// <summary>
        /// TraceId lỗi
        /// </summary>
        public string TraceId { get; set; }
        #endregion
        #region Contructor

        /// <summary>
        /// Constructor không tham số
        /// </summary>
        /// Author: NCCONG (09/02/2023)
        public ErrorResult() { }

        /// <summary>
        /// Constructor có tham số
        /// </summary>
        /// <param name="errorCode">Mã lỗi</param>
        /// <param name="devMsg">Tin nhắn lỗi cho Dev</param>
        /// <param name="userMsg">Tin nhắn lỗi cho người dùng</param>
        /// <param name="moreInfo">Thông tin thêm về lỗi</param>
        /// <param name="traceId">ID theo dõi lỗi</param>
        /// Author: NCCONG (09/02/2023)
        public ErrorResult(ErrorCode errorCode, string devMsg, string userMsg, object moreInfo, string? traceId = null)
        {
            ErrorCode = errorCode;
            DevMsg = devMsg;
            UserMsg = userMsg;
            MoreInfo = moreInfo;
            TraceId = traceId;
        }

        #endregion
    }
}
