using AMISAPP_MISA.Common.Entities.DTO;
using AMISAPP_MISA.Common.Resources;
using AMISAPP_MISA.Common.ViewModel;
using AMISAPP_MISA.DL.BaseDL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static AMISAPP_MISA.Common.Attributes.AmisAttributes;

namespace AMISAPP_MISA.BL.BaseBL
{
    public class BaseBL<T>:IBaseBL<T>
    {
        #region Field

        private IBaseDL<T> _baseDL;

        #endregion

        #region Contructor

        public BaseBL(IBaseDL<T> baseDL)
        {
            _baseDL = baseDL;
        }

        #endregion

        #region Method

        /// <summary>
        /// Hàm lấy toàn bộ bản ghi
        /// </summary>
        /// <returns>toàn bộ bản ghi</returns>
        /// Author : NCCONG (08/03/2023)
        public dynamic GetAllRecord()
        {
            return _baseDL.GetAllRecord();
        }

        /// <summary>
        /// Hàm lấy thông tin bản ghi theo Id
        /// </summary>
        /// <param name="Id">Id</param>
        /// <returns>1 bản ghi</returns>
        /// Author : NCCONG (08/03/2023)
        public dynamic GetRecordById(Guid Id)
        {
            return _baseDL.GetRecordById(Id);
        }

        /// <summary>
        /// Hàm lấy id theo list Id
        /// </summary>
        /// <param name="recordIds">list id</param>
        /// <returns>Trả về tất cả các bản ghi trong list id</returns>
        /// Author: NCCONG(09/03/2023)
        public List<T> GetRecordsById(List<Guid> recordIds)
        {
            var result = _baseDL.GetRecordsById(recordIds);
            return result;
        }

        /// <summary>
        /// Hàm thêm mới 1 bản ghi 
        /// </summary>
        /// <param name="record">Bản ghi muốn thêm </param>
        /// <returns> Trả về 1 nếu insert thành công trả về 0 nếu insert thất bại</returns>
        /// Author : NCCONG (08/03/2023)
        public ServiceResponse InsertRecord(T record)
        {
            var validateResults = ValidateRequestData(record);

            if (validateResults != null && validateResults.IsSuccess)
            {
                var checkCode = GetCheckCode(record, Guid.Empty);
                if (checkCode != null && checkCode.IsSuccess)
                {
                    var newRecord = _baseDL.InsertRecord(record);
                    if (newRecord > 0)
                    {
                        return new ServiceResponse
                        {
                            IsSuccess = true,
                            Data = newRecord
                        };
                    }
                    else
                    {
                        return new ServiceResponse
                        {
                            IsSuccess = false,
                            Data = new ErrorResult
                            (
                                Common.Enums.ErrorCode.InsertFailed,
                                Resource.DevMsg_InsertFailed,
                                Resource.UserMsg_InsertFailed,
                                Resource.MoreInfo_InsertFailed
                            )
                        };
                    }
                }
                else
                {
                    return checkCode;
                }
            }
            else
            {
                return new ServiceResponse
                {
                    IsSuccess = false,
                    Data = validateResults?.Data
                };
            }
        }

        /// <summary>
        /// Hàm sửa 1 bảng ghi
        /// </summary>
        /// <param name="recordId">Id bản ghi cần sửa</param>
        /// <param name="record">Bản ghi cần sửa</param>
        /// <returns>số bản ghi ảnh hưởng</returns>
        /// Author: NCCONG (09/03/2023)
        public ServiceResponse UpdateRecord(Guid recordId, T record)
        {
            var validateResults = ValidateRequestData(record);

            if (validateResults != null && validateResults.IsSuccess)
            {
                var checkCode = GetCheckCode(record, recordId);
                if (checkCode != null && checkCode.IsSuccess)
                {
                    var newRecord = _baseDL.UpdateRecord(recordId, record);
                    if (newRecord > 0)
                    {
                        return new ServiceResponse
                        {
                            IsSuccess = true,
                            Data = newRecord
                        };
                    }
                    else
                    {
                        return new ServiceResponse
                        {
                            IsSuccess = false,
                            Data = new ErrorResult
                            (
                                Common.Enums.ErrorCode.UpdateFailed,
                                Resource.DevMsg_UpdateFailed,
                                Resource.UserMsg_UpdateFailed,
                                Resource.MoreInfo_DeleteFailed
                            )
                        };
                    }
                }
                else
                {
                    return checkCode;
                }

            }
            else
            {
                return new ServiceResponse
                {
                    IsSuccess = false,
                    Data = validateResults?.Data
                };
            }
        }

