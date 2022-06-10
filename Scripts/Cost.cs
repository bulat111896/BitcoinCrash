using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Cost : MonoBehaviour
{
    public Sprite spriteElon;
    public GameObject panelLost;
    public Text cost, change, panelLostTitle, panelLostText;
    public Image line, elon;
    public long totalCost;
    const int startCost = 5000000;

    public void Win()
    {
        panelLost.SetActive(true);
        if (PlayerPrefs.GetInt("Language") == 1)
        {
            panelLostTitle.text = "Биткойн продан!";
            panelLostText.text = "Биткоин продан по курсу " + GetBeautifulNumber(totalCost, false) + " долларов за 1 шт.";
        }
        else
        {
            panelLostTitle.text = "Bitcoin is sold!";
            panelLostText.text = "Bitcoin was sold at the rate of $" + GetBeautifulNumber(totalCost, false) + " for 1 pc.";
        }
        if (!PlayerPrefs.HasKey("BestScore") || System.Convert.ToInt64(PlayerPrefs.GetString("BestScore")) < totalCost)
            PlayerPrefs.SetString("BestScore", totalCost.ToString());
        Audio.audioCtrl.PlayOpen();
        Audio.audioCtrl.PlayClick();
    }

    public static string GetBeautifulNumber(long cents, bool isChangeSize)
    {
        if (cents < 0)
            cents *= -1;
        string dollars = (cents / 100).ToString();
        string resultStr = string.Empty;
        for (int i = 0; i < dollars.Length; i++)
        {
            if (i % 3 == 0 && i > 0)
                resultStr += ",";
            resultStr += dollars[dollars.Length - 1 - i];
        }
        dollars = string.Empty;
        for (int i = resultStr.Length - 1; i >= 0; i--)
            dollars += resultStr[i];
        return dollars + (isChangeSize ? ("<size=200><color=#ffffff85>.") : ".") + (cents % 100).ToString("00") + (isChangeSize ? "</color></size>" : "");
    }

    public void Change(long cents)
    {
        totalCost += cents;

        if (totalCost <= 1)
        {
            totalCost = 1;
            panelLost.SetActive(true);
            elon.sprite = spriteElon;
            Audio.audioCtrl.PlayOpen();
        }
        else if (totalCost >= 100000000000000000)
            totalCost = 100000000000000000;

        cost.text = "$" + GetBeautifulNumber(totalCost, true);

        bool isGrown = totalCost > startCost;
        change.color = isGrown ? new Color(0.2f, 1f, 0.2f) : new Color(1f, 0.2f, 0.2f);
        line.color = isGrown ? new Color(0.3f, 0.4f, 0.2f) : new Color(0.45f, 0.2f, 0.2f);
        change.text = (isGrown ? "▲ " : "▼ ") + GetBeautifulNumber(totalCost - startCost, false) + " (" + (isGrown ? "+" : "-") + GetBeautifulNumber((long)((double)(startCost - totalCost) / startCost * 10000), false) + "%)";
    }

    IEnumerator Start()
    {
        totalCost = startCost;
        while (Touch.isFirstTouch)
            yield return null;
        yield return new WaitForSeconds(1f);
        while (!panelLost.activeSelf)
        {
            Change(Random.Range((int)(totalCost / 2000), (int)(totalCost / 500)) * Player.direction);
            yield return new WaitForSeconds(0.5f);
        }
    }
}