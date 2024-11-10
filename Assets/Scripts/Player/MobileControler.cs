using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MobileControler : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    private RawImage joystickBG;
    private RawImage joystick;
    private Vector2 inputVector; // Получение координаты джостика

    void Start()
    {
        joystickBG = GetComponent<RawImage>();
        joystick = transform.GetChild(0).GetComponent<RawImage>();
    }

    public virtual void OnPointerDown(PointerEventData ped)
    {
        OnDrag(ped);
    }
    public virtual void OnPointerUp(PointerEventData ped)
    {
        inputVector = Vector2.zero;
        joystick.rectTransform.anchoredPosition = Vector2.zero;
    }
    public virtual void OnDrag(PointerEventData ped)
    {
        Vector2 pos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(joystickBG.rectTransform, ped.position, ped.pressEventCamera, out pos))
        {
            pos.x = (pos.x / joystickBG.rectTransform.sizeDelta.x);
            pos.y = (pos.y / joystickBG.rectTransform.sizeDelta.x);

            inputVector = new Vector2(pos.x * 2, pos.y * 2); // Установка координат джостика в координаты касания
            inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;

            joystick.rectTransform.anchoredPosition = new Vector2(inputVector.x * (joystickBG.rectTransform.sizeDelta.x / 2), inputVector.y * (joystickBG.rectTransform.sizeDelta.y / 2));
        }
    }
    public float Horizontal()
    {
        if (inputVector.x != 0) return inputVector.x;
        else return Input.GetAxisRaw("Horizontal");
    }
    public float Vertical()
    {
        if (inputVector.y != 0) return inputVector.y;
        else return Input.GetAxisRaw("Vertical");
    }

}
