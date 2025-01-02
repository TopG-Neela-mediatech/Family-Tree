using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace TMKOC.CountWithMe.Matching
{
    public class LivesManager : MonoBehaviour
    {
        [SerializeField] private Transform livesParentTransform;
        [SerializeField] private Image[] filledHeartImages;         
        [SerializeField] private GameObject heartBreakEffect;
        public event Action OnLivesOver; 
        private int lives = 3;     
        

        private void Start()
        {
            GameManagerMatching.Instance.OnLevelStart += ScaleLiveParent;
            GameManagerMatching.Instance.OnLevelStart += EnableLives;
            GameManagerMatching.Instance.OnLevelWin += DeScaleLiveParent;
            GameManagerMatching.Instance.OnLevelLose += DeScaleLiveParent;
        }
        private void ResetLives() => lives = 3;
        private void EnableLives()
        {
            ResetLives();
            foreach (var item in filledHeartImages)
            {
                item.enabled = true;
            }
        }
        private void DeScaleLiveParent()
        {
            livesParentTransform.DOScale(0f, 0.5f);
        }
        private void ScaleLiveParent()
        {
            livesParentTransform.DOScale(1f, 1f);
        }
        public void ReduceLive()
        {
            StartCoroutine(ReduceLiveAfterDelay());
        }
        private IEnumerator ReduceLiveAfterDelay()
        {
            lives--;
            if (lives <= 0)
            {
                OnLivesOver?.Invoke();
                GameManagerMatching.Instance.SoundManager.PlayLevelLoseAudio();
            }
            SetParticleEffectPosition(lives);
            heartBreakEffect.SetActive(true);
            yield return new WaitForSeconds(1.25f);
            heartBreakEffect.SetActive(false);
            filledHeartImages[lives].enabled = false;
            if (lives <= 0)
            {
                yield return new WaitForSeconds(0.8f);
                GameManagerMatching.Instance.InvokeLevelLose();
            }
        }
        private void SetParticleEffectPosition(int liveNumber)
        {
            if (liveNumber == 2)
            {
                heartBreakEffect.transform.localPosition = new Vector3(87f, heartBreakEffect.transform.localPosition.y, 0f);
            }
            if (liveNumber == 1)
            {
                heartBreakEffect.transform.localPosition = new Vector3(0f, heartBreakEffect.transform.localPosition.y, 0f);
            }
            if (liveNumber == 0)
            {
                heartBreakEffect.transform.localPosition = new Vector3(-87f, heartBreakEffect.transform.localPosition.y, 0f);
            }
        }
        private void OnDestroy()
        {
            GameManagerMatching.Instance.OnLevelStart -= ScaleLiveParent;
            GameManagerMatching.Instance.OnLevelStart -= EnableLives;
            GameManagerMatching.Instance.OnLevelWin -= DeScaleLiveParent;
            GameManagerMatching.Instance.OnLevelLose -= DeScaleLiveParent;
        }
    }
}

