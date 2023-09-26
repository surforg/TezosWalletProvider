using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Beacon.Sdk.Beacon.Sign;
using Surf.Helpers;
using Surf.Logger;
using Surf.Model;
using Surf.Platform.Client;
using Surf.WalletProvider.Interface;
using TezosSDK.Tezos;
using TezosSDK.Tezos.Wallet;
using UnityEngine;

namespace InfinitySDK.Scripts.Provider
{
    public class TezosWalletProvider : IWalletProviderAndroid, IWalletProviderIOS, IWalletProviderWebGL
    {
        private TaskCompletionSource<WalletData> _connectionTcs;
        private TaskCompletionSource<MessageSignData> _signingTcs;
        private TaskCompletionSource<string> _disconnectionTcs;
        
        private Tezos _tezos;
#if MAINNET
        private const string KEY_NETWORK_NAME = "mainnet";//"jakartanet";
        private const string KEY_NETWORK_RPC = "https://rpc.tzbeta.net/";//"https://jakartanet.tezos.marigold.dev";
        private const string KEY_CONTRACT_ADDRESS = "KT1WguzxyLmuKbJhz3jNuoRzzaUCncfp6PFE";//"KT1DMWAeaP6wxKWPFDLGDkB7xUg563852AjD";
        private const string KEY_INDEXER_NODE = "https://api.mainnet.tzkt.io/v1/operations/{0}/status"; //"https://api.mainnet.tzkt.io/v1/operations/{transactionHash}/status";
        private const int KEY_SOFT_CURRENCY_ID = 0;
#else
        private const string KEY_NETWORK_NAME = "ghostnet";//"jakartanet";
        private const string KEY_NETWORK_RPC = "https://rpc.ghostnet.teztnets.xyz";//"https://jakartanet.tezos.marigold.dev";
        private const string KEY_CONTRACT_ADDRESS = "KT1WguzxyLmuKbJhz3jNuoRzzaUCncfp6PFE";//"KT1DMWAeaP6wxKWPFDLGDkB7xUg563852AjD";
        private const string KEY_INDEXER_NODE = "https://api.ghostnet.tzkt.io/v1/operations/{0}/status"; //"https://api.mainnet.tzkt.io/v1/operations/{transactionHash}/status";
        private const int KEY_SOFT_CURRENCY_ID = 0;
#endif
        
        private string _payload;
        private string _publicKey;

        private WalletData _walletData;
        
        public async Task Initialize()
        {
            _tezos = new Tezos();
            _tezos.MessageReceiver.AccountConnected += OnAccountConnected;
            _tezos.MessageReceiver.AccountDisconnected += OnAccountDisconnected;
            _tezos.MessageReceiver.AccountConnectionFailed += OnAccountConnectionFailed;
            _tezos.MessageReceiver.ContractCallFailed += OnContractCallFailed;
            _tezos.MessageReceiver.PayloadSigned += OnPayloadSigned;
            return;
        }
        
        public async Task<WalletData> ConnectToWallet()
        {
            _connectionTcs = new TaskCompletionSource<WalletData>();
            SurfLogger.Log($"Going for connection");
            _tezos.Wallet.Connect(WalletProviderType.beacon);
            return await _connectionTcs.Task;
        }


        public async Task<MessageSignData> Sign(string walletAddress, string challenge)
        {
            _payload = challenge;
            _signingTcs = new TaskCompletionSource<MessageSignData>();
            SurfLogger.Log($"Going for signing");
            _tezos.Wallet.RequestSignPayload(SignPayloadType.micheline, challenge);
#if UNITY_ANDROID
            //_tezos.RequestPermission();
            Application.OpenURL($"tezos://?type=tzip10");            
#endif
            return await _signingTcs.Task;
        }

        public event Action<WalletData> Connected;
        public event Action<WalletData> Disconnected;
        public event Action Rejected;
        public void Connect()
        {
            ConnectToWallet();
        }

        public void Disconnect()
        {
            _disconnectionTcs = new TaskCompletionSource<string>();
            SurfLogger.Log($"Going for disconnection");
#if UNITY_ANDROID
            _tezos.Wallet.Disconnect();
            return;
#endif
            var tmp = _walletData;
            _walletData = null;
            Disconnected?.Invoke(tmp);
        }
        private void OnAccountConnected(string result)
        {
            /*if (_connectionTcs == null)
            {
                SurfLogger.LogWarning($"OnAccountConnected received while _connectionTcs is null");
                return;
            }*/
            
            MainThreadDispatcher.AddMainThreadCallback(() =>
            {
                SurfLogger.Log($"[TEST] OnAccountConnected.result:{result}");
                var json = JsonSerializer.Deserialize<JsonElement>(result);
                var account = json.GetProperty("accountInfo");
                var address = account.GetProperty("address").GetString();
                var publicKey = account.GetProperty("publicKey").GetString();

                if (string.IsNullOrEmpty(address))
                {
                    _connectionTcs?.TrySetException(new InvalidOperationException("Wallet address is not valid"));
                    Rejected?.Invoke();
                    return;
                }
                
                _publicKey = publicKey;
                _walletData = new WalletData
                {
                    WalletAddress = address, /*PublicKey = publicKey,*/ ChainID = "0", IdentType = IdentType.TezosWallet
                };
                _connectionTcs?.TrySetResult(_walletData);
                Connected?.Invoke(_walletData);
            });
        }

        private void OnAccountDisconnected(string result)
        {
            SurfLogger.Log($"[TEST] OnAccountDisconnected.result:{result}");
            var tmp = _walletData;
            _walletData = null;
            Disconnected?.Invoke(tmp);
        }

        private void OnAccountConnectionFailed(string result)
        {
            SurfLogger.Log($"[TEST] OnAccountConnectionFailed.result:{result}");
            _connectionTcs?.TrySetException(new InvalidOperationException("Wallet connection rejected by user"));
            Rejected?.Invoke();
        }
        
        private void OnPayloadSigned(string signature)
        {
            MainThreadDispatcher.AddMainThreadCallback(() =>
            {
                SurfLogger.Log($"[TEST] OnPayloadSigned.signature:{signature}");
                var signatureDeserialized = signature;
                var json = JsonSerializer.Deserialize<JsonElement>(signature);
                signatureDeserialized = json.GetProperty("signature").GetString();
                SurfLogger.Log($"[TEST] publicKey:{_publicKey} || payload:{_payload} || signature:{signatureDeserialized}");
                _signingTcs.TrySetResult(new MessageSignData {MessageSignature = signatureDeserialized});
            });
        }
        
        private void OnContractCallFailed(string result)
        {
            SurfLogger.Log($"[TEST] OnContractCallFailed.result:{result}");
        }
    }
}