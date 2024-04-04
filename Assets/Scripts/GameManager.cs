using System.Collections;
using DG.Tweening;
using UnityEngine;

public class GameManager : MonoBehaviour
{
     SpawnerManager spawnerManager;
     BoardManager boardManager;
     private ShapeManager currentShape;
     
     [Header("Counters")]
     [Range(0.01f,1f)]
     [SerializeField] private float movingDownTime = 0.5f;
     private float movingDownLevelCounter;
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

    
     public bool isGameOver = false;

     public bool isRightDirection = true;
     public IconTurnOnOff rotateIcon;

     public GameObject gameOverPanel;
     private ScoreManager scoreManager;
     public ParticleEffectManager[] levelUpEffects = new ParticleEffectManager[5];
     
     enum Direction{none,left,right,up,down}
     private Direction suruklemeYonu = Direction.none;
     private Direction suruklemeBitisYonu = Direction.none;

     private float sonrakiDokunmaZamani;
     private float sonrakiSuruklemeZamani;
     [Range(0.05f, 1f)] public float minDokunmaZamani = 0.15f;
     [Range(0.05f, 1f)] public float minSuruklemeZamani = 0.3f;

     private bool dokunduMu = false;
     private void Awake()
     {
         spawnerManager = GameObject.FindObjectOfType<SpawnerManager>();
         boardManager = GameObject.FindObjectOfType<BoardManager>();
         scoreManager = GameObject.FindObjectOfType<ScoreManager>();
     }

     private void OnEnable()
     {
         TouchManager.dragEvent += surukle;
         TouchManager.swipeEvent += suruklemeBitti;
         TouchManager.topEvent += tap;
     }

     private void OnDisable()
     {
         TouchManager.dragEvent -= surukle;
         TouchManager.swipeEvent -= suruklemeBitti;
         TouchManager.topEvent -= tap;
     }
     
     public void startGame()
    {
        

        if (spawnerManager)
        {
            spawnerManager.makeNullAll();
            if (currentShape==null)
            {
                currentShape = spawnerManager.createShape();
                currentShape.transform.position = makeInteger(currentShape.transform.position);
            }
        }

        if (gameOverPanel)
        {
            gameOverPanel.SetActive(false);
        }

        movingDownLevelCounter = movingDownTime;
    }

    private void Update()
    {
        if (!boardManager || !spawnerManager || !currentShape || isGameOver || !scoreManager)
        {
            return;
        }

        enterControl();
    }

    private void enterControl()
    {//pc controll inputs
        if (Input.GetKeyDown("right") || (Input.GetKey("right") && Time.time > rightLeftClickCounter))
        {
            righMovementFNC();
        }else if (Input.GetKeyDown("left") || (Input.GetKey("left") && Time.time > rightLeftClickCounter))
        {
            leftMovementFNC();
        }else if (Input.GetKey("up") && Time.time > rightLeftTurnCounter)
        {
            turnRightLeftFNC();
        }else if ((Input.GetKey("down") && Time.time > pressingDownButtonCounter) || Time.time>movingDownCounter)
        {
            downFasterFNC();
        }//android controll inputs
        else if ((suruklemeBitisYonu==Direction.right && Time.time>sonrakiSuruklemeZamani)||(suruklemeYonu==Direction.right && Time.time>sonrakiDokunmaZamani))
        {
            righMovementFNC();
            sonrakiDokunmaZamani = Time.time + minDokunmaZamani;
            sonrakiSuruklemeZamani = Time.time + minSuruklemeZamani;
            
            //suruklemeYonu = Direction.none;
            //suruklemeBitisYonu = Direction.none;
        }else if ((suruklemeBitisYonu==Direction.left && Time.time>sonrakiSuruklemeZamani)||(suruklemeYonu==Direction.left&&Time.time>sonrakiDokunmaZamani))
        {
            leftMovementFNC();
           // suruklemeYonu = Direction.none;
           // suruklemeBitisYonu = Direction.none;
        }else if ((suruklemeBitisYonu == Direction.up && Time.time > sonrakiSuruklemeZamani)||(dokunduMu))
        {
            turnRightLeftFNC();
            sonrakiSuruklemeZamani = Time.time + minSuruklemeZamani;
            //suruklemeBitisYonu = Direction.none;
        }else if (suruklemeYonu==Direction.down&& Time.time>sonrakiDokunmaZamani)
        {
            downFasterFNC();
           // suruklemeYonu = Direction.none;
        }

        suruklemeYonu = Direction.none;
        suruklemeBitisYonu = Direction.none;
        dokunduMu = false;
    }

