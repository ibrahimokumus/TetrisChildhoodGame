using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeManager : MonoBehaviour
{
   [SerializeField] private bool isRotatable = true;
   
   private Vector3 originalPosition;
   
    
   public void rightMovement()
   {
      transform.Translate(Vector3.right,Space.World);
   }
   
   public void leftMovement()
   {
      transform.Translate(Vector3.left,Space.World);
   }
   public void upMovement()
   {
      transform.Translate(Vector3.up,Space.World);
   }
   public void downMovement()
   {
      transform.Translate(Vector3.down,Space.World);
   }
   public void rightRotation()
   {
      
         if (isRotatable)
         {
            transform.Rotate(0, 0, -90);
         }
   }
   
   public void leftRotation()
   {
      if (isRotatable)
      {
         transform.Rotate(0, 0, 90);
      }
   }

  
   
  

}
