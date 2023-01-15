using UnityEngine;
using UnityEngine.EventSystems;

public class IconHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private bool mouseHoveringOver = false;
    [SerializeField] private GameObject icon;
    [SerializeField] private GameObject iconHovered;
    

    public void OnPointerEnter(PointerEventData eventData)
    {
        icon.SetActive(false);
        iconHovered.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        icon.SetActive(true);
        iconHovered.SetActive(false);
    }
}
