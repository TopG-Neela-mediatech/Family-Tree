using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TMKOC.CountWithMe
{
    public class NumberDisplayScript : MonoBehaviour
    {
        [SerializeField] private Button objectButton;
        [SerializeField] private TextMeshProUGUI numberText;
        [SerializeField] private Image objectImage;
        [SerializeField] private GameObject particelEffectPrefab;
        [SerializeField] private CountingTutorialManager tutorialManager;
        private Color[] colorList = new[] { Color.cyan, Color.magenta, Color.red };
        private static int number = 0;
        bool clickedOnce = false;
        static bool callOnce;


        private void Start()
        {
            GameManagerCounting.Instance.OnLevelStart += (() => callOnce = false);
            GameManagerCounting.Instance.OnLevelStart += () => clickedOnce = false;
            GameManagerCounting.Instance.OnLevelStart += (() => number = 0);
            GameManagerCounting.Instance.CountingLevelManager.OnSpawningFinished += StartRandomShake;
            GameManagerCounting.Instance.CountingLevelManager.OnSpawningFinished += (() => SetButtonStatus(true));
            GameManagerCounting.Instance.OnLevelEnd += OnLevelEnd;
            GameManagerCounting.Instance.OnGameEnd += OnLevelEnd;
            objectButton.onClick.AddListener(DisplayText);
            SetButtonStatus(false);
            ResetTextObject();
        }


        void RandomTextColor() => numberText.color = colorList[UnityEngine.Random.Range(0, colorList.Length)];
        private void SetButtonStatus(bool status) { if (objectButton) objectButton.enabled = status; }


        void DisplayText()
        {
            clickedOnce = true;
            SetButtonStatus(false);
            number++;
            numberText.enabled = true;
            RandomTextColor();
            numberText.rectTransform.localScale = Vector3.zero;
            numberText.text = "" + number;
            GameManagerCounting.Instance.CountingSoundManager.PlayCurrentNumberSound(number);
            particelEffectPrefab.SetActive(true);
            numberText.rectTransform.DOScale(0.7f, 0.5f);
            numberText.rectTransform.DOShakePosition(0.5f, 15f);
            numberText.rectTransform.DOLocalMoveY(100f, 1f).OnComplete(() =>
            {
                CheckAndEnableNextButton();
                objectImage.DOFade(0.5f, 0f);
                particelEffectPrefab.SetActive(false);
            });
        }
        void ResetTextObject()
        {
            numberText.enabled = false;
            numberText.rectTransform.localPosition = Vector3.zero;
        }
        private void StartRandomShake()
        {
            if (!clickedOnce && gameObject.activeSelf)
            {
                StartCoroutine(RandomShakeCoroutine());
            }
        }
        private IEnumerator RandomShakeCoroutine()
        {
            while (!clickedOnce)
            {
                this.tutorialManager.StartHint(transform.position);
                transform.DOShakeRotation(0.5f, 20);
                float randomDelay = UnityEngine.Random.Range(1f, 3f);
                yield return new WaitForSeconds(randomDelay);
            }
        }
        private static void CheckAndEnableNextButton()
        {
            if (number >= GameManagerCounting.Instance.CountingLevelManager.currentLevelNumber + 1 && !callOnce)
            {
                callOnce = true;
                GameManagerCounting.Instance.CountingLevelManager.EnableNextLevelButton();
            }
        }
        private void OnLevelEnd()
        {
            if (gameObject.activeSelf)
            {
                StartCoroutine(DeScaleOnLevelEnd());
            }
        }
        private IEnumerator DeScaleOnLevelEnd()
        {
            yield return new WaitForSeconds(1f);
            gameObject.transform.DOScale(0f, 1.5f).OnComplete(() =>
                             {
                                 gameObject.SetActive(false);
                                 gameObject.transform.localScale = Vector3.one;
                                 numberText.text = "";
                                 objectImage.DOFade(1f, 0f);
                             }
                             );
        }
        private void OnDestroy()
        {
            GameManagerCounting.Instance.OnLevelStart -= (() => callOnce = false);
            GameManagerCounting.Instance.OnLevelStart -= () => clickedOnce = false;
            GameManagerCounting.Instance.OnLevelStart -= (() => number = 0);
            GameManagerCounting.Instance.CountingLevelManager.OnSpawningFinished -= StartRandomShake;
            GameManagerCounting.Instance.CountingLevelManager.OnSpawningFinished -= (() => SetButtonStatus(true));
            GameManagerCounting.Instance.OnLevelEnd -= OnLevelEnd;
            GameManagerCounting.Instance.OnGameEnd -= OnLevelEnd;
        }

    }
}

