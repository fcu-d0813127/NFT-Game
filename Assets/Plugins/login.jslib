mergeInto(LibraryManager.library, {
  IsInited: async function(playerAccount) {
    await window.myContract.methods.isInited(
              UTF8ToString(playerAccount)).call()
                  .then((response) => {
                    myGameInstance.SendMessage(
                        'Initialization',
                        'CheckInited',
                        response ? 1 : 0);
                  });
  },
  Init: async function(name, carrer) {
    await window.myContract.methods.init(
        UTF8ToString(name), parseInt(carrer, 10)).send({
            from: window.playerAccount
        });
    myGameInstance.SendMessage('CreateButton', 'LoadInitScene');
  },
  LoadSkill: async function(playerAccount) {
    await window.myContract.methods.skillOf(
              UTF8ToString(playerAccount)).call()
                  .then((response) => {
                    myGameInstance.SendMessage(
                        'Initialization',
                        'SetSkill',
                        response.toString());
                  });
  },
  LoadAbility: async function(playerAccount) {
    await window.myContract.methods.abilityOf(
              UTF8ToString(playerAccount)).call()
                  .then((response) => {
                    for (let i = 0; i < 4; i++) {
                      response[i] = undefined;
                    }
                    myGameInstance.SendMessage(
                        'Initialization',
                        'SetAbility',
                        JSON.stringify(response));
                  });
  },
  LoadEquipment: async function(playerAccount) {
    await window.myContract.methods.equipmentOf(
              UTF8ToString(playerAccount)).call()
                  .then((response) => {
                    for (let i = 0; i < 5; i++) {
                      response[i] = undefined;
                    }
                    myGameInstance.SendMessage(
                        'Initialization',
                        'SetEquipment',
                        JSON.stringify(response));
                  });
  },
  LoadPlayerStatus: async function(playerAccount) {
    await window.myContract.methods.playerStatusOf(
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
  }
});