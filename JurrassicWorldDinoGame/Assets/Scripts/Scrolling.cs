//using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class Scrolling : MonoBehaviour
{
    [SerializeField] private RawImage img;
    [SerializeField] private float x, y;
    private bool isScrolling;
    public float scrollMax;
    private Vector2 defaultPos;

    //private Camera cam;
    //private RectTransform rectTransform;
    //private Vector2 size;
    //private Vector2 zero;


    private void Start()
    {
        isScrolling = true;
        defaultPos = img.uvRect.position;
        //cam = Camera.main;
        //rectTransform = GetComponent<RectTransform>();
        //zero = Vector2.zero;
    }


    void Update()
    {
        
        //rectTransform.sizeDelta = new Vector2();

        /*All the commented code is to do with scaling the background up and down
          To make it match the size of the viewport
          Main difficulty was efficiently acquiring the size of the viewport each update and checking if it has changed
          Also this may not be needed if we were to use a fixed size window in the browser and not care about mobile*/
        if(isScrolling)
        {
            //If is scrolling, adjust the image by (x,y) * time passed.
            //X is set to 0.01 in unity, Y is set to 0 as we dont want vertical scrolling
            if (img.uvRect.x > scrollMax)
            {
                img.uvRect = new Rect(new Vector2(defaultPos.x - scrollMax, defaultPos.y) , img.uvRect.size);
            }
            else
            {
                img.uvRect = new Rect(img.uvRect.position + new Vector2(x, y) * Time.deltaTime, img.uvRect.size);
            }
        }

    }


    //Two public interface methods for toggling the scrolling
    public void StartScrolling()
    {
        isScrolling = true;

    }
    public void StopScrolling()
    {
        isScrolling = false;
    }
}
