using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerProfileManager : MonoBehaviour
{
    public GameObject playerProfilePrefab;
    public RectTransform playerProfileContainer;
    public PlayerTokensSprite playerTokensSprite;
    public Transform playerTokensContainer;
    public PlayerTokensSpawner playerTokensSpawner;

    private PlayerProfile[] playerProfiles;

    public void CreatePlayerProfileUI()
    {
        int numberOfPlayers = GameManager.Instance.GetNumberOfPlayers();

        playerProfiles = new PlayerProfile[numberOfPlayers]; // Store player profiles

        // Retrieve the spawned player tokens from PlayerTokensSpawner
        GameObject[] playerTokens = playerTokensSpawner.GetSpawnedPlayerTokens();

        for (int i = 0; i < numberOfPlayers; i++)
        {
            GameObject playerProfileGO = Instantiate(playerProfilePrefab, playerProfileContainer);

            Vector3 position = GetPlayerProfilePosition(i);
            playerProfileGO.GetComponent<RectTransform>().anchoredPosition = position;

            PlayerProfile playerProfile = playerProfileGO.GetComponent<PlayerProfile>();

            string playerName = GetPlayerName(i);
            Sprite playerTokenSprite = GetPlayerTokenSprite(i);
            int playerMoney = GetPlayerMoney(i);
            int sitesOwned = GetSitesOwned(i);
            int housesOwned = GetHousesOwned(i);
            int utilitiesOwned = GetUtilitiesOwned(i);
            string route = GetRoute(i);

            playerProfile.SetPlayerProfile(playerName, playerTokenSprite, playerMoney, sitesOwned, housesOwned, utilitiesOwned, route);

            // Assign player tokens to PlayerProfile
            playerProfile.SetPlayerToken(playerTokens[i]);

        }

        Debug.Log("Player Profile UI Created");
    }

    private Vector3 GetPlayerProfilePosition(int playerIndex)
    {
        int numberOfPlayers = PlayerPrefs.GetInt("NumberOfPlayers");

        if (numberOfPlayers == 2)
        {
            if (playerIndex == 0)
            {
                return new Vector3(0f, 240f, 0f);
            }
            else if (playerIndex == 1)
            {
                return new Vector3(0f, 120f, 0f);
            }
        }
        else if (numberOfPlayers == 3)
        {
            if (playerIndex == 0)
            {
                return new Vector3(0f, 240f, 0f);
            }
            else if (playerIndex == 1)
            {
                return new Vector3(0f, 120f, 0f);
            }
            else if (playerIndex == 2)
            {
                return new Vector3(0f, 0f, 0f);
            }
        }
        else if (numberOfPlayers == 4)
        {
            if (playerIndex == 0)
            {
                return new Vector3(0f, 240f, 0f);
            }
            else if (playerIndex == 1)
            {
                return new Vector3(0f, 120f, 0f);
            }
            else if (playerIndex == 2)
            {
                return new Vector3(0f, 0f, 0f);
            }
            else if (playerIndex == 3)
            {
                return new Vector3(0f, -120f, 0f);
            }
        }

        return Vector3.zero;
    }

    private string GetPlayerName(int playerIndex)
    {
        switch (playerIndex)
        {
            case 0:
                return "Red";
            case 1:
                return "Blue";
            case 2:
                return "Green";
            case 3:
                return "Yellow";
            default:
                return "";
        }
    }

    private Sprite GetPlayerTokenSprite(int playerIndex)
    {
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

    private int GetPlayerMoney(int playerIndex)
    {
        return 25000;
    }

    private int GetSitesOwned(int playerIndex)
    {
        // Return the number of sites owned by the player with the given index
        // Implement your own logic here or use a data structure to store the information
        return 0;
    }

    private int GetHousesOwned(int playerIndex)
    {
        // Return the number of houses owned by the player with the given index
        // Implement your own logic here or use a data structure to store the information
        return 0;
    }

    private int GetUtilitiesOwned(int playerIndex)
    {
        // Return the number of utilities owned by the player with the given index
        // Implement your own logic here or use a data structure to store the information
        return 0;
    }

    private string GetRoute(int playerIndex)
    {
        // Return the route (air, water, land , noRoute) for the player with the given index
        // Implement your own logic here or use a data structure to store the information
        return "noRoute";
    }

    public void SetPlayerToken(int index, GameObject playerToken)
    {
        PlayerProfile playerProfile = playerProfileContainer.GetChild(index).GetComponent<PlayerProfile>();
        playerProfile.SetPlayerToken(playerToken);
    }


    public void UpdatePlayerProfileUI(int[] playerTurnOrder)
    {
        Debug.Log("Received Player Turn Order in Player Profile Manager: " + string.Join(", ", playerTurnOrder));

        for (int i = 0; i < playerTurnOrder.Length; i++)
        {
            GameObject playerProfileGO = playerProfileContainer.GetChild(playerTurnOrder[i]).gameObject;

            Vector3 position = GetPlayerProfilePosition(i);
            playerProfileGO.GetComponent<RectTransform>().anchoredPosition = position;
        }
    }


    public PlayerProfile[] GetPlayerProfiles()
    {
        return playerProfileContainer.GetComponentsInChildren<PlayerProfile>();
    }
}
