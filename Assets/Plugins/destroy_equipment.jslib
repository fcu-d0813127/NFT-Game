mergeInto(LibraryManager.library, {
  DestroyEquipmentSmartContract: async function(tokenId) {
    const approveAddress = await window.equipmentContract.methods
        .getApproved(tokenId).call()
          .then((response) => {
            return response;
          });
    if (approveAddress != window.data.MAJOR_ADDRESS) {
      await window.equipmentContract.methods
          .approve(window.data.MAJOR_ADDRESS, tokenId).send({
            from: window.data.PLAYER_ACCOUNT
          });
    }
    await window.majorContract.methods.destroyEquipment(tokenId).send({
          from: window.data.PLAYER_ACCOUNT
        });
    myGameInstance.SendMessage(
      'DestroyEquipment',
      'ClearMouse');
  }
});
