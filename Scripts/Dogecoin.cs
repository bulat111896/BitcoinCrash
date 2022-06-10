using UnityEngine;

public class Dogecoin : MonoBehaviour
{
    void OnEnable()
    {
        transform.position = new Vector3(4.5f, Random.Range(-4f, 2.4f), 0);
        GetComponent<Animation>().Play();
    }

    void Update()
    {
        if (Player.speed == 0)
            return;
        transform.position += Vector3.left * Player.speed * 0.5f * Time.deltaTime;
        if (transform.position.x < -5f)
            gameObject.SetActive(false);
    }
}