using System.Collections;
using DG.Tweening;
using UnityEngine;

public class ScreenFadeManager : MonoBehaviour
{
   public float initializeAlpha = 1f;
   public float finishAlpha = 0f;

   public float waitingTime = 0f;
   public float fadeTime = 1f;

   private void Start()
   {
      GetComponent<CanvasGroup>().alpha = initializeAlpha;
      StartCoroutine(fadeRoutine());
   }

   IEnumerator fadeRoutine()
   {
      yield return new WaitForSeconds(waitingTime);
      GetComponent<CanvasGroup>().DOFade(finishAlpha, fadeTime);
   }
}
