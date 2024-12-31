using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TMKOC.FamilyTree
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class SpriteScaler : MonoBehaviour
    {  
        [SerializeField]private SpriteRenderer spriteRenderer;
        [SerializeField] private float targetWidth = 1.0f;
        [SerializeField] private float targetHeight = 1.0f;
      
      
        void Start()
        {
            ScaleToTargetSize();
        }
        void ScaleToTargetSize()
        {           
            float spriteWidth = spriteRenderer.sprite.bounds.size.x;
            float spriteHeight = spriteRenderer.sprite.bounds.size.y;           
            float scaleX = targetWidth / spriteWidth;
            float scaleY = targetHeight / spriteHeight;          
            transform.localScale = new Vector3(scaleX, scaleY, 1);
        }
    }
}

