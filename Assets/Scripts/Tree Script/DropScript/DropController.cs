using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace TMKOC.FamilyTree
{
    public class DropController : MonoBehaviour
    {
        [SerializeField] private int value;
        [SerializeField] private TextMeshProUGUI dataText;
        [SerializeField] private Collider2D col;
       // [SerializeField] private Image displaySprite;
        public static bool canCheck;
        private bool isEmpty;


        public int GetValue() => value;
        public void EnableCollider()=>col.enabled = true;


        private void Start()
        {
            canCheck = true;
            isEmpty = true;
        }
        /*public void SetRevealedData(Sprite sprite, string data)
        {
            displaySprite.sprite = sprite;
            dataText.text = data;
        }*/
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent<DragScript>(out DragScript familyMember))
            {
                if (familyMember != null && canCheck && isEmpty)
                {
                    isEmpty = false;
                    canCheck = false;
                    familyMember.isOnTree = true;
                    familyMember.transform.DOMove(transform.position, 0.5f).OnComplete(() =>
                    {
                        if (value == familyMember.value)
                        {
                            canCheck = true;
                            familyMember.enabled = false;
                            GameManager.Instance.LevelManager.EnableNextMember();
                        }
                        else
                        {
                            familyMember.ReturnToOriginalPosition();
                            isEmpty = true;                            
                        }
                    });
                }
            }
        }

    }
}
 

/*public void DisableChecking()
        {
            this.isEmpty = false;
            canCheck = false;
            this.col.enabled = false;
        }*/