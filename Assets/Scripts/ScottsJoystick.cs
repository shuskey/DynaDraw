using UnityEngine;
using UnityEngine.InputSystem.OnScreen;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.EventSystems;

[AddComponentMenu("Input/On-Screen Stick")]
public class ScottsJoystick : OnScreenControl, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
	[InputControl(layout = "Vector2")]
	[SerializeField]
	private string m_ControlPath;

	protected override string controlPathInternal
	{
		get => m_ControlPath;
		set => m_ControlPath = value;
	}

	public int MovementRange = 100;

	Vector3 m_StartPos;

	void Start()
	{
		m_StartPos = transform.position;
	}

	void UpdateVirtualAxes(Vector3 value)
	{
		var delta = m_StartPos - value;
		delta.y = -delta.y;
		delta /= MovementRange;
		SendValueToControl(new Vector2(-delta.x, delta.y));
	}

	public void OnDrag(PointerEventData data)
	{
		Vector3 newPos = Vector3.zero;

			int deltax = (int)(data.position.x - m_StartPos.x);
			deltax = Mathf.Clamp(deltax, -MovementRange, MovementRange);
			newPos.x = deltax;

			int deltay = (int)(data.position.y - m_StartPos.y);
			deltay = Mathf.Clamp(deltay, -MovementRange, MovementRange);
			newPos.y = deltay;
		
		transform.position = new Vector3(m_StartPos.x + newPos.x, m_StartPos.y + newPos.y, m_StartPos.z + newPos.z);
		UpdateVirtualAxes(transform.position);
	}

	public void OnPointerUp(PointerEventData data)
	{
		transform.position = m_StartPos;
		UpdateVirtualAxes(m_StartPos);
	}

	public void OnPointerDown(PointerEventData data) { }

}