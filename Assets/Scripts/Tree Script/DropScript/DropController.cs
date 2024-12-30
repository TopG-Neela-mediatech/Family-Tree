using DG.Tweening;
using UnityEngine;

public class DropController : MonoBehaviour
{
    [SerializeField] private int value;
    public static bool canCheck;


    public int GetValue() => value;

    private void Start()
    {
        canCheck = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<DragScript>(out DragScript familyMember))
        {
            if (familyMember != null && canCheck)
            {
                canCheck = false;
                familyMember.isOnTree = true;
                familyMember.transform.DOMove(transform.position, 0.5f).OnComplete(() =>
                {
                    if (value == familyMember.value)
                    {
                        //check if all correct
                        Debug.Log("Win");
                        canCheck = true;
                    }
                    else
                    {
                        familyMember.ReturnToOriginalPosition();
                    }
                });
            }
        }
    }
}
