using UnityEngine;
using UnityEngine.Events;

namespace AlterunaPlatfromer
{
	public class InvertEvent : MonoBehaviour
	{
	    public UnityEvent<bool> OnEvent;

	    public void Invoke(bool state) => OnEvent.Invoke(!state);
	}
}
