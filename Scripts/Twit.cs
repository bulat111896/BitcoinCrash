using UnityEngine;
using System.Collections;

public class Twit : MonoBehaviour
{
    public enum Mood { positive, negative }

    public Mood mood;
    Animation anim;
    bool isAnimStarted;

    IEnumerator Wait()
    {
        isAnimStarted = true;
        anim.Play("Twit_down");
        while (anim.isPlaying)
            yield return null;
        gameObject.SetActive(false);
    }

    void Awake()
    {
        anim = GetComponent<Animation>();
    }

    void OnEnable()
    {
        isAnimStarted = false;
        StopAllCoroutines();
        transform.position = new Vector3(4.5f, Random.Range(-4f, 2.4f), 0);
        anim.Play("Twit_up");
    }

    void Update()
    {
        if (Player.speed == 0)
            return;
        transform.position += Vector3.left * Player.speed * 0.3f * Time.deltaTime;
        if (!isAnimStarted && transform.position.x < -3f)
            StartCoroutine(Wait());
    }
}