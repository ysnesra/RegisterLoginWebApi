# RegisterLoginWebApi

Register ve Login Formu Projesi

CQRS tasarımı ile MediatR kütüphanesini kullandığım WebAPI projesidir.
DDD(Domain Dream Development) Tekniği ile Onion Architecture mimarisi kullanılmıştır.
IdentityServer yapısı ve Hangfire kullanılarak Authenticate işlemi yapılmıştır.

Enum tanımlanıp Mail için 1 ,Sms için 2 değerleri verildi.

IdentityServer yapısı kullanıldı.
Register olurken aynı mail adresinin tekrarlanmaması kontrolü IdentityServer ın userManager Servisi kullanılarak yapıldı

OtpLogin ile OTP üretip ;
buradaki OTP Id si ve doğrulama kodu ile Login işlemi gerçekleştirildi ve Jwt Token üretildi.

OTP üretme işleminde Hangfire yapısı kullanıldı.
Hangfire için Quartz kütüphanesi indirildi. QuartzService yazıldı. 5 sn de bir işlem yapmamızı sağlar ve 
TwoFactorAuthenticationTransactions tablosundaki IsSend değerini True ya çeker.


![otpdogrulama](https://github.com/ysnesra/RegisterLoginWebApi/assets/104023688/7aa9e7e9-ede6-45ba-910f-173369fbe5ea)




