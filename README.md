# ⚡ RegisterLoginWebApi ⚡

Register ve Login Formu Projesi

CQRS tasarımı ile MediatR kütüphanesini kullandığım WebAPI projesidir.
DDD(Domain Dream Development) Tekniği ile Onion Architecture mimarisi kullanılmıştır.
IdentityServer yapısı ve Hangfire kullanılarak Authenticate işlemi yapılmıştır.

-   _Enum tanımlanıp Mail için 1 ,Sms için 2 değerleri verildi._

-   _IdentityServer yapısı kullanıldı._
    _Register olurken; aynı mail adresinin tekrarlanmaması kontrolü IdentityServer ın userManager Servisi kullanılarak yapıldı
                     ; _parola kontrolü IdentityServer ın signInManager Servisi kullanılarak yapıldı
    
-   _OtpLogin ile OTP üretip ;_
    _buradaki OTP Id si ve doğrulama kodu ile Login işlemi gerçekleştirildi ve Jwt Token üretildi.

-   _OTP üretme işleminde Hangfire yapısı kullanıldı._
    _Hangfire için Quartz kütüphanesi indirildi. QuartzService yazıldı. 5 sn de bir işlem yapmamızı sağlar ve 
    _TwoFactorAuthenticationTransactions tablosundaki IsSend değerini True ya çeker.


![otpdogrulama](https://github.com/ysnesra/RegisterLoginWebApi/assets/104023688/7aa9e7e9-ede6-45ba-910f-173369fbe5ea)

-   _Kullanıcı OtpLogin'den Channel:1 olarak giriş yaparsa-> Sms ile doğrulama kodu gider:_
                       _Channel:2 olarak giriş yaparsa-> Maile doğrulama kodu gider:_



|![1otpLoginRequest](https://github.com/ysnesra/RegisterLoginWebApi/assets/104023688/28366b8b-0192-4f5f-a7fc-71cb351c8f28)  |![2otpLoginResponse](https://github.com/ysnesra/RegisterLoginWebApi/assets/104023688/421d76de-28aa-494d-88ed-b7d3cd90224e) |
|--|--|
|  |  |

-   _Bu doğrulama kodu (OneTimePassword) ve OneTimePasswordId si Login olurken girilecek:_
    _Login olunca Jwt token üretilir
    
|![3loginrequest](https://github.com/ysnesra/RegisterLoginWebApi/assets/104023688/a3f37d3e-0bc2-4e93-89a6-672750fe5b36)  |![4logincommanduservalue](https://github.com/ysnesra/RegisterLoginWebApi/assets/104023688/3b3f5b31-1881-4d1b-bdf4-fc02143d4cdb) |
|--|--|
|  |  |

![5loginResponse](https://github.com/ysnesra/RegisterLoginWebApi/assets/104023688/43dea836-3275-4eba-9cae-e2fe927927ef)

|--|--|
|  |  |

NUNIT TEST

![Ekran Resmi 2023-09-30 23 03 04](https://github.com/ysnesra/RegisterLoginWebApi/assets/104023688/b484634e-b5ab-4679-8ff2-4bdcb3824435)

UNIT TEST ile ilgili detaylı makaleme aşağıdaki linkten bakabilirsiniz :
https://medium.com/@esrayasin/unit-test-d9d854d14e2d



