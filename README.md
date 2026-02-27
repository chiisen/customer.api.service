# 🚀 customer.api.service  
## ✨ 介紹
customer api service 模擬自身的 API 服務，用於串接客戶端 API 功能。 🔗

## 📝 改版紀錄
- 🚀 升級專案至 .NET 10，享受最新效能與語法特性。
- ✅ 新增【獲取遊戲列表】、【取得遊戲 Token】、【獲取信用】、【轉移信用】、【驗證轉移信用】、【提款所有信用】、【創建用戶】、【註銷用戶】、【取得注單明細】、【檢索歷史 URL】、【檢索輸贏】
- 📦 將共用元件模組化，收納至外部 NuGet 套件 `31Library`。
- 🕒 Request 中的 `Timestamp` 參數預設由程式自動產生，操作時無需手動填寫，以避免驗證錯誤。
- 🔧 Request 中的 `Method` 參數已預設配置正確，無需重複填寫或覆蓋。

## 💡 補充
- 🛠️ Visual Studio 2022 指定本地端 NuGet 套件來源指標
![離線安裝 NuGet 套件](https://i.imgur.com/rHldGdh.png) 🖼️


