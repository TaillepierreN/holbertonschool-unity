using UnityEngine;
using UnityEngine.EventSystems;


public class HoverSound : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] private AudioSource hoverSound;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (hoverSound != null)
        {
            hoverSound.Play(); 
        }
    }
}
