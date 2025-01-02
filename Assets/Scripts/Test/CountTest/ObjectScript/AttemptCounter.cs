using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace TMKOC.CountWithMe.Test
{
    public class AttemptCounter : MonoBehaviour
    {
        
        [SerializeField] private Image correctImage;
        [SerializeField] private Image incorrectImage;


        private void Start()
        {
            DisableCorrectIncorrect();
            TestGameManager.Instance.OnGameEnd += DisableCorrectIncorrect;
        }
        private void DisableCorrectIncorrect()
        {
            correctImage.enabled = false;
            incorrectImage.enabled = false;
        }
        

        public void EnableCorrect() => correctImage.enabled = true;
        public void EnableIncorrect()=>incorrectImage.enabled = true;

        private void OnDestroy()
        {
            TestGameManager.Instance.OnGameEnd -= DisableCorrectIncorrect;
        }
    }
}
