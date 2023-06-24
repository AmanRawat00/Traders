using UnityEngine;
using System;
using System.Collections.Generic;

public class PlayerData
{
    public int playerIndex;
    public int diceResult;

    public PlayerData(int index, int result)
    {
        playerIndex = index;
        diceResult = result;
    }
}

public class PlayerTurnManager : MonoBehaviour
{
    public GameObject dicePrefab;
    public Transform diceContainer;

    private List<PlayerData> playerDataList;
    private int[] diceResults;
    private int[] sortedPlayerIndex;

    public GameManager gameManager;

    public void CreateDiceInstantiate()
    {
        int numberOfPlayers = GameManager.Instance.GetNumberOfPlayers();
        diceResults = new int[numberOfPlayers];
        playerDataList = new List<PlayerData>();


        for (int i = 0; i < numberOfPlayers; i++)
        {
            GameObject diceObject = Instantiate(dicePrefab, diceContainer);

            PlayerDiceRoller diceRoller = diceObject.GetComponent<PlayerDiceRoller>();
            diceRoller.Initialize(this);

            Vector3 position = GetDicePosition(i);
            diceObject.GetComponent<RectTransform>().anchoredPosition = position;
            diceRoller.RollDice();
        }
    }

    private Vector3 GetDicePosition(int playerIndex)
    {
        int numberOfPlayers = PlayerPrefs.GetInt("NumberOfPlayers");

        if (numberOfPlayers == 2)
        {
            if (playerIndex == 0)
            {
                return new Vector3(270f, 215f, 0f);
            }
            else if (playerIndex == 1)
            {
                return new Vector3(270f, 105f, 0f);
            }
        }
        else if (numberOfPlayers == 3)
        {
            if (playerIndex == 0)
            {
                return new Vector3(270f, 225f, 0f);
            }
            else if (playerIndex == 1)
            {
                return new Vector3(270f, 105f, 0f);
            }
            else if (playerIndex == 2)
            {
                return new Vector3(270f, -15f, 0f);
            }
        }
        else if (numberOfPlayers == 4)
        {
            if (playerIndex == 0)
            {
                return new Vector3(270f, 225f, 0f);
            }
            else if (playerIndex == 1)
            {
                return new Vector3(270f, 105f, 0f);
            }
            else if (playerIndex == 2)
            {
                return new Vector3(270f, -15f, 0f);
            }
            else if (playerIndex == 3)
            {
                return new Vector3(270f, -135f, 0f);
            }
        }

        return Vector3.zero;
    }



    public int GetDiceResult(int playerIndex, int diceResult)
    {
        Debug.Log("Player " + playerIndex);
        Debug.Log("Dice Result: " + diceResult);

        diceResults[playerIndex] = diceResult;

        if (IsArrayFull())
        {
            CheckArrayCondition();
        }

        return diceResults[playerIndex];
    }

    private bool IsArrayFull()
    {
        for (int i = 0; i < diceResults.Length; i++)
        {
            if (diceResults[i] == 0) 
            {
                return false;
            }
        }

        return true;
    }

    private void CheckArrayCondition()
    {
        bool hasSameResult = false;

        for (int i = 0; i < diceResults.Length - 1; i++)
        {
            for (int j = i + 1; j < diceResults.Length; j++)
            {
                if (diceResults[i] == diceResults[j])
                {
                    hasSameResult = true;
                    break;
                }
            }

            if (hasSameResult)
                break;
        }

        if (hasSameResult)
        {
            UpdateGame();
        }
        else
        {
            for (int i = 0; i < diceResults.Length; i++)
            {
                AssignDiceResult(i, diceResults[i]);
            }

            SortDiceResults();
            ExtractPlayerIndexes();
        }
    }

    private void AssignDiceResult(int playerIndex, int diceResult)
    {
        PlayerData playerData = new PlayerData(playerIndex, diceResult);
        playerDataList.Add(playerData);
        Debug.Log("Player " + playerData.playerIndex + " dice result: " + playerData.diceResult);
    }

    private void SortDiceResults()
    {
        playerDataList.Sort((a, b) => b.diceResult.CompareTo(a.diceResult));

        Debug.Log("Sorted dice results:");
        foreach (PlayerData playerData in playerDataList)
        {
            Debug.Log("Player " + playerData.playerIndex + " dice result: " + playerData.diceResult);
        }
    }

    private void ExtractPlayerIndexes()
    {
        sortedPlayerIndex = new int[playerDataList.Count];

        for (int i = 0; i < playerDataList.Count; i++)
        {
            sortedPlayerIndex[i] = playerDataList[i].playerIndex;
        }

        gameManager.UpdateTurnOrder(sortedPlayerIndex);  
    }

    public void DestroyDiceObjects()
    {
        foreach (Transform child in diceContainer)
        {
            Destroy(child.gameObject);
        }      
    }

    private void UpdateGame()
    {
        Debug.Log("Updating game...");

            foreach (Transform child in diceContainer)
            {
                Destroy(child.gameObject);
            }
 
        playerDataList.Clear();
        Array.Clear(diceResults, 0, diceResults.Length);

        CreateDiceInstantiate();
    }
}
