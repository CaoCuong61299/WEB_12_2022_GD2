using AMISAPP_MISA.BL.BaseBL;
using AMISAPP_MISA.Common.Entities;
using AMISAPP_MISA.Common.Entities.DTO;
using AMISAPP_MISA.Common.Resources;
using AMISAPP_MISA.Common.ViewModel;
using AMISAPP_MISA.DL.WorkingShiftDL;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMISAPP_MISA.BL.WorkingShiftBL
{
    public class WorkingShiftBL : BaseBL<WorkingShift>,IWorkingShiftBL
    {
        private IWorkingShiftDL _workingShiftDL;

        public WorkingShiftBL(IWorkingShiftDL workingShiftDL) : base(workingShiftDL)
        {
            _workingShiftDL= workingShiftDL;
        }

        /// <summary>
        /// Hàm export excel
        /// </summary>
        /// <returns>File excel</returns>
        /// Author: NCCONG(09/03/2023)
        public dynamic ExportExcel()
        {
            var workingShift = _workingShiftDL.ExportExcel();
            var stream = new MemoryStream();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var package = new ExcelPackage(stream))
            {
                var sheet = package.Workbook.Worksheets.Add("employee");
                // do du lieu vao database 

                sheet.Cells[1, 1].LoadFromCollection(workingShift, false);
                BindingFormatForExcel(sheet, workingShift);
                package.Save();
            }
            stream.Position = 0;
            return stream;
        }

        /// <summary>
        /// Hàm format filexcel
        /// </summary>
        /// <param name="worksheet">file excel</param>
        /// <param name="workingShifts">số bản ghi</param>
        /// Author: NCCONG(09/03/2023)
        private void BindingFormatForExcel(ExcelWorksheet worksheet, List<WorkingShift> workingShifts)
        {
            // Set default width cho tất cả column
            worksheet.DefaultColWidth = 15;
            // Set font chữ cho value
            worksheet.Cells["A:R"].Style.Font.SetFromFont("Times New Roman", 11);
            // Set độ dài của cột
            worksheet.Column(1).Width = 5;
            worksheet.Column(3).Width = 25;
            worksheet.Column(6).Width = 25;
            worksheet.Column(7).Width = 25;
            worksheet.Column(8).Width = 25;
            worksheet.Column(9).Width = 30;
            // Gộp ô tiêu đề
            worksheet.Cells["A1:R1"].Merge = true;
            // Căn giữa cho ô tiêu đề
            worksheet.Cells["A1:R1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            // Set giá trị cho ô tiêu đề
            worksheet.Cells[1, 1].Value = "Ca Làm Việc";
            // Set font chữ cho ô tiêu đề
            worksheet.Cells[1, 1].Style.Font.SetFromFont("Arial", 16);
            // Set Font chữ đậm ô tiêu đề
            worksheet.Cells[1, 1].Style.Font.Bold = true;
            // Gộp hàng 2
            worksheet.Cells["A2:R2"].Merge = true;
            worksheet.Cells["A2:R2"].Value = " ";

            int colum = workingShifts.Count + 3;
            worksheet.Cells[$"A3:R{colum}"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            worksheet.Cells[$"A3:R{colum}"].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            worksheet.Cells[$"A3:R{colum}"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            worksheet.Cells[$"A3:R{colum}"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            // Tạo header
            worksheet.Cells[3, 1].Value = "STT";
            worksheet.Cells[3, 2].Value = "Mã ca";
            worksheet.Cells[3, 3].Value = "Tên ca";
            worksheet.Cells[3, 4].Value = "Giờ bắt đầu ca";
            worksheet.Cells[3, 5].Value = "Chấm vào đầu ca từ";
            worksheet.Cells[3, 6].Value = "Chấm vào đầu ca đến";
            worksheet.Cells[3, 7].Value = "Giờ kết thúc ca";
            worksheet.Cells[3, 8].Value = "Chấm ra cuối ca từ";
            worksheet.Cells[3, 9].Value = "Chấm ra cuối ca đến";
            worksheet.Cells[3, 10].Value = "Giờ bắt đầu nghỉ giữa ca";
            worksheet.Cells[3, 11].Value = "Chấm ra giữa ca từ";
            worksheet.Cells[3, 12].Value = "Chấm ra giữa ca từ";
            worksheet.Cells[3, 13].Value = "Giờ kết thúc nghỉ giữa ca";
            worksheet.Cells[3, 14].Value = "Chấm vào giữa ca đến";
            worksheet.Cells[3, 15].Value = "Số giờ công";
            worksheet.Cells[3, 16].Value = "Số ngày công";
            worksheet.Cells[3, 17].Value = "Hệ số ngày thường";
            worksheet.Cells[3, 18].Value = "Hệ số ngày nghỉ";

            worksheet.Cells["S:V"].Clear();
            worksheet.Cells["S:V"].ClearFormulaValues();


            // Lấy range vào tạo format cho range đó ở đây là từ A1 tới D1
            using (var range = worksheet.Cells["A3:R3"])
            {
                // Set PatternType
                range.Style.Fill.PatternType = ExcelFillStyle.DarkGray;
                // Set Màu cho Background
                range.Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                // Canh giữa cho các text
                range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                // Set Font cho text  trong Range hiện tại
                range.Style.Font.SetFromFont("Arial", 10);
                // Set Font chữ đậm
                range.Style.Font.Bold = true;
                // Set Border
                range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            }

            // Đẩy data vào file excel
            for (int i = 0; i < workingShifts.Count; i++)
            {
                var item = workingShifts[i];
                worksheet.Cells[i + 4, 1].Value = i + 1;
                worksheet.Cells[i + 4, 2].Value = item.WorkingShiftCode;
                worksheet.Cells[i + 4, 3].Value = item.WorkingShiftName;
                worksheet.Cells[i + 4, 4].Value = item.ShiftStartTime;
                worksheet.Cells[i + 4, 5].Value = item.TimekeepingShiftStart;
                worksheet.Cells[i + 4, 6].Value = item.TimekeepingShiftEnd;
                worksheet.Cells[i + 4, 7].Value = item.ShiftEndTime;
                worksheet.Cells[i + 4, 8].Value = item.TimekeepingOutShiftStart;
                worksheet.Cells[i + 4, 9].Value = item.TimekeepingOutShiftEnd;
                worksheet.Cells[i + 4, 10].Value = item.BreakTimeBetweenShiftStart;
                worksheet.Cells[i + 4, 11].Value = item.TimekeepingBreakTimeBetweenShifts;
                worksheet.Cells[i + 4, 12].Value = item.BreakTimeBetweenShiftEnd;
                worksheet.Cells[i + 4, 13].Value = item.BreakTimeBetweenShift;
                worksheet.Cells[i + 4, 14].Value = item.WorkingHour;
                worksheet.Cells[i + 4, 15].Value = item.WorkingDay;
                worksheet.Cells[i + 4, 16].Value = item.WeekdaySalaryCoefficients;
                worksheet.Cells[i + 4, 17].Value = item.WeekendSalaryCoefficient;
                worksheet.Cells[i + 4, 18].Value = item.HolidayPayCoefficient;
            }
            worksheet.Cells[2, 4, workingShifts.Count + 4, 4].Style.Numberformat.Format = "HH:mm";
            worksheet.Cells[2, 5, workingShifts.Count + 4, 5].Style.Numberformat.Format = "HH:mm";
            worksheet.Cells[2, 6, workingShifts.Count + 4, 6].Style.Numberformat.Format = "HH:mm";
            worksheet.Cells[2, 7, workingShifts.Count + 4, 7].Style.Numberformat.Format = "HH:mm";
            worksheet.Cells[2, 8, workingShifts.Count + 4, 8].Style.Numberformat.Format = "HH:mm";
            worksheet.Cells[2, 9, workingShifts.Count + 4, 9].Style.Numberformat.Format = "HH:mm";
            worksheet.Cells[2, 10, workingShifts.Count + 4, 10].Style.Numberformat.Format = "HH:mm";
            worksheet.Cells[2, 11, workingShifts.Count + 4, 11].Style.Numberformat.Format = "HH:mm";
            worksheet.Cells[2, 12, workingShifts.Count + 4, 12].Style.Numberformat.Format = "HH:mm";
            worksheet.Cells[2, 13, workingShifts.Count + 4, 13].Style.Numberformat.Format = "HH:mm";
            worksheet.Cells[2, 14, workingShifts.Count + 4, 14].Style.Numberformat.Format = "#,##0";
            worksheet.Cells[2, 15, workingShifts.Count + 4, 15].Style.Numberformat.Format = "##,#";
            worksheet.Cells[2, 16, workingShifts.Count + 4, 16].Style.Numberformat.Format = "##,#";
            worksheet.Cells[2, 17, workingShifts.Count + 4, 17].Style.Numberformat.Format = "##,#";
            worksheet.Cells[2, 18, workingShifts.Count + 4, 18].Style.Numberformat.Format = "##,#";
            worksheet.Cells[2, 4, workingShifts.Count + 4, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            worksheet.Cells[2, 5, workingShifts.Count + 4, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            worksheet.Cells[2, 6, workingShifts.Count + 4, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            worksheet.Cells[2, 7, workingShifts.Count + 4, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            worksheet.Cells[2, 8, workingShifts.Count + 4, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            worksheet.Cells[2, 9, workingShifts.Count + 4, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            worksheet.Cells[2, 10, workingShifts.Count + 4, 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            worksheet.Cells[2, 11, workingShifts.Count + 4, 11].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            worksheet.Cells[2, 12, workingShifts.Count + 4, 12].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            worksheet.Cells[2, 13, workingShifts.Count + 4, 13].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            worksheet.Cells[2, 14, workingShifts.Count + 4, 14].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;


        }

        /// <summary>
        /// Hàm lấy tất cả bản ghi
        /// </summary>
        /// <returns>Tất cả bản ghi</returns>
        /// Author: NCCONG(09/03/2023)
        public List<WorkingShift> GetAll()
        {
            var result = _workingShiftDL.GetAll();
            return result;
        }

        /// <summary>
        /// Hàm kiểm tra trùng mã
        /// </summary>
        /// <param name="workingShift">Đối tượng cần kiểm tra trùng mã</param>
        /// <param name="workingShiftId">Id đối tượng cần kiểm tra trùng mã</param>
        /// <returns>True : Không trùng mã False : Có trùng mã </returns>
        /// Author: NCCONG (09/03/2023)
        protected override ServiceResponse GetCheckCode(WorkingShift workingShift, Guid workingShiftId)
        {
            var checkCode = _workingShiftDL.GetCheckCode(workingShift.WorkingShiftCode);

            if (checkCode != null)
            {
                if (!checkCode.Equals(workingShiftId))
                {
                    return new ServiceResponse
                    {
                        IsSuccess = false,
                        Data = new ErrorResult(
                        Common.Enums.ErrorCode.DuplicateCode,
                        Resource.DevMsg_DuplicateCode,
                        Resource.UserMsg_DuplicateCode,
                        Resource.MoreInfo_DuplicateCode)
                    };
                }
                else
                {
                    return new ServiceResponse
                    {
                        IsSuccess = true
                    };
                }
            }
            return new ServiceResponse { IsSuccess = true };
        }

        public PagingResult<WorkingShift> Paging(int pageSize, int pageNumber, string workingShiftFilter, List<CustomParam> customParam)
        {
            StringBuilder sbWhere = new StringBuilder();

            if(customParam != null)
            {
                foreach (var param in customParam)
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
                    else if(param.Operator == "greater")
                    {
                        sbWhere.AppendFormat(" AND {0} > '{1}'", param.ValueField, param.InputValue);
                    }
                    else if (param.Operator == "smaller")
                    {
                        sbWhere.AppendFormat(" AND {0} < '{1}'", param.ValueField, param.InputValue);
                    }
                }

            }
            var p_Where = sbWhere.ToString();
            var result = _workingShiftDL.Paging(pageSize, pageNumber, workingShiftFilter,p_Where);

            return result;
        }
    }
}
