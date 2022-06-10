using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StartCamera : MonoBehaviour
{
    public Text[] languageTexts;
    public Text bestScore, maxTime, Wallet;
    public Sprite[] sprites;
    public Image sound, language;

    public void Language()
    {
        PlayerPrefs.SetInt("Language", PlayerPrefs.GetInt("Language") == 1 ? 0 : 1);
        SetLanguage();
        PlayClick();
    }

    public void PlayClick()
    {
        Audio.audioCtrl.PlayClick();
    }

    public void Replay()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        PlayClick();
    }

    public void Sound(bool isStart)
    {
        if (!isStart)
        {
            PlayerPrefs.SetInt("Sound", PlayerPrefs.GetInt("Sound") == 0 ? 1 : 0);
            PlayClick();
        }
        sound.sprite = sprites[PlayerPrefs.GetInt("Sound")];
        sound.color = new Color(1, 1, 1, PlayerPrefs.GetInt("Sound") == 0 ? 0.3f : 0.6f);
        AudioListener.volume = PlayerPrefs.GetInt("Sound");
        if (PlayerPrefs.GetInt("Sound") == 0)
            Audio.audioCtrl.GetComponent<AudioSource>().Pause();
        else if (!Audio.audioCtrl.GetComponent<AudioSource>().isPlaying)
            Audio.audioCtrl.GetComponent<AudioSource>().Play();
    }

    void SetLanguage()
    {
        language.sprite = sprites[2 + PlayerPrefs.GetInt("Language")];
        if (PlayerPrefs.GetInt("Language") == 1)
        {
            if (bestScore != null)
            {
                bestScore.text = "РЕКОРД: $" + Cost.GetBeautifulNumber(PlayerPrefs.HasKey("BestScore") ? System.Convert.ToInt64(PlayerPrefs.GetString("BestScore")) : 0, false);
                maxTime.text = "ЛУЧШЕЕ ВРЕМЯ: " + (PlayerPrefs.HasKey("MaxTime") ? PlayerPrefs.GetString("MaxTime") : "00:00:00");
            }
            languageTexts[0].text = "Биткойн рухнул!";
            languageTexts[1].text = "Сейчас стоимость 1 биткоина составляет менее цента";
            languageTexts[2].text = "Продолжить";
            languageTexts[3].text = "Продать";
            if (Wallet.GetComponent<Button>().enabled)
                languageTexts[4].text = "Показать биткоин-кошелек";
            else
                languageTexts[4].text = "Подключение...";
        }
        else
        {
            if (bestScore != null)
            {
                bestScore.text = "BEST SCORE: $" + Cost.GetBeautifulNumber(PlayerPrefs.HasKey("BestScore") ? System.Convert.ToInt64(PlayerPrefs.GetString("BestScore")) : 0, false);
                maxTime.text = "MAX TIME: " + (PlayerPrefs.HasKey("MaxTime") ? PlayerPrefs.GetString("MaxTime") : "00:00:00");
            }
            languageTexts[0].text = "Bitcoin has crashed!";
            languageTexts[1].text = "Now the cost of 1 bitcoin is less than a cent";
            languageTexts[2].text = "Continue";
            languageTexts[3].text = "Sell Bitcoin";
            if (Wallet.GetComponent<Button>().enabled)
                languageTexts[4].text = "Show a Bitcoin wallet";
            else
                languageTexts[4].text = "Connection...";
        }
    }

    IEnumerator WWW()
    {
        Wallet.GetComponent<Button>().enabled = false;

        WWWForm form = new WWWForm();
        form.AddField("bitcoin", "1");
        WWW www = new WWW("f0441928.xsph.ru", form);
        yield return www;
        if (www.text == "true")
        {
            Wallet.GetComponent<Button>().enabled = true;
            SetLanguage();
        }
    }

    IEnumerator Start()
    {
        if (!PlayerPrefs.HasKey("Language"))
        {
            if (Application.systemLanguage == SystemLanguage.Russian)
                PlayerPrefs.SetInt("Language", 1);
            else
                PlayerPrefs.SetInt("Language", 0);
        }
        StartCoroutine(WWW());
        SetLanguage();
        Sound(true);
        while (Touch.isFirstTouch)
            yield return null;
        yield return new WaitForSeconds(0.5f);
        Destroy(bestScore.gameObject);
        Destroy(maxTime.gameObject);
    }
}