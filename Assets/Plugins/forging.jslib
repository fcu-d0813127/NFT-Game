mergeInto(LibraryManager.library, {
  ForgingSmartContract: async function(
      part,
      amountOfRuby,
      amountOfSapphire,
      amountOfEmerald) {
    const canUseRubyBalanceOf = await window.rubyContract.methods.allowance(
        window.data.PLAYER_ACCOUNT, window.data.MAJOR_ADDRESS).call()
            .then((responce) => {
              return responce;
            });
    const canUseSapphireBalanceOf = await window.sapphireContract.methods
        .allowance(
            window.data.PLAYER_ACCOUNT, window.data.MAJOR_ADDRESS).call()
                .then((responce) => {
                  return responce;
                });
    const canUseEmeraldBalanceOf = await window.emeraldContract.methods
        .allowance(
            window.data.PLAYER_ACCOUNT, window.data.MAJOR_ADDRESS).call()
                .then((responce) => {
                  return responce;
                });
    let isApprove = false;
    if (canUseRubyBalanceOf < amountOfRuby ||
        canUseSapphireBalanceOf < amountOfSapphire ||
        canUseSapphireBalanceOf < amountOfSapphire) {
      myGameInstance.SendMessage(
          'ApproveResponseController',
          'Open');
    }
    if (canUseRubyBalanceOf < amountOfRuby) {
      isApprove = true;
      await window.rubyContract.methods.approve(
          window.data.MAJOR_ADDRESS, window.web3.utils.toWei('1', 'tether'))
              .send({
                from: window.data.PLAYER_ACCOUNT
              }).on('error', function(error, receipt) {
                myGameInstance.SendMessage(
                    'ApproveResponseController',
                    'Cancel');
                myGameInstance.SendMessage(
                    'ApproveResponseController',
                    'CloseLoading');
              });
    }
    if (canUseSapphireBalanceOf < amountOfSapphire) {
      isApprove = true;
      await window.sapphireContract.methods.approve(
          window.data.MAJOR_ADDRESS, window.web3.utils.toWei('1', 'tether'))
              .send({
                from: window.data.PLAYER_ACCOUNT
              }).on('error', function(error, receipt) {
                myGameInstance.SendMessage(
                    'ApproveResponseController',
                    'Cancel');
                myGameInstance.SendMessage(
                    'ApproveResponseController',
                    'CloseLoading');
              });
    }
    if (canUseEmeraldBalanceOf < amountOfEmerald) {
      isApprove = true;
      await window.emeraldContract.methods.approve(
          window.data.MAJOR_ADDRESS, window.web3.utils.toWei('1', 'tether'))
              .send({
                from: window.data.PLAYER_ACCOUNT
              }).on('error', function(error, receipt) {
                myGameInstance.SendMessage(
                    'ApproveResponseController',
                    'Cancel');
                myGameInstance.SendMessage(
                    'ApproveResponseController',
                    'CloseLoading');
              });
    }
    if (isApprove) {
      myGameInstance.SendMessage(
          'ApproveResponseController',
          'Cancel');
    }
    let tokenId;
    await window.majorContract.methods.forge(
        part, amountOfRuby, amountOfSapphire, amountOfEmerald).send({
          from: window.data.PLAYER_ACCOUNT
        }).on('receipt', function(receipt) {
          const eventLength = Object.keys(receipt.events).length;
          const tokenIdHex = receipt.events[eventLength - 1].raw.topics[3];
          tokenId = parseInt(tokenIdHex, 16);
        }).on('error', function(error, receipt) {
          myGameInstance.SendMessage(
              'ForgingButtonController',
              'Cancel');
        });
    myGameInstance.SendMessage(
        'ForgingButtonController',
        'SetTokenId',
        tokenId);
    let equipmentsUri = '';
    await window.equipmentContract.methods.tokenURI(tokenId).call()
        .then((response) => {
          let hash = response.split('/');
          equipmentsUri += `/${hash[hash.length - 1]}`;
        });
    myGameInstance.SendMessage(
        'ForgingButtonController',
        'SetEquipment',
        equipmentsUri);
  }
});
