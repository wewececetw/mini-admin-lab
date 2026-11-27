# mini-admin-lab 學習交接（作品實戰）

更新時間：2026-11-27

## 進度

- 來源：從 `csharp-mini-admin`（課綱成品）複製骨架
- **作品實戰 421 必做已完成**；可選 **商品 Update 全線也完成**
- 已完成：
  - 第 1～2 刀＝登出／session（綠燈）
  - 第 3～9 刀＝商品列表／新增（後端＋前端綠燈）
  - 第 10～13 刀＝刪除全線（記憶體＋API＋前端綠燈）
  - 第 14 刀＝頁面導覽 `routerLink`（綠燈）
  - 第 15～16 刀＝會員 `MembersApi.create`＋頁面新增（綠燈）
  - 第 17～20 刀＝商品 Update（記憶體＋API＋ProductsApi＋頁面綠燈）
- 可選延伸（不擋收尾）：會員刪除、兩邊 CRUD 做滿、細角色
- 下一步：摸熟後可排**總複習**；課綱回看開 **mini-admin** repo
- 課綱回看：開 **mini-admin** repo，不要在本 repo 當講義館

## 今天／本次

- 題 17～20：商品 Update 全線；421 必做＋商品 Update 收尾
- 前端展示：登入卡、頂欄導覽、表單／列表版面；商品列可點入表單

## 學生卡過、需鞏固

- 登出曾誤用 `computed`
- `localStorage` 三處分工
- 記憶體 repo：`List` 要放欄位
- `http.get` 不要把參數貼在回傳型別後面
- API／前端外殼需老師先建
- `ApiProduct` 已同形時可直接 `set(list)`，不必再 map
- `post` 泛型：單筆用 `ApiProduct`，列表才用 `ApiProduct[]`
- signal 取值要加 `()`：`token()`／`id()`
- URL 複數：`/api/products`（曾寫成 `/api/product`）
- `delete` 回 `void`：用 `filter` 拿掉，不要 `[...list, p]`
- `routerLink` 要進 component `imports`，否則 `href` 是 null
- 會員列表列是 `MemberRow`：新增後要 map `{ id, name, active: true }`
- `http.put` 要 URL＋body＋headers 三件齊；別留空字串
- 更新列表用 `map` 替換同 id，不是 `[...list, p]`
- ts／html 要一起改（曾漏 `product-update-btn`）

## 421 範圍

- 必做：登出／session；商品 CRUD 主線；會員收斂（列表＋至少新增或刪除）→ **已齊**
- 商品 Update → **已做完**（原可砍項）
- 可砍：細角色、UI 大改、兩邊 CRUD 都做滿

## 開場句

- Mac：「總複習」或「可選：會員刪除」
- 回看課綱：切回 mini-admin
