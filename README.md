# customer.api.service  
## 介紹
customer api service 模擬自己的 API 服務串接客戶的 API 功能  

## 改版紀錄
- 新增【獲取遊戲列表】、【取得遊戲 Token】、【獲取信用】、【轉移信用】、【驗證轉移信用】、【提款所有信用】、【創建用戶】、【註銷用戶】、【取得注單明細】、【檢索歷史 URL】、【檢索輸贏】
- 把共用的部分，提到外部 NuGet 套件 31Library 上
- Request 中的 Timestamp 參數，預設由程式產生，看到此餐數無須填寫，避免驗證錯誤
- Request 中的 Method 參數，預設已經設定正確，無須再次覆蓋此內容，無須填寫

## 補充
- Visual Studio 2022 指定本地端 NuGet 套件來源
![離線安裝 NuGet 套件](https://i.imgur.com/rHldGdh.png)
