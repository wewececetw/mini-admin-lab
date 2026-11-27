# MiniAdmin

小型後台練習專案：JWT 登入後管理**會員**與**商品**。

後端 ASP.NET Core + SQLite（會員）／記憶體（商品）；前端 Angular standalone。

## 做了什麼

- JWT 登入、登出、路由守衛
- **商品**：列表／新增／更新／刪除（前後端）
- **會員**：後端 CRUD；前端目前列表、搜尋、新增（尚未做修改／刪除）
- 單元測試（後端 xUnit、前端 Vitest）

刻意沒有：GitHub Actions／CI、細角色權限 UI、兩邊 CRUD 做滿。

## 怎麼啟動

要兩個終端，先後端再前端。

```bash
# 後端 → http://localhost:5212
cd backend/MiniAdmin.Api
dotnet run --launch-profile http

# 前端 → http://localhost:4200（proxy 轉 /api）
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

僅本機示範用，**不是正式帳密**：

| 帳號 | 密碼 | 備註 |
|------|------|------|
| `admin` | `secret` | Admin（可打會員刪除 API） |
| `user` | `secret` | 一般使用者 |

畫面也可直接按「模擬登入」（等同 admin）。

## 結構

```
backend/
  MiniAdmin.Core/        商業邏輯
  MiniAdmin.Core.Tests/
  MiniAdmin.Api/         Web API
  MiniAdmin.Api.Tests/
frontend/                Angular
```

> 路徑不要放在含 `#` 的資料夾，否則 `ng serve` 可能白畫面。
