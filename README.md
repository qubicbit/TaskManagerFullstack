# Task Manager – Fullstack .NET 10 + JavaScript
Detta projekt är en fullstack‑applikation där ett .NET 10 Web API hanterar CRUD‑operationer för tasks, och en HTML/CSS/JavaScript‑frontend kommunicerar med API:t via fetch(). Applikationen innehåller validering, modaler, DOM‑manipulation och en 3D‑animation med Three.js. Backend‑delen är byggd med en ren arkitektur som använder services, DTOs, validators och Entity Framework Core för datalagring. Projektet inkluderar även ett automatiserat TestRunner‑verktyg som kör rollbaserade tester (Admin, User, Public) mot alla endpoints via Swagger och genererar en access‑matris.


# Testning (TestRunner)
Projektet innehåller ett automatiserat testverktyg som körs via PowerShell‑scriptet run-tests.ps1 i rotmappen. Testerna verifierar rollbaserad access (Admin, User, Public) och läser automatiskt in alla endpoints från Swagger.

Kör tester
Från projektets rot:
.\run-tests.ps1

Kör specifik roll:
.\run-tests.ps1 admin
.\run-tests.ps1 user
.\run-tests.ps1 public

Vad som testas
Alla endpoints från Swagger
Statuskoder för Admin, User och Public
Inloggning och åtkomstkontroll
Sammanfattning per kategori (Tasks, Users, Tags, Categories, Auth, Admin)
Access‑matris som visar vilka roller som har tillgång

Syfte
TestRunner säkerställer att API:et har korrekt rollbaserad access, att endpoints svarar som förväntat och att inga oavsiktliga öppna eller stängda endpoints finns.
