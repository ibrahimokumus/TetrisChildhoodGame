
using System;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
   private int score = 0;
   private int rows;
   public int level = 1;

   public int numberOfRows = 5;
   private int minRow = 1;
   private int maxRow = 4;
   public TextMeshProUGUI rowTxt;
   public TextMeshProUGUI scoreTxt;
   public TextMeshProUGUI levelTxt;

   public bool isPassedLevel = false;
   private void Start()
   {
      reset();
   }

   public void rowScore(int row)
   {
      isPassedLevel = false;
      row = Mathf.Clamp(row, minRow, maxRow);// row will be between minRow and maxRow
      switch (row)
      {
         case 1:
            score += 5 * level;
            break;
         case 2:
            score += 10 * level;
            break;
         case 3:
            score += 20 * level;
            break;
         case 4:
            score += 40 * level;
            break;
      }

      rows -= row;
      if (rows<=0)
      {
         increaseLevel();
      }
      updateTxt();
   }

   void updateTxt()
   {
      
      if (scoreTxt)
      {
         scoreTxt.text = score.ToString();
      }

      if (levelTxt)
      {
         levelTxt.text = level.ToString();
      }

      if (rowTxt)
      {
         rowTxt.text = rows.ToString();
      }
   }

   public void increaseLevel()
   {
      level++;
      rows = numberOfRows * level;
      isPassedLevel = true;
   }
   public void reset()
   {
      level = 1;
      rows = numberOfRows * level;
      updateTxt();
   }
   
}
