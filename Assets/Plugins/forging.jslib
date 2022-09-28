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
              });
    }
    if (canUseSapphireBalanceOf < amountOfSapphire) {
      isApprove = true;
      await window.sapphireContract.methods.approve(
          window.data.MAJOR_ADDRESS, window.web3.utils.toWei('1', 'tether'))
              .send({
                from: window.data.PLAYER_ACCOUNT
              });
    }
    if (canUseEmeraldBalanceOf < amountOfEmerald) {
      isApprove = true;
      await window.emeraldContract.methods.approve(
          window.data.MAJOR_ADDRESS, window.web3.utils.toWei('1', 'tether'))
              .send({
                from: window.data.PLAYER_ACCOUNT
              });
    }
    if (isApprove) {
      myGameInstance.SendMessage(
          'ApproveResponseController',
          'Cancel');
    }
    const tokenId = await window.majorContract.methods.forge(
        part, amountOfRuby, amountOfSapphire, amountOfEmerald).send({
          from: window.data.PLAYER_ACCOUNT
        }).then((response) => {
          window.tempData = response;
          const eventLength = Object.keys(response.events).length;
          const tokenIdHex = response.events[eventLength - 1].raw.topics[3];
          const tokenIdDec = parseInt(tokenIdHex, 16);
          return tokenIdDec;
        });
    let equipments = [];
    await window.equipmentContract.methods.tokenStatOf(tokenId).call()
        .then((response) => {
          for (let i = 0; i < 5; i++) {
            response[i] = undefined;
          }
          const newResponse = {
            "tokenId": tokenId,
            "equipmentStatus": response
          };
          equipments.push(newResponse);
        });
    const newResponse = {
      "equipments": equipments
    };
    myGameInstance.SendMessage(
        'ForgingButtonController',
        'SetEquipment',
        JSON.stringify(newResponse));
  }
});
