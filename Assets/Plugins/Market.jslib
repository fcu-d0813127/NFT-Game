mergeInto(LibraryManager.library, {
  ProductList: async function() {
    await window.Market_Contract.methods.getProductList().call()
      .then((response) => {
        myGameInstance.SendMessage(
        'MarketCanvas',
        'SetProductList',
        response.toString());
      });
  },
  GetBalanceOf: async function() {
    await window.ERC20_Contract.methods.balanceOf(window.data.PLAYER_ACCOUNT).call()
      .then((response) => {
        myGameInstance.SendMessage(
        'MarketCanvas',
        'setBalanceOf',
        response.toString());
      });
  },
  GetApprove: async function() {
    await window.ERC20_Contract.methods.approve(window.data.MARKET_ADDRESS, window.web3.utils.toWei('1', 'tether')).send({from: window.data.PLAYER_ACCOUNT});
      myGameInstance.SendMessage('MarketCanvas', 'Purchase');
  },
  PurchaseProduct: async function(_tokenId) {
    await window.Market_Contract.methods.purchaseProduct(_tokenId).send({from: window.data.PLAYER_ACCOUNT});
    myGameInstance.SendMessage('MarketCanvas', 'PurchaseMessage');
  },
  GetAllowanceOf: async function(_price, _balance){
    const price = UTF8ToString(_price);
    const balance = UTF8ToString(_balance);
    if(balance >= price){
      await window.ERC20_Contract.methods.allowance(window.data.PLAYER_ACCOUNT, window.data.MARKET_ADDRESS).call()
      .then((response) => {
        if(response >= price){
          myGameInstance.SendMessage(
          'MarketCanvas',
          'Purchase');
        }
        else{
          myGameInstance.SendMessage(
          'MarketCanvas',
          'ERC20Approve');
        }
      })
    }
    else{
      myGameInstance.SendMessage(
      'MarketCanvas',
      'ErrorMessage');
    }
  }
});
