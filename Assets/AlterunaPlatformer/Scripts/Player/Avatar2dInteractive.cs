using UnityEngine;
using UnityEngine.Events;
using Avatar = Alteruna.Avatar;

namespace AlterunaPlatfromer
{
	public class Avatar2dInteractive : MonoBehaviour
	{

		public KeyCode key;
		
		public UnityEvent<Avatar> OnEnter;
		public UnityEvent<Avatar> OnExit;
		
		public UnityEvent OnInteract;

		private void Awake()
		{
			enabled = false;
		}

		private void OnTriggerEnter2D(Collider2D col)
		{
			Avatar avatar = col.attachedRigidbody.gameObject.GetComponentInChildren<Avatar>();

			if (avatar && avatar.IsOwner)
			{
				enabled = true;
				OnEnter.Invoke(avatar);
			}
		}

		private void OnTriggerExit2D(Collider2D col)
		{
			Avatar avatar = col.attachedRigidbody.gameObject.GetComponentInChildren<Avatar>();

			if (avatar && avatar.IsOwner)
			{
				enabled = false;
				OnExit.Invoke(avatar);
			}
		}

		private void Update()
		{
			if (Input.GetKeyDown(key))
			{
				OnInteract.Invoke();
			}
		}
	}
}
