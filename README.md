# NFT Game Formal Edition

# 2022/06/24 此次進度大略完成

所有怪物程式碼重構並新增受傷狀態
## 副本2
* 新增三種怪物
* 新增城堡素材

## 副本3
* 沿用副本2怪物及素材
* 新增Boss1

## 新架構說明

### 普通怪物
* ```EnemyAiControllor``` : Ai狀態相關
* ```HpControllor``` : 管理Hp 

### Boss
* ```BossAiControllor``` : Ai狀態相關，新增了遠程攻擊(run狀態時機率觸發)
* ```HpControllor``` : 管理Hp

### HpBar
HpBar(血條顯示)與Hp管理分開了，普通敵人掛載在身上，Boss會掛在UI上

### EnemyCreater / DungeonOEnemyCreater
* ```EnemyCreater``` : 變成類似函式庫，只是處理生成的邏輯
* ```Dungeon編號EnemyCreater``` : 指定該場景要生成哪些怪物以及在哪裡

### Dungeon3Controllor
管理這個場景的特殊行為

處理Boss血條出現的時機以及攝影機拉近特效