    private void downFasterFNC()
    {
        movingDownCounter = Time.time + movingDownLevelCounter;
        pressingDownButtonCounter = Time.time + pressingDownButtonTime;
        if (currentShape)
        {
            currentShape.downMovement();

            if (!boardManager.validPosition(currentShape))
            {
                if (boardManager.outFromScene(currentShape))
                {
                    currentShape.upMovement();
                    isGameOver = true;
                    if (gameOverPanel)
                    {
                        gameOverPanel.transform.localScale = Vector3.zero;
                        gameOverPanel.SetActive(true);
                        gameOverPanel.transform.DOScale(1, 0.5f).SetEase(Ease.OutBack);
                    }

                    SoundManager.instance.makeSoundEffect(5);
                }
                else
                {
                    locateShape();
                }
            }
        }
    }

    private void turnRightLeftFNC()
    {
        currentShape.rightRotation();
        rightLeftTurnCounter = Time.time + rightLeftTurnTime;
        if (!boardManager.validPosition(currentShape))
        {
            SoundManager.instance.makeSoundEffect(1);
            currentShape.leftMovement();
        }
        else
        {
            isRightDirection = !isRightDirection;
            if (rotateIcon)
            {
                rotateIcon.chooseIcon(isRightDirection);
            }

            SoundManager.instance.makeSoundEffect(2);
        }
    }

    private void leftMovementFNC()
    {
        currentShape.leftMovement();
        rightLeftClickCounter = Time.time + rightLeftClickTime;
        if (!boardManager.validPosition(currentShape))
        {
            SoundManager.instance.makeSoundEffect(1);
            currentShape.rightMovement();
        }
        else
        {
            SoundManager.instance.makeSoundEffect(2);
        }
    }

    private void righMovementFNC()
    {
        currentShape.rightMovement();
        rightLeftClickCounter = Time.time + rightLeftClickTime;
        if (!boardManager.validPosition(currentShape))
        {
            SoundManager.instance.makeSoundEffect(1);
            currentShape.leftMovement();
        }
        else
        {
            SoundManager.instance.makeSoundEffect(2);
        }
    }

    private void locateShape()
    {
        if (currentShape)
        {
            // update the times for new shape 
            rightLeftClickCounter = Time.time;
            pressingDownButtonCounter=Time.time;
            rightLeftTurnCounter = Time.time;
        
            currentShape.upMovement();
            currentShape.makeLocateEffect();
            boardManager.takeShapeInsideGrid(currentShape);
            SoundManager.instance.makeSoundEffect(4);
            if (spawnerManager)
            {
                currentShape = spawnerManager.createShape();
            }
            StartCoroutine(boardManager.deleleAllRows());
            if (boardManager.completedLineCounter > 0)
            {
                scoreManager.rowScore(boardManager.completedLineCounter);
                if (scoreManager.isPassedLevel)
                {
                    SoundManager.instance.makeSoundEffect(6);
                    //decreasing movingDownTime depends on level. 
                    movingDownLevelCounter = movingDownTime - Mathf.Clamp(((float)scoreManager.level - 1) * 0.1f, 0.05f, 1f);
                    StartCoroutine(levelUp());
                }
                else
                {
                    if (boardManager.completedLineCounter > 1)
                    {
                        SoundManager.instance.makeVocalSound();
                    }
                }
            
                SoundManager.instance.makeSoundEffect(4);
            }
        }
        
    }

    public void rotateIconDirection()
    {
        isRightDirection = !isRightDirection;
        currentShape.rotateRightDirection(isRightDirection);

        if (!boardManager.validPosition(currentShape))
        {
            currentShape.rotateRightDirection(!isRightDirection);
            SoundManager.instance.makeSoundEffect(1);
        }
        else
        {
            if (rotateIcon)
            {
                rotateIcon.chooseIcon(isRightDirection);
            }
            SoundManager.instance.makeSoundEffect(2);
        }
    }
    //making value integer if it is float value
    Vector2 makeInteger(Vector2 vector)
    {
        return new Vector2(Mathf.Round(vector.x), Mathf.Round(vector.y));
    }

    IEnumerator levelUp()
    {
        yield return new WaitForSeconds(0.2f);
        int counter = 0;
        while (counter<levelUpEffects.Length)
        {
            levelUpEffects[counter].playEffect();
            yield return new WaitForSeconds(0.1f);
            counter++;
        }
    }

    void surukle(Vector2 suruklemeHareketi)
    {
        suruklemeYonu = detectDirection(suruklemeHareketi);
    }

    private void suruklemeBitti(Vector2 hareket)
    {
        suruklemeBitisYonu = detectDirection(hareket);
    }

    Direction detectDirection(Vector2 surukleme)
    {
        Direction suruklemeYonu = Direction.none;
        if (Mathf.Abs(surukleme.x)>Mathf.Abs(surukleme.y))
        {
            suruklemeYonu = (surukleme.x >= 0) ? Direction.right : Direction.left;
        }
        else
        {
            suruklemeYonu = (surukleme.y > 0) ? Direction.up : Direction.down;
        }

        return suruklemeYonu;
    }

    void tap(Vector2 surukleme)
    {
        dokunduMu = true;
    }
}
