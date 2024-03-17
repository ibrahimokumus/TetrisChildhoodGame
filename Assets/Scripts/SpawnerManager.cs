using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    [SerializeField] ShapeManager[] allShapes;
    
    
    
    public ShapeManager createShape()
    {
        // we choose a shape from allShapes array randomly
        int index = Random.Range(0, allShapes.Length);
        ShapeManager shape = Instantiate(allShapes[index],transform.position,Quaternion.identity) as ShapeManager;
       
        
        if (shape)
        {
            return shape;
        }
        else
        {
            Debug.Log(" not found Shape");
            return null;
        }
        
        
       
    }
   
}
