mergeInto(LibraryManager.library, {
  IsInited: async function(playerAccount) {
    await window.majorContract.methods.isInited(
        UTF8ToString(playerAccount)).call()
            .then((response) => {
              myGameInstance.SendMessage(
                  'Initialization',
                  'CheckInited',
                  response ? 1 : 0);
            });
  },
  Init: async function(name) {
    await window.majorContract.methods.init(
        UTF8ToString(name)).send({
          from: window.data.PLAYER_ACCOUNT
        });
    myGameInstance.SendMessage('CreateButton', 'LoadInitScene');
  },
  LoadSkill: async function(playerAccount) {
    await window.majorContract.methods.skillOf(
        UTF8ToString(playerAccount)).call()
            .then((response) => {
              myGameInstance.SendMessage(
                  'Initialization',
                  'SetSkill',
                  response.toString());
            });
  },
  LoadAbility: async function(playerAccount) {
    await window.majorContract.methods.abilityOf(
        UTF8ToString(playerAccount)).call()
            .then((response) => {
              for (let i = 0; i < 5; i++) {
                response[i] = undefined;
              }
              myGameInstance.SendMessage(
                  'Initialization',
                  'SetAbility',
                  JSON.stringify(response));
            });
  },
  LoadEquip: async function(playerAccount) {
    await window.majorContract.methods.equipmentOf(
        UTF8ToString(playerAccount)).call()
            .then((response) => {
              for (let i = 0; i < 5; i++) {
                response[i] = undefined;
              }
              myGameInstance.SendMessage(
                  'Initialization',
                  'SetEquips',
                  JSON.stringify(response));
            });
  },
  LoadEquipment: async function(playerAccount, isInit) {
    let account = UTF8ToString(playerAccount);
    let balanceOf = await window.equipmentContract.methods.balanceOf(
        account).call()
            .then((response) => {
              return response;
            });
    let tokenIds = [];
    let tokenIdString = '';
    for (let i = 0; i < balanceOf; i++) {
      await window.equipmentContract.methods.tokenOfOwnerByIndex(
          account, i).call()
              .then((response) => {
                tokenIdString += `/${response}`;
                tokenIds.push(response);
              });
    }
    let equipmentsUri = '';
    for (let i = 0; i < balanceOf; i++) {
      await window.equipmentContract.methods.tokenURI(
          tokenIds[i]).call()
              .then((response) => {
                let hash = response.split('/');
                equipmentsUri += `/${hash[hash.length - 1]}`;
              });
    }
    let targetGameObject = '';
    if (isInit == 1) {
      targetGameObject = 'Initialization';
    } else {
      targetGameObject = 'RefreshEquipment';
    }
    myGameInstance.SendMessage(
        targetGameObject,
        'SetEquipmentTokenId',
        tokenIdString);
    myGameInstance.SendMessage(
        targetGameObject,
        'SetEquipment',
        equipmentsUri);
  },
  LoadPlayerStatus: async function(playerAccount) {
    await window.majorContract.methods.playerStatusOf(
        UTF8ToString(playerAccount)).call()
            .then((response) => {
              for (let i = 0; i < 4; i++) {
                response[i] = undefined;
              }
              myGameInstance.SendMessage(
                  'Initialization',
                  'SetPlayerStatus',
                  JSON.stringify(response));
            });
  },
  LoadRuby: async function(playerAccount) {
    let balanceOf = await window.rubyContract.methods.balanceOf(
        UTF8ToString(playerAccount)).call()
            .then((response) => {
              return response;
            });
    let result = window.web3.utils.fromWei(balanceOf);
    myGameInstance.SendMessage(
        'Initialization',
        'SetRuby',
        result);
  },
  LoadSapphire: async function(playerAccount) {
    let balanceOf = await window.sapphireContract.methods.balanceOf(
        UTF8ToString(playerAccount)).call()
            .then((response) => {
              return response;
            });
    let result = window.web3.utils.fromWei(balanceOf);
    myGameInstance.SendMessage(
        'Initialization',
        'SetSapphire',
        result);
  },
  LoadEmerald: async function(playerAccount) {
    let balanceOf = await window.emeraldContract.methods.balanceOf(
        UTF8ToString(playerAccount)).call()
            .then((response) => {
              return response;
            });
    let result = window.web3.utils.fromWei(balanceOf);
    myGameInstance.SendMessage(
        'Initialization',
        'SetEmerald',
        result);
  },
  exchangeMaterial: async function(enemyBooty) {
    var exchange = UTF8ToString(enemyBooty).split(',');
    for(let i = 0; i < exchange.length; i++){
      console.log(exchange[i]);
    }
    var material = [0, 0, 0];
    await window.majorContract.methods.exchangeMaterial(exchange).send({from: window.data.PLAYER_ACCOUNT})
    .on('transactionHash', function(hash){
      console.log(hash);
    })
    .on('receipt', function(receipt){
      var decimalNumber = 0;
      for (var key in receipt.events) {
        decimalNumber = parseInt(receipt.events[key].raw.data, 16);
        if(receipt.events[key].address === window.data.RUBY_ADDRESS) {
          material[0] += decimalNumber / Math.pow(10, 18);
        }
        else if(receipt.events[key].address === window.data.SAPPHIRE_ADDRESS) {
          material[1] += decimalNumber / Math.pow(10, 18);
        }
        else if(receipt.events[key].address === window.data.EMERALD_ADDRESS) {
          material[2] += decimalNumber / Math.pow(10, 18);
        }
      }
    });
    var materialTemp = material[0] + ',' + material[1] + ',' + material[2];
    myGameInstance.SendMessage('FireTraderNPC', 'ShowRewardMaterial', materialTemp);
  }
});