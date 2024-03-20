
using UnityEngine;
using UnityEngine.UI;
public class IconTurnOnOff : MonoBehaviour
{
   public Sprite enableIcon;
   public Sprite disableIcon;
   private Image iconImage;

   public bool defaulState = true;

   private void Start()
   {
      iconImage = GetComponent<Image>();
      // single line if statement. if defailState is true, assign enableIcon,otherwise assign disableIcon
      iconImage.sprite = defaulState ? enableIcon : disableIcon;
      
   }

   public void chooseIcon(bool iconState)
   {
      if (!iconImage || !enableIcon || !disableIcon)
      {
         return;
      }
      iconImage.sprite = iconState ? enableIcon : disableIcon;
   }
}
