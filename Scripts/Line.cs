using UnityEngine;
using System.Collections;

public class Line : MonoBehaviour
{
    public static float edgeX;
    public GameObject panelLost;
    LineRenderer lineRenderer;
    bool isCan;

    IEnumerator WaitPlayerAnimEnd()
    {
        yield return new WaitForSeconds(1);
        isCan = true;
    }

    IEnumerator GarbageCollector()
    {
        while (Touch.isFirstTouch)
            yield return null;
        while (!panelLost.activeSelf)
        {
            yield return new WaitForSeconds(1f);
            while (lineRenderer.GetPosition(0).x < edgeX && lineRenderer.GetPosition(1).x < edgeX)
            {
                Vector3[] mass = new Vector3[lineRenderer.positionCount - 1];
                for (int i = 0; i < lineRenderer.positionCount - 1; i++)
                    mass[i] = lineRenderer.GetPosition(i + 1);
                lineRenderer.positionCount = mass.Length;
                lineRenderer.SetPositions(mass);
            }
        }
    }

    public void SetNewPoint()
    {
        Vector3[] mass = new Vector3[lineRenderer.positionCount + 1];
        for (int i = 0; i < lineRenderer.positionCount; i++)
            mass[i] = lineRenderer.GetPosition(i);
        mass[lineRenderer.positionCount] = new Vector3(-1f, transform.position.y, 0);
        lineRenderer.positionCount = mass.Length;
        lineRenderer.SetPositions(mass);
    }

    void Awake()
    {
        edgeX = -5f * Screen.width / Screen.height;
    }

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        StartCoroutine(GarbageCollector());
        StartCoroutine(WaitPlayerAnimEnd());
    }

    void Update()
    {
        if (!isCan || panelLost.activeSelf)
            return;
        Vector3[] mass = new Vector3[lineRenderer.positionCount];
        for (int i = 0; i < lineRenderer.positionCount - 1; i++)
            mass[i] = lineRenderer.GetPosition(i) - Vector3.right * Player.speed * 0.3f * Time.deltaTime;
        mass[lineRenderer.positionCount - 1] = new Vector3(-1f, transform.position.y, 0);
        lineRenderer.SetPositions(mass);
    }
}