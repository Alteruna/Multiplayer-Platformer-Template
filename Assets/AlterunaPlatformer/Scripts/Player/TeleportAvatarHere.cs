using UnityEngine;
using Avatar = Alteruna.Avatar;

namespace AlterunaPlatfromer
{
	public class TeleportAvatarHere : MonoBehaviour
	{
	    public void Teleport(Avatar avatar) => avatar.transform.position = transform.position;
	}
}
