# Pharmacy Management System

A full-stack solution for tracking medicine inventory and sales.

## Features
- **Medicine Grid**: With color-coded alerts (Red: < 30 days expiry, Yellow: < 10 stock).
- **Search**: Real-time filtering by name or brand.
- **Sales Tracking**: Logic to decrement stock and log transaction history in JSON.

## How to Run

### 1. Backend (.NET Core API)
- Access folder: `cd PharmacyApi`
- Run command: `dotnet run`
- API is available at: `https://localhost:7240`

### 2. Frontend (Angular)
- Access folder: `cd pharmacy-ui`
- Install dependencies: `npm install`
- Run command: `npm start` (or `ng serve`)
- App is available at: `http://localhost:4200`

## Technical Details
- **Backend**: .NET 8, Web API, JSON File Storage.
- **Frontend**: Angular 19, Standalone Components, RxJS.
