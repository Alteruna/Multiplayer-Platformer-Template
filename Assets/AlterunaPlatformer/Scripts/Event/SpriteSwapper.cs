using System;
using Alteruna;
using UnityEngine;
using UnityEngine.Events;

namespace AlterunaPlatfromer
{
	public class SpriteSwapper : AttributesSync
	{
		public Sprite[] Sprites;
		public SpriteRenderer SpriteRenderer;
		public UnityEvent<int> OnSpriteChange;

		public int CurrentSpriteId { get; private set; } = -1;

		[SynchronizableMethod]
		public void SetSprite(int id)
		{
			SpriteRenderer.sprite = Sprites[id];
			CurrentSpriteId = id;
			OnSpriteChange.Invoke(id);
		}

		public void SetSpriteGlobal(int id)
		{
			if (id == CurrentSpriteId)
			{
				return;
			}

			if (id < 0 || id >= Sprites.Length)
			{
				throw new IndexOutOfRangeException();
			}

			BroadcastRemoteMethod(0, id);
			SetSprite(id);
		}

		public void SetSpriteGlobal() => SetSpriteGlobal((CurrentSpriteId + 1) % Sprites.Length);
	}
}
