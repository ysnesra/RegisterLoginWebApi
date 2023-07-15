# RegisterLoginWebApi

Register ve Login Formu Projesi

CQRS tasarımı ile MediatR kütüphanesini kullandığım WebAPI projesidir.
DDD(Domain Dream Development) Tekniği ile Onion Architecture mimarisi kullanılmıştır.
IdentityServer yapısı ve Hangfire kullanılarak Authenticate işlemi yapılmıştır.

**************************
Core.Security -> Identity klasörüne-> AppRole ve AppUser entityleri oluşturuldu.
    AppRole: IdentityRole Identity sınıfından inherit edildi
    AppUser: IdentityUser Identity sınıfından inherit edildi

DevsProject -> Persistence -> Contexts içine -> AppIdentityDbContext tanımlandı.

PersistenceServiceRegistration.cs extension clasına -> AddIdentityServerConfig servisi oluşturularak configuration yapıldı,database bağlantıs verildi.
Bu servis program.cs'ye de tanımlandı.

**************************
Kullanıcı Register olma:
Application-> Auths-> RegisterCommand oluşturuldu.
IdentityServer ın UserManager servisi kullanılarak kullanıcı işlemleri yapıldı.
!Kullanıcı Üye olurken Token oluşturmaya gerek yok 

Exception hatalarını özelleştirildi:
Core.CrossCuttingConcerns -> Exceptions klasöründe 
RegisterFailedException, EmailCanNotBeDuplicated... Exception classlarıyla hata ile ilgili açıklama mesaj yazıldı.
AuthController'da Register operasyonu oluşturuldu.

**************************
Sisteme giriş - Login olma:
Application-> Auths-> LoginCommand oluşturuldu.
Email ve Password bilgileri ile Login olundu.
JwtToken üretildi

**************************
OTP(Doğrulama Kodu) ile Login olma:

Core.Security->Entities -> için "OtpAuthenticator" entitysi ve "TwoFactorAuthenticationTransaction" entitysi oluşturuldu.
İkiaşamalı doğrulama yapılacağından; AppUser(1)-TwoFactorAuthenticationTransaction(N) ilişksi oluşturuldu.
Migration yapıldı
Bu entitylerin veritabanı işlemleri için 
->Application içinde Services->Repositories->ITwoFactorAuthenticationRepository interfacei oluşturuldu
->Persistence içinde Repositories içine->TwoFactorAuthenticationRepository oluşturuldu.

Core.Security->Dtos -> "OneTimePasswordDto" oluşturuldu. "OtpLoginCommand" de bu OneTimePasswordDtosu dönülür
OtpLoginCommand'de;
   TwoFactorAuthenticationRepository injecte edilerek CreateOpt fonksitonu ile Opt(OneTimePassword) üretildi.




