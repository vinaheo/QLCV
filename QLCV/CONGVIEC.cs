//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QLCV
{
    using System;
    using System.Collections.Generic;
    
    public partial class CONGVIEC
    {
        public CONGVIEC()
        {
            this.BAOCAOCONGVIECs = new HashSet<BAOCAOCONGVIEC>();
            this.BINHLUANs = new HashSet<BINHLUAN>();
            this.PHANCONGs = new HashSet<PHANCONG>();
        }
    
        public int ID { get; set; }
        public Nullable<int> IDNGUOITAO { get; set; }
        public string TIEUDE { get; set; }
        public string NOIDUNG { get; set; }
        public string GHICHU { get; set; }
        public Nullable<System.DateTime> NGAYTAO { get; set; }
        public Nullable<bool> HOANTHANH { get; set; }
        public Nullable<int> IDDANGCONGVIEC { get; set; }
        public Nullable<System.DateTime> NGAYCAPNHAT { get; set; }
        public Nullable<bool> XOA { get; set; }
        public Nullable<int> PARENTID { get; set; }
        public string TAPTIN { get; set; }
        public string THUMUC { get; set; }
    
        public virtual ICollection<BAOCAOCONGVIEC> BAOCAOCONGVIECs { get; set; }
        public virtual ICollection<BINHLUAN> BINHLUANs { get; set; }
        public virtual DANGCONGVIEC DANGCONGVIEC { get; set; }
        public virtual NGUOIDUNG NGUOIDUNG { get; set; }
        public virtual ICollection<PHANCONG> PHANCONGs { get; set; }
    }
}
