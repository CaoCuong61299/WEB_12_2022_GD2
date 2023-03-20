using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AMISAPP_MISA.Common.Attributes.AmisAttributes;

namespace AMISAPP_MISA.Common.Entities
{
    /// <summary>
    /// Thông tin ca làm việc
    /// </summary>
    public class WorkingShift
    {
        /// <summary>
        /// Khóa chính
        /// </summary>
        [PrimaryKey]
        public Guid WorkingShiftId { get; set; }

        /// <summary>
        /// Mã Ca
        /// </summary>
        public string? WorkingShiftCode { get; set; }

        /// <summary>
        /// Tên ca
        /// </summary>
        public string? WorkingShiftName { get; set; }

        /// <summary>
        /// Giờ bắt đầu ca
        /// </summary>
        public TimeSpan? ShiftStartTime { get; set; }

        /// <summary>
        /// Chấm vào đầu ca từ
        /// </summary>
        public TimeSpan? TimekeepingShiftStart { get; set; }

        /// <summary>
        /// Chấm vào đầu ca đến
        /// </summary>
        public TimeSpan? TimekeepingShiftEnd { get; set; }

        /// <summary>
        /// Giờ kết thúc ca
        /// </summary>
        public TimeSpan? ShiftEndTime { get; set; }

        /// <summary>
        /// Chấm ra cuối ca từ
        /// </summary>
        public TimeSpan? TimekeepingOutShiftStart { get; set; }

        /// <summary>
        /// Chấm ra cuối ca đến
        /// </summary>
        public TimeSpan? TimekeepingOutShiftEnd { get; set; }

        /// <summary>
        /// Giờ bắt đầu nghỉ giữa ca
        /// </summary>
        public TimeSpan? BreakTimeBetweenShiftStart { get; set; }

        /// <summary>
        /// Chấm ra nghỉ giữa ca từ
        /// </summary>
        public TimeSpan? TimekeepingBreakTimeBetweenShifts { get; set; }

        /// <summary>
        /// Giờ kết thúc nghỉ giữa ca
        /// </summary>
        public TimeSpan? BreakTimeBetweenShiftEnd { get; set; }

        /// <summary>
        /// Chấm vào nghỉ giữa ca đến
        /// </summary>
        public TimeSpan? BreakTimeBetweenShift { get; set; }

        /// <summary>
        /// Ngày công
        /// </summary>
        public Double? WorkingDay { get; set; }

        /// <summary>
        /// Giờ công
        /// </summary>
        public Double? WorkingHour { get; set; }

        /// <summary>
        /// Hệ số ngày thường
        /// </summary>
        public Double? WeekdaySalaryCoefficients { get; set; }

        /// <summary>
        /// Hệ số ngày nghỉ
        /// </summary>
        public Double? WeekendSalaryCoefficient { get; set; }

        /// <summary>
        /// Hệ số ngày lễ
        /// </summary>
        public Double? HolidayPayCoefficient { get; set; }

        
        /// <summary>
        /// Người tạo
        /// </summary>
        public string? CreatedBy { get; set; }

        /// <summary>
        /// Ngày tạo
        /// </summary>
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Người sửa
        /// </summary>
        public string? ModifyBy { get; set; }

        /// <summary>
        /// Ngày sửa
        /// </summary>
        public DateTime? ModifyDate { get; set; }


    }
}
