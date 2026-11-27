# MiniAdmin Lab — 作品實戰（421）

從課綱 repo `csharp-mini-admin` 複製骨架；課綱回看請用原 repo。

原說明：

邊學邊做的小型後台管理系統：會員 + 商品 CRUD、JWT 登入、角色權限。

## 結構

```
mini-admin/
  MiniAdmin.slnx
  backend/
    MiniAdmin.Core/       商業邏輯（純 C#，Part 1 每章在這裡蓋）
    MiniAdmin.Core.Tests/ xUnit 測試
    MiniAdmin.Api/        ASP.NET Core Web API (.NET 10, Controllers)
    MiniAdmin.Api.Tests/  xUnit 測試
  learn/                  Playground 與第 1 章練習
  frontend/               Angular 22（standalone components）
```

> 路徑不要放在含 `#` 的資料夾：Angular 的 Vite 會把 `#` 當 URL fragment，`ng serve` 會白畫面。

## 啟動

```bash
# 後端
cd backend/MiniAdmin.Api && dotnet run

# 測試
dotnet test

# 前端（http://localhost:4200）
cd frontend && npm install && npm start
```

## 章節進度

兼用：Part 1–2 觀念順序跟 [W3Schools C# Syllabus](https://www.w3schools.com/cs/cs_syllabus.php)，相鄰頁併成一章（給有 PHP 經驗的人）；練習用該章觀念蓋 mini-admin。
Part 1 走完 Exceptions 之前，不教 `List`／泛型／LINQ／async。商業邏輯寫在 `backend/MiniAdmin.Core`。
**Part 4** 觀念順序對齊 [Vue Guide Essentials](https://vuejs.org/guide/essentials/application.html)，每一節映射 [Angular.dev](https://angular.dev/essentials)（Essentials 偏薄處改對 In-depth）；不單靠 Angular Essentials 幾頁帶過。

- [x] 第 0 章：環境骨架（.NET 10 API + xUnit + Angular 22）＝ Get Started

**Part 1：W3Schools C# × 專案（`MiniAdmin.Core`）**

| 章 | 觀念 | W3Schools | 專案任務 |
|---|---|---|---|
| [x] 1 | 程式結構、Console | Syntax, Output, Comments | Hello 練習（`learn/`） |
| [x] 2 | 型別與變數、轉型 | Variables, Data Types, Type Casting, User Input | 商品價格計算：含稅價、數量轉換、平均評分 |
| [x] 3 | 運算子與字串 | Operators, Math, Strings | 會員編號 `M0001`、Email 遮罩、報表文字 |
| [x] 4 | 流程控制 | Booleans, If...Else, Switch, While, For, Break/Continue | 會員等級規則、密碼強度檢查 |
| [x] 5 | 陣列 | Arrays | 商品 Id 陣列、用 index 取值 |
| [x] 6 | 方法 | Methods, Parameters, Overloading | 把已學規則收成方法（多載、預設參數） |
| [x] 7 | 類別與物件 | OOP, Classes, Members, Constructors, Access, Properties | `Member`、`Product` 類別 |
| [x] 8 | 繼承與介面 | Inheritance, Polymorphism, Abstraction, Interface | `IMemberRepository`、一般會員 / 管理員 |
| [x] 9 | Enum、檔案、例外 | Enums, Files, Exceptions | 會員等級 enum、查無會員、簡單文字 log |

**Part 2：職場 C#（W3Schools 沒有的）**

| 章 | 觀念 | 專案任務 |
|---|---|---|
| [x] 10 | 集合與 Nullable | `List` 記憶體會員清單、暱稱 `string?` |
| [x] 11 | 泛型、委派、lambda | 通用分頁結果 `PagedResult<T>` |
| [x] 12 | LINQ | 會員搜尋、篩選、排序、分頁 |
| [x] 13 | async / await | Repository 改成非同步 |

**Part 3：後端 API**

- [x] 第 14 章：ASP.NET Core Web API（Controller、路由、DI），接上 `MiniAdmin.Core`
- [x] 第 15 章：EF Core + SQLite，取代記憶體版會員清單
- [x] 第 16 章：會員 CRUD API
- [x] 第 17 章：JWT 登入與角色權限

**Part 4：前端（Vue Essentials 順序 → Angular）**

| 章 | 觀念（Vue Essentials 對照） | Angular 官方 | 專案任務 |
|---|---|---|---|
| [x] 18 | App／模板／signal／事件 | [Components](https://angular.dev/essentials/components) + [Templates](https://angular.dev/essentials/templates) + [Signals](https://angular.dev/guide/signals) | 首頁標題 `MiniAdmin`、模擬登入狀態 |
| [x] 19 | computed／`@if`／`@for`／class | Signals `computed` + Templates 控制流 | 假資料會員清單：顯示、條件、列表 |
| [x] 20 | 表單綁定＋子元件（可帶一點 Material） | [Forms](https://angular.dev/guide/forms) + Components | 搜尋輸入＋`MembersList` 子元件 |
| [x] 21 | Service + HttpClient（對 Vue composable／fetch） | [DI](https://angular.dev/guide/di) + HttpClient | 串現有會員／登入 API |
| [x] 22 | 路由＋守衛＋登入頁 | [Router](https://angular.dev/guide/routing) + guards | 登入流程、未登入擋會員頁 |

Watchers／Lifecycle／Template Refs 本課暫略（專案用不到）。

**Part 5：收尾**

- [x] 第 23 章：Docker 與部署概念

每章採 TDD：先寫失敗測試 → 確認紅燈 → 實作變綠燈。

## 學習時程（2026/11/21 – 12 月初）

| 日期 | 章節 |
|---|---|
| 11/21（六） | 地基複習、第 8、9 章（Part 1 收尾；10、11 章順延） |
| 11/22（日） | 第 10、11、12 章（有餘力再碰 13） |
| 11/23（一） | 第 13、14 章（async、API 骨架） |
| 11/24（二） | 第 15 章（EF Core + SQLite） |
| 11/25（三） | 第 16 章（CRUD API） |
| 11/26（四） | 第 17 章（JWT）；可開始第 18（Vue→Angular 地圖） |
| 11/27（五） | 休息 |
| 11/28（六） | 第 18、19 章（模板／signal／事件；computed／控制流） |
| 11/29（日） | 第 20 章 + buffer；前端未完可延 |
| 12/1 起 | 第 21–22 已完；先第 23（Docker）／作品實戰，總複習排作品後 |

- 14 到 17 章每天以 2 章為上限，進度落後就用 buffer 補，不壓縮這幾章的時間。
- **Part 4 地基優先**：多出的前端章可吃進 12/1 起前幾天，不要為趕時程跳過 Vue→Angular 對照磚塊。
- **地基檢查點**：每完成一個 Part（Part 1 / 2 / 3 / 4）做一次短複習，卡過的觀念重寫一次。
- **總複習（必做，排在作品後）**：先做第 23／作品實戰（side project）；摸熟後再花半天到一天總複習（口頭講解 + 盲寫關鍵片段 + 從 API 追到前端）。不要第 22 剛完就考。
- 作品實戰兩週：前後端打通與自己的功能；總複習放作品後。
- **Cursor ↔ Grok 交接**：共用 `docs/handoff.md`（Mac Cursor 每章更新；iPhone 開這份貼給 Grok，或 push 後用 GitHub App 看）。
