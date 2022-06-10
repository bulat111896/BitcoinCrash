using UnityEngine;
using System.Collections;

public class TwitsController : MonoBehaviour
{
    public GameObject[] twits;
    public GameObject dogecoin;

    IEnumerator Start()
    {
        while (Touch.isFirstTouch)
            yield return null;
        yield return new WaitForSeconds(1.5f);
        while (!Touch.isFirstTouch)
        {
            int randomRange = 0;
            while (true)
            {
                randomRange = Random.Range(0, twits.Length);
                if (!twits[randomRange].activeSelf)
                    break;
            }
            twits[randomRange].SetActive(true);
            yield return new WaitForSeconds(20f / Player.speed);
            if (Random.Range(0, 7) == 0)
                dogecoin.SetActive(true);
            yield return new WaitForSeconds(5f / Player.speed);
        }
    }
}