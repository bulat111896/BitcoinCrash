using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CopyWallet : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        GetComponent<Text>().color = new Color(1, 1, 1, 0.6f);
        transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        GetComponent<Text>().color = new Color(1, 1, 1, 0.9f);
        transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 0.8f);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        GUIUtility.systemCopyBuffer = "0xBC3Ee6C1ff1D4920BA49a069e8708FD800d25b69";
        new AndroidJavaClass("com.example.androidnativelibrary.AndroidBridge").CallStatic("ShowToast", PlayerPrefs.GetInt("Language") == 1 ? "Успешно скопировано!" : "Copied successfully!", 0);
        Audio.audioCtrl.PlayClick();
    }
}