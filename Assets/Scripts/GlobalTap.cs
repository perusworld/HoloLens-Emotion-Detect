using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using HoloToolkit.Unity;
using HoloToolkit.Unity.InputModule;

public class GlobalTap : MonoBehaviour, IInputClickHandler  {

	[Tooltip("The event fired on a Holo tap.")]
	public UnityEvent Tap;

	void Start () {
		InputManager.Instance.PushFallbackInputHandler(gameObject);
	}
	
	void Update () {
		
	}
	public virtual void OnInputClicked(InputClickedEventData eventData)
	{
			Tap.Invoke();
			eventData.Use();
	}
}
