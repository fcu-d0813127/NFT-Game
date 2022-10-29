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
              }).on('error', function(error, receipt) {
                myGameInstance.SendMessage(
                    'ApproveResponseController',
                    'Cancel');
              });
      myGameInstance.SendMessage(
          'ApproveResponseController',
          'Cancel');
    }
    await window.majorContract.methods.enterDungeon(indexOfDungeon).send({
          from: window.data.PLAYER_ACCOUNT
        }).on('error', function(error, receipt) {
          myGameInstance.SendMessage(
            'Entry',
            'Cancel');
        });
    myGameInstance.SendMessage(
        'Entry',
        'EntryDungeonScene');
    await window.ERC20_Contract.methods.balanceOf(
        window.data.PLAYER_ACCOUNT).call()
            .then((response) => {
              console.log('My balance of: ' + window.web3.utils.fromWei(response));
            });
  }
});
