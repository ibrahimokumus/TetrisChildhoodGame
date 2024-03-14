using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    public ShapeManager[] allShapes;
    public ShapeManager createdObjectForPivot;

    void Start()
    {
        createShape();
    }
    public ShapeManager createShape()
    {
        int index = Random.Range(0, allShapes.Length);
        ShapeManager shape = Instantiate(allShapes[index],transform.position,Quaternion.identity) as ShapeManager;
        createdObjectForPivot.setPivot(shape);
        
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
