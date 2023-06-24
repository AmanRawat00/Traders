using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerProfile : MonoBehaviour
{
    public Image playerTokenImage;
    public TMP_Text playerNameText;
    public TMP_Text playerMoneyText;
    public TMP_Text sitesOwnedText;
    public TMP_Text housesOwnedText;
    public TMP_Text utilitiesOwnedText;
    public Image routeImage;

    private string playerName;
    private int playerMoney;
    private int sitesOwned;
    private int housesOwned;
    private int utilitiesOwned;
    private string route;
    private Vector3 tokenPosition;

    public void SetPlayerProfile(string playerName, Sprite playerTokenSprite, int playerMoney, int sitesOwned, int housesOwned, int utilitiesOwned, string route)
    {
        this.playerName = playerName;
        this.playerMoney = playerMoney;
        this.sitesOwned = sitesOwned;
        this.housesOwned = housesOwned;
        this.utilitiesOwned = utilitiesOwned;
        this.route = route;

        playerNameText.text = playerName;
        playerTokenImage.sprite = playerTokenSprite;
        playerMoneyText.text = playerMoney.ToString();
        sitesOwnedText.text = sitesOwned.ToString();
        housesOwnedText.text = housesOwned.ToString();
        utilitiesOwnedText.text = utilitiesOwned.ToString();
        SetRouteImage(route);
    }

    public void SetPlayerToken(GameObject playerToken)
    {
        playerTokenImage.gameObject.SetActive(true);
        playerTokenImage.sprite = playerToken.GetComponent<SpriteRenderer>().sprite;
    }

    public string GetPlayerName()
    {
        return playerName;
    }

    public int GetPlayerMoney()
    {
        return playerMoney;
    }
    public Vector3 GetPlayerTokenPosition()
    {
        return tokenPosition;
    }

    public void SetPlayerTokenPosition(Vector3 newPosition)
    {
        tokenPosition = newPosition;
        // Update the position of the player token using the newPosition value
        // Replace this comment with the actual code to update the player token's position
    }

    public void AddMoney(int amount)
    {
        playerMoney += amount;
        playerMoneyText.text = playerMoney.ToString();
    }

    public void DeductMoney(int amount)
    {
        playerMoney -= amount;
        playerMoneyText.text = playerMoney.ToString();
    }

    public void AddSiteOwned()
    {
        sitesOwned++;
        sitesOwnedText.text = "Sites Owned: " + sitesOwned.ToString();
    }

    public void AddHouseOwned()
    {
        housesOwned++;
        housesOwnedText.text = "Houses Owned: " + housesOwned.ToString();
    }

    public void AddUtilityOwned()
    {
        utilitiesOwned++;
        utilitiesOwnedText.text = "Utilities Owned: " + utilitiesOwned.ToString();
    }

    public void SetRoute(string route)
    {
        this.route = route;
        SetRouteImage(route);
    }

    public string GetRoute()
    {
        return route;
    }

    public void SetTokenPosition(Vector3 position)
    {
        tokenPosition = position;
        transform.position = position; 
    }

    private void SetRouteImage(string route)
    {
        switch (route)
        {
            case "Air":
                routeImage.sprite = airRouteSprite;
                break;
            case "Water":
                routeImage.sprite = waterRouteSprite;
                break;
            case "Land":
                routeImage.sprite = landRouteSprite;
                break;
            case "noRoute":
                routeImage.sprite = noRouteSprite;
                break;
            default:
                routeImage.sprite = null;
                break;
        }
    }

    public Sprite airRouteSprite;
    public Sprite waterRouteSprite;
    public Sprite landRouteSprite;
    public Sprite noRouteSprite;


}
