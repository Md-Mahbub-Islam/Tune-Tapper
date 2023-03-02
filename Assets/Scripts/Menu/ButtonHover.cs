using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonHover : MonoBehaviour, IPointerEnterHandler
{
    public AudioClip hoverSound;
    private AudioSource audioSource;

    void Start()
    {
        // Get the AudioSource component attached to the game object
        audioSource = GetComponent<AudioSource>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Play the hover sound when the mouse pointer enters the button
        audioSource.PlayOneShot(hoverSound);
    }
}
