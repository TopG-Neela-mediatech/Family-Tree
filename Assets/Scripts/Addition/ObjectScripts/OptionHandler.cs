using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace TMKOC.CountWithMe.Add
{
    public class OptionHandler : MonoBehaviour
    {
        [SerializeField] private Image optionImage;
        [SerializeField] private Button optionButton;
        [SerializeField] private Button optionButton2;
        [SerializeField] private Button optionButton3;
        [SerializeField] private Transform answerBoxTransform;
        public static event Action OnCorrectSelected;
        private Vector3 ogPosition;
        private static bool calledOnce;


        public int currentValue { get; set; }
        public void SetOptionImage(Sprite sprite) => optionImage.sprite = sprite;


        private void Awake()
        {
            ogPosition = transform.localPosition;
        }
        private void Start()
        {
            DisableAllButton();
            optionButton.onClick.AddListener(StartCheckingAnimation);
            calledOnce = false;
            GameManagerAdd.Instance.OnLevelStart += () => calledOnce = false;
            GameManagerAdd.Instance.LevelManager.OnLevelStartAnimationOver += () => EnableAllButton();
            GameManagerAdd.Instance.OnGameEnd += ResetOption;
            GameManagerAdd.Instance.OnLevelLose += ResetOption;
            GameManagerAdd.Instance.OnLevelWin += ResetOption;
        }
        private void ResetOption()
        {
            this.transform.DOScale(1f, 0f).OnComplete(() =>
            {
                this.transform.DOLocalMove(ogPosition, 0f);
            });
        }
        private void StartCheckingAnimation()
        {
            DisableAllButton();
            GameManagerAdd.Instance.SoundManager.PlaySFX(Sounds.numberTappedSound);
            this.transform.DOMove(answerBoxTransform.position, 0.5f).OnComplete(() =>
            {
                this.transform.DOScale(1.3f, 0.5f).OnComplete(() =>
                {
                    CheckIfCorrect();
                });
            });
        }
        private void DisableAllButton()
        {
            if (!calledOnce)
            {
                calledOnce = true;
                optionButton.enabled = false;
                optionButton2.enabled = false;
                optionButton3.enabled = false;
                calledOnce = false;
            }
        }
        private void EnableAllButton()
        {
            if (!calledOnce)
            {
                optionButton.enabled = true;
                optionButton2.enabled = true;
                optionButton3.enabled = true;
                calledOnce = false;
            }
        }
        private void CheckIfCorrect()
        {
            if (currentValue == GameManagerAdd.Instance.LevelManager.correctAnswer)
            {
                DisableAllButton();
                GameManagerAdd.Instance.LevelManager.EnableConfetti();
                OnCorrectSelected?.Invoke();
                StartCoroutine(InvokeWinAfterDelay());
                GameManagerAdd.Instance.SoundManager.PlaySFX(Sounds.correctChoice);
            }
            else
            {
                GameManagerAdd.Instance.SoundManager.PlaySFX(Sounds.incorrectChoice);
                GameManagerAdd.Instance.SoundManager.PlayIncorrectAudio();
                GameManagerAdd.Instance.LivesManager.ReduceLive();
                this.transform.DOScale(1f, 0.5f).OnComplete(() =>
                 {
                     this.transform.DOLocalMove(ogPosition, 0.5f).OnComplete(() =>
                   {
                       if (GameManagerAdd.Instance.LivesManager.GetLives() > 0)
                       {
                           EnableAllButton();
                       }
                   }
                   );
                 }
                 );
            }
        }
        private IEnumerator InvokeWinAfterDelay()
        {
            yield return new WaitForSeconds(0.5f);
            GameManagerAdd.Instance.LevelManager.ScaleOnEnd(this.transform);
        }
        private void OnDestroy()
        {
            GameManagerAdd.Instance.OnLevelStart -= () => calledOnce = false;
            GameManagerAdd.Instance.LevelManager.OnLevelStartAnimationOver -= () => EnableAllButton();
            GameManagerAdd.Instance.OnGameEnd -= ResetOption;
            GameManagerAdd.Instance.OnLevelLose -= ResetOption;
            GameManagerAdd.Instance.OnLevelWin -= ResetOption;
        }
    }
}
