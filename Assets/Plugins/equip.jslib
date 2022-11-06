mergeInto(LibraryManager.library, {
  Equip: async function(
      helmet,
      chestplate,
      leggings,
      boots,
      weapon) {
    await window.majorContract.methods.equip(
        helmet, chestplate, leggings, boots, weapon).send({
          from: window.data.PLAYER_ACCOUNT
        }).on('error', function(error, receipt) {
          myGameInstance.SendMessage(
              'SaveEquip',
              'Cancel');
        });
    myGameInstance.SendMessage(
        'SaveEquip',
        'SaveEquips');
  }
});
