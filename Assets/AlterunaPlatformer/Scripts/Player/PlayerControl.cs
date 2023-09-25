using Alteruna;
using UnityEngine;
using Avatar = Alteruna.Avatar;

namespace AlterunaPlatfromer
{
	[RequireComponent(typeof(Avatar), typeof(InputSynchronizable))]
	public class PlayerControl : Synchronizable
	{
		/// <summary>
		/// Target body.
		/// </summary>
		public Rigidbody2D Rigidbody;

		/// <summary>
		/// JumpSFX
		/// </summary>
		public PlayAudioSync Jump;

		/// <summary>
		/// Horizontal walking force.
		/// </summary>
		public float Speed = 5;

		/// <summary>
		/// Force applied on a jump.
		/// </summary>
		public float JumpStrength = 5;

		/// <summary>
		/// Points that look for ground to validate a jump.
		/// </summary>
		public Transform[] ContactPoints;

		/// <summary>
		/// Margin for grounded.
		/// </summary>
		private const float RAY_LENGHT = 0.1f;

		/// <summary>
		/// Force applied while holding the up key during a jump.
		/// </summary>
		private const float JUMP_LONG_FORCE = 0.75f;

		/// <summary>
		/// Duration for the debug rays.
		/// </summary>
		private const float DEBUG_RAY_DURATION = 0.1f;
		
		/// <summary>
		/// Possesing avatar.
		/// </summary>
		private Avatar _avatar;
		
		/// <summary>
		/// Synchronized Input
		/// </summary>
		private InputSynchronizable _inputManager;
		
		private SyncedAxis _horizontalAxis;
		private SyncedAxis _verticalAxis;

		private void Start()
		{
			_avatar = GetComponent<Avatar>();
			_inputManager = GetComponent<InputSynchronizable>();
			
			// Register input axes.
			_horizontalAxis = new SyncedAxis(_inputManager, "Horizontal");
			_verticalAxis = new SyncedAxis(_inputManager, "Vertical");

			// Activate camera follow.
			if (_avatar.IsPossessed)
			{
				// Activate camera follow now.
				if (_avatar.IsOwner)
				{
					CameraFollow.NewTarget(transform);
				}
			}
			else
			{
				// Subscribe to event in order to activate camera follow later.
				_avatar.OnPossessed.AddListener(user =>
				{
					if (_avatar.IsOwner)
					{
						CameraFollow.NewTarget(transform);
					}
				});
			}
		}

		private void FixedUpdate()
		{
			// Get current velocity and apply horizontal movement.
			Vector2 newVel = new Vector2(Speed * _horizontalAxis, Rigidbody.velocity.y);

			// Press up.
			if (_verticalAxis > 0f)
			{
				// Not moving vertically.
				if (Mathf.Abs(newVel.y) < 0.01f)
				{
					// Check if grounded.
					for (int i = 0, l = ContactPoints.Length; i < l; i++)
					{
						if (ValidCollision(Physics2D.RaycastAll(ContactPoints[i].position, Vector2.down, RAY_LENGHT)))
						{
#if UNITY_EDITOR
							Debug.DrawRay(ContactPoints[i].position, Vector3.down * 0.1f, Color.red, DEBUG_RAY_DURATION);
#endif
							// Jump!
							newVel.y = JumpStrength;
							Jump.Play();
							break;
						}
#if UNITY_EDITOR
						Debug.DrawRay(ContactPoints[i].position, Vector3.down * 0.1f, Color.green, DEBUG_RAY_DURATION);
#endif
					}
				}
				// Hold up to jump higher.
				// When moving up.
				else if (newVel.y > 0)
				{
					// apply small force up.
					newVel.y += Mathf.Min(JUMP_LONG_FORCE, newVel.y / (-Physics.gravity.y * Rigidbody.gravityScale * 3));
				}
			}

			// Apply force.
			Rigidbody.velocity = newVel;
		}

		/// <summary>
		/// Check if collided excluding body
		/// </summary>
		bool ValidCollision(RaycastHit2D[] hits)
		{
			for (int i = 0, l = hits.Length; i < l; i++)
			{
				if (hits[i].transform != Rigidbody.transform)
				{
					return true;
				}
			}

			return false;
		}

		// Sync position on collision
		public void OnCollisionEnter2D(Collision2D col)
		{
			if (_avatar.IsOwner)
			{
				Commit();
				SyncUpdate();
			}
		}

		public override void AssembleData(Writer writer, byte LOD = 100)
		{
			// write position as vector2
			writer.Write(Rigidbody.position);
		}

		public override void DisassembleData(Reader reader, byte LOD = 100)
		{
			// read position as vector2
			Rigidbody.position = reader.ReadVector2();
		}
	}
}
