using UnityEngine;

namespace AlterunaPlatfromer
{
    public class PlayAudioHere : MonoBehaviour
    {

        public AudioSource AudioSource;

        private Transform _transform;

        private void Awake() => _transform = transform;

        public void Play()
        {
            AudioSource.Play();
        }
        
        public void Play(AudioClip clip)
        {
            AudioSource.PlayOneShot(clip);
        }
        
        public void Play(Vector3 pos)
        {
            _transform.position = pos;
            AudioSource.Play();
        }
        
        public void Play(Vector3 pos, AudioClip clip)
        {
            _transform.position = pos;
            AudioSource.PlayOneShot(clip);
        }
    }
}
