using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TMKOC.FamilyTree
{
    public class PositionSetter : MonoBehaviour
    {
        [SerializeField] private Transform familyMemberParentTransform;
        [SerializeField] private Transform treeParentTransform;

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
            if (screenAspect < 1.4f)
            {
                familyMemberParentTransform.position = new Vector3(-4f, 0, 0);
                treeParentTransform.position = new Vector3(2.3f, -0.25f, 0);
                familyMemberParentTransform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                treeParentTransform.localScale = new Vector3(0.7f,0.7f,0.7f);
            }
            else//16:9
            {
                familyMemberParentTransform.position = new Vector3(-8.3f, 0, 0);
                treeParentTransform.position = new Vector3(3f, -0.25f, 0f);
                familyMemberParentTransform.localScale = new Vector3(2f, 2f, 2f);
                treeParentTransform.localScale = Vector3.one;
            }
        }
    }
}
