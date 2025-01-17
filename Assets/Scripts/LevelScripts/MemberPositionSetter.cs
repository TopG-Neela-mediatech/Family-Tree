using UnityEngine;

namespace TMKOC.FamilyTree
{
    public class MemberPositionSetter : MonoBehaviour
    {
        [SerializeField] private Transform familyMemberParentTransform;
        [SerializeField] private Vector3 scale_mobile;
        [SerializeField] private Vector3 scale_tablet;
        [SerializeField] private Vector3 position_mobile;
        [SerializeField] private Vector3 position_tablet;


        public void SetFamilyMemberPositionAndScale(Transform fMember)
        {
            SetPositionAndScale(DetectAspectRatio(), fMember);
        }
        private float DetectAspectRatio()
        {
            float screenAspect = (float)Screen.width / (float)Screen.height;
            return screenAspect;
        }
        private void SetPositionAndScale(float screenAspect, Transform familyMember)
        {
            if (screenAspect < 1.5f)
            {
                familyMember.position = position_tablet;
                familyMember.localScale = scale_tablet;
            }
            else//16:9
            {
                familyMember.position = position_mobile;
                familyMember.localScale = scale_mobile;
            }
        }
    }
}
