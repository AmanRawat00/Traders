using UnityEngine;
using UnityEngine.UI;
using System;

public class DiceRoller : MonoBehaviour
{
    public GameObject dice;
    public GameObject dice1;
    public GameObject dice2;
    public Button diceButton;
    public Sprite[] diceSideSprites;
    public Image[] diceImages;
    public MessageManager messageManager;

    private bool isRolling = false;
    private float rollDuration = 1f;
    private float rollTimer = 0f;
    private int dice1Result = 0;
    private int dice2Result = 0;
    private bool isDiceRolled = false;
    public DiceBGColorChanger diceBGColorChanger;


    public event Action OnDiceRolled;

    public void ActivateDice()
    {
        dice.gameObject.SetActive(true);
        diceButton.onClick.AddListener(OnDiceButtonClicked);
        diceButton.interactable = !isDiceRolled;

        diceBGColorChanger.EnableBGColorChanger();

    }

    private void Update()
    {
        if (isRolling)
        {
            messageManager.HideMessage();
            messageManager.HideMessage();
            messageManager.DisplayMessage("Dices Are Rolling");

            rollTimer += Time.deltaTime;

            if (rollTimer >= rollDuration)
            {
                isRolling = false;
                dice1Result = UnityEngine.Random.Range(1, 7);
                dice2Result = UnityEngine.Random.Range(1, 7);
                UpdateDiceVisuals(dice1Result, dice2Result);

                isDiceRolled = true;
                diceButton.interactable = !isDiceRolled;
                diceBGColorChanger.SetActive(false);
                OnDiceRolled?.Invoke();
            }
            else
            {
                RollDiceAnimation();
            }
        }
    }

    private void OnDiceButtonClicked()
    {
        if (!isRolling && !isDiceRolled)
        {
            isRolling = true;
            rollTimer = 0f;
        }
    }

    private void RollDiceAnimation()
    {
        int randomSide1 = UnityEngine.Random.Range(0, 6);
        int randomSide2 = UnityEngine.Random.Range(0, 6);

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

    public int[] GetDiceResult()
    {
        int[] diceResults = new int[2];
        diceResults[0] = dice1Result;
        diceResults[1] = dice2Result;
        return diceResults;
    }

    public void DeactivateDice()
    {
        dice.gameObject.SetActive(false);    
    }
}