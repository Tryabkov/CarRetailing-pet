## 🚗 Auto Catalog — Pet-проект для просмотра и размещения объявлений

### Описание

**Auto Catalog** — это веб-приложение для размещения и просмотра объявлений о продаже автомобилей. Он создан как pet-проект для отработки:

- fullstack-разработки
- Entity Framework и PostgreSQL
- Clean Architecture
- авторизации через JWT и cookie
- раздельного frontend/backend
- CORS настройки при cross-domain
- REST API и React UI

---

### 🛠️ Стек и технологии

#### Backend: ASP.NET Core 8

- C# / ASP.NET Core WebApi
- Entity Framework и PosgreSQL
- JWT
- Clean Architecture: Layered architecture, DI, SOLID

#### Frontend: 
- React 18 + Vite


### 📓 То-до / идеи для развития

- Загрузка фото через распеределённое хранилище
- Фильтрация в том числе по расстоянию
- Кэширование через Redis
- Аналитика
- Встраиване Kafka для микросервисов
- Роли и права
- Пагинация
- Docker и CI/CD

---

### 📁 Запуск

**Backend:**

```bash
cd WebApi/
dotnet run
```

**Frontend:**

```bash
cd frontend/
npm run dev
```

