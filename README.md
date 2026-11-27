# MiniAdmin Lab（自學作品）

小型後台管理系統：會員與商品管理、JWT 登入與角色權限。

## 功能

- JWT 登入／登出，Angular 路由守衛保護頁面
- 角色權限：未登入回 401，非 Admin 刪除會員回 403
- 商品：列表、新增、修改、刪除（前後端全線）
- 會員：列表、搜尋、新增（EF Core + SQLite；前端尚未做修改／刪除）
- 單元測試（後端 xUnit、前端 Vitest），每個功能先寫測試再實作（TDD）

## 技術

C#、ASP.NET Core（.NET 10）、EF Core／SQLite、xUnit、Angular、Vitest、Docker

## 怎麼跑

開兩個終端（或後端改用 Docker）。

```bash
# 後端 → http://localhost:5212
cd backend/MiniAdmin.Api
dotnet run --launch-profile http

# 或用 Docker 跑 API（同樣對應本機 5212）
cd backend
docker compose up --build

# 前端 → http://localhost:4200（proxy 把 /api 轉到 5212）
cd frontend
npm install
npm start -- --proxy-config proxy.conf.json
```

測試：

```bash
dotnet test
cd frontend && npm test -- --watch=false
```

## 示範帳號

| 帳號 | 密碼 | 角色 |
|------|------|------|
| `admin` | `secret` | Admin |
| `user` | `secret` | 一般使用者 |

※ 帳號寫死在程式中，僅供示範，非正式驗證機制。畫面「模擬登入」等同 `admin`。

## 開發方式

每個功能先寫測試再實作（TDD）：後端 xUnit，前端 Angular／Vitest。

學習交接筆記見 [`docs/handoff.md`](docs/handoff.md)（非作品說明本體）。
