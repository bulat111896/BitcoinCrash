using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player : MonoBehaviour
{
    public static float speed;
    public static int direction;
    public GameObject panelLost;
    public Touch touch;
    public Cost cost;
    public Text timer;

    IEnumerator CountTime()
    {
        while (Touch.isFirstTouch)
            yield return null;
        float saveTime = Time.time, time = 0;
        byte minute = 0, second = 0;
        while (!panelLost.activeSelf)
        {
            time = Time.time - saveTime;
            second = (byte)(time - minute * 60);
            if (second == 60)
                minute++;
            if (minute == 60)
                break;
            timer.text = minute.ToString("00") + ":" + second.ToString("00") + ":" + ((time - (int)time) * 100).ToString("00").Substring(0, 2);
            yield return null;
        }
        timer.color = new Color(1, 0.3f, 0.3f, timer.color.a);
        if (time > PlayerPrefs.GetFloat("MaxTimeFloat"))
        {
            PlayerPrefs.SetFloat("MaxTimeFloat", time);
            PlayerPrefs.SetString("MaxTime", timer.text);
        }
        speed = 0;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Respawn")
        {
            long cents = (long)(cost.totalCost / (collision.GetComponent<Twit>().mood == Twit.Mood.positive ? Random.Range(4f, 12f) : Random.Range(-2.5f, -1.5f)));
            cost.Change(cents == 0 ? 1 : cents);
            collision.gameObject.SetActive(false);
            Audio.audioCtrl.PlayTwit();
        }
        else if (collision.name == "Dogecoin")
        {
            long cents = (long)(cost.totalCost / Random.Range(2f, 4f));
            cost.Change(cents < 3 ? 100 : cents);
            collision.gameObject.SetActive(false);
            Audio.audioCtrl.PlayCoin();
        }
    }

    void Awake()
    {
        direction = 0;
        speed = 4f;
    }

    void Start()
    {
        StartCoroutine(CountTime());
    }

    void Update()
    {
        if (panelLost.activeSelf)
            return;
        if (direction != 0)
            speed += 0.1f * Time.deltaTime;
        transform.position += Vector3.up * (direction * 2.5f + speed * direction * 0.05f) * Time.deltaTime;
        if (transform.position.y < -4.2f || transform.position.y > 2.4f)
        {
            transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -4.2f, 2.4f), 0);
            cost.Change((long)(cost.totalCost / Random.Range(-2f, -1.2f)));
            touch.OnPointerDown(null);
            Camera.main.GetComponent<Animation>().Play();
            Audio.audioCtrl.PlayCollision();
        }
    }
}