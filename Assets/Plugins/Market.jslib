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
  ERC20balanceOf: async function() {
    await window.ERC20_Contract.methods.balanceOf(window.data.PLAYER_ACCOUNT).call()
      .then((response) => {
        myGameInstance.SendMessage(
        'MarketCanvas',
        'setBalanceOf',
        response.toString());
      });
  },
  ERC20approve: async function() {
    await window.ERC20_Contract.methods.approve(window.data.MARKET_ADDRESS, window.web3.utils.toWei('1', 'tether')).send({from: window.data.PLAYER_ACCOUNT});
    myGameInstance.SendMessage('MarketCanvas', 'Purchase');
  },
  purchaseProduct: async function(_tokenId) {
    await window.Market_Contract.methods.purchaseProduct(UTF8ToString(_tokenId)).send({from: window.data.PLAYER_ACCOUNT});
    myGameInstance.SendMessage('MarketCanvas', 'PurchaseMessage');
  },
  allowance: async function(_price, _balance){
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
      'ErrorMessage',
      1);
    }
  },
  ERC721approve: async function(_tokenId) {
    await window.ERC721_Contract.methods.approve(window.data.MARKET_ADDRESS, UTF8ToString(_tokenId)).send({from: window.data.PLAYER_ACCOUNT});
    myGameInstance.SendMessage('MarketCanvas', 'ListProduct');
  },
  listProduct: async function(_tokenId, _price) {
    await window.Market_Contract.methods.listProduct(UTF8ToString(_tokenId), UTF8ToString(_price)).send({from: window.data.PLAYER_ACCOUNT});
    myGameInstance.SendMessage('MarketCanvas', 'ListMessage');
  },
  unlistProduct: async function(_tokenId) {
    await window.Market_Contract.methods.unlistProduct(UTF8ToString(_tokenId)).send({from: window.data.PLAYER_ACCOUNT});
    myGameInstance.SendMessage('MarketCanvas', 'UnlistMessage');
  },
  ProductListFromOwnerOf: async function() {
    await window.Market_Contract.methods.productListFromOwnerOf(window.data.PLAYER_ACCOUNT).call()
      .then((response) => {
        myGameInstance.SendMessage(
          'MarketCanvas',
          'setUnlistProductList',
          response.toString());
      });
  },
  ERC721balanceOf: async function() {
    await window.ERC721_Contract.methods.balanceOf(window.data.PLAYER_ACCOUNT).call()
    .then((response) => {
      myGameInstance.SendMessage(
        'MarketCanvas',
        'getERC721balanceOf',
        response.toString());
    });
  },
  tokenOfOwnerByIndex: async function(_boundary) {
    var outputString = [];
    for(var i = 0; i < _boundary; i++){
      await window.ERC721_Contract.methods.tokenOfOwnerByIndex(window.data.PLAYER_ACCOUNT, i).call()
      .then((response) =>{
        outputString.push(response.toString());
      })
    }
    myGameInstance.SendMessage(
      'MarketCanvas',
      'getListTokenId',
      outputString.toString());
  },
  tokenStatOf: async function(_tokenId, _pagemode){
    const TokenId = UTF8ToString(_tokenId).split(',');
    var outputString = [];
    for(var i = 0; i < TokenId.length - 1; i++){
      await window.ERC721_Contract.methods.tokenStatOf(TokenId[i]).call()
      .then((response) =>{
        outputString.push(JSON.stringify(response));
      })
    }
    if(_pagemode == 1){
      myGameInstance.SendMessage(
      'MarketCanvas',
      'SetProductTokenStatList',
      outputString.toString());
    }
    else if(_pagemode == 2){
      myGameInstance.SendMessage(
      'MarketCanvas',
      'SetListProductTokenStatList',
      outputString.toString());
    }
    else if(_pagemode == 3){
      myGameInstance.SendMessage(
        'MarketCanvas',
        'SetUnlistProductTokenStatList',
        outputString.toString());
    }
  }
});
