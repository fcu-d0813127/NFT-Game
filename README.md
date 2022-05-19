# NFT Game Formal Edition
## 前置動作
- 在Unity最上方找到 Window -> Package Manager -> 在+號的右邊，切換到"Unity Registry" -> 找到"Input System"然後import.
- 當準備要Build成WebGL時先到File -> Build Settings -> Player Settings -> Other Settings -> 找到"Active Input Handling*" -> 切換到"Both".
## 功能
### 登入
- 基本的註冊跟登入功能
### 背包
- B鍵可以打開背包，背包開啟狀態下，ESC可關閉背包
- 使用滑鼠左鍵點擊道具或裝備
  - 當滑鼠沒有持有物品 && 被點擊的欄位有物品 -> 將物品拿起
    - 此條件下如按住shift則可將堆疊在一起的物品做分割
  - 當滑鼠持有物品 && 被點擊的欄位有物品
    - 兩者相同
      - 判斷該欄位還夠不夠放(這裡指該物品的堆疊上限)
        - 夠 -> 將滑鼠上的全部放進去
        - 不夠 -> 將該欄位補滿，剩餘的還在滑鼠上
        - 如被點擊的欄位物品已滿 -> 與手中的做交換
    - 兩者不同
      - 將兩者互換
### 角色移動
- 使用方向鍵來移動角色
### 角色狀態
- 使用S鍵來打開角色資訊，用ESC來關閉
- 當可分配能力點數不為0時，可以對自己的能力來分配，分配完成後按下Save按鈕來儲存