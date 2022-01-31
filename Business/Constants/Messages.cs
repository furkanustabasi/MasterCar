namespace Business.Constants
{
    public static class Messages
    {
        public static readonly string AddedSuccess = "Yükleme başarılı";
        public static readonly string AddedError = "Yükleme başarısız";
        public static readonly string DeleteSuccess = "Silme başarılı";
        public static readonly string DeleteError = "Silme başarısız";
        public static readonly string UpdateSuccess = "Güncelleme başarılı";
        public static readonly string UpdateError = "Güncelleme başarısız";
        public static readonly string ListedSuccess = "Listeleme başarılı";
        public static readonly string ListedError = "Listeleme başarısız";

        public static readonly string NotFound = "Kayıt bulunamadı.";
        public static readonly string FaultSearchNotFound = "Hatalı arama yaptınız, kayıt bulunamadı";

        public static string UserNotExist = "Kullanici mevcut degil";
        public static string UserEmailExist = "E-mail zaten kayitli";
        public static string UserEmailNotAvailable = "Kullanici e-maili gecersiz";

        public static string CustomerNotExist = "Musteri mevcut degil";
        public static string NotAddedCustomer = "Müsteri eklenirken bir sorun olustu";

        public static string RentalCarNotAvailable = "Kiralanmak istenen arac daha once kiralanmis";
        public static string RentalNotExist = "Kiralama mevcut degil";

        public static string ErrorUpdatingImage = "Resim guncellenirken hata olustu";
        public static string ErrorDeletingImage = "Resim silinirken hata olustu";
        public static string CarImageLimitExceeded = "Bu araca daha fazla resim eklenemez";
        public static string CarImageIdNotExist = "Araba resmi mevcut degil";
        public static string UserAlreadyCustomer = "Kullanici zaten bir musteridir";
        public static string GetDefaultImage = "Arabanin bir resmi olmadigi icin varsayilan resim getirildi";
        public static string NoPictureOfTheCar = "Arabanin hic resmi yok";

        public static string AuthorizationDenied = "Bu islemi yapmak icin yetkiniz yok";
        public static string UserRegistered = "Kullanici kayit basarili";
        public static string UserNotFound = "Kullanici bulunamadi";
        public static string PasswordError = "Sifre hatali";
        public static string SuccessfulLogin = "Giris basarili";
        public static string UserAlreadyExists = "Kullanici zaten sisteme kayitli";
        public static string AccessTokenCreated = "Token basariyla olusturuldu";
        public static string PasswordChanged = "Sifre basariyla degistirildi";
    }
}
