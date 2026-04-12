# Task Manager – Fullstack .NET 10 + JavaScript

Detta projekt är en fullstack‑applikation där ett **.NET 10 Web API** hanterar CRUD‑operationer för tasks, och en **HTML/CSS/JavaScript‑frontend** kommunicerar med API:t via `fetch()`. Applikationen innehåller validering, modaler, DOM‑manipulation och en 3D‑animation med Three.js. Projektet demonstrerar en ren arkitektur med services, DTOs, validators och en databas kopplad via Entity Framework Core.

---

## 🚀 Funktionalitet

- Skapa nya tasks  
- Redigera befintliga tasks via modal  
- Ta bort en eller flera tasks via egen Delete‑modal  
- Live‑validering i frontend (röd/grön highlight)  
- Backend‑validering med FluentValidation  
- Fetch‑anrop till API för alla CRUD‑operationer  
- Dynamisk DOM‑uppdatering av task‑listan  
- 3D‑animation i högerkolumnen (Three.js)  

---

## ▶️ Hur man kör projektet

### **Backend (.NET API)**

1. Öppna backend‑projektet i Visual Studio eller VS Code  
2. Kör kommandot: dotnet run

3. API:t startar på t.ex. `http://localhost:5003`  
4. API:t är kopplat till en SQL Server‑databas via Entity Framework Core  

### **Frontend**

1. Öppna `index.html` i en live‑server (t.ex. VS Code Live Server)  
2. Frontend körs på t.ex. `http://127.0.0.1:8080`  
3. Frontend kommunicerar med API:t via `fetch()` i `app.js`  

---

## 📡 API Endpoints

### **TasksController**

| Metod | Endpoint | Beskrivning |
|-------|----------|-------------|
| GET | `/api/tasks` | Hämta alla tasks |
| GET | `/api/tasks/{id}` | Hämta en specifik task |
| POST | `/api/tasks` | Skapa en ny task |
| PUT | `/api/tasks/{id}` | Uppdatera en task |
| DELETE | `/api/tasks/{id}` | Ta bort en task |

### **Validering**

Backend använder FluentValidation.  
Fel returneras i JSON och visas i frontend.

---

## 🧩 Backend‑arkitektur

Backend är uppbyggt enligt en ren och tydlig struktur:

- **Controllers** – tar emot HTTP‑anrop  
- **Services** – affärslogik (ITaskService + TaskService)  
- **DTOs** – dataöverföring mellan API och frontend  
- **Validators** – FluentValidation för inkommande data  
- **DbContext** – Entity Framework Core + SQL Server  
- **Models** – TaskItem representerar databastabellen  

### Exempel: Service‑lager

```csharp
public async Task<TaskReadDto> CreateAsync(TaskCreateDto dto)
{
    var task = new TaskItem
    {
        Title = dto.Title,
        Description = dto.Description,
        IsCompleted = false,
        CreatedAt = DateTime.Now
    };

    _context.Tasks.Add(task);
    await _context.SaveChangesAsync();

    return new TaskReadDto
    {
        Id = task.Id,
        Title = task.Title,
        Description = task.Description,
        IsCompleted = task.IsCompleted,
        CreatedAt = task.CreatedAt
    };
}


🌐 Hur frontend pratar med API:et
Frontend använder fetch() för alla API‑anrop:

const response = await fetch(API_URL, {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(newTask)
});

DOM‑manipulation 
Frontend använder DOM för att:

hämta element (getElementById, querySelectorAll)

skapa nya task‑element (createElement)

uppdatera text och klasser (textContent, classList)

visa och gömma modaler (style.display)

lyssna på events (addEventListener)

Exempel:
const div = document.createElement("div");
div.innerHTML = `
    <input type="checkbox" class="task-select" value="${task.id}">
    <div class="task-text">
        <div class="task-title">${task.title}</div>
        <div class="task-desc">${task.description}</div>
    </div>
`;
container.appendChild(div);

Databas
Projektet använder:

Entity Framework Core

SQL Server

Code‑first migrations

Seed‑data i OnModelCreating

Tasks lagras i databasen och hämtas via services‑lagret.

💭 Reflektion
✔ Vad gick bra
-
-Att koppla frontend och backend med fetch‑anrop

Att få DOM‑manipulationen att uppdatera UI:t dynamiskt

Att implementera både frontend‑ och backend‑validering

Att skapa egna modaler för Edit och Delete

Att strukturera API:t med services och databas

⚠️ Vad var svårt
-Frontend‑delen var den svåraste delen av projektet. I backend finns det tydliga strukturer att följa: controllers, services, DTOs, modeller och databaskopplingar. Allt är organiserat och .NET ger mycket stöd. Men i frontend behövde jag själv bygga upp hela strukturen med HTML, CSS och JavaScript utan någon färdig mall. Jag fick skapa egna modaler, egen validering, DOM‑uppdateringar och fetch‑anrop helt från grunden. Det tog tid att få allt att fungera tillsammans på ett snyggt och stabilt sätt.


-Att strukturera frontend och backend i samma solution var lite förvirrande i början. Visual Studio har ingen färdig mall för en fristående HTML/CSS/JS‑frontend, så jag behövde själv skapa en Solution Folder och lägga in frontend‑filerna manuellt. Det tog ett tag att förstå hur man organiserar projektet på ett tydligt sätt, men till slut fick jag en bra struktur där backend och frontend ligger separerade men fortfarande i samma solution.

📌 Framtida förbättringar
JWT‑auth (login, register, roller)

Admin‑panel

Sortering och filtrering av tasks

Dark/light mode

Drag‑and‑drop för att sortera tasks

👤 Utvecklad av
Ehsan 
Fullstack .NET / JavaScript