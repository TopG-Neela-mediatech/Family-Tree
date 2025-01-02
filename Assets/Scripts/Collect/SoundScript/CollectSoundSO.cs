using UnityEngine;

namespace TMKOC.CountWithMe.Collect
{
    [CreateAssetMenu(fileName = "CollectSoundData", menuName = "ScriptableObject/CollectSoundSO")]
    public class CollectSoundSO : ScriptableObject
    {
        public AudioClip[] common1to10;
        public AudioClip[] levelStartAudio;
        public AudioClip[] levelLoseAudio;
        public AudioClip[] incorrectAudio;
        public AudioClip[] levelEndAudio;
        public AudioClip[] timeUpAudio;
        public AudioClip gameIntro;
        public AudioClip gameOutro;
    }
}
