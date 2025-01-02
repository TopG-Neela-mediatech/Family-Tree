using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;


namespace TMKOC.CountWithMe.Add
{
    public class LivesManagerAdd : MonoBehaviour
    {
        [SerializeField] private Transform livesParentTransform;
        [SerializeField] private Image[] filledHeartImages;
        [SerializeField] private GameObject heartBreakEffect;
        public event Action OnLivesOver;
        private int lives = 3;


        private void Start()
        {
            GameManagerAdd.Instance.LevelManager.OnLevelStartAnimationOver += ScaleLiveParent;
            GameManagerAdd.Instance.LevelManager.OnLevelStartAnimationOver += EnableLives;
            GameManagerAdd.Instance.OnLevelWin += DeScaleLiveParent;
            GameManagerAdd.Instance.OnLevelLose += DeScaleLiveParent;
        }


        private void ResetLives() => lives = 3;
        public int GetLives() => lives;


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
                //GameManagerAdd.Instance.SoundManager.PlayLevelLoseAudio();
            }
            SetParticleEffectPosition(lives);
            heartBreakEffect.SetActive(true);
            yield return new WaitForSeconds(1.25f);
            heartBreakEffect.SetActive(false);
            filledHeartImages[lives].enabled = false;
            if (lives <= 0)
            {
                yield return new WaitForSeconds(0.5f);
                GameManagerAdd.Instance.InvokeLevelLose();
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
            GameManagerAdd.Instance.LevelManager.OnLevelStartAnimationOver -= ScaleLiveParent;
            GameManagerAdd.Instance.LevelManager.OnLevelStartAnimationOver -= EnableLives;
            GameManagerAdd.Instance.OnLevelWin -= DeScaleLiveParent;
            GameManagerAdd.Instance.OnLevelLose -= DeScaleLiveParent;
        }
    }
}
