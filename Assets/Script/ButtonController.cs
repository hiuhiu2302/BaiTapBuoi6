using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool isButtonPressed = false;
    private Button button;

    private void Start()
    {
        button = GetComponent<Button>();
    }

    private void Update()
    {
        if (isButtonPressed)
        {
            Player player = GetComponentInParent<Player>();
            player.Chaytrai();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isButtonPressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isButtonPressed = false;
    }
}