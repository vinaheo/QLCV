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
    
    public partial class BAOCAOCONGVIEC
    {
        public int ID { get; set; }
        public Nullable<int> IDNGUOITAO { get; set; }
        public Nullable<int> IDCONGVIEC { get; set; }
        public Nullable<int> IDPHANCONG { get; set; }
        public string NOIDUNG { get; set; }
        public Nullable<System.DateTime> NGAYTAO { get; set; }
    
        public virtual CONGVIEC CONGVIEC { get; set; }
        public virtual NGUOIDUNG NGUOIDUNG { get; set; }
    }
}
