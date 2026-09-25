namespace QuanLyKhachSan.Services
{
    public class KetQuaXuLy
    {
        public bool ThanhCong { get; set; }
        public string ThongBao { get; set; }

        public static KetQuaXuLy Ok(string tb)
        {
            return new KetQuaXuLy { ThanhCong = true, ThongBao = tb };
        }

        public static KetQuaXuLy Fail(string tb)
        {
            return new KetQuaXuLy { ThanhCong = false, ThongBao = tb };
        }
    }
}