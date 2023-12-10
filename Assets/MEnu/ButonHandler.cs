using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class ButonHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Color normalColor = Color.green;
    private Color highlightColor = Color.red;
    private Image buttonImage;
    public AudioClip AudioClip;
    public AudioSource audioSource;
    void Start()
    {
        buttonImage = GetComponent<Image>();
        normalColor = buttonImage.color;
        if (normalColor == highlightColor)
        {
            normalColor = Color.green;
        }
        //audioSource = GetComponent<AudioSource>();
        audioSource.clip = AudioClip;
        // Przypisz dŸwiêk do komponentu AudioSource


    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonImage.color = highlightColor;
        audioSource.Play();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonImage.color = normalColor;
    }
    public void Reset()
    {
        // Debug.Log(gameObject.name + " "+ "res" + normalColor.ToString() +" " +  highlightColor.ToString());
        buttonImage.color = normalColor;
    }
}
