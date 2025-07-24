# Product.API

A RESTful Web API built with ASP.NET Core 8 that allows you to perform full CRUD operations on Products, along with stock increment/decrement capabilities. Each product has a unique **6-digit Product ID**.

## 🚀 Features

- Create, Read, Update, Delete (CRUD) for Products
- Auto-generated **6-digit unique Product IDs**
- Endpoints to **increment** or **decrement stock**
- Uses SQL Server As Database
- Built with **.NET 8** and **Entity Framework Core**
- Uses Code First Approach
- Swagger support for testing endpoints
- Written UnitTestes using MSTEST and MOQ

## Steps to be followed To Run Locally

1.Clone the repository

2.Configure the database

 Update appsettings.json with your local SQL Server Connection String
   
3.Apply EF Migrations

  For initial migration execute this command:
  
**dotnet ef migrations add InitialCreate --project Products.DAL --startup-project Products.Service**

To update the database:

**dotnet ef migrations database update --project Products.DAL --startup-project Products.Service**

if there any changes after the initial migration:

**dotnet ef migrations add `<ChangesNames>` --project Products.DAL --startup-project Products.Service**

and update the database:

**dotnet ef migrations database update --project Products.DAL --startup-project Products.Service**

4.Run the API
 dotnet run 
API will be available at:

**https://localhost:7206**

- ## 📦 Product Model

Each product has the following fields:

| Field | Type | Description |
|----------------|-----------|------------------------------|
| ProductId | int | 6-digit unique identifier |
| ProductName | string | Product name |
| ProductDescription | string | Optional description |
| Category | string | Optional Category |
| Price | decimal | Price of the product |
| StockAvailable | int | Number of items in stock |
| CreatedOn | DateTime | Timestamp of creation |
| UpdatedOn | DateTime | Timestamp of creation |

---

## 🛣️ API Endpoints

### 📘 Read

- `GET /api/products`
Returns all products.

- `GET /api/products/{id}`
Returns a product by its ID.

### ✍️ Create

- `POST /api/products`
Creates a new product. Product ID is auto-generated.

### 🛠️ Update

- `PUT /api/products/{id}`
Updates the product by ID.

### ❌ Delete

- `DELETE /api/products/{id}`
Deletes a product by ID.

### 🔽 Decrement Stock

- `PUT /api/products/decrement-stock/{id}/{quantity}`
Decreases the stock of a product.

### 🔼 Add to Stock

- `PUT /api/products/add-to-stock/{id}/{quantity}`
Increases the stock of a product.

## 🧪 Sample JSON (POST /api/products)

Request json
{
"productname": "Gaming Keyboard",
"productdescription": "Mechanical RGB backlit keyboard",
"category": Keyboard,
"price": 149.99,
"stockAvailable": 20
}

Response:
201 Created
{
  "success": true,
  "data": {
    "productId": 100045,
    "productName": "Pen",
    "productDescription": "Pen",
    "category": "Pen",
    "price": 23,
    "stockAvailable": 23,
    "createdOn": "2025-07-24T09:56:25.5302559+05:30",
    "updatedOn": null
  },
  "message": null
}

Sample JSON(GET /api/products)

Response: 200 OK

{"success":true,"data":[{"productId":100000,"productName":"Book","productDescription":"Book","category":"Stationery","price":35.00,"stockAvailable":10,"createdOn":"2025-07-23T14:50:57.927","updatedOn":"2025-07-23T20:23:17.7364307"},{"productId":100001,"productName":"Gaming Keyboard","productDescription":"Mechanical RGB backlit keyboard","category":"Keyboard","price":27.00,"stockAvailable":5,"createdOn":"2025-07-23T14:50:57.927","updatedOn":null},"message":null}



