using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System.Collections;


namespace TMKOC.CountWithMe.Collect
{
    public class CollectDrop : MonoBehaviour
    {
        [SerializeField] private Image image;
        [HideInInspector] public static int sum;
        [SerializeField] private Button resetButton;
        private CollectDrag collectDrag;
        private Sprite defaultSprite;
        private static bool isFilling;
        private static bool callOnce;
        private bool isFilled;
        private static int currentNumberCount;


        private void Start()
        {
            GameManagerCollect.Instance.OnLevelStart += ResetValues;
            CollectLevelManager.IncorrectChoice += ResetValues;
            resetButton.onClick.AddListener(ResetDropStatus);
            defaultSprite = image.sprite;
        }
        private void ResetValues()
        {
            resetButton.enabled = false;
            image.sprite = defaultSprite;
            sum = 0;
            isFilled = false;
            isFilling = false;
            callOnce = false;
            currentNumberCount = 0;
            collectDrag = null;
        }
        private IEnumerator EnableButtonAfterDelay()
        {
            yield return new WaitForSeconds(1f);
            resetButton.enabled = true;
        }
        private void ResetDropStatus()
        {
            if (collectDrag != null)
            {
                resetButton.enabled = false;
                image.sprite = defaultSprite;
                sum -= collectDrag.currentNumber;
                isFilled = false;
                isFilling = false;
                callOnce = false;
                currentNumberCount -= 1;
                collectDrag = null;
            }
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out CollectDrag collectible))
            {
                if (collectible != null && !isFilled && !isFilling)
                {
                    collectDrag = collectible;
                    currentNumberCount++;
                    GameManagerCollect.Instance.SoundManager.PlayCurrentNumberSound(currentNumberCount);
                    if (!callOnce)
                    {
                        GameManagerCollect.Instance.LevelManager.ScaleUpCheckAndEnableButton();
                    }
                    isFilling = true;
                    isFilled = true;
                    collectible.EnableCollider(false);
                    collectible.ChangeAlphaToOne();
                    collectible.transform.DOMove(transform.position, 0.5f).OnComplete(() =>
                    {
                        image.sprite = collectible.GetImage().sprite;
                        isFilling = false;
                        collectible.MoveToOGPosi();
                    });
                    sum += collectible.currentNumber;
                    StartCoroutine(EnableButtonAfterDelay());
                }
            }
        }
        private void OnDestroy()
        {
            GameManagerCollect.Instance.OnLevelStart -= ResetValues;
            CollectLevelManager.IncorrectChoice -= ResetValues;
        }
    }
}
