﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AMISAPP_MISA.Common.Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Resource {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resource() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("AMISAPP_MISA.Common.Resources.Resource", typeof(Resource).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Delete to database return 0.
        /// </summary>
        public static string DevMsg_DeleteFailed {
            get {
                return ResourceManager.GetString("DevMsg_DeleteFailed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Current code is duplicate.
        /// </summary>
        public static string DevMsg_DuplicateCode {
            get {
                return ResourceManager.GetString("DevMsg_DuplicateCode", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Catched an exception.
        /// </summary>
        public static string DevMsg_Exception {
            get {
                return ResourceManager.GetString("DevMsg_Exception", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Insert to database return 0.
        /// </summary>
        public static string DevMsg_InsertFailed {
            get {
                return ResourceManager.GetString("DevMsg_InsertFailed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Update to database return 0.
        /// </summary>
        public static string DevMsg_UpdateFailed {
            get {
                return ResourceManager.GetString("DevMsg_UpdateFailed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to One or more validation.
        /// </summary>
        public static string DevMsg_ValidateFailed {
            get {
                return ResourceManager.GetString("DevMsg_ValidateFailed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to https://openapi.misa.com.vn/errorcode/e006.
        /// </summary>
        public static string MoreInfo_DeleteFailed {
            get {
                return ResourceManager.GetString("MoreInfo_DeleteFailed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to https://openapi.misa.com.vn/errorcode/e002.
        /// </summary>
        public static string MoreInfo_DuplicateCode {
            get {
                return ResourceManager.GetString("MoreInfo_DuplicateCode", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to https://openapi.misa.com.vn/errorcode/e001.
        /// </summary>
        public static string MoreInfo_Exception {
            get {
                return ResourceManager.GetString("MoreInfo_Exception", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to https://openapi.misa.com.vn/errorcode/e004.
        /// </summary>
        public static string MoreInfo_InsertFailed {
            get {
                return ResourceManager.GetString("MoreInfo_InsertFailed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to https://openapi.misa.com.vn/errorcode/e005.
        /// </summary>
        public static string MoreInfo_UpdateFailed {
            get {
                return ResourceManager.GetString("MoreInfo_UpdateFailed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to DELETE FROM comlumntable.
        /// </summary>
        public static string Proc_ColumnTable_Delete {
            get {
                return ResourceManager.GetString("Proc_ColumnTable_Delete", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to DELETE FROM workingshift WHERE WorkingShiftId IN (&apos;{0}&apos;).
        /// </summary>
        public static string Proc_WorkingShift_DeleteListId {
            get {
                return ResourceManager.GetString("Proc_WorkingShift_DeleteListId", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT * FROM workingshift  WHERE WorkingShiftId IN (&apos;{0}&apos;).
        /// </summary>
        public static string Proc_WorkingShift_GetByListId {
            get {
                return ResourceManager.GetString("Proc_WorkingShift_GetByListId", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Xóa bản ghi thất bại..
        /// </summary>
        public static string UserMsg_DeleteFailed {
            get {
                return ResourceManager.GetString("UserMsg_DeleteFailed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Mã hiện tại đang bị trùng.
        /// </summary>
        public static string UserMsg_DuplicateCode {
            get {
                return ResourceManager.GetString("UserMsg_DuplicateCode", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Có lỗi xảy ra! Vui lòng liên hệ với MISA.
        /// </summary>
        public static string UserMsg_Exception {
            get {
                return ResourceManager.GetString("UserMsg_Exception", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Thêm mới bản ghi thất bại..
        /// </summary>
        public static string UserMsg_InsertFailed {
            get {
                return ResourceManager.GetString("UserMsg_InsertFailed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Sửa bản ghi thất bại..
        /// </summary>
        public static string UserMsg_UpdateFailed {
            get {
                return ResourceManager.GetString("UserMsg_UpdateFailed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Dữ liệu đầu vào không hợp lệ.
        /// </summary>
        public static string UserMsg_ValidateFailed {
            get {
                return ResourceManager.GetString("UserMsg_ValidateFailed", resourceCulture);
            }
        }
    }
}
