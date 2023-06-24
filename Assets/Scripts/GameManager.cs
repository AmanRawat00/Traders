using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public Button skipEntryButton;
    public CameraManager cameraManager;
    public TransitionManager transitionManager;
    public BoardSetupManager boardSetupManager;
    public MessageManager messageManager;
    public PlayerTokensSpawner playerTokensSpawner;
    public PlayerProfileManager playerProfileManager;
    public PlayerTurnManager playerTurnManager;
    public DiceRoller diceRoller;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
                if (instance == null)
                {
                    GameObject obj = new GameObject("GameManager");
                    instance = obj.AddComponent<GameManager>();
                }
            }
            return instance;
        }
    }

    private enum GameState
    {
        GameEntry,
        GameInitialization,
        PlayerTurn,
        PropertyAuction,
        ChanceCommunityChestBonus,
        Jail,
        PropertyUpgrades,
        Bankruptcy,
        Trade,
        Event,
        Victory,
        GamePaused,
        GameOver
    }

    private GameState currentState;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Resources.UnloadUnusedAssets();
        SetGameState(GameState.GameEntry);

        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlayMusic("Main Track High");
        }
    }

    private void SetGameState(GameState newState)
    {
        currentState = newState;

        switch (currentState)
        {
            case GameState.GameEntry:
                ActivateEntryCameraTopView();
                SceneTransition();
                ActivateSkipGameEntry();
                break;

            case GameState.GameInitialization:
                DeactivateSkipGameEntry();
                ActivateMainCamera();
                DestroyEntryCameras();
                WelcomeMessage();
                break;

            case GameState.PlayerTurn:
                PlayerTurn(currentPlayerIndex);
                break;

            case GameState.PropertyAuction:
                break;

            case GameState.ChanceCommunityChestBonus:
                break;

            case GameState.Jail:
                break;

            case GameState.PropertyUpgrades:
                break;

            case GameState.Bankruptcy:
                break;

            case GameState.Trade:
                break;

            case GameState.Event:
                break;

            case GameState.Victory:
                break;

            case GameState.GamePaused:
                break;

            case GameState.GameOver:
                break;
        }
    }

    private int numberOfPlayers;
    private int[] playerTurnOrder; 

    public void SetNumberOfPlayers(int players)
    {
        numberOfPlayers = players;
        PlayerPrefs.SetInt("NumberOfPlayers", numberOfPlayers);
    }

    public int GetNumberOfPlayers()
    {
        if (numberOfPlayers == 0)
        {
            numberOfPlayers = PlayerPrefs.GetInt("NumberOfPlayers");
        }
        return numberOfPlayers;
    }


    //////////////////////////////////////////// Game State = Game Entry //////////////////////////////////////////////////////

    private void ActivateEntryCameraTopView()
    {
        cameraManager.ActivateEntryCameraTopView();
    }

    private void SceneTransition()
    {
        transitionManager.OnTransitionDone += OnEntryTransitionDone;
        transitionManager.StartFadeOutWhite();
    }

    private void ActivateSkipGameEntry()
    {
        if (skipEntryButton != null)
        {
            skipEntryButton.gameObject.SetActive(true);
        }
    }

    private void OnEntryTransitionDone()
    {
        transitionManager.OnTransitionDone -= OnEntryTransitionDone;
        SetBoard();
    }

    private void SetBoard()
    {
        boardSetupManager.OnBoardBaseSetupComplete += OnBoardBaseSetupComplete;
        boardSetupManager.StartSetupBoard();

        transitionManager.ResetTransition();
    }

    private void OnBoardBaseSetupComplete()
    {
        boardSetupManager.OnBoardBaseSetupComplete -= OnBoardBaseSetupComplete;
        EntryCameraTopViewZoomIn();
    }

    private void EntryCameraTopViewZoomIn()
    {
        cameraManager.OnCameraSettingDone += OnEntryCameraTVZoomInDone;
        cameraManager.StartEntryCameraTopViewZoomIn();
    }

    private void OnEntryCameraTVZoomInDone()
    {
        cameraManager.OnCameraSettingDone -= OnEntryCameraTVZoomInDone;
        SwitchEntryCamera();
    }

    private void SwitchEntryCamera()
    {
        cameraManager.SwitchToEntryCameraBottomLeft();
        cameraManager.DestroyEntryCameraTopView();

        transitionManager.DeactivateBlackImage();
        transitionManager.ResetTransition();

        cameraManager.OnCameraSettingDone += OnEntryCameraBLZoomInDone;
        cameraManager.StartEntryCameraBottomLeftZoomIn();
    }

    private void OnEntryCameraBLZoomInDone()
    {
        transitionManager.DeactivateBlackImage();
        transitionManager.ResetTransition();

        cameraManager.OnCameraSettingDone -= OnEntryCameraBLZoomInDone;
        StartRevolutionAndFallTiles();
    }

    private void StartRevolutionAndFallTiles()
    {
        cameraManager.OnCameraSettingDone += OnRevolveEntryCameraDone;
        cameraManager.StartRevolveEntryCamera();
        boardSetupManager.StartSetupTiles();
    }

    private void OnRevolveEntryCameraDone()
    {
        cameraManager.OnCameraSettingDone -= OnRevolveEntryCameraDone;
        SmoothSwitchToMainCamera();
    }

    private void SmoothSwitchToMainCamera()
    {
        cameraManager.OnCameraSettingDone += OnSmoothSwitchToMainCameraDone;
        cameraManager.StartSmoothSwitchToMainCamera();
    }

    private void OnSmoothSwitchToMainCameraDone()
    {
        cameraManager.OnCameraSettingDone -= OnSmoothSwitchToMainCameraDone;
        SetGameStateGameInitialization();
    }

    public void SkipGameEntry()
    {
        transitionManager.DeactivateBlackImage();
        transitionManager.ResetTransition();
        transitionManager.OnTransitionDone += OnSkipTransitionDone;
        transitionManager.StartFadeInBlack();

        cameraManager.OnSkipButtonPress();
    }

    private void OnSkipTransitionDone()
    {
        transitionManager.OnTransitionDone -= OnSkipTransitionDone;
        SetGameStateGameInitialization();

        boardSetupManager.OnSkipButtonPress();
    }

    private void SetGameStateGameInitialization()
    {
        transitionManager.DeactivateBlackImage();
        transitionManager.ResetTransition();
        SetGameState(GameState.GameInitialization);
    }


    ///////////////////////////////////////////// Game State = Game Initialization ////////////////////////////////////////////

    private void DeactivateSkipGameEntry()
    {
        if (skipEntryButton != null)
        {
            skipEntryButton.gameObject.SetActive(false);
        }
    }

    private void ActivateMainCamera()
    {
        cameraManager.ActivateMainCamera();
    }

    private void DestroyEntryCameras()
    {
        cameraManager.DestroyEntryCameras();
    }

    private void WelcomeMessage()
    {
        messageManager.DisplayClickableMessage("Welcome to the Traders", new Vector2(2000f, 870f), Color.black, 72, Color.white, new Vector2(0f, 0f));
        messageManager.MessageClicked += () => OnMessageClicked("Welcome Message");
    }

    private bool isMessageClicked = false;
    private string messageType;

    private void OnMessageClicked(string type)
    {
        messageType = type;
        isMessageClicked = true;
        messageManager.MessageClicked -= () => OnMessageClicked(type);
    }

    private void Update()
    {
        if (isMessageClicked)
        {
            switch (messageType)
            {
                case "Welcome Message":
                    SetPlayerTokens();                  
                    ChooseTokenMessage();
                    break;

                case "Choose Token Message":
                    DecideTurnMessage();
                    break;

                case "Decide Turn Message":
                    SetPlayerTurnOrder();
                    break;

                case "Turn Decided Message":
                    DestroyDicesPrefabs();
                    UpdatePlayerTokensAndProfileUI();
                    BeginGameMessage();
                    break;

                case "Begin Game Message":
                    BeginGame();
                    break;

                default:
                    break;
            }

            isMessageClicked = false;
            messageType = null;
        }
    }

    private void SetPlayerTokens()
    {
        playerTokensSpawner.SpawnPlayerTokens();
        SetPlayerProfileUI();
    }

    private void SetPlayerProfileUI()
    {
        playerProfileManager.CreatePlayerProfileUI();
    }

    private void ChooseTokenMessage()
    {
        messageManager.DisplayClickableMessage("Decide with your friend and Choose Your Token", new Vector2(2000f, 50f), Color.black, 36, Color.white, new Vector2(0f, 10f));
        messageManager.MessageClicked += () => OnMessageClicked("Choose Token Message");
    }

    private void DecideTurnMessage()
    {
        messageManager.DisplayClickableMessage("Let's decide turns , who will play first");
        messageManager.MessageClicked += () => OnMessageClicked("Decide Turn Message");
    }

    private void SetPlayerTurnOrder()
    {
        playerTurnManager.CreateDiceInstantiate();
        messageManager.DisplayMessage("Dice will decide the turn Order");
    }

    public void UpdateTurnOrder(int[] sortedPlayerIndexes)
    {
        playerTurnOrder = sortedPlayerIndexes;

        SaveTurnOrderData();
    }

    private void SaveTurnOrderData()
    {
        for (int i = 0; i < playerTurnOrder.Length; i++)
        {
            int playerIndex = playerTurnOrder[i];
            PlayerPrefs.SetInt("TurnOrder_" + i, playerIndex);
        }
        TurnDecidedMessage();
    }

    private void TurnDecidedMessage()
    {
        messageManager.HideMessage();
        messageManager.DisplayClickableMessage("Higher the number on dice First you will play");
        messageManager.MessageClicked += () => OnMessageClicked("Turn Decided Message");
    }

    private void UpdatePlayerTokensAndProfileUI()
    {
        UpdatePlayerTokens();
        UpdatePlayerProfileUI();
    }

    private void DestroyDicesPrefabs()
    {
        playerTurnManager.DestroyDiceObjects();
    }

    private void UpdatePlayerTokens()
    {
        playerTokensSpawner.UpdatePlayerTokens(playerTurnOrder);
    }

    private void UpdatePlayerProfileUI()
    {
        playerProfileManager.UpdatePlayerProfileUI(playerTurnOrder);
    }

    private void BeginGameMessage()
    {
        messageManager.DisplayClickableMessage("Let's start the game");
        messageManager.MessageClicked += () => OnMessageClicked("Begin Game Message");     
    }

    private void BeginGame()
    {
        Debug.Log("Traders game started!");
        SetGameStatePlayerTurn();
    }

    public void SetGameStatePlayerTurn()
    {
        SetGameState(GameState.PlayerTurn);
    }


    ///////////////////////////////////////////// Game State = Player Turn  ////////////////////////////////////////////

    public Transform airWaypointContainer;
    public Transform waterWaypointContainer;
    public Transform landWaypointContainer;

    private PlayerProfile[] playerProfiles;
    private int currentPlayerIndex = 0;
    private int currentPlayerNumber;
    private string route;

    private void PlayerTurn(int playerIndex)
    {
        playerProfiles = playerProfileManager.GetPlayerProfiles();

        currentPlayerNumber = playerTurnOrder[playerIndex];
        PlayerProfile currentPlayerProfile = playerProfiles[currentPlayerNumber];
        string currentPlayerName = currentPlayerProfile.GetPlayerName();

        GiveDiceToPlayer();
        messageManager.DisplayMessage(currentPlayerName + ", Roll the dice", new Vector2(2000f, 50f), Color.black, 36, Color.white, new Vector2(0f, 10f));
        // Perform actions for the current player's turn
        // ...

        // Move the player token, handle dice rolls, property interactions, etc.
        // ...

        // Proceed to the next player's turn
        // NextPlayerTurn();
    }

    private void GiveDiceToPlayer()
    {
        diceRoller.ActivateDice();
        diceRoller.OnDiceRolled += DiceRolled;
    }

    private void DiceRolled()
    {
        diceRoller.OnDiceRolled -= DiceRolled;

        int[] diceResults = diceRoller.GetDiceResult();
        int diceResult1 = diceResults[0];
        int diceResult2 = diceResults[1];

        int diceSum = diceResult1 + diceResult2;

        messageManager.HideMessage();
        messageManager.DisplayMessage(diceSum + " Rolled  ");

        CheckRoute(diceSum, currentPlayerNumber);
    }

    public void CheckRoute(int sum, int currentPlayerNumber)
    {
        PlayerProfile currentPlayerProfile = playerProfiles[currentPlayerNumber];
        if (currentPlayerProfile.GetRoute() == "noRoute")
        {
            if (sum == 2 || sum == 3 || sum == 5)
            {
                route = "Air";
            }
            else if (sum == 4 || sum == 6 || sum == 11 || sum == 12)
            {
                route = "Water";
            }
            else if (sum == 7 || sum == 8 || sum == 9 || sum == 10)
            {
                route = "Land";
            }

            currentPlayerProfile.SetRoute(route);
            MovePlayerToWaypoint(currentPlayerProfile, route);
        }
    }

    private void MovePlayerToWaypoint(PlayerProfile playerProfile, string route)
    {
        Transform[] targetWaypoints = null;

        switch (route)
        {
            case "Air":
                targetWaypoints = airWaypointContainer.GetComponentsInChildren<Transform>();
                break;
            case "Water":
                targetWaypoints = waterWaypointContainer.GetComponentsInChildren<Transform>();
                break;
            case "Land":
                targetWaypoints = landWaypointContainer.GetComponentsInChildren<Transform>();
                break;
            default:
                Debug.LogError("Invalid route for player: " + playerProfile.GetPlayerName());
                return;
        }

        // Convert the array to a List<Transform> and skip the first element
        List<Transform> targetWaypointsList = targetWaypoints.ToList();
        targetWaypointsList.RemoveAt(0);

        // Convert the List back to an array
        targetWaypoints = targetWaypointsList.ToArray();

        // Get dice results
        int[] diceResults = diceRoller.GetDiceResult();
        int diceResult1 = diceResults[0];
        int diceResult2 = diceResults[1];
        int totalSteps = diceResult1 + diceResult2;

        // Calculate movement parameters
        float movementDuration = 2.0f;
        float movementSpeed = totalSteps / movementDuration;

        StartCoroutine(MovePlayerAlongWaypoints(targetWaypoints, movementSpeed, playerProfile));
    }
    
    private IEnumerator MovePlayerAlongWaypoints(Transform[] waypoints, float movementSpeed, PlayerProfile playerProfile)
    {
        int currentWaypointIndex = 0;
        int totalWaypoints = waypoints.Length;

        while (currentWaypointIndex < totalWaypoints)
        {
            Vector3 startPosition = playerProfile.GetPlayerTokenPosition();
            Vector3 targetPosition = waypoints[currentWaypointIndex].position;

            float distance = Vector3.Distance(startPosition, targetPosition);
            float startTime = Time.time;

            while (Vector3.Distance(playerProfile.GetPlayerTokenPosition(), targetPosition) > 0.01f)
            {
                float elapsedTime = Time.time - startTime;
                float fractionOfDistance = Mathf.Clamp01(elapsedTime * movementSpeed / distance);
                Vector3 newPosition = Vector3.Lerp(startPosition, targetPosition, fractionOfDistance);

                playerProfile.SetPlayerTokenPosition(newPosition);
                yield return null;
            }

            playerProfile.SetPlayerTokenPosition(targetPosition);
            currentWaypointIndex++;
        }

        // Perform actions after the player has reached the final waypoint
        // ...
    }

}
