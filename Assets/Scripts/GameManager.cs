
using UnityEngine;

public class GameManager : MonoBehaviour
{
     SpawnerManager spawnerManager;
     BoardManager boardManager;
     private ShapeManager currentShape;
     
     [Header("Counters")]
     [Range(0.01f,1f)]
     [SerializeField] private float movingDownTime = 0.5f;
     private float movingDownCounter;
     [Range(0.01f,1f)]
     [SerializeField] private float rightLeftClickTime = 0.25f;
     private float rightLeftClickCounter;
     [Range(0.01f,1f)]
     [SerializeField] private float rightLeftTurnTime = 0.25f;
     private float rightLeftTurnCounter;
     [Range(0.01f,1f)]
     [SerializeField] private float pressingDownButtonTime = 0.25f;
     private float pressingDownButtonCounter;
    void Start()
    {
        spawnerManager = GameObject.FindObjectOfType<SpawnerManager>();
        boardManager = GameObject.FindObjectOfType<BoardManager>();

        if (spawnerManager)
        {
            if (currentShape==null)
            {
                currentShape = spawnerManager.createShape();
                currentShape.transform.position = makeInteger(currentShape.transform.position);
            }
        }
    }

    private void Update()
    {
        if (!boardManager || !spawnerManager || !currentShape)
        {
            return;
        }

        enterControl();
    }

    private void enterControl()
    {
        if (Input.GetKeyDown("right") || (Input.GetKey("right") && Time.time > rightLeftClickCounter))
        {
            currentShape.rightMovement();
            rightLeftClickCounter = Time.time + rightLeftClickTime;
            if (!boardManager.validPosition(currentShape))
            {
                currentShape.leftMovement();
            }
            
        }else if (Input.GetKeyDown("left") || (Input.GetKey("left") && Time.time > rightLeftClickCounter))
        {
            currentShape.leftMovement();
            rightLeftClickCounter = Time.time + rightLeftClickTime;
            if (!boardManager.validPosition(currentShape))
            {
                currentShape.rightMovement();
            }
            
        }else if (Input.GetKey("up") && Time.time > rightLeftTurnCounter)
        {
            currentShape.rightRotation();
            rightLeftTurnCounter = Time.time + rightLeftTurnTime;
            if (!boardManager.validPosition(currentShape))
            {
                currentShape.rightMovement();
            }
            
        }else if (Input.GetKey("down") && Time.time > pressingDownButtonCounter || Time.time>movingDownCounter)
        {
            movingDownCounter = Time.time + movingDownTime;
            pressingDownButtonCounter = Time.time + pressingDownButtonTime; 
            if (currentShape)
            {
                currentShape.downMovement();

                if (!boardManager.validPosition(currentShape))
                {
                    locateShape();
                }
            }
            
        }
        
    }

    private void locateShape()
    {
        // update the times for new shape 
        rightLeftClickCounter = Time.time;
        pressingDownButtonCounter=Time.time;
        rightLeftTurnCounter = Time.time;
        
        currentShape.upMovement();
        boardManager.takeShapeInsideGrid(currentShape);
        if (spawnerManager)
        {
            currentShape = spawnerManager.createShape();
        }
        boardManager.deleleAllRows();
    }

    //making value integer if it is float value
    Vector2 makeInteger(Vector2 vector)
    {
        return new Vector2(Mathf.Round(vector.x), Mathf.Round(vector.y));
    }
    
}