        /// <summary>
        /// Xóa 1 bản ghi
        /// </summary>
        /// <param name="recordIds">ID bản ghi cần xóa</param>
        /// <returns>Số bản ghi ảnh hưởng</returns>
        /// Author : NCCONG (08/03/2023)
        public int DeleteRecord(List<Guid> recordIds)
        {
            return _baseDL.DeleteRecord(recordIds);
        }

        /// <summary>
        /// Validate Request
        /// </summary>
        /// <param name="record">Bản ghi cần validate</param>
        /// <returns>IsSucces: True dữ liệu đầu vào hợp lệ, IsSucces: false dữ liệu đầu vào không hợp lệ </returns>
        /// Author : NCCONG (08/03/2023)
        protected virtual ServiceResponse ValidateRequestData(T? record)
        {
            // Validate dữ liệu đầu vào
            var properties = typeof(T).GetProperties();
            var validateFailures = new List<string>();
            foreach (var property in properties)
            {
                var propertyValue = property.GetValue(record);
                var foreignAttribute = (ForeignKey?)Attribute.GetCustomAttribute(property, typeof(ForeignKey));
                if (foreignAttribute != null && string.IsNullOrEmpty(propertyValue?.ToString().Trim()))
                {
                    validateFailures.Add(foreignAttribute.ErrorMessage);
                }

                var isNotNullOrEmptyAttribute = (IsNotNullOrEmptyAttribute?)Attribute.GetCustomAttribute(property, typeof(IsNotNullOrEmptyAttribute));
                if (isNotNullOrEmptyAttribute != null && string.IsNullOrEmpty(propertyValue?.ToString().Trim()))
                {
                    validateFailures.Add(isNotNullOrEmptyAttribute.ErrorMessage);
                }

                var lengthAttribute = (StringLengthAtribute?)Attribute.GetCustomAttribute(property, typeof(StringLengthAtribute));
                if (lengthAttribute != null && propertyValue?.ToString().Length > lengthAttribute.LengthAtribute)
                {
                    validateFailures.Add(lengthAttribute.ErrorMessage);
                }

            }

            if (validateFailures.Count > 0)
            {
                return new ServiceResponse
                {
                    IsSuccess = false,
                    Data = validateFailures
                };
            }

            return new ServiceResponse
            {
                IsSuccess = true
            };
        }

        /// <summary>
        /// Hàm kiểm tra trùng mã
        /// </summary>
        /// <param name="record">Đối tượng cần kiểm tra trùng mã</param>
        /// <param name="recordId">Id đối tượng cần kiểm tra trùng mã</param>
        /// <returns>True : Không trùng mã False : Có trùng mã </returns>
        /// Author: NCCONG (09/03/2023)
        protected virtual ServiceResponse GetCheckCode(T record, Guid recordId)
        {
            return new ServiceResponse { IsSuccess = true };
        }
  
        /// <summary>
        /// Hàm phân trang và tìm kiếm
        /// </summary>
        /// <param name="pageSize">số bản ghi</param>
        /// <param name="pageNumber">số trang</param>
        /// <param name="workingShiftFilter">từ khóa để tìm kiếm</param>
        /// <param name="customParams">điều kiện lọc</param>
        /// <returns>số bản ghi có trong điều kiện</returns>
        /// Author: NCCONG(09/03/2023)
        public PagingResult<T> GetPagingAndFilter(int pageSize, int pageNumber, string workingShiftFilter,List<CustomParam> customParams)
        {
            StringBuilder sbWhere = new StringBuilder();

            if (customParams != null)
            {
                foreach (var param in customParams)
                {
                    if (param.Operator == "contains")
                    {
                        sbWhere.AppendFormat(" AND {0} LIKE '%{1}%'", param.ValueField, param.InputValue);

                    }
                    else if (param.Operator == "notcontains")
                    {
                        sbWhere.AppendFormat(" AND {0} NOT LIKE '%{1}%'", param.ValueField, param.InputValue);

                    }
                    else if (param.Operator == "equals")
                    {
                        sbWhere.AppendFormat(" AND {0} = '{1}'", param.ValueField, param.InputValue);

                    }
                    else if(param.Operator == "other")
                    {
                        sbWhere.AppendFormat(" AND {0} != '{1}'", param.ValueField, param.InputValue);
                    }
                }

            }
            var p_Where = sbWhere.ToString();
            var result = _baseDL.GetPagingAndFilter(pageSize, pageNumber, workingShiftFilter, p_Where);
            return result;
        }

        /// <summary>
        /// Hàm xóa 1 bản ghi theo id
        /// </summary>
        /// <param name="recordId">id</param>
        /// <returns>số bản ghi đã xóa</returns>
        /// Author : NCCONG(09/03/2023)
        public int DeleteOneRecord(Guid recordId)
        {
            return _baseDL.DeleteOneRecord(recordId);
        }
        #endregion
    }
}
