# Tasty by Three Musketeers — Canteen Ordering API

![.NET 9](https://img.shields.io/badge/.NET-9-512BD4?logo=dotnet&logoColor=white)
![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-Web%20API-5C2D91?logo=dotnet&logoColor=white)
![EF Core](https://img.shields.io/badge/EF%20Core-SQLite-003B57?logo=sqlite&logoColor=white)
![JWT](https://img.shields.io/badge/Auth-JWT-000000?logo=jsonwebtokens&logoColor=white)

A REST API for an office canteen. Employees browse the menu and place orders, either for pickup at the counter or delivered to their desk ("desk mode"), and pay for them. They get notifications as their order moves from *Pending* to *Completed*. Admins manage the menu and switch desk delivery on or off.

## Features

- **Authentication**: sign up and log in, with passwords hashed using **BCrypt** and **JWT bearer tokens** that carry the user's role.
- **Role-based authorization**: admin-only endpoints, such as switching desk mode, are protected with `[Authorize(Roles = "Admin")]`.
- **Menu management**: create, read, update and delete menu items, each with an availability flag.
- **Ordering**: place counter or desk orders. Orders containing unavailable items, and desk orders while desk mode is off, are rejected with a clear message. Order status goes Pending → Confirmed → Preparing → Prepared → Completed.
- **Payments**: record a payment (method, UPI ID, transaction ID) against an order. A completed payment moves the order to *Confirmed* and notifies the user.
- **Notifications**: a notification is stored for each order event (order placed, status changed, payment) and can be listed per user.
- **Swagger UI** with JWT support, so you can authorize once and try every endpoint in the browser.

## Architecture

The project keeps a separation between domain, application logic, data access and the HTTP layer:

```
Core/
  Models/           Domain entities: User, MenuItem, Order, OrderItem, Payment, Notification
  Interfaces/       Repository abstractions (IRepository<T>, IOrderRepository, ...)
  Repositories/     EF Core implementations, with a generic Repository<T> base class
  Security/         JWT token service
Applications/
  Services/         Business logic: OrderService, PaymentService, NotificationService
Infrastructure/
  Data/             CanteenDbContext (EF Core) and runtime configuration (desk mode)
WebAPI/
  Controllers/      Auth, MenuItem, Order, Payments, Notifications, Canteen (admin)
  DTOs/             Request and response contracts
Migrations/         EF Core migrations, applied automatically on startup
```

Services and repositories are registered with ASP.NET Core dependency injection, and controllers only depend on abstractions or services.

## Tech stack

C# · ASP.NET Core 9 Web API · Entity Framework Core 9 (SQLite) · JWT bearer authentication · BCrypt.Net · Swagger / Swashbuckle

## Getting started

**Prerequisites:** [.NET 9 SDK](https://dotnet.microsoft.com/download) or newer.

```bash
git clone https://github.com/vishalgupta2446/canteen-ordering-api.git
cd canteen-ordering-api
dotnet run --launch-profile http
```

- On first start, the SQLite database (`canteen.db`) is created automatically from the EF Core migrations.
- Swagger UI: http://localhost:5274/swagger

> Only have the .NET 10 runtime installed? Run it with `DOTNET_ROLL_FORWARD=Major dotnet run --launch-profile http`.

### Try it out

1. **Sign up**: `POST /api/Auth/signup`
   ```json
   { "username": "admin", "password": "Admin@123", "email": "admin@example.com", "role": "Admin" }
   ```
2. **Log in**: `POST /api/Auth/login` returns `{ "token": "..." }`.
3. In Swagger, click **Authorize** and paste the token.
4. **Add menu items**: `POST /api/MenuItem`
   ```json
   { "name": "Masala Dosa", "price": 80, "description": "Crispy dosa with coconut chutney", "isAvailable": true }
   ```
5. Place an order (`POST /api/Order`), pay for it (`POST /api/Payments`), update its status (`PUT /api/Order/{id}/status?status=Preparing`) and read your notifications (`GET /api/Notifications/{userId}`).

## API overview

| Method | Endpoint                               | Description                          | Auth  |
| ------ | -------------------------------------- | ------------------------------------ | ----- |
| POST   | `/api/Auth/signup`                     | Register a user                      | –     |
| POST   | `/api/Auth/login`                      | Log in and receive a JWT             | –     |
| GET    | `/api/MenuItem`                        | List menu items                      | –     |
| GET    | `/api/MenuItem/{id}`                   | Get a menu item                      | –     |
| POST   | `/api/MenuItem`                        | Create a menu item                   | –     |
| PUT    | `/api/MenuItem/{id}`                   | Update a menu item                   | –     |
| DELETE | `/api/MenuItem/{id}`                   | Delete a menu item                   | –     |
| POST   | `/api/Order`                           | Place an order                       | –     |
| GET    | `/api/Order/user/{userId}`             | A user's order history               | –     |
| PUT    | `/api/Order/{orderId}/status`          | Update an order's status             | –     |
| POST   | `/api/Payments`                        | Pay for an order                     | –     |
| GET    | `/api/Notifications/{userId}`          | A user's notifications               | –     |
| POST   | `/api/Canteen/toggleDeskMode`          | Turn desk delivery on or off         | Admin |

## Roadmap

- [ ] Assign the Admin role on the server instead of accepting it at sign-up, and add `[Authorize]` to the menu, order and payment endpoints.
- [ ] Add unit tests for `OrderService` and `PaymentService` (xUnit + in-memory SQLite).
- [ ] Global exception handling with RFC 9457 problem-details responses.
- [ ] Finish connecting the Blazor WebAssembly front end (built separately) to this API.
