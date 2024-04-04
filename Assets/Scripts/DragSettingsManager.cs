using UnityEngine;
using UnityEngine.UI;

public class DragSettingsManager : MonoBehaviour
{
    private GameManager gameManager;
    private TouchManager touchManager;

    public Slider dokunmaSlider;
    public Slider suruklemeSlider;
    public Slider dokunmaHizSlider;
    
    void Start()
    {
        touchManager = GameObject.FindObjectOfType<TouchManager>();
        gameManager = GameObject.FindObjectOfType<GameManager>();

        if (dokunmaSlider!=null)
        {
            dokunmaSlider.value = 100;
            dokunmaSlider.minValue = 50;
            dokunmaSlider.maxValue = 150;
        }
        if (suruklemeSlider!=null)
        {
            suruklemeSlider.value = 50;
            suruklemeSlider.minValue = 20;
            suruklemeSlider.maxValue = 250;
        }
        if (dokunmaHizSlider!=null)
        {
            dokunmaHizSlider.value = 0.15f;
            dokunmaHizSlider.minValue = 0.05f;
            dokunmaHizSlider.maxValue = 0.5f;
        }
    }

    public void updateSettingsPanel()
    {
        if (dokunmaSlider !=null && touchManager!=null)
        {
            touchManager.minDragging = (int)dokunmaSlider.value;
        }

        if (suruklemeSlider != null && touchManager != null)
        {
            touchManager.minDragDistance = (int)suruklemeSlider.value;
        }

        if (dokunmaHizSlider!=null&& gameManager!=null)
        {
            gameManager.minDokunmaZamani = dokunmaHizSlider.value;
        }
    }
}
