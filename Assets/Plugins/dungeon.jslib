mergeInto(LibraryManager.library, {
  EntryDungeonSmartContract: async function(indexOfDungeon) {
    const dungeonCost = await window.majorContract.methods.dungeonOf(
        indexOfDungeon).call()
            .then((response) => {
              return window.web3.utils.fromWei(response.cost);
            });
    const myBalanceOf = await window.ERC20_Contract.methods.balanceOf(
        window.data.PLAYER_ACCOUNT).call()
            .then((response) => {
              return window.web3.utils.fromWei(response);
            });
    const canUseBalanceOf = await window.ERC20_Contract.methods.allowance(
        window.data.PLAYER_ACCOUNT, window.data.MAJOR_ADDRESS).call()
            .then((response) => {
              return window.web3.utils.fromWei(response);
            });
    if (canUseBalanceOf < dungeonCost) {
      if (myBalanceOf < dungeonCost) {
        console.log('Your money not enough!');
        return;
      }
      myGameInstance.SendMessage(
          'ApproveResponseController',
          'Open');
      await window.ERC20_Contract.methods.approve(
          window.data.MAJOR_ADDRESS, window.web3.utils.toWei('1', 'tether'))
              .send({
                from: window.data.PLAYER_ACCOUNT
              });
      myGameInstance.SendMessage(
          'ApproveResponseController',
          'Cancel');
    }
    await window.majorContract.methods.enterDungeon(indexOfDungeon).send({
      from: window.data.PLAYER_ACCOUNT
    })
    myGameInstance.SendMessage(
        'Entry',
        'EntryDungeonScene');
    await window.ERC20_Contract.methods.balanceOf(
        window.data.PLAYER_ACCOUNT).call()
            .then((response) => {
              console.log('My balance of: ' + window.web3.utils.fromWei(response));
            });
  },
  dungeonOf: async function(indexOfDungeon) {
    var enterControl = 0;
    await window.majorContract.methods.dungeonOf(
        indexOfDungeon).call()
            .then((response) => {
              var remainEnemy = response[1];
              var singleEnemy = response[3];
              for (var i = 0; i < 5; i++) {
                if (parseInt(remainEnemy[i], 10) < parseInt(singleEnemy[i], 10)) {
                  console.log(parseInt(remainEnemy[i], 10));
                  console.log(parseInt(singleEnemy[i], 10));
                  enterControl = 1;
                  break;
                }
              }
            });
    myGameInstance.SendMessage(
      'Entry',
      'SetEntryButtonInteractable',
      enterControl
    );
  }
});
