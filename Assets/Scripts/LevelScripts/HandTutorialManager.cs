using DG.Tweening;
using UnityEngine;

namespace TMKOC.FamilyTree
{
    public class HandTutorialManager : MonoBehaviour
    {
        [SerializeField] private Transform parentT;
        [SerializeField] private Transform handTransform;
        private Vector3 startPoint;

        private void Start()
        {
            startPoint = new Vector3(0,-4f,0);
        }

        public void StartHandTutorial(Vector3 pos1, Transform pos2)
        {
            handTransform.DOLocalMove(pos1, 2f).OnComplete(() =>
            {
                handTransform.SetParent(pos2);
                handTransform.DOLocalMove(pos2.position, 2f).OnComplete(() =>
                {
                    handTransform.SetParent(parentT);
                    handTransform.DOLocalMove(startPoint, 0f);
                });
            });
        }
    }
}
