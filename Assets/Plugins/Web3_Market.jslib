mergeInto(LibraryManager.library, {
  OnLogin: async function() {
    if (window.ethereum) {
      const PLAYER_ACCOUNT = (await window.ethereum.request({
          method: 'eth_requestAccounts'
      }))[0];
      const CONTRACT_Market_ADDRESS = '0x3EA04BB8058cCE8261729c1832D3377229e976FB';
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
                                    },
                                    {
                                      "inputs": [
                                        {
                                          "internalType": "address",
                                          "name": "_account",
                                          "type": "address"
                                        }
                                      ],
                                      "name": "productListFromOwnerOf",
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
      const CONTRACT_ERC20_ADDRESS = '0x8061981898f8A26c5bB6fFE263AA4188B0c7CcD8';
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
      const CONTRACT_ERC721_ADDRESS = '0x75e2Ad969737ffc773d36aF22c6eb4cF8bF106BD';
      const CONTRACT_ERC721_ABI = [
                                    {
                                      "inputs": [],
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
                                          "name": "approved",
                                          "type": "address"
                                        },
                                        {
                                          "indexed": true,
                                          "internalType": "uint256",
                                          "name": "tokenId",
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
                                          "name": "owner",
                                          "type": "address"
                                        },
                                        {
                                          "indexed": true,
                                          "internalType": "address",
                                          "name": "operator",
                                          "type": "address"
                                        },
                                        {
                                          "indexed": false,
                                          "internalType": "bool",
                                          "name": "approved",
                                          "type": "bool"
                                        }
                                      ],
                                      "name": "ApprovalForAll",
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
                                          "indexed": true,
                                          "internalType": "uint256",
                                          "name": "tokenId",
                                          "type": "uint256"
                                        }
                                      ],
                                      "name": "Transfer",
                                      "type": "event"
                                    },
                                    {
                                      "inputs": [],
                                      "name": "NAME",
                                      "outputs": [
                                        {
                                          "internalType": "string",
                                          "name": "",
                                          "type": "string"
                                        }
                                      ],
                                      "stateMutability": "view",
                                      "type": "function",
                                      "constant": true
                                    },
                                    {
                                      "inputs": [],
                                      "name": "SYMBOL",
                                      "outputs": [
                                        {
                                          "internalType": "string",
                                          "name": "",
                                          "type": "string"
                                        }
                                      ],
                                      "stateMutability": "view",
                                      "type": "function",
                                      "constant": true
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
                                          "name": "tokenId",
                                          "type": "uint256"
                                        }
                                      ],
                                      "name": "approve",
                                      "outputs": [],
                                      "stateMutability": "nonpayable",
                                      "type": "function"
                                    },
                                    {
                                      "inputs": [
                                        {
                                          "internalType": "address",
                                          "name": "owner",
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
                                      "type": "function",
                                      "constant": true
                                    },
                                    {
                                      "inputs": [
                                        {
                                          "internalType": "uint256",
                                          "name": "tokenId",
                                          "type": "uint256"
                                        }
                                      ],
                                      "name": "getApproved",
                                      "outputs": [
                                        {
                                          "internalType": "address",
                                          "name": "",
                                          "type": "address"
                                        }
                                      ],
                                      "stateMutability": "view",
                                      "type": "function",
                                      "constant": true
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
                                          "name": "operator",
                                          "type": "address"
                                        }
                                      ],
                                      "name": "isApprovedForAll",
                                      "outputs": [
                                        {
                                          "internalType": "bool",
                                          "name": "",
                                          "type": "bool"
                                        }
                                      ],
                                      "stateMutability": "view",
                                      "type": "function",
                                      "constant": true
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
                                      "type": "function",
                                      "constant": true
                                    },
                                    {
                                      "inputs": [],
                                      "name": "owner",
                                      "outputs": [
                                        {
                                          "internalType": "address",
                                          "name": "",
                                          "type": "address"
                                        }
                                      ],
                                      "stateMutability": "view",
                                      "type": "function",
                                      "constant": true
                                    },
                                    {
                                      "inputs": [
                                        {
                                          "internalType": "uint256",
                                          "name": "tokenId",
                                          "type": "uint256"
                                        }
                                      ],
                                      "name": "ownerOf",
                                      "outputs": [
                                        {
                                          "internalType": "address",
                                          "name": "",
                                          "type": "address"
                                        }
                                      ],
                                      "stateMutability": "view",
                                      "type": "function",
                                      "constant": true
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
                                          "name": "tokenId",
                                          "type": "uint256"
                                        }
                                      ],
                                      "name": "safeTransferFrom",
                                      "outputs": [],
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
                                          "name": "tokenId",
                                          "type": "uint256"
                                        },
                                        {
                                          "internalType": "bytes",
                                          "name": "_data",
                                          "type": "bytes"
                                        }
                                      ],
                                      "name": "safeTransferFrom",
                                      "outputs": [],
                                      "stateMutability": "nonpayable",
                                      "type": "function"
                                    },
                                    {
                                      "inputs": [
                                        {
                                          "internalType": "address",
                                          "name": "operator",
                                          "type": "address"
                                        },
                                        {
                                          "internalType": "bool",
                                          "name": "approved",
                                          "type": "bool"
                                        }
                                      ],
                                      "name": "setApprovalForAll",
                                      "outputs": [],
                                      "stateMutability": "nonpayable",
                                      "type": "function"
                                    },
                                    {
                                      "inputs": [],
                                      "name": "specificContract",
                                      "outputs": [
                                        {
                                          "internalType": "address",
                                          "name": "",
                                          "type": "address"
                                        }
                                      ],
                                      "stateMutability": "view",
                                      "type": "function",
                                      "constant": true
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
                                      "type": "function",
                                      "constant": true
                                    },
                                    {
                                      "inputs": [
                                        {
                                          "internalType": "uint256",
                                          "name": "index",
                                          "type": "uint256"
                                        }
                                      ],
                                      "name": "tokenByIndex",
                                      "outputs": [
                                        {
                                          "internalType": "uint256",
                                          "name": "",
                                          "type": "uint256"
                                        }
                                      ],
                                      "stateMutability": "view",
                                      "type": "function",
                                      "constant": true
                                    },
                                    {
                                      "inputs": [
                                        {
                                          "internalType": "address",
                                          "name": "owner",
                                          "type": "address"
                                        },
                                        {
                                          "internalType": "uint256",
                                          "name": "index",
                                          "type": "uint256"
                                        }
                                      ],
                                      "name": "tokenOfOwnerByIndex",
                                      "outputs": [
                                        {
                                          "internalType": "uint256",
                                          "name": "",
                                          "type": "uint256"
                                        }
                                      ],
                                      "stateMutability": "view",
                                      "type": "function",
                                      "constant": true
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
                                      "type": "function",
                                      "constant": true
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
                                          "name": "tokenId",
                                          "type": "uint256"
                                        }
                                      ],
                                      "name": "transferFrom",
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
                                      "name": "tokenStatOf",
                                      "outputs": [
                                        {
                                          "internalType": "uint8",
                                          "name": "rarity",
                                          "type": "uint8"
                                        },
                                        {
                                          "internalType": "uint8",
                                          "name": "part",
                                          "type": "uint8"
                                        },
                                        {
                                          "internalType": "uint8",
                                          "name": "level",
                                          "type": "uint8"
                                        },
                                        {
                                          "components": [
                                            {
                                              "internalType": "uint16",
                                              "name": "atk",
                                              "type": "uint16"
                                            },
                                            {
                                              "internalType": "uint16",
                                              "name": "matk",
                                              "type": "uint16"
                                            },
                                            {
                                              "internalType": "uint16",
                                              "name": "def",
                                              "type": "uint16"
                                            },
                                            {
                                              "internalType": "uint16",
                                              "name": "mdef",
                                              "type": "uint16"
                                            },
                                            {
                                              "internalType": "uint16",
                                              "name": "cri",
                                              "type": "uint16"
                                            },
                                            {
                                              "internalType": "uint16",
                                              "name": "criDmgRatio",
                                              "type": "uint16"
                                            }
                                          ],
                                          "internalType": "struct Nft.Attribute",
                                          "name": "attribute",
                                          "type": "tuple"
                                        },
                                        {
                                          "internalType": "uint8[3]",
                                          "name": "skills",
                                          "type": "uint8[3]"
                                        }
                                      ],
                                      "stateMutability": "view",
                                      "type": "function",
                                      "constant": true
                                    },
                                    {
                                      "inputs": [
                                        {
                                          "internalType": "uint256",
                                          "name": "tokenId",
                                          "type": "uint256"
                                        }
                                      ],
                                      "name": "tokenURI",
                                      "outputs": [
                                        {
                                          "internalType": "string",
                                          "name": "",
                                          "type": "string"
                                        }
                                      ],
                                      "stateMutability": "view",
                                      "type": "function",
                                      "constant": true
                                    },
                                    {
                                      "inputs": [
                                        {
                                          "internalType": "bytes4",
                                          "name": "interfaceId",
                                          "type": "bytes4"
                                        }
                                      ],
                                      "name": "supportsInterface",
                                      "outputs": [
                                        {
                                          "internalType": "bool",
                                          "name": "",
                                          "type": "bool"
                                        }
                                      ],
                                      "stateMutability": "view",
                                      "type": "function",
                                      "constant": true
                                    },
                                    {
                                      "inputs": [
                                        {
                                          "internalType": "address",
                                          "name": "_player",
                                          "type": "address"
                                        },
                                        {
                                          "internalType": "uint8",
                                          "name": "_rarity",
                                          "type": "uint8"
                                        },
                                        {
                                          "internalType": "uint8",
                                          "name": "_part",
                                          "type": "uint8"
                                        },
                                        {
                                          "internalType": "uint8",
                                          "name": "_level",
                                          "type": "uint8"
                                        },
                                        {
                                          "internalType": "uint16",
                                          "name": "_atk",
                                          "type": "uint16"
                                        },
                                        {
                                          "internalType": "uint16",
                                          "name": "_matk",
                                          "type": "uint16"
                                        },
                                        {
                                          "internalType": "uint16",
                                          "name": "_def",
                                          "type": "uint16"
                                        },
                                        {
                                          "internalType": "uint16",
                                          "name": "_mdef",
                                          "type": "uint16"
                                        },
                                        {
                                          "internalType": "uint16",
                                          "name": "_cri",
                                          "type": "uint16"
                                        },
                                        {
                                          "internalType": "uint16",
                                          "name": "_criDmgRatio",
                                          "type": "uint16"
                                        },
                                        {
                                          "internalType": "uint8[3]",
                                          "name": "_skills",
                                          "type": "uint8[3]"
                                        }
                                      ],
                                      "name": "createNFT",
                                      "outputs": [
                                        {
                                          "internalType": "uint256",
                                          "name": "",
                                          "type": "uint256"
                                        }
                                      ],
                                      "stateMutability": "nonpayable",
                                      "type": "function"
                                    },
                                    {
                                      "inputs": [
                                        {
                                          "internalType": "address",
                                          "name": "_contract",
                                          "type": "address"
                                        }
                                      ],
                                      "name": "setSpecificContract",
                                      "outputs": [],
                                      "stateMutability": "nonpayable",
                                      "type": "function"
                                    }
                                  ];
      window.web3 = new Web3(window.ethereum);
      window.data = {};
      window.data.PLAYER_ACCOUNT = PLAYER_ACCOUNT;
      window.data.MARKET_ADDRESS = "0x3EA04BB8058cCE8261729c1832D3377229e976FB";
      window.data.ERC20_ADDRESS = "0x8061981898f8A26c5bB6fFE263AA4188B0c7CcD8";
      window.Market_Contract = new window.web3.eth.Contract(
                              CONTRACT_Market_ABI, CONTRACT_Market_ADDRESS);
      window.ERC20_Contract = new window.web3.eth.Contract(
                              CONTRACT_ERC20_ABI, CONTRACT_ERC20_ADDRESS);
      window.ERC721_Contract = new window.web3.eth.Contract(
                              CONTRACT_ERC721_ABI, CONTRACT_ERC721_ADDRESS);
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
