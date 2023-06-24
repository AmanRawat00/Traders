using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class MessageManager : MonoBehaviour
{
    public TextMeshProUGUI messageText;
    public Image messageBG;
    public Button messageButton;

    public event Action MessageClicked;

    public void DisplayClickableMessage(string message, Vector2 bgSize = default, Color? bgColor = null, int? fontSize = null, Color? fontColor = null, Vector2 position = default)
    {
        EnableInputDetection();
        messageButton.gameObject.SetActive(true);
        messageText.text = message;

        messageBG.rectTransform.sizeDelta = bgSize == default ? messageBG.rectTransform.sizeDelta : bgSize;

        messageBG.color = bgColor ?? messageBG.color;

        messageText.fontSize = fontSize ?? messageText.fontSize;

        messageText.color = fontColor ?? messageText.color;

        messageBG.rectTransform.anchoredPosition = position == default ? messageBG.rectTransform.anchoredPosition : position;

    }

    private void EnableInputDetection()
    {
        messageButton.onClick.AddListener(HandleMessageClick);
    }

    private void HandleMessageClick()
    {
        HideClickableMessage();

        MessageClicked?.Invoke();
    }

    private void HideClickableMessage()
    {
        messageText.text = "";
        if (messageButton != null)
        {
            messageButton.gameObject.SetActive(false);
        }
    }

    public void DisplayMessage(string message, Vector2 bgSize = default, Color? bgColor = null, int? fontSize = null, Color? fontColor = null, Vector2 position = default)
    {
        messageButton.gameObject.SetActive(true);
        messageText.text = message;

        messageBG.rectTransform.sizeDelta = bgSize == default ? messageBG.rectTransform.sizeDelta : bgSize;

        messageBG.color = bgColor ?? messageBG.color;

        messageText.fontSize = fontSize ?? messageText.fontSize;

        messageText.color = fontColor ?? messageText.color;

        messageBG.rectTransform.anchoredPosition = position == default ? messageBG.rectTransform.anchoredPosition : position;

        DisableInputDetection(); // Remove the listener to disable message click
        UnregisterMessageClicked(); // Unregister the MessageClicked event
    }

    private void DisableInputDetection()
    {
        messageButton.onClick.RemoveListener(HandleMessageClick);
    }

    private void UnregisterMessageClicked()
    {
        MessageClicked = null;
    }

    public void HideMessage()
    {
        messageText.text = "";
        if (messageButton != null)
        {
            messageButton.gameObject.SetActive(false);
        }
    }
}
