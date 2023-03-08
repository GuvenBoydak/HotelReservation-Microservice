# HotelReservation-Microservice

### Identity-Service
- Kullanıcı ilgili bilgileri doldurup kayıt olabilir.
- Admın ve User gıbı 2 adet yetkimiz var `admin@admin` Email ile kayıt olanlar Admin yetkisine sahip oluyor
- Kullanıcı Email ve Pasword ile sisteme giriş yapabiliyor.
- Tüm kullanıcıları  listelene veya ID gore listeleme yapılabilir.

### HotelReservation-Service
#### Package
- Ekleme, Silme, Güncelleme ve ID göre yada Tüm pakatler listeleme işlemleri yapılabilir.

#### RoomType
- Ekleme, Silme, Güncelleme ve ID göre yada Tüm oda tipleri listeleme işlemleri yapılabilir.

#### Reservation
- Ekleme, Silme ve ID göre, Reservasyon Confirmation numarısına göre veya Tüm reservasyonlar listeleme işlemleri yapılabilir.
- Ekleme işlemi tamamlandıgında ReservationCreatedIntegrationEvent fırlatılır.
- Bu eventi dinleyen FakePaymentService ödeme işlemi Başarısız ise FailedPaymentProcessIntegrationEvent fırlatır.
- Bu eventı dinleyen ReservationService eklenen reservasyonu confirmation numarası ile siler.

### FakePayment-Service
- ReservationCreatedIntegrationEvent eventı dinler ve kredi kart bilgilerini kontrol ederek Başarılı veya başarısız event fırlatır.
- SuccessPaymentProcessIntegrationEvent (NotificationService dinler.)
- FailedPaymentProcessIntegrationEvent (NotificationService ve ReservationService dinler.)

### Notification-Service
- FakePaymentService den gelen SuccessPaymentProcessIntegrationEvent veya FailedPaymentProcessIntegrationEvent eventları dinleyerek kullanıcılara mail gönderme işlemi yapar.
