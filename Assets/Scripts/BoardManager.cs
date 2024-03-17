
using Unity.Mathematics;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    [SerializeField] private Transform tilePrefab;

    public int high =22;
    public int wide = 10;

    private Transform[,] grid;

    private void Awake()
    {
        grid = new Transform[wide, high];
    }

    void Start()
    {
        createEmptySquare();
    }

    bool insideBoard(int x, int y)
    {
        return (x>=0 && x<wide && y>=0);
    }

    public bool validPosition(ShapeManager shape)
    {
        foreach (Transform child in shape.transform)
        {
            Vector2 position = makeInteger(child.position);
            if (!insideBoard((int)position.x,(int)position.y))
            {
                return false;
            }
          
            if (position.y<high)
            {
                if (isSquareFull((int)position.x, (int)position.y, shape))
                {
                    return false;
                }
            }
            
        }

        return true;
    }

    bool isSquareFull(int x, int y, ShapeManager shapeManager)
    {
        return (grid[x, y] != null && grid[x, y].parent != shapeManager.transform);
    }
    void createEmptySquare()
    {
        
        for (int y = 0; y < high; y ++)
        {
            for (int x = 0; x < wide; x++)
            { 
                Transform tile = Instantiate(tilePrefab, new Vector3(x, y, 0), quaternion.identity);
                tile.name = "x " + x.ToString() + " ," + " y" + y.ToString();
                tile.parent = this.transform;                                                            

            }
        }
        
        
    }

    public void takeShapeInsideGrid(ShapeManager shapeManager)
    {
        if (shapeManager == null)
        {
            return;
        }

        foreach (Transform child in shapeManager.transform)
        {
            Vector2 position = makeInteger(child.position);
            grid[(int)position.x, (int)position.y] = child;
        }
    }
    
    //making value integer if it is float value
    Vector2 makeInteger(Vector2 vector)
    {
        return new Vector2(Mathf.Round(vector.x), Mathf.Round(vector.y));
    }
   
    
}