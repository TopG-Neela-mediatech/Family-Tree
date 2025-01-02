using DG.Tweening;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TMKOC.CountWithMe.Add
{
    public class ObjectHandler : MonoBehaviour
    {
        [SerializeField] private Image objectImage;
        [SerializeField] private Button objectButton;
        [SerializeField] private TextMeshProUGUI numberText;
        [SerializeField] private GameObject particelEffectPrefab;
        private Color[] colorList = new[] { Color.yellow };
        private static int number = 0;
        private bool clickedOnce;
        Quaternion tempQuaternion;
        private static event Action OnReachingCorrectNumber;
        private static int correctAnswer;
        private Coroutine shakeCoroutine;


        public void SetObjectImage(Sprite sprite) => objectImage.sprite = sprite;
        void RandomTextColor() => numberText.color = colorList[UnityEngine.Random.Range(0, colorList.Length)];


        private void Start()
        {
            ScaleDown();
            GameManagerAdd.Instance.LevelManager.OnLevelStartAnimationOver += ScaleUp;
            GameManagerAdd.Instance.OnLevelStart += () => clickedOnce = false;
            GameManagerAdd.Instance.OnLevelWin += ScaleDown;
            GameManagerAdd.Instance.OnLevelLose += ScaleDown;
            GameManagerAdd.Instance.OnLevelStart += () => number = 0;
            GameManagerAdd.Instance.OnLevelStart += () => objectButton.enabled = false;
            GameManagerAdd.Instance.LevelManager.OnLevelStartAnimationOver += StartRandomShake;
            GameManagerAdd.Instance.LevelManager.OnLevelStartAnimationOver += () => objectButton.enabled = true;
            GameManagerAdd.Instance.OnLevelStart += () => objectImage.DOFade(1f, 0f);
            objectButton.onClick.AddListener(DisplayText);
            tempQuaternion = Quaternion.identity;
            GameManagerAdd.Instance.OnLevelStart += () => transform.localRotation = tempQuaternion;
            OptionHandler.OnCorrectSelected += () => clickedOnce = true;
            GameManagerAdd.Instance.OnLevelStart += () => correctAnswer = GameManagerAdd.Instance.LevelManager.correctAnswer;
            OnReachingCorrectNumber += () => clickedOnce = true;
            GameManagerAdd.Instance.OnLevelWin += () => StopCoroutine(shakeCoroutine);
            GameManagerAdd.Instance.OnLevelLose += () => StopCoroutine(shakeCoroutine);
        }
        private void StartRandomShake()
        {
            if (!clickedOnce && gameObject.activeSelf)
            {
                shakeCoroutine = StartCoroutine(RandomShakeCoroutine());
            }
        }


        private void ScaleUp() => this.transform.DOScale(1f, 1f);
        private void ScaleDown() => this.transform.DOScale(0f, 0f);


        void DisplayText()
        {
            CheckIfReachedCorrectAmount();
            if (!clickedOnce)
            {
                clickedOnce = true;
                objectButton.enabled = false;
                number++;
                numberText.enabled = true;
                RandomTextColor();
                numberText.rectTransform.localScale = Vector3.zero;
                numberText.text = "" + number;
                GameManagerAdd.Instance.SoundManager.PlayCurrentNumberSound(number);
                particelEffectPrefab.SetActive(true);
                numberText.rectTransform.DOScale(1f, 0.5f);
                numberText.rectTransform.DOShakePosition(0.5f, 15f);
                numberText.rectTransform.DOLocalMoveY(75f, 1f).OnComplete(() =>
                {
                    objectImage.DOFade(0.5f, 0f);
                    numberText.text = "";
                    particelEffectPrefab.SetActive(false);
                    numberText.rectTransform.DOLocalMoveY(0f, 0f);

                });
            }
        }
        private void CheckIfReachedCorrectAmount()
        {
            if (number >= correctAnswer)
            {
                OnReachingCorrectNumber?.Invoke();
            }
        }
        private IEnumerator RandomShakeCoroutine()
        {
            while (!clickedOnce)
            {
                transform.DOShakeRotation(0.5f, 20);
                float randomDelay = UnityEngine.Random.Range(1f, 3f);
                yield return new WaitForSeconds(randomDelay);
            }
        }
        private void OnDestroy()
        {
            GameManagerAdd.Instance.OnLevelStart -= () => clickedOnce = false;
            GameManagerAdd.Instance.OnLevelStart -= () => number = 0;
            GameManagerAdd.Instance.OnLevelStart -= () => objectButton.enabled = false;
            GameManagerAdd.Instance.LevelManager.OnLevelStartAnimationOver -= StartRandomShake;
            GameManagerAdd.Instance.LevelManager.OnLevelStartAnimationOver -= () => objectButton.enabled = true;
            GameManagerAdd.Instance.LevelManager.OnLevelStartAnimationOver -= ScaleUp;
            GameManagerAdd.Instance.OnLevelWin -= ScaleDown;
            GameManagerAdd.Instance.OnLevelLose -= ScaleDown;
            GameManagerAdd.Instance.OnLevelStart -= () => objectImage.DOFade(1f, 0f);
            GameManagerAdd.Instance.OnLevelStart -= () => transform.localRotation = tempQuaternion;
            OptionHandler.OnCorrectSelected -= () => clickedOnce = true;
            OnReachingCorrectNumber -= () => clickedOnce = true;
            GameManagerAdd.Instance.OnLevelStart -= () => correctAnswer = GameManagerAdd.Instance.LevelManager.correctAnswer;
            GameManagerAdd.Instance.OnLevelWin -= () => StopCoroutine(shakeCoroutine);
            GameManagerAdd.Instance.OnLevelLose -= () => StopCoroutine(shakeCoroutine);
        }
    }
}
