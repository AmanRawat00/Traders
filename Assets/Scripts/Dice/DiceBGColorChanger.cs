using UnityEngine;
using UnityEngine.UI;

public class DiceBGColorChanger : MonoBehaviour
{
    public Image backgroundImage;
    public float colorChangeInterval = 1f;

    private string[] colorCodes = new string[]
    {  "#FF0000", "#FF0D00", "#FF1A00", "#FF2700", "#FF3400", "#FF4100", "#FF4F00", "#FF5C00",
    "#FF6900", "#FF7600", "#FF8300", "#FF9000", "#FF9E00", "#FFAA00", "#FFB800", "#FFC500",
    "#FFD300", "#FFDF00", "#FFEB00", "#FFF800", "#FAFF00", "#EDFF00", "#E0FF00", "#D3FF00",
    "#C5FF00", "#B8FF00", "#AAFF00", "#9DFF00", "#90FF00", "#83FF00", "#76FF00", "#69FF00",
    "#5CFF00", "#4FFF00", "#42FF00", "#35FF00", "#28FF00", "#1BFF00", "#0EFF00", "#00FF00",
    "#00FF0D", "#00FF1A", "#00FF27", "#00FF34", "#00FF41", "#00FF4F", "#00FF5C", "#00FF69",
    "#00FF76", "#00FF83", "#00FF90", "#00FF9E", "#00FFAA", "#00FFB8", "#00FFC5", "#00FFD3",
    "#00FFDF", "#00FFEB", "#00FFF8", "#00FAFF", "#00EDFF", "#00E0FF", "#00D3FF", "#00C5FF",
    "#00B8FF", "#00AAFF", "#009DFF", "#0090FF", "#0083FF", "#0076FF", "#0069FF", "#005CFF",
    "#004FFF", "#0042FF", "#0035FF", "#0028FF", "#001BFF", "#000EFF", "#0000FF", "#0D00FF",
    "#1A00FF", "#2700FF", "#3400FF", "#4100FF", "#4F00FF", "#5C00FF", "#6900FF", "#7600FF",
    "#8300FF", "#9000FF", "#9E00FF", "#AA00FF", "#B800FF", "#C500FF", "#D300FF", "#DF00FF",
    "#EB00FF", "#F800FF", "#FF00FA", "#FF00ED", "#FF00E0", "#FF00D3", "#FF00C5", "#FF00B8",
    "#FF00AA", "#FF009D", "#FF0090", "#FF0083", "#FF0076", "#FF0069", "#FF005C", "#FF004F",
    "#FF0042", "#FF0035", "#FF0028", "#FF001B", "#FF000E"
    };

    private int currentIndex = 0;
    private float timer = 0f;
    private bool isActive = false;

    public void EnableBGColorChanger()
    {
        isActive = true;
        SetBackgroundColor();
    }

    private void Update()
    {
        if (!isActive)
            return;

        timer += Time.deltaTime;

        if (timer >= colorChangeInterval)
        {
            timer = 0f;
            ChangeBackgroundColor();
        }
    }

    public void SetActive(bool isActive)
    {
        this.isActive = isActive;
    }

    private void SetBackgroundColor()
    {
        if (backgroundImage != null && colorCodes.Length > 0)
        {
            Color color;
            if (ColorUtility.TryParseHtmlString(colorCodes[currentIndex], out color))
            {
                backgroundImage.color = color;
            }
        }
    }

    private void ChangeBackgroundColor()
    {
        if (backgroundImage != null && colorCodes.Length > 0)
        {
            currentIndex = (currentIndex + 1) % colorCodes.Length;
            SetBackgroundColor();
        }
    }
}
