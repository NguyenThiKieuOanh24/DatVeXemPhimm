﻿namespace DatVeXemPhim.Models
{
    public class PhongChieu
    {
        public int iD { get; set; }
        public string tenPhong { get; set; }

        public ICollection<XuatChieu> XuatChieus { get; set; }
    }
}
