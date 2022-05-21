mergeInto(LibraryManager.library, {
  OnLogin: async function() {
    if (window.ethereum) {
      const web3 = new Web3(window.ethereum);
      const PLAYER_ACCOUNT = (await window.ethereum.request({
          method: 'eth_requestAccounts'
      }))[0];
      const CONTRACT_ADDRESS = '0xDaF261cFAFd378CE56104797dd68fa30f01f6f29';
      const CONTRACT_ABI = [{"inputs":[],"name":"clear","outputs":[],"stateMutability":"nonpayable","type":"function"},{"inputs":[{"internalType":"string","name":"_name","type":"string"},{"internalType":"uint8","name":"_carrer","type":"uint8"}],"name":"init","outputs":[],"stateMutability":"nonpayable","type":"function"},{"inputs":[{"internalType":"address","name":"_account","type":"address"}],"name":"abilityOf","outputs":[{"internalType":"uint256","name":"str","type":"uint256"},{"internalType":"uint256","name":"intllegence","type":"uint256"},{"internalType":"uint256","name":"dex","type":"uint256"},{"internalType":"uint256","name":"luk","type":"uint256"}],"stateMutability":"view","type":"function"},{"inputs":[{"internalType":"address","name":"_account","type":"address"}],"name":"equipmentOf","outputs":[{"internalType":"uint256","name":"helmet","type":"uint256"},{"internalType":"uint256","name":"chestplate","type":"uint256"},{"internalType":"uint256","name":"leggings","type":"uint256"},{"internalType":"uint256","name":"boots","type":"uint256"},{"internalType":"uint256","name":"weapon","type":"uint256"}],"stateMutability":"view","type":"function"},{"inputs":[{"internalType":"address","name":"_account","type":"address"}],"name":"isInited","outputs":[{"internalType":"bool","name":"","type":"bool"}],"stateMutability":"view","type":"function"},{"inputs":[{"internalType":"address","name":"_account","type":"address"}],"name":"playerStatusOf","outputs":[{"internalType":"string","name":"name","type":"string"},{"internalType":"uint8","name":"carrer","type":"uint8"},{"internalType":"uint256","name":"siteOfDungeon","type":"uint256"},{"internalType":"uint256","name":"timestamp","type":"uint256"}],"stateMutability":"view","type":"function"},{"inputs":[{"internalType":"address","name":"_account","type":"address"}],"name":"skillOf","outputs":[{"internalType":"uint8[8]","name":"skills","type":"uint8[8]"}],"stateMutability":"view","type":"function"}];
      window.playerAccount = PLAYER_ACCOUNT;
      window.myContract =
          new web3.eth.Contract(CONTRACT_ABI, CONTRACT_ADDRESS);
      myGameInstance.SendMessage(
          'LoginButton',
          'SetPlayerAccount',
          PLAYER_ACCOUNT);
    }
  },
  EnableChangeAccountReload: function() {
    window.ethereum.on('accountsChanged', function() {
      window.location.reload();
    });
  }
});