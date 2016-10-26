using UnityEngine;
using System.Collections;

public class Slingshot : MonoBehaviour
{
    public static Slingshot S;
    public GameObject prefabProjectile;
    public float velocityMult = 4f;
    public bool _________________________;

    public GameObject launchPoint;
    public Vector3 launchPosition;
    public GameObject projectile;
    public bool aimingMode;

    void Awake()
    {
        S = this;
        Transform launchPointTrans = transform.Find("LaunchPoint");
        launchPoint = launchPointTrans.gameObject;
        launchPoint.SetActive(false);
        launchPosition = launchPointTrans.position;
    }

    void Update()
    {
        if (!aimingMode)
        {
            return;
        }

        Vector3 mousePosition2D = Input.mousePosition;
        mousePosition2D.z = -Camera.main.transform.position.z;
        Vector3 mousePosition3D = Camera.main.ScreenToWorldPoint(mousePosition2D);

        Vector3 mouseDelta = mousePosition3D - launchPosition;
        float maxMagnitude = this.GetComponent<SphereCollider>().radius;

        if (mouseDelta.magnitude > maxMagnitude)
        {
            mouseDelta.Normalize();
            mouseDelta *= maxMagnitude;
        }

        Vector3 projectilePosition = launchPosition + mouseDelta;
        projectile.transform.position = projectilePosition;

        if (Input.GetMouseButtonUp(0))
        {
            aimingMode = false;

            var rigidbody = projectile.GetComponent<Rigidbody>();
            rigidbody.isKinematic = false;
            rigidbody.velocity = -mouseDelta*velocityMult;

            FollowCam.S.pointOfInterest = projectile;
            projectile = null;

            MissionDemolition.ShotFired();
        }
    }

    void OnMouseEnter()
    {
        launchPoint.SetActive(true);
    }

    void OnMouseExit()
    {
        launchPoint.SetActive(false);
    }

    void OnMouseDown()
    {
        aimingMode = true;
        projectile = Instantiate(prefabProjectile);
        projectile.transform.position = launchPosition;
        projectile.GetComponent<Rigidbody>().isKinematic = true;
    }
}
