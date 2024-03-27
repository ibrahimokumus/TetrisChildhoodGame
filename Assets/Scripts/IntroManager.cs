using System;
using System.Collections;
using UnityEngine;
using DG.Tweening;
public class IntroManager : MonoBehaviour
{

    public GameObject[] numbers;
    public GameObject numberTransform;
    public GameManager gameManager;

    private void Awake()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        
    }

    private void Start()
    {
        StartCoroutine(showNumberScene());
    }

    IEnumerator showNumberScene()
    {
        yield return new WaitForSeconds(0.1f);
        numberTransform.GetComponent<RectTransform>().DORotate(Vector3.zero, 0.3f).SetEase(Ease.OutBack);
        numberTransform.GetComponent<CanvasGroup>().DOFade(1, 0.3f);
        yield return new WaitForSeconds(0.2f);
        
         int counter = 0;

         while (counter<numbers.Length)
         {
             numbers[counter].GetComponent<RectTransform>().DOLocalMoveY(0, 0.5f); // for moving up
             numbers[counter].GetComponent<CanvasGroup>().DOFade(1, 0.5f);// for showing
             numbers[counter].GetComponent<RectTransform>().DOScale(2f, 0.3f).SetEase(Ease.OutBounce).OnComplete(()=>
                 numbers[counter].GetComponent<RectTransform>().DOScale(1f,0.3f).SetEase(Ease.InBack));
             yield return new WaitForSeconds(1.5f);
             
             counter++;
             numbers[counter-1].GetComponent<RectTransform>().DOLocalMoveY(150f, 0.5f);
             numbers[counter-1].GetComponent<CanvasGroup>().DOFade(0, 0.5f);
             yield return new WaitForSeconds(0.1f);
         }
            //after finishing counting back, disappear the image background
         numberTransform.GetComponent<CanvasGroup>().DOFade(0, 0.5f).OnComplete(() =>
             {
                 //after disappearing the image background, set false the panel then start the game
                 numberTransform.transform.parent.gameObject.SetActive(false);
                 gameManager.startGame();
             }
             );
    }

    


}
