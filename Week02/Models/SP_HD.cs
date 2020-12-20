namespace Week02.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SP_HD
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(50)]
        public string MASP { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string MAHD { get; set; }

        public int? Soluong { get; set; }

        public double? Gia { get; set; }

        public virtual SANPHAM SANPHAM { get; set; }
    }
}
