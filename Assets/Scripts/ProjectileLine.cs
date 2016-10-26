using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ProjectileLine : MonoBehaviour
{
    public static ProjectileLine S;

    public float minDist = .1f;
    public bool _________________________;

    public LineRenderer line;
    private GameObject _pointOfInterest;
    public List<Vector3> points;

    void Awake()
    {
        S = this;
        line = GetComponent<LineRenderer>();
        line.enabled = false;
        points = new List<Vector3>();
    }

    public GameObject PointOfInterest
    {
        get
        {
            return _pointOfInterest;
        }
        set
        {
            _pointOfInterest = value;
            if (_pointOfInterest != null)
            {
                line.enabled = false;
                points = new List<Vector3>();
                AddPoint();
            }
        }
    }

    public void Clear()
    {
        _pointOfInterest = null;
        line.enabled = false;
        points = new List<Vector3>();
    }

    public void AddPoint()
    {
        var point = _pointOfInterest.transform.position;

        if (points.Count > 0 && (point - lastPoint).magnitude < minDist)
        {
            return;
        }

        if (points.Count == 0)
        {
            var launchPosition = Slingshot.S.launchPoint.transform.position;
            var launchPosDiff = point - launchPosition;

            points.Add(point + launchPosDiff);
            points.Add(point);
            line.SetVertexCount(2);

            line.SetPosition(0, points[0]);
            line.SetPosition(1, points[1]);

            line.enabled = true;
        }
        else
        {
            points.Add(point);
            line.SetVertexCount(points.Count);
            line.SetPosition(points.Count - 1, lastPoint);
            line.enabled = true;
        }
    }

    public Vector3 lastPoint
    {
        get
        {
            if (points == null)
            {
                return Vector3.zero;
            }
            return points[points.Count - 1];
        }
    }

    void FixedUpdate()
    {
        if (PointOfInterest == null)
        {
            if (FollowCam.S.pointOfInterest != null)
            {
                if (FollowCam.S.pointOfInterest.tag == "Projectile")
                {
                    PointOfInterest = FollowCam.S.pointOfInterest;
                }
                else
                {
                    return;
                }
            }
            else
            {
                return;
            }
        }

        AddPoint();
        if (PointOfInterest.GetComponent<Rigidbody>().IsSleeping())
        {
            PointOfInterest = null;
        }
    }
}
