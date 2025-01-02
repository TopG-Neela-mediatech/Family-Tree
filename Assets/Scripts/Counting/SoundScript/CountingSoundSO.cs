using UnityEngine;

namespace TMKOC.CountWithMe
{
    [CreateAssetMenu(fileName = "CountingSoundData", menuName = "ScriptableObject/CountingSoundSO")]
    public class CountingSoundSO : ScriptableObject
    {
        public AudioClip[] common1to10;
        public AudioClip[] levelStartAudio;
        public AudioClip[] levelEndAudio;
        public AudioClip gameIntro;
        public AudioClip gameOutro;
    }
}
