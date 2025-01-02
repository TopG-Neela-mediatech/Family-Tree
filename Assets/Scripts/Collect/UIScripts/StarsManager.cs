using AssetKits.ParticleImage;
using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace TMKOC.CountWithMe.Collect
{
    public class StarsManager : MonoBehaviour
    {
        [SerializeField] private Transform starParentTransform;
        [SerializeField] private Image[] starImages;
        [SerializeField] ParticleImage starEffect;


        public void MoveStarsDown() => starParentTransform.DOLocalMoveY(0f, 0.5f);
        private void MoveStarsUp() => starParentTransform.DOLocalMoveY(150f, 0f);


        private void Start()
        {
            GameManagerCollect.Instance.OnLevelWin += () => EnableStar(GameManagerCollect.Instance.LevelManager.GetCurrentLevelNumber());
            GameManagerCollect.Instance.OnLevelStart += MoveStarsUp;
            GameManagerCollect.Instance.OnGameEnd += MoveStarsUp;
            GameManagerCollect.Instance.OnLevelLose += MoveStarsUp;
            DisableAllStars();
            SetStarsOnStart(GameManagerCollect.Instance.LevelManager.GetCurrentLevelNumber());
        }
        private void DisableAllStars()
        {
            for (int i = 0; i < starImages.Length; i++)
            {
                starImages[i].enabled = false;
            }
        }
        private void SetStarsOnStart(int levelNumber)
        {
            for (int i = 0; i < levelNumber - 1; i++)
            {
                starImages[i].enabled = true;
            }
        }
        private void EnableStar(int levelCompleted)
        {
            StartCoroutine(EnableStarAfterDelay(levelCompleted));
        }
        private IEnumerator EnableStarAfterDelay(int levelCompleted)
        {
            starEffect.attractorTarget = starImages[levelCompleted - 1].transform;
            starEffect.Play();
            yield return new WaitForSeconds(1.3f);
            starEffect.Stop();
            starImages[levelCompleted - 1].enabled = true;
            starImages[levelCompleted - 1].transform.DOPunchScale(new Vector3(1.2f, 1.2f, 1.2f), 0.3f, 0);
        }
        private void OnDestroy()
        {
            GameManagerCollect.Instance.OnLevelWin -= () => EnableStar(GameManagerCollect.Instance.LevelManager.GetCurrentLevelNumber());
            GameManagerCollect.Instance.OnLevelStart -= MoveStarsUp;
            GameManagerCollect.Instance.OnGameEnd -= MoveStarsUp;
            GameManagerCollect.Instance.OnLevelLose -= MoveStarsUp;
        }
    }
}
