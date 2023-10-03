# 🏄‍♂️ Surf SDK Documentation

## 🌐 Connection Types

## Table of Contents

- [Connections and SDK API](#events-and-callbacks)
- [Game Room API](#game-room-api)
- [Storage API](#storage-api)
- [Token Balance API](#token-balance-api)
- [Remote Config API](#remote-config-api)
- [Reward APIs](#reward-api)

## Events and Callbacks

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

## [Game Room API]

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

**Description:** Use this to get game histories.
- **Returns:** List of Game History, ordered in descending order. Meaning, the 0th index is the most recent match.
- **Usage:**
    ```csharp
    var matchResults = await SurfApi.GetMatchResults();
    ```

### 🏆 GetMatchRankings

**Description:** Use this to get game histories.
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
## [Token Balance API]
Usually in your game, all players are going to have a soft currency + hard currency. Hard currency is going to be change based on user's connection type. User's who connected via tezos wallet will have xtz currency and user's who connected via social wallet will have fiat(USD) currency 

### 🌟 GetBalances

**Description:** Gets a list of tokens that contains both soft and hard currencies.

- **Returns:** A `Task<List<CurrencyAmount>>` containing a list of currency amounts representing both soft and hard currencies.

- **Usage:**
    ```csharp
    var tokenBalances = await SurfApi.GetBalances();
    ```
---
