using UnityEngine;
using UnityEngine.InputSystem.OnScreen;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.EventSystems;

[AddComponentMenu("Input/On-Screen Button")]
public class ScottsButtonHandler : OnScreenControl, IPointerDownHandler, IPointerUpHandler
{

    [InputControl(layout = "Button")]
    [SerializeField]
    private string m_ControlPath;

    protected override string controlPathInternal
    {
        get => m_ControlPath;
        set => m_ControlPath = value;
    }
    public void OnPointerUp(PointerEventData data)
    {
        SendValueToControl(0.0f);
    }

    public void OnPointerDown(PointerEventData data)
    {
        SendValueToControl(1.0f);
    }
    public void SetDownState()
    {
        SendValueToControl(1.0f);
    }

    public void SetUpState()
    {
        SendValueToControl(0.0f);
    }
}
