using System;
using UnityEngine;


namespace TMKOC.FamilyTree
{
    public class TreeController : MonoBehaviour
    {
        [SerializeField] private DropController[] dropControllers;
        [SerializeField] private Vector3 scale_mobile;
        [SerializeField] private Vector3 scale_tablet;
        [SerializeField] private Vector3 position_mobile;
        [SerializeField] private Vector3 position_tablet;


        private void Awake()
        {
            SetPosition(DetectAspectRatio());
        }
        private float DetectAspectRatio()
        {
            float screenAspect = (float)Screen.width / (float)Screen.height;
            return screenAspect;
        }
        public DropController GetDropController(int key)
        {
            DropController dc = Array.Find(dropControllers, i => i.GetValue() == key);
            if (dc == null)
            {
                Debug.Log("Drop Controller Not Found");
            }
            return dc;
        }
        private void SetPosition(float screenAspect)
        {
            if (screenAspect < 1.5f)
            {
                transform.position = position_tablet;
                transform.localScale = scale_tablet;
            }
            else//16:9
            {
                transform.position = position_mobile;
                transform.localScale = scale_mobile;
            }
        }
    }
}
