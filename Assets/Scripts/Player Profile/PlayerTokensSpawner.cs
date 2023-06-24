using UnityEngine;

public class PlayerTokensSpawner : MonoBehaviour
{
    public GameObject playerPrefab;
    public Transform playerTokenContainer;
    public PlayerProfileManager profileManager;
    private GameObject[] playerTokens; // Declare playerTokens as a class-level variable

    public void SpawnPlayerTokens()
    {
        int numberOfPlayers = GameManager.Instance.GetNumberOfPlayers();
        Debug.Log("Number of Players: " + numberOfPlayers);

        playerTokens = new GameObject[numberOfPlayers]; // Initialize the playerTokens array

        for (int i = 0; i < numberOfPlayers; i++)
        {
            Vector3 startingPosition = GetStartingPosition(i);

            GameObject currentPlayer = Instantiate(playerPrefab, startingPosition, Quaternion.identity, playerTokenContainer);

            Sprite playerTokenSprite = GetPlayerTokenSprite(i);
            if (playerTokenSprite != null)
            {
                SpriteRenderer playerSpriteRenderer = currentPlayer.GetComponent<SpriteRenderer>();
                if (playerSpriteRenderer != null)
                {
                    playerSpriteRenderer.sprite = playerTokenSprite;
                }
            }

            currentPlayer.transform.rotation = Quaternion.Euler(-60f, 0f, 0f);

            playerTokens[i] = currentPlayer;

        }

        Debug.Log("Player Tokens Spawned");
    }

    public GameObject[] GetSpawnedPlayerTokens()
    {
        return playerTokens;
    }


    private Vector3 GetStartingPosition(int playerIndex)
    {
        int numberOfPlayers = PlayerPrefs.GetInt("NumberOfPlayers");

        if (numberOfPlayers == 2)
        {
            if (playerIndex == 0)
            {
                return new Vector3(-2.2f, 0f, 0f);
            }
            else if (playerIndex == 1)
            {
                return new Vector3(2.2f, 0f, 0f);
            }
        }
        else if (numberOfPlayers == 3)
        {
            if (playerIndex == 0)
            {
                return new Vector3(-4.4f, 0f, 0f);
            }
            else if (playerIndex == 1)
            {
                return new Vector3(0f, 0f, 0f);
            }
            else if (playerIndex == 2)
            {
                return new Vector3(4.4f, 0f, 0f);
            }
        }
        else if (numberOfPlayers == 4)
        {
            if (playerIndex == 0)
            {
                return new Vector3(-6.6f, 0f, 0f);
            }
            else if (playerIndex == 1)
            {
                return new Vector3(-2.2f, 0f, 0f);
            }
            else if (playerIndex == 2)
            {
                return new Vector3(2.2f, 0f, 0f);
            }
            else if (playerIndex == 3)
            {
                return new Vector3(6.6f, 0f, 0f);
            }
        }

        return Vector3.zero;
    }

    private Sprite GetPlayerTokenSprite(int playerIndex)
    {
        PlayerTokensSprite playerTokensSprite = FindObjectOfType<PlayerTokensSprite>();

        switch (playerIndex)
        {
            case 0:
                return playerTokensSprite.redSprite;
            case 1:
                return playerTokensSprite.blueSprite;
            case 2:
                return playerTokensSprite.greenSprite;
            case 3:
                return playerTokensSprite.yellowSprite;
            default:
                return null;
        }
    }

    public void UpdatePlayerTokens(int[] playerTurnOrder)
    {
        Debug.Log("Received Player Turn Order in Player Tokens Spawner: " + string.Join(", ", playerTurnOrder));

        for (int i = 0; i < playerTurnOrder.Length; i++)
        {
            GameObject currentPlayer = playerTokenContainer.GetChild(playerTurnOrder[i]).gameObject;

            Vector3 position = GetStartingPosition(i);
            currentPlayer.transform.position = position;
        }
    }
}
