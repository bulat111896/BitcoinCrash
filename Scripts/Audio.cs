using UnityEngine;
using UnityEngine.UI;

public class Audio : MonoBehaviour
{
    public static Audio audioCtrl;
    public AudioClip collision, changeDirection, open, twit, click, coin;

    void Awake()
    {
        if (!PlayerPrefs.HasKey("Sound"))
            PlayerPrefs.SetInt("Sound", 1);
        if (audioCtrl == null)
        {
            audioCtrl = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    public void PlayCollision()
    {
        GetComponent<AudioSource>().PlayOneShot(collision);
    }

    public void PlayChangeDirection()
    {
        GetComponent<AudioSource>().PlayOneShot(changeDirection);
    }

    public void PlayOpen()
    {
        GetComponent<AudioSource>().PlayOneShot(open);
    }

    public void PlayTwit()
    {
        GetComponent<AudioSource>().PlayOneShot(twit);
    }

    public void PlayClick()
    {
        GetComponent<AudioSource>().PlayOneShot(click);
    }

    public void PlayCoin()
    {
        GetComponent<AudioSource>().PlayOneShot(coin);
    }
}