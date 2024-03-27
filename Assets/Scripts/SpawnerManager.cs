using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;


public class SpawnerManager : MonoBehaviour
{
    [SerializeField] ShapeManager[] allShapes;
    [SerializeField] private Image[] shapeImages = new Image[2];//shown 2 images on screen 
    private ShapeManager[] orderShapes = new ShapeManager[2];

    private void Awake()
    {
      //  makeNullAll();
    }

    public ShapeManager createShape()
    {
        // we choose a shape from allShapes array randomly
       /// int index = Random.Range(0, allShapes.Length);
        //ShapeManager shape = Instantiate(allShapes[index],transform.position,Quaternion.identity) as ShapeManager;
        ShapeManager shape = null;
        shape = takeCurrentOrder();
        shape.gameObject.SetActive(true);
        shape.transform.position = transform.position;
        
        
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

    IEnumerator showShapeImage()
    {
        for (int i = 0; i < shapeImages.Length; i++)
        {
            shapeImages[i].GetComponent<CanvasGroup>().alpha = 0f;
            shapeImages[i].GetComponent<RectTransform>().localScale = Vector3.zero;
        }

        yield return new WaitForSeconds(0.1f);
        int counter=0;
        while (counter<shapeImages.Length)
        {
            shapeImages[counter].GetComponent<CanvasGroup>().DOFade(1,0.5f);
            shapeImages[counter].GetComponent<RectTransform>().DOScale(1, 0.5f).SetEase(Ease.OutBack);
            counter++;
            yield return new WaitForSeconds(0.1f);
        }
        
    }

    ShapeManager randomCreateShape()
    {
        int randomShape = Random.Range(0, allShapes.Length);
        if (allShapes[randomShape])
        {
            return allShapes[randomShape];
        }
        else
        {
            return null;
        }
    }

   public void makeNullAll()
    {
        for (int i = 0; i < orderShapes.Length; i++)
        {
            orderShapes[i] = null;
        }
        fillOrder();
    }

    void fillOrder()
    {
        for (int i = 0; i < orderShapes.Length; i++)
        {
            if (!orderShapes[i])
            {
                orderShapes[i] = Instantiate(randomCreateShape(),transform.position,Quaternion.identity) as ShapeManager;
                orderShapes[i].gameObject.SetActive(false);
                shapeImages[i].sprite = orderShapes[i].shapeSprite;
            }
        }

        StartCoroutine(showShapeImage());
    }

    ShapeManager takeCurrentOrder()
    {
        ShapeManager nextShape = null;
        if (orderShapes[0])
        {
            nextShape = orderShapes[0];
        }

        for (int i = 1; i < orderShapes.Length; i++)
        {
            orderShapes[i - 1] = orderShapes[i];
            shapeImages[i - 1].sprite = orderShapes[i-1].shapeSprite;
        }

        orderShapes[orderShapes.Length - 1] = null;
        fillOrder();
        return nextShape;
    }
}
