//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WCFServisler
{
    using System;
    using System.Collections.Generic;
    
    public partial class Siparisler
    {
        public int OrderId { get; set; }
        public int UrunId { get; set; }
        public int Quantity { get; set; }
        public int KisiId { get; set; }
        public decimal ToplamFiyat { get; set; }
    
        public virtual Kisiler Kisiler { get; set; }
        public virtual Urunler Urunler { get; set; }
    }
}
