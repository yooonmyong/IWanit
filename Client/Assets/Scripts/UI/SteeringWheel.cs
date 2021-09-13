using UnityEngine;
using System.Collections;

public class SteeringWheel : MonoBehaviour
{
    public RectTransform[] parentings = new RectTransform[7];
    private Vector3 previousPosition = Vector3.zero;
    private Vector3 positionDelta = Vector3.zero;
    private Vector3 clickUpPosition = Vector3.zero;
    private float rotatingSpeed = 1.0f;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            previousPosition = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            clickUpPosition = Input.mousePosition;
            if (clickUpPosition.x == previousPosition.x
                && clickUpPosition.y == previousPosition.y)
            {
                string parenting = 
                    GetSelectedParenting
                    (
                        Camera.main.ScreenToWorldPoint(clickUpPosition)
                    ).gameObject.name;
                
                Debug.Log("You choose " + parenting);
            }
        }

        if (Input.GetMouseButton(0))
        {
            positionDelta = Input.mousePosition - previousPosition;
            transform.Rotate
            (
                transform.forward,
                -Vector3.Dot(positionDelta, Camera.main.transform.right)
                    * Time.deltaTime * rotatingSpeed,
                Space.World
            );
        }
    }

    private Transform GetSelectedParenting(Vector3 clickPosition)
    {
        float minDistance = Mathf.Infinity;
        RectTransform selectedParenting = null;

        foreach (RectTransform parenting in parentings)
        {
            float distance =
                Vector3.Distance(parenting.transform.position, clickPosition);

            if (distance < minDistance)
            {
                minDistance = distance;
                selectedParenting = parenting;
            }
        }

        return selectedParenting;
    }
}