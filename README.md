# FullStack AI Chat Project

Full Stack AI Chat uygulaması — Backend **.NET 8 + Docker + Render**, Frontend **React + Vercel** yapısıyla geliştirilmiştir.  
Uygulama kullanıcı kayıt, giriş, JWT doğrulama, mesaj gönderme ve OpenAI API ile sohbet etme özelliklerine sahiptir.

---

## 🚀 Proje Mimarisi

Bu proje iki ana servisten oluşur:

### **1️⃣ Backend (ASP.NET Core 8 / C# / Entity Framework / JWT)**

- Kullanıcı kayıt & giriş (Register / Login)
- JWT token ile Authentication & Authorization
- Mesajların veritabanına kaydedilmesi
- OpenAI API ile chat entegrasyonu
- Dockerize edilip **Render** üzerinde deploy edilmiştir.

### **2️⃣ Frontend (React / Vite / Tailwind / Axios)**

- Kullanıcı arayüzü (Login/Register + Chat ekranı)
- JWT token yönetimi
- Axios ile backend'e istek gönderme
- Vercel üzerinde deploy edilmiştir.

---

## 🌍 Kullanılan Teknolojiler

### Backend

- .NET 8 Web API
- Entity Framework Core
- SQL (Render PostgreSQL veya MSSQL desteği)
- JWT Authentication
- Docker
- Render Web Service

### Frontend

- React
- Vite
- React Router v6
- Axios
- Tailwind CSS
- Vercel Deploy

---

## 🐳 Docker & Deploy Süreci

Backend Dockerfile şu işlemleri yapar:

1. .NET 8 base image üzerinde API çalıştırılır.
2. SDK image ile proje restore + build + publish edilir.
3. Publish edilen dosyalar final imaja kopyalanır.
4. ENTRYPOINT olarak `dotnet backend.dll` çalışır.

Render deploy sırasında Dockerfile’ı otomatik olarak build eder.  
Lokal build yapmana gerek yoktur.

---

## 🌐 Deploy Bağlantıları

- **Backend (Render)**: `https://fullstack-ai-chat-backend.onrender.com`
- **Frontend (Vercel)**: `https://full-stack-ai-chat-project.vercel.app`

---

## 🔐 CORS Yapılandırması

Backend tarafındaki CORS politikası:

```csharp
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy =>
        {
            policy.WithOrigins("https://full-stack-ai-chat-project.vercel.app")
                  .AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowCredentials();
        });
});
```
