using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerDiceRoller : MonoBehaviour
{
    public GameObject dice1;
    public GameObject dice2;
    public TextMeshProUGUI diceResultText;
    public Sprite[] diceSideSprites;
    public Image[] diceImages;

    private float rollDuration = 1f;
    private bool isRolling = false;
    private float rollTimer = 0f;
    private int dice1Result = 0;
    private int dice2Result = 0;

    private PlayerTurnManager turnManager;

    public void Initialize(PlayerTurnManager turnManager)

    {
        this.turnManager = turnManager;
    }

    public void RollDice()
    {
        isRolling = true;
        rollTimer = 0f;

        dice1Result = Random.Range(1, 7);
        dice2Result = Random.Range(1, 7);
   
    }

    private void Update()
    {
        if (isRolling)
        {
            rollTimer += Time.deltaTime;

            if (rollTimer >= rollDuration)
            {
                isRolling = false;
                UpdateDiceVisuals(dice1Result, dice2Result);
                DisplayResult(dice1Result + dice2Result);

                int totalDiceResult = dice1Result + dice2Result;
                turnManager.GetDiceResult(GetPlayerIndex(), totalDiceResult);
            }
            else
            {
                RollDiceAnimation();
            }
        }
    }

    private void RollDiceAnimation()
    {
        int randomSide1 = Random.Range(0, 6);
        int randomSide2 = Random.Range(0, 6);

        Sprite sprite1 = diceSideSprites[randomSide1];
        Sprite sprite2 = diceSideSprites[randomSide2];

        diceImages[0].sprite = sprite1;
        diceImages[1].sprite = sprite2;
    }

    private void UpdateDiceVisuals(int roll1, int roll2)
    {
        diceImages[0].sprite = diceSideSprites[roll1 - 1];
        diceImages[1].sprite = diceSideSprites[roll2 - 1];
    }

    private void DisplayResult(int diceResult)
    {
        diceResultText.text = diceResult.ToString();
    }

    private int GetPlayerIndex()
    {
        return transform.GetSiblingIndex();
    }
}
