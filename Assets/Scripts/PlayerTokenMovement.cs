using UnityEngine;
using System.Collections;

public class PlayerTokenMovement : MonoBehaviour
{
    public Transform airWaypointContainer;
    public Transform waterWaypointContainer;
    public Transform landWaypointContainer;
    public GameObject diceRollerObject;
    public int playerSpeed = 5 ;
    private DiceRoller diceRoller;
    private string waypointType;
    private int currentPosition = 0;

    private void Start()
    {
        diceRoller = diceRollerObject.GetComponent<DiceRoller>();
        waypointType = null;
    }

    public void RollDice()
    {
        if (waypointType != null)
        {
            // Already rolled the dice for this turn
            return;
        }

        // Roll the dice using the DiceRoller script
        int[] diceResults = diceRoller.GetDiceResult();
        int diceResult1 = diceResults[0];
        int diceResult2 = diceResults[1];
        int sum = diceResult1 + diceResult2;

        if (sum == 2 || sum == 3 || sum == 5)
        {
            waypointType = "Air";
        }
        else if (sum == 4 || sum == 6 || sum == 11 || sum == 12)
        {
            waypointType = "Water";
        }
        else if (sum == 7 || sum == 8 || sum == 9 || sum == 10)
        {
            waypointType = "Land";
        }

        // Move the player token based on the dice roll
        MovePlayer(sum);
    }

    private void MovePlayer(int diceSum)
    {
        // Hide the dice
        diceRollerObject.SetActive(false);

        Transform targetWaypoint = null;

        if (waypointType == "Air")
        {
            targetWaypoint = airWaypointContainer.GetChild(currentPosition + diceSum);
        }
        else if (waypointType == "Water")
        {
            targetWaypoint = waterWaypointContainer.GetChild(currentPosition + diceSum);
        }
        else if (waypointType == "Land")
        {
            targetWaypoint = landWaypointContainer.GetChild(currentPosition + diceSum);
        }

        float travelTime = Vector3.Distance(transform.position, targetWaypoint.position) / playerSpeed;

        StartCoroutine(MoveTowardsWaypoint(targetWaypoint.position, travelTime));

        currentPosition += diceSum;

        if (currentPosition >= targetWaypoint.parent.childCount)
        {
            // Reached the end, turn ends
            EndTurn();
        }
    }

    private IEnumerator MoveTowardsWaypoint(Vector3 targetPosition, float travelTime)
    {
        float elapsedTime = 0f;
        Vector3 startingPosition = transform.position;

        while (elapsedTime < travelTime)
        {
            float normalizedTime = elapsedTime / travelTime;
            transform.position = Vector3.Lerp(startingPosition, targetPosition, normalizedTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;
    }

    public void EndTurn()
    {
        waypointType = null;
        diceRollerObject.SetActive(true);
        // Activate end turn button or perform other end turn actions
    }
}

