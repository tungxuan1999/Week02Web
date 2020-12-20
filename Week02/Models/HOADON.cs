namespace Week02.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HOADON")]
    public partial class HOADON
    {
        [Key]
        [StringLength(50)]
        public string MAHD { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Ngaytao { get; set; }

        [StringLength(50)]
        public string KH_ID { get; set; }

        public double? Tongtien { get; set; }

        public int? Trangthai { get; set; }

        [StringLength(256)]
        public string Diachi { get; set; }

        public virtual KHACHHANG KHACHHANG { get; set; }
    }
}
