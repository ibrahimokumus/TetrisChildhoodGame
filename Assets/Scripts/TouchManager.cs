using UnityEngine;

public class TouchManager : MonoBehaviour
{
   public delegate void TouchEventDelegate(Vector2 swipePos);

   public static event TouchEventDelegate dragEvent;
   public static event TouchEventDelegate swipeEvent;
   public static event TouchEventDelegate topEvent;
   private Vector2 touchMovement;
   [Range(50,250)]
   public int minDragging = 100;

   [Range(50, 250)] 
   public int minDragDistance = 50;
   
   
   public bool taniKullanilsinMi = false;
   private float onClickedMaxTime = 0f;
   public float clickOnScreenTime = 0.1f;
   
  

   private void Update()
   {
      if (Input.touchCount>0)
      {
         Touch touch = Input.touches[0];
         if (touch.phase == TouchPhase.Began)
         {
            touchMovement = Vector2.zero;
            onClickedMaxTime = Time.time + clickOnScreenTime;
         }
         else if(touch.phase==TouchPhase.Moved || touch.phase==TouchPhase.Stationary)
         {
            touchMovement += touch.deltaPosition;
            if (touchMovement.magnitude>minDragDistance)
            {
               dragging();
            }
         }else if (touch.phase== TouchPhase.Ended)
         {
            if (touchMovement.magnitude>minDragDistance)
            {
               suruklemeBitti();
            }else if (Time.time<onClickedMaxTime)
            {
               onClicked();
            }
         }
      }
   }

   void dragging()
   {
      if (dragEvent!=null)
      {
         dragEvent(touchMovement);
      }
   }
   

   void suruklemeBitti()
   {
      if (swipeEvent!=null)
      {
         swipeEvent(touchMovement);
      }
   }

   string suruklemeTani(Vector2 suruklemeHareketi)
   {
      string direction = "";
      if (Mathf.Abs(suruklemeHareketi.x)>Mathf.Abs(suruklemeHareketi.y))
      {
         direction = (suruklemeHareketi.x >= 0) ? "sag" : "sol";//single line "if" statement
      }
      else
      {
         direction = (suruklemeHareketi.y >= 0) ? "yukari" : "asagi";
      }

      return direction;
   }

   void onClicked()
   {
      if (topEvent != null)
      {
         topEvent(touchMovement);
      }
   }
}
