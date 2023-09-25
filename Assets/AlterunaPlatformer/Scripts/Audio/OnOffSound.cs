using UnityEngine;
using UnityEngine.Events;

namespace AlterunaPlatfromer
{
    public class OnOffSound : MonoBehaviour
    {
        public AudioSource AudioSource;

        public AudioClip ClipOn, ClipOff;

        public UnityEvent<bool> OnPlay;

        public void Play(int v) => Play((v & 1) == 1);

        public void Play(bool v)
        {
            AudioSource.PlayOneShot(v ? ClipOn : ClipOff);
            OnPlay.Invoke(v);
        }
    }
}