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
    
    public partial class CHUCDANH
    {
        public CHUCDANH()
        {
            this.NGUOIDUNGs = new HashSet<NGUOIDUNG>();
        }
    
        public int ID { get; set; }
        public string TENCHUCDANH { get; set; }
        public string TENVIETTAT { get; set; }
    
        public virtual ICollection<NGUOIDUNG> NGUOIDUNGs { get; set; }
    }
}
