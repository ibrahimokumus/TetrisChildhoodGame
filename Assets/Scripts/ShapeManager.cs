using UnityEngine;

public class ShapeManager : MonoBehaviour
{
   [SerializeField] private bool isRotatable = true;
   public Sprite shapeSprite;
   GameObject[] locateEffects;

  private void Start()
  {
     locateEffects = GameObject.FindGameObjectsWithTag("LocateEffect");
  }

  public void makeLocateEffect()
  {
     int counter = 0;
     foreach (Transform child in gameObject.transform)
     {
        if (locateEffects[counter])
        {
           locateEffects[counter].transform.position =
              new Vector3(child.position.x, child.position.y, 0f);
           ParticleEffectManager particleEffectManager = locateEffects[counter].GetComponent<ParticleEffectManager>();
            if(particleEffectManager)
            {
               particleEffectManager.playEffect();
            }
        }
        counter++;
     }
  }
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


   public void rotateRightDirection(bool isRightDirection)
   {
      if (isRightDirection)
      {
         rightRotation();
      }
      else
      {
         leftRotation();
      }
   }
   
  

}
