using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TMKOC.FamilyTree
{
    public class TreeController : MonoBehaviour
    {
        [SerializeField] private DropController[] dropControllers;


        public DropController GetDropController(int key)
        {
            DropController dc = Array.Find(dropControllers,i=>i.GetValue()==key);
            return dc;
        }
    }
}
