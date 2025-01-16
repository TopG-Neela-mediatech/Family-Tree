using UnityEngine;

namespace TMKOC.FamilyTree
{
    public class PositionSetter : MonoBehaviour
    {
        [SerializeField] private Transform familyMemberParentTransform;


        private void Awake()
        {
            SetPosition(DetectAspectRatio());
        }
        private float DetectAspectRatio()
        {
            float screenAspect = (float)Screen.width / (float)Screen.height;
            return screenAspect;
        }
        private void SetPosition(float screenAspect)
        {
            if (screenAspect < 1.5f)
            {                
                familyMemberParentTransform.localScale = new Vector3(2f, 2f, 2f);                
            }
            else//16:9
            {
                familyMemberParentTransform.position = new Vector3(-7.6f, 0f, 0);                                            
            }
        }
    }
}
