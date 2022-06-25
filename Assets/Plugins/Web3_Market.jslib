mergeInto(LibraryManager.library, {
  OnLogin: async function() {
    if (window.ethereum) {
      const PLAYER_ACCOUNT = (await window.ethereum.request({
          method: 'eth_requestAccounts'
      }))[0];
      const CONTRACT_Market_ADDRESS = '0x3D9d48d8B80D1AE748513fa6c1d824c6CC465Fde';
      const CONTRACT_Market_ABI = [
                                    {
                                      "inputs": [
                                        {
                                          "internalType": "uint256",
                                          "name": "_tokenId",
                                          "type": "uint256"
                                        },
                                        {
                                          "internalType": "uint256",
                                          "name": "_price",
                                          "type": "uint256"
                                        }
                                      ],
                                      "name": "listProduct",
                                      "outputs": [],
                                      "stateMutability": "nonpayable",
                                      "type": "function"
                                    },
                                    {
                                      "inputs": [
                                        {
                                          "internalType": "uint256",
                                          "name": "_tokenId",
                                          "type": "uint256"
                                        }
                                      ],
                                      "name": "purchaseProduct",
                                      "outputs": [],
                                      "stateMutability": "nonpayable",
                                      "type": "function"
                                    },
                                    {
                                      "inputs": [
                                        {
                                          "internalType": "address",
                                          "name": "_addrERC20",
                                          "type": "address"
                                        },
                                        {
                                          "internalType": "address",
                                          "name": "_addrERC721",
                                          "type": "address"
                                        }
                                      ],
                                      "stateMutability": "nonpayable",
                                      "type": "constructor"
                                    },
                                    {
                                      "inputs": [
                                        {
                                          "internalType": "uint256",
                                          "name": "_tokenId",
                                          "type": "uint256"
                                        }
                                      ],
                                      "name": "unlistProduct",
                                      "outputs": [],
                                      "stateMutability": "nonpayable",
                                      "type": "function"
                                    },
                                    {
                                      "inputs": [],
                                      "name": "getProductList",
                                      "outputs": [
                                        {
                                          "components": [
                                            {
                                              "internalType": "uint256",
                                              "name": "productTokenId",
                                              "type": "uint256"
                                            },
                                            {
                                              "internalType": "address",
                                              "name": "productOwner",
                                              "type": "address"
                                            },
                                            {
                                              "internalType": "uint256",
                                              "name": "productPrice",
                                              "type": "uint256"
                                            }
                                          ],
                                          "internalType": "struct Market.Product[]",
                                          "name": "",
                                          "type": "tuple[]"
                                        }
                                      ],
                                      "stateMutability": "view",
                                      "type": "function"
                                    }
                                  ];
      const CONTRACT_ERC20_ADDRESS = '0xeCe1b9F2D9E26af7952bBBe53C0C6e7CD1Dc57aF';
      const CONTRACT_ERC20_ABI = [
                                  {
                                    "inputs": [
                                      {
                                        "internalType": "string",
                                        "name": "name_",
                                        "type": "string"
                                      },
                                      {
                                        "internalType": "string",
                                        "name": "symbol_",
                                        "type": "string"
                                      }
                                    ],
                                    "stateMutability": "nonpayable",
                                    "type": "constructor"
                                  },
                                  {
                                    "anonymous": false,
                                    "inputs": [
                                      {
                                        "indexed": true,
                                        "internalType": "address",
                                        "name": "owner",
                                        "type": "address"
                                      },
                                      {
                                        "indexed": true,
                                        "internalType": "address",
                                        "name": "spender",
                                        "type": "address"
                                      },
                                      {
                                        "indexed": false,
                                        "internalType": "uint256",
                                        "name": "value",
                                        "type": "uint256"
                                      }
                                    ],
                                    "name": "Approval",
                                    "type": "event"
                                  },
                                  {
                                    "anonymous": false,
                                    "inputs": [
                                      {
                                        "indexed": true,
                                        "internalType": "address",
                                        "name": "from",
                                        "type": "address"
                                      },
                                      {
                                        "indexed": true,
                                        "internalType": "address",
                                        "name": "to",
                                        "type": "address"
                                      },
                                      {
                                        "indexed": false,
                                        "internalType": "uint256",
                                        "name": "value",
                                        "type": "uint256"
                                      }
                                    ],
                                    "name": "Transfer",
                                    "type": "event"
                                  },
                                  {
                                    "inputs": [
                                      {
                                        "internalType": "address",
                                        "name": "owner",
                                        "type": "address"
                                      },
                                      {
                                        "internalType": "address",
                                        "name": "spender",
                                        "type": "address"
                                      }
                                    ],
                                    "name": "allowance",
                                    "outputs": [
                                      {
                                        "internalType": "uint256",
                                        "name": "",
                                        "type": "uint256"
                                      }
                                    ],
                                    "stateMutability": "view",
                                    "type": "function"
                                  },
                                  {
                                    "inputs": [
                                      {
                                        "internalType": "address",
                                        "name": "spender",
                                        "type": "address"
                                      },
                                      {
                                        "internalType": "uint256",
                                        "name": "amount",
                                        "type": "uint256"
                                      }
                                    ],
                                    "name": "approve",
                                    "outputs": [
                                      {
                                        "internalType": "bool",
                                        "name": "",
                                        "type": "bool"
                                      }
                                    ],
                                    "stateMutability": "nonpayable",
                                    "type": "function"
                                  },
                                  {
                                    "inputs": [
                                      {
                                        "internalType": "address",
                                        "name": "account",
                                        "type": "address"
                                      }
                                    ],
                                    "name": "balanceOf",
                                    "outputs": [
                                      {
                                        "internalType": "uint256",
                                        "name": "",
                                        "type": "uint256"
                                      }
                                    ],
                                    "stateMutability": "view",
                                    "type": "function"
                                  },
                                  {
                                    "inputs": [],
                                    "name": "decimals",
                                    "outputs": [
                                      {
                                        "internalType": "uint8",
                                        "name": "",
                                        "type": "uint8"
                                      }
                                    ],
                                    "stateMutability": "view",
                                    "type": "function"
                                  },
                                  {
                                    "inputs": [
                                      {
                                        "internalType": "address",
                                        "name": "spender",
                                        "type": "address"
                                      },
                                      {
                                        "internalType": "uint256",
                                        "name": "subtractedValue",
                                        "type": "uint256"
                                      }
                                    ],
                                    "name": "decreaseAllowance",
                                    "outputs": [
                                      {
                                        "internalType": "bool",
                                        "name": "",
                                        "type": "bool"
                                      }
                                    ],
                                    "stateMutability": "nonpayable",
                                    "type": "function"
                                  },
                                  {
                                    "inputs": [
                                      {
                                        "internalType": "address",
                                        "name": "spender",
                                        "type": "address"
                                      },
                                      {
                                        "internalType": "uint256",
                                        "name": "addedValue",
                                        "type": "uint256"
                                      }
                                    ],
                                    "name": "increaseAllowance",
                                    "outputs": [
                                      {
                                        "internalType": "bool",
                                        "name": "",
                                        "type": "bool"
                                      }
                                    ],
                                    "stateMutability": "nonpayable",
                                    "type": "function"
                                  },
                                  {
                                    "inputs": [],
                                    "name": "name",
                                    "outputs": [
                                      {
                                        "internalType": "string",
                                        "name": "",
                                        "type": "string"
                                      }
                                    ],
                                    "stateMutability": "view",
                                    "type": "function"
                                  },
                                  {
                                    "inputs": [],
                                    "name": "symbol",
                                    "outputs": [
                                      {
                                        "internalType": "string",
                                        "name": "",
                                        "type": "string"
                                      }
                                    ],
                                    "stateMutability": "view",
                                    "type": "function"
                                  },
                                  {
                                    "inputs": [],
                                    "name": "totalSupply",
                                    "outputs": [
                                      {
                                        "internalType": "uint256",
                                        "name": "",
                                        "type": "uint256"
                                      }
                                    ],
                                    "stateMutability": "view",
                                    "type": "function"
                                  },
                                  {
                                    "inputs": [
                                      {
                                        "internalType": "address",
                                        "name": "to",
                                        "type": "address"
                                      },
                                      {
                                        "internalType": "uint256",
                                        "name": "amount",
                                        "type": "uint256"
                                      }
                                    ],
                                    "name": "transfer",
                                    "outputs": [
                                      {
                                        "internalType": "bool",
                                        "name": "",
                                        "type": "bool"
                                      }
                                    ],
                                    "stateMutability": "nonpayable",
                                    "type": "function"
                                  },
                                  {
                                    "inputs": [
                                      {
                                        "internalType": "address",
                                        "name": "from",
                                        "type": "address"
                                      },
                                      {
                                        "internalType": "address",
                                        "name": "to",
                                        "type": "address"
                                      },
                                      {
                                        "internalType": "uint256",
                                        "name": "amount",
                                        "type": "uint256"
                                      }
                                    ],
                                    "name": "transferFrom",
                                    "outputs": [
                                      {
                                        "internalType": "bool",
                                        "name": "",
                                        "type": "bool"
                                      }
                                    ],
                                    "stateMutability": "nonpayable",
                                    "type": "function"
                                  }
                                ];
      window.web3 = new Web3(window.ethereum);
      window.data = {};
      window.data.PLAYER_ACCOUNT = PLAYER_ACCOUNT;
      window.data.MARKET_ADDRESS = "0x3D9d48d8B80D1AE748513fa6c1d824c6CC465Fde";
      window.data.ERC20_ADDRESS = "0xeCe1b9F2D9E26af7952bBBe53C0C6e7CD1Dc57aF";
      window.Market_Contract = new window.web3.eth.Contract(
                              CONTRACT_Market_ABI, CONTRACT_Market_ADDRESS);
      window.ERC20_Contract = new window.web3.eth.Contract(
                              CONTRACT_ERC20_ABI, CONTRACT_ERC20_ADDRESS);
      myGameInstance.SendMessage(
          'MarketCanvas',
          'ConnectAccount',
          PLAYER_ACCOUNT);
    }
  },
  EnableChangeAccountReload: function() {
    window.ethereum.on('accountsChanged', function() {
      window.location.reload();
    });
  }
});
