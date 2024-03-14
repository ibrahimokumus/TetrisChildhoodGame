using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeManager : MonoBehaviour
{
   [SerializeField] private bool isRotatable = true;
   [SerializeField] private ShapeManager pivot;
   private Vector3 originalPosition;
    void Start()
   {
      //InvokeRepeating("downMovement",0.1f,0.5f);
     StartCoroutine(otoMovement());
    // StartCoroutine(otoMovement2());
   }
   
    public void setPivot(ShapeManager shapeObject)
    {
       pivot = shapeObject;
    }
    
   public void rightMovement()
   {
      transform.Translate(new Vector2(0.5f,0),Space.World);
   }
   
   public void leftMovement()
   {
      transform.Translate(new Vector2(-0.5f,0),Space.World);
   }
   public void upMovement()
   {
      transform.Translate(new Vector2(0,0.5f),Space.World);
   }
   public void downMovement()
   {
      transform.Translate(new Vector2(0,-0.5f),Space.World);
   }
   public void rightRotation()
   {
      
         if (isRotatable)
         {
               originalPosition = transform.position;
               transform.RotateAround(pivot.transform.GetChild(0).position, Vector3.forward, -90f);
               transform.position = originalPosition;
         }
   }
   
   public void leftRotation()
   {
      if (isRotatable)
      {
         originalPosition = transform.position;
         transform.RotateAround(pivot.transform.GetChild(0).position, Vector3.forward, 90f);
         transform.position = originalPosition;
      }
   }

   IEnumerator otoMovement()
   {
      while (true)
      {
         downMovement();
         yield return new WaitForSeconds(0.5f);
      }
      
   }
   
   IEnumerator otoMovement2()
   {
      while (true)
      {
         leftRotation();
         yield return new WaitForSeconds(0.75f);
      }
      
   }

}
