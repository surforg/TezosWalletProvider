# 🏄‍♂️ Surf SDK Documentation

## 🌐 Connection Types

## Table of Contents

- [Connections and SDK API](#connections-and-sdk-api)
- [NFT API](#nft-api)
- [Game Room API](#game-room-api)
- [Storage API](#storage-api)
- [Token Balance API](#token-balance-api)
- [Remote Config API](#remote-config-api)
- [Reward APIs](#reward-api)
- [Persistent Game Rooms Guide](#persistent-game-rooms-guide)

## Connections and SDK API

### 🚀 SdkReady

**Description:** Subscribes to Surf SDK's initialization. Fires only one time.
- **Usage (Example 1):**
    ```csharp
    SurfApi.SdkReady += () => {
        // Your code here
    };
    ```
- **Usage (Example 2):**
    ```csharp
    SurfApi.SdkReady += OnSdkReady;
    private void OnSdkReady()
    {
        // Your code here
    }
    ```

### 🔒 EnsureSdkReady

**Description:** Subscribes to Surf SDK's initialization or fires action immediately. Can be used to 
- **Usage (Example 1):**
    ```csharp
    SurfApi.EnsureSdkReady += () => {
        // Your code here
    };
    ```
- **Usage (Example 2):**
    ```csharp
    SurfApi.EnsureSdkReady += OnEnsureSdkReady;
    private void OnEnsureSdkReady()
    {
        // Your code here
    }
    ```

### 📲 SocialConnected

**Description:** Subscribes to social connection event to let you know once the user is connected with social credentials like Gmail, Facebook, etc. Returns connected social details.
- **Usage (Example 1):**
    ```csharp
    SurfApi.SocialConnected += (socialData) => {
        // Your code here
    };
    ```
- **Usage (Example 2):**
    ```csharp
    SurfApi.SocialConnected += OnSocialConnected;
    private void OnSocialConnected(SocialLoginData socialData)
    {
        // Your code here
    }
    ```

### 🚪 SocialDisconnected

**Description:** Subscribes to social disconnection event to let you know once the user is disconnected from social credentials like Gmail, Facebook, etc. Returns disconnected social details.
- **Usage (Example 1):**
    ```csharp
    SurfApi.SocialDisconnected += (socialData) => {
        // Your code here
    };
    ```
- **Usage (Example 2):**
    ```csharp
    SurfApi.SocialDisconnected += OnSocialDisconnected;
    private void OnSocialDisconnected(SocialLoginData socialData)
    {
        // Your code here
    }
    ```

### 💼 WalletConnected

**Description:** Subscribes to wallet connection event to let you know once the user is connected. Returns connected wallet address.
- **Usage (Example 1):**
    ```csharp
    SurfApi.WalletConnected += (walletAddress) => {
        // Your code here
    };
    ```
- **Usage (Example 2):**
    ```csharp
    SurfApi.WalletConnected += OnWalletConnected;
    private void OnWalletConnected(string walletAddress)
    {
        // Your code here
    }
    ```

### 🚫 WalletConnectionRejected

**Description:** Subscribes to wallet connection rejection event to let you know once the user rejects the connection prompt.
- **Usage (Example 1):**
    ```csharp
    SurfApi.WalletConnectionRejected += () => {
        // Your code here
    };
    ```
- **Usage (Example 2):**
    ```csharp
    SurfApi.WalletConnectionRejected += OnWalletConnectionRejected;
    private void OnWalletConnectionRejected()
    {
        // Your code here
    }
    ```

### 🔌 WalletDisconnected

**Description:** Subscribes to wallet disconnection event to let you know once the user is disconnected. Returns disconnected wallet address.
- **Usage (Example 1):**
    ```csharp
    SurfApi.WalletDisconnected += (walletAddress) => {
        // Your code here
    };
    ```
- **Usage (Example 2):**
    ```csharp
    SurfApi.WalletDisconnected += OnWalletDisconnected;
    private void OnWalletDisconnected(string walletAddress)
    {
        // Your code here
    }
    ```
### 🔄 GetConnectionType

**Description:** Get the current connection type based on the user's login data.

- **Returns:** A `ConnectionType` representing the user's connection type, which can be one of the following:
    - `ConnectionType.None`: No connection
    - `ConnectionType.Social`: Social connection
    - `ConnectionType.NonSocial`: Non-social connection

- **Usage:**
    ```csharp
    var connectionType = SurfApi.GetConnectionType();
    ```

### 🔄 GameRoomsUpdated

**Description:** Subscribe to get the latest game rooms array. Subscribe to this event only one time since game rooms update themselves frequently and invoke this event. This returns all game rooms. So you need to update your rooms according to this.
- **Usage (Example 1):**
    ```csharp
    SurfApi.GameRoomsUpdated += (gameRooms) => {
        // Your code here to handle updated game rooms
    };
    ```
- **Usage (Example 2):**
    ```csharp
    SurfApi.GameRoomsUpdated += OnGameRoomsUpdated;
    private void OnGameRoomsUpdated(RemoteData.GameRoom[] gameRooms)
    {
        // Your code here to handle updated game rooms
    }
    ```

## 💳 Non-Social API

### 📲 ConnectWallet

**Description:** Use to invoke wallet connection popup, make sure you are subscribed to connection event if you need to know once the user's wallet is connected. You can also await the Task returned from this method to do whatever you want to do after the connection.
- **Usage:**
    ```csharp
    SurfApi.ConnectWallet();
    ```

## 📧 Social API

### 📧 ConnectWithSocial

**Description:** Use to connect with social credentials like Gmail, Facebook, etc.
- **Parameters:**
    - `socialLoginType`: Type of social login (e.g., `SocialLoginType.Facebook`)
    - `options` (optional): Additional options for social connection
- **Usage:**
    ```csharp
    SurfApi.ConnectWithSocial(SocialLoginType.Facebook, options);
    ```

## 🔌 Disconnect API

### 🔌 DisconnectWallet

**Description:** Use to disconnect wallet connection or guide the user to the wallet app to disconnect the wallet connection. Make sure you are subscribed to the disconnection event if you need to know once the user's wallet is disconnected. You can also await the Task that's returned from this method to perform actions after disconnection.
- **Usage:**
    ```csharp
    SurfApi.DisconnectWallet();
    ```
## NFT API

### 🖼️ GetNFTs {#get-nfts}

**Description:** Gets the user's NFTs if any.

- **Requires:** Wallet connection.

- **Returns:** A `Task<List<NFT>>` containing a list of NFTs.

- **Usage:**
    ```csharp
    var nfts = await SurfApi.GetNFTs();
    ```
    
## Game Room API

### 🚀 JoinRoom

**Description:** Use this to validate if a player can join a room. Returns a playToken object which needs to be passed to other game room APIs.
- **Parameters:**
    - `room`: The game room the player tries to join
    - `feeType`: Fee type of the room
- **Usage:**
    ```csharp
    var joinRoomResponse = await SurfApi.JoinRoom(room);
    ```

### 🚫 CancelJoinRoom

**Description:** Use this if a player disconnects or leaves before the game starts. Refunds the player's entry fee.
- **Parameters:**
    - `playToken`: PlayToken object that comes from the JoinRoom API
- **Usage:**
    ```csharp
    var result = await SurfApi.CancelJoinRoom(playToken);
    ```

### 🏆 SetPlayResult

**Description:** Use this when a player is eliminated.
- **Parameters:**
    - `playID`: PlayID
    - `playResults`: Dictionary of play results
- **Usage:**
    ```csharp
    var response = await SurfApi.SetPlayResult(playID, playResults);
    ```

### 🏆 GetMatchResults

**Description:** Use this to get match results.
- **Returns:** List of match results, ordered in descending order. Meaning, the 0th index is the most recent match.
- **Usage:**
    ```csharp
    var matchResults = await SurfApi.GetMatchResults();
    ```

### 🏆 GetMatchRankings

**Description:** Use this to get match rankings.
- **Parameters:**
    - `playID`: PlayID
- **Usage:**
    ```csharp
    var matchRankings = await SurfApi.GetMatchRankings(playID);
    ```
## Remote Config API

### 📡 GetRemoteConfig

**Description:** Use this method to retrieve remote configuration settings. You can optionally specify a version to get a specific configuration, or leave it empty to fetch the latest configuration.

- **Parameters:**
    - `version` (optional): The version of the remote configuration to retrieve. Leave it empty to get the latest

- **Returns:** A string containing the remote configuration settings.

- **Usage:**
    ```csharp
    var remoteConfig = await SurfApi.GetRemoteConfig();
    ```

## [Reward API]

### 🎁 CheckReward

**Description:** Use this method to check if a reward can be claimed based on the provided `RewardClaimRequest`.

- **Parameters:**
    - `rewardClaimRequest`: An object containing information about the reward claim request.

- **Returns:** A `Task<bool>` representing whether the reward can be claimed (`true`) or not (`false`).

- **Usage:**
    ```csharp
    var canClaim = await SurfApi.CheckReward(rewardClaimRequest);
    ```

### 🎁 ClaimReward

**Description:** Use this method to claim a reward based on the provided `RewardClaimRequest`.

- **Parameters:**
    - `rewardClaimRequest`: An object containing information about the reward claim request.

- **Returns:** A `Task<CurrencyAmount>` representing the amount of currency and currency type rewarded upon successful claim.

- **Usage:**
    ```csharp
    var currencyAmount = await SurfApi.ClaimReward(rewardClaimRequest);
    ```
## [Storage API]

### 📦 Name

**Description:** Gets or sets the name stored in the backend. Usually for player's name

- **Getter Usage:**
    ```csharp
    var name = SurfApi.Storage.Name;
    ```

- **Setter Usage:**
    ```csharp
    SurfApi.Storage.Name = "NewName";
    ```

### 🖼️ Picture

**Description:** Gets or sets the picture stored in the backend. Usually for profile picture id

- **Getter Usage:**
    ```csharp
    var picture = SurfApi.Storage.Picture;
    ```

- **Setter Usage:**
    ```csharp
    SurfApi.Storage.Picture = "7";
    ```

### 📄 Data

**Description:** Gets or sets the data stored in the backend. Usually for a generic data that you can deserialize and use on your end.

- **Getter Usage:**
    ```csharp
    var data = SurfApi.Storage.Data;
    ```

- **Setter Usage:**
    ```csharp
    SurfApi.Storage.Data = "NewData";
    ```

### 🔄 Get

**Description:** Gets the up-to-date stored data immediately.

- **Parameters:**
    - `defaultValue` (optional): Gives you the default value if stored data is null or empty.

- **Returns:** A `StorageData` object representing the stored data.

- **Usage:**
    ```csharp
    var storageData = SurfApi.Storage.Get(defaultValue);
    ```

### 🔃 GetAsync

**Description:** Gets the up-to-date stored data asynchronously.

- **Returns:** A `Task<StorageData>` representing the stored data.

- **Usage:**
    ```csharp
    var storageData = await SurfApi.Storage.GetAsync();
    ```

### 💾 Set

**Description:** Sets the data to store asynchronously, returns true if the storing process succeeded. Changes the cached data immediately. Triggers a change event immediately. If unsuccessful, rolls back to the previous data.

- **Parameters:**
    - `data`: Data to store.

- **Returns:** Returns true if the storing process succeeded.

- **Usage:**
    ```csharp
    var success = await SurfApi.Storage.Set(data);
    ```

### 🔄 StorageChanged

**Description:** Subscribe to changes in the stored data, also immediately calls the handler. As a Best Practice, only subscribe one time.

- **Parameters:**
    - `onStorageChanged`: The handler to be called when the storage changes.

- **Usage (Example 1):**
    ```csharp
    SurfApi.Storage.StorageChanged += (storageData) => {
        // Your code here to handle storage data changes
    };
    ```
- **Usage (Example 2):**
    ```csharp
    SurfApi.Storage.StorageChanged += OnStorageChanged;
    private void OnStorageChanged(StorageData storageData)
    {
        // Your code here to handle storage data changes
    }
    ```
## Token Balance API
Usually in your game, all players are going to have a soft currency + hard currency. Hard currency is going to be change based on user's connection type. User's who connected via tezos wallet will have xtz currency and user's who connected via social wallet will have fiat(USD) currency 

### 🌟 GetBalances

**Description:** Gets a list of tokens that contains both soft and hard currencies.

- **Returns:** A `Task<List<CurrencyAmount>>` containing a list of currency amounts representing both soft and hard currencies.

- **Usage:**
    ```csharp
    var tokenBalances = await SurfApi.GetBalances();
    ```
---

## Persistent Game Rooms Guide
When integrating the Surf SDK for handling persistent game rooms, consider the following guidelines to ensure a smooth user experience:
1. **Use `EnsureSdkReady` Event:** Before loading your lobby or any game-related screens, make sure to subscribe to the `EnsureSdkReady` event to ensure that the Surf SDK is initialized and ready to interact with game rooms.
2. **Listen for Game Room Update Event:**
    - Subscribe to the `GameRoomsUpdated` event to receive updates on game room configurations.
    - Use the event to get room configurations and set up your rooms for matchmaking.
3. **Joining a Game Room:**
    - When a user clicks on the "Play" button for a specific game room, call the `JoinRoom` API.
    - This ensures that the entry fee for the room is deducted from the player's balance when they call the `JoinRoom` API.
    - If the API does not returns any `errors`, proceed with your match-making and start the game. This ensures that the user can join the room and the game can be initiated.
    - If the API does return an `error`, you can show an error popup depending on the error that user had, it can be insufficient balance or already in match. In case of an exception, you can show a something went wrong popup.
4. **Handling Player Results (Server-side API):**
    - For all players, call the server-side API to report the game results.
    - Endpoint: `/game-play/result`
    - Use the API to communicate the final score, rank and reward(optional) of each player when they finish playing. This can be because they are eliminated from the game or they exited the game without dying. If you pass reward field with this endpoint, it will update player's balance aswell so you shouldn't use Update Player Balance API.
5. **Updating Player Balance (Server-side API):**
    - When a player decides to exit the game, call the server-side API to update the player's balance.
    - Endpoint: `/user/balance`
    - Ensure that the player's balance is correctly adjusted to reflect their participation in the game. USE this api ONLY for documented cases under server-side api documentation
Server-side API documentation link => https://docs.google.com/document/d/1sHpTzfAYIHVL0aIMZddhM8cwyVFuHj4KmORyjBHiAlQ/
By following these guidelines, you can effectively implement and manage persistent game rooms using the Surf SDK. Deducting entry fees when players join a room ensures that the game economy is maintained, and using the server-side API for handling player results allows for accurate reporting of game results.
