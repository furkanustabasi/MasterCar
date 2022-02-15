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
        public static readonly string IsExist = "Kayıt bulunmaktadır";

        public static readonly string UserNotExist = "Kullanici mevcut degil";
        public static readonly string UserEmailExist = "E-mail zaten kayitli";
        public static readonly string UserEmailNotAvailable = "Kullanici e-maili gecersiz";
                     
        public static readonly string CustomerNotExist = "Musteri mevcut degil";
        public static readonly string NotAddedCustomer = "Müsteri eklenirken bir sorun olustu";
                    
        public static readonly string RentalCarNotAvailable = "Kiralanmak istenen arac daha once kiralanmis";
        public static readonly string RentalNotExist = "Kiralama mevcut degil";
                     
        public static readonly string ErrorUpdatingImage = "Resim guncellenirken hata olustu";
        public static readonly string ErrorDeletingImage = "Resim silinirken hata olustu";
        public static readonly string CarImageLimitExceeded = "Bu araca daha fazla resim eklenemez";
        public static readonly string CarImageIdNotExist = "Araba resmi mevcut degil";
        public static readonly string UserAlreadyCustomer = "Kullanici zaten bir musteridir";
        public static readonly string GetDefaultImage = "Arabanin bir resmi olmadigi icin varsayilan resim getirildi";
        public static readonly string NoPictureOfTheCar = "Arabanin hic resmi yok";
                  
        public static readonly string AuthorizationDenied = "Bu islemi yapmak icin yetkiniz yok";
        public static readonly string UserRegistered = "Kullanici kayit basarili";
        public static readonly string UserNotFound = "Kullanici bulunamadi";
        public static readonly string PasswordError = "Sifre hatali";
        public static readonly string SuccessfulLogin = "Giris basarili";
        public static readonly string UserAlreadyExists = "Kullanici zaten sisteme kayitli";
        public static readonly string AccessTokenCreated = "Token basariyla olusturuldu";
        public static readonly string PasswordChanged = "Sifre basariyla degistirildi";
    }
}
