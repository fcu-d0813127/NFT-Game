mergeInto(LibraryManager.library, {
  AbilitySave: async function(
      str,
      intllegence,
      dex,
      vit,
      luk) {
    await window.majorContract.methods.distributeAbility(
        str, intllegence, dex, vit, luk).send({
          from: window.data.PLAYER_ACCOUNT
        });
  }
});
