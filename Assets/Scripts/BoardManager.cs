
using Unity.Mathematics;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    [SerializeField] private Transform tilePrefab;

    public float high = 9f;
    public float wide = 4f;
    
    void Start()
    {
        CreateEmptySquare();
    }
    void CreateEmptySquare()
    {
        
        for (float y = 0; y < high; y = y+0.5f)
        {
            for (float x = 0; x < wide; x = x +0.5f)
            { 
                Transform tile = Instantiate(tilePrefab, new Vector3(x, y, 0), quaternion.identity);
                tile.name = "x " + x.ToString() + " ," + " y" + y.ToString();
                tile.parent = this.transform;                                                            
        
        
            }
        }
        
        
    }

   
    
}
