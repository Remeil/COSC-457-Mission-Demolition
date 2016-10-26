using UnityEngine;
using System.Collections;

public class FollowCam : MonoBehaviour
{
    static public FollowCam S;
    public Vector2 minXY;
    public float easing = 0.05f;

    public bool __________________;

    public GameObject pointOfInterest;
    public float cameraZPosition;

    void Awake()
    {
        S = this;
        cameraZPosition = this.transform.position.z;
    }

    void FixedUpdate()
    {
        Vector3 destination;
        if (pointOfInterest == null)
        {
            destination = Vector3.zero;
            //return;
        }
        else
        {
            destination = pointOfInterest.transform.position;

            if (pointOfInterest.tag == "Projectile")
            {
                if (pointOfInterest.GetComponent<Rigidbody>().IsSleeping())
                {
                    pointOfInterest = null;
                    return;
                }
            }
        }

        //destination = pointOfInterest.transform.position;
        destination.x = Mathf.Max(minXY.x, destination.x);
        destination.y = Mathf.Max(minXY.y, destination.y);
        destination = Vector3.Lerp(transform.position, destination, easing);

        destination.z = cameraZPosition;
        transform.position = destination;

        this.GetComponent<Camera>().orthographicSize = destination.y + 10;
    }
}
