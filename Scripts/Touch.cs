using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class Touch : MonoBehaviour, IPointerDownHandler
{
    public GameObject[] toDestroy;
    public GameObject panelDonate;
    public Animation sell;
    public Image bitcoin_sign;
    public Text title;
    public RectTransform panel;
    public Transform Elon;
    public Line line;
    public static bool isFirstTouch;
    bool isCan;

    IEnumerator GameAnim()
    {
        while (panel.anchoredPosition.y > 0.01f)
        {
            panel.anchoredPosition = Vector3.Lerp(panel.anchoredPosition, new Vector3(0, 0, 0), 10f * Time.deltaTime);
            Elon.position = Vector3.Lerp(Elon.position, new Vector3(Elon.position.x, 0, 0), 15f * Time.deltaTime);
            title.color -= new Color(0, 0, 0, 15f * Time.deltaTime);
            bitcoin_sign.color = title.color;
            title.GetComponent<RectTransform>().position += Vector3.up * 400f * Time.deltaTime;
            yield return null;
        }
        panel.anchoredPosition = new Vector3(0, 0, 0);
        Elon.position = new Vector3(Elon.position.x, 0, 0);
        Destroy(title.gameObject);
        sell.gameObject.SetActive(true);
        sell.Play();
    }

    IEnumerator Wait(float time)
    {
        isCan = false;
        yield return new WaitForSeconds(time);
        isCan = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!isCan)
            return;
        if (panelDonate != null && panelDonate.activeSelf)
            return;
        if (isFirstTouch)
        {
            isFirstTouch = false;
            for (int i = 0; i < toDestroy.Length; i++)
            {
                Destroy(toDestroy[i]);
            }
            toDestroy = null;
            StartCoroutine(GameAnim());
        }
        Player.direction = Player.direction != 1 ? 1 : -1;
        line.SetNewPoint();
        if (eventData != null)
            Audio.audioCtrl.PlayChangeDirection();
        StartCoroutine(Wait(0.04f));
    }

    void Awake()
    {
        isFirstTouch = true;
    }

    void Start()
    {
        sell.gameObject.SetActive(false);
        panel.anchoredPosition = new Vector3(0, 230, 0);
        Elon.position = new Vector3(-Line.edgeX, -3f, 0);
        StartCoroutine(Wait(1.5f));
    }
}