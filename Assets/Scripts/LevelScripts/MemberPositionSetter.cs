using UnityEngine;
using UnityEngine.UI;


namespace TMKOC.FamilyTree
{
    public class MemberPositionSetter : MonoBehaviour
    {
        [SerializeField] private Transform familyMemberParentTransform;
        [SerializeField] private Image fullTreeImage;
        [SerializeField] private Image quizTreeImage;
        [SerializeField] private Image menuFullTreeImage;
        [SerializeField] private Vector3 scale_mobile;
        [SerializeField] private Vector3 scale_tablet;
        [SerializeField] private Vector3 position_mobile;
        [SerializeField] private Vector3 position_tablet;


        private void Start()
        {
            SetFullTreeScale(DetectAspectRatio());
            SetQuizTreeScale(DetectAspectRatio());
        }
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
        private void SetFullTreeScale(float screenAspect)
        {
            if (screenAspect < 1.5f)
            {
                fullTreeImage.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            }
            else
            {
                fullTreeImage.transform.localScale = new Vector3(2f, 2f, 2f);
            }
        }
        private void SetQuizTreeScale(float screenAspect)
        {
            if (screenAspect < 1.5f)
            {
                quizTreeImage.transform.localScale = new Vector3(1f, 1f, 1f);
                menuFullTreeImage.transform.localScale = Vector3.one;
                quizTreeImage.transform.localPosition = new Vector3(0f, 0f, 0f);
            }
            else
            {
                quizTreeImage.transform.localPosition = new Vector3(200f, 0f, 0f);//setting position for android quiz tree 
                quizTreeImage.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                menuFullTreeImage.transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
            }
        }
    }
}
