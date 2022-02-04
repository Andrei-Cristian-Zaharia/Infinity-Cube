using UnityEngine;
using UnityEngine.EventSystems;

public class Tutorial : MonoBehaviour, IPointerDownHandler
{
    public GameObject tutorialPanel;

    void Start()
    {
        tutorialPanel.SetActive(true);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Destroy(tutorialPanel);
    }
}
