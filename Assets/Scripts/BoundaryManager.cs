using UnityEngine;
using System.Collections;

public class BoundaryManager : MonoBehaviour
{
    public float xMin;
    public float xMax;
    public float yMin;
    public float yMax;

    static bool isAlive = true;

    private void Start()
    {
        // Set the boundary coordinates based on the camera's view
        SetBoundary();
        StartCoroutine(CheckForResChange());
    }


    IEnumerator CheckForResChange()
    {

        Vector2 resolution = new Vector2(Screen.width, Screen.height);

        while(isAlive)
        {
            
            if(Screen.width != resolution.x || Screen.height != resolution.y)
            {
                resolution = new Vector2(Screen.width, Screen.height);
                SetBoundary();
            }

            yield return new WaitForSeconds(1);
        }
    }

    private void SetBoundary()
    {
        Camera camera = GetComponent<Camera>();
        Vector3 bottomLeft = camera.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector3 topRight = camera.ViewportToWorldPoint(new Vector3(1, 1, 0));

        xMin = bottomLeft.x;
        xMax = topRight.x;
        yMin = bottomLeft.y;
        yMax = topRight.y;
    }
}