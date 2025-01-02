using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TMKOC.CountWithMe.Matching
{
    public class GetNumber : MonoBehaviour
    {
        [SerializeField] private BoxCollider2D colllider;


        public int currentNumber { get; private set; }
        private void EnableCollider() => colllider.enabled = true;
        private void DisableCollider() => colllider.enabled = false;


        private void Start()
        {
            GameManagerMatching.Instance.OnLevelStart += EnableCollider;
            GameManagerMatching.Instance.OnLevelWin += DisableCollider;
            GameManagerMatching.Instance.OnLevelLose += DisableCollider;
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent<DragScriptMatching>(out DragScriptMatching nextObject))
            {
                if (nextObject != null)
                {
                    currentNumber = nextObject.currentNumber;
                }
            }
        }
        private void OnDestroy()
        {
            GameManagerMatching.Instance.OnLevelStart -= EnableCollider;
            GameManagerMatching.Instance.OnLevelWin -= DisableCollider;
            GameManagerMatching.Instance.OnLevelLose -= DisableCollider;
        }
    }
}
