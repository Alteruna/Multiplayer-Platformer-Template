using Alteruna;
using UnityEngine;

namespace AlterunaPlatfromer
{
    public class PlayAudioSync : AttributesSync
    {
        public AudioSource AudioSource;
        
        public void Play()
        {
            InvokeRemoteMethod();
            PlayRemote();
        }

        [SynchronizableMethod]
        private void PlayRemote()
        {
            AudioSource.Play();
        }
    }
}
