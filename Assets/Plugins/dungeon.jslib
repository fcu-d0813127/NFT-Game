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
      let approveNum;
      if (myBalanceOf < 1000) {
        approveNum = myBalanceOf;
      } else {
        approveNum = '1000';
      }
      await window.ERC20_Contract.methods.approve(
          window.data.MAJOR_ADDRESS, window.web3.utils.toWei(approveNum))
              .send({
                from: window.data.PLAYER_ACCOUNT
              });
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
  }
});