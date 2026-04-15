# FjordLine Booking System

Et backend-API for håndtering av fergeavganger og passasjerbestillinger for Fjord Line.
Systemet lar brukere se avganger, opprette og kansellere bestillinger, samt se passasjerlister.

Prosjektet er laget som en take-home oppgave med fokus på backend-design, forretningslogikk og API-struktur.

---

## 🧱 Teknologistack

* .NET 10 (ASP.NET Core Web API)
* C#
* xUnit (enhetstesting)
* Swagger / OpenAPI (API-dokumentasjon)
* In-memory lagring (ingen ekstern database)

---

## 🚀 Hvordan kjøre prosjektet

### ▶️ Alternativ 1: Kjør lokalt

1. Klon repositoryet
2. Åpne løsningen i Rider eller Visual Studio
3. Kjør API-prosjektet:

```bash id="nor1"
dotnet run
```

Deretter åpner du:

```text id="nor2"
http://localhost:5082/swagger
```

---

### 🐳 Alternativ 2: Kjør med Docker

#### 1. Bygg Docker-image

```bash id="nor3"
docker build -t fjordline-app .
```

#### 2. Kjør containeren

```bash id="nor4"
docker run -p 8080:8080 fjordline-app
```

#### 3. Åpne Swagger

```text id="nor5"
http://localhost:8080/swagger
```

---

## 📝 Notater

* API-et bruker in-memory lagring (ingen database nødvendig)
* Swagger UI er tilgjengelig for testing av alle endepunkter
* Sørg for at Docker Desktop kjører dersom du bruker container-versjonen

