using UnityEngine;

public class LinesDraw : MonoBehaviour
{
    public GameObject prefab;

    void Start()
    {
        for (float i = -5f; i < 5f + 0.01f; i += 0.25f)
            Instantiate(prefab, new Vector3(0, i, 0), Quaternion.identity);
        for (float i = Line.edgeX - 0.5f; i < -Line.edgeX + 0.5f; i += 0.5f)
            Instantiate(prefab, new Vector3(i, 0, 0), Quaternion.Euler(0, 0, 90));
    }
}