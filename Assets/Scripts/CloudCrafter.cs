using UnityEngine;
using System.Collections;

public class CloudCrafter : MonoBehaviour
{
    public int numClouds = 40;
    public GameObject[] cloudPrefabs;
    public Vector3 cloudPositionMax;
    public Vector3 cloudPositionMin;
    public float cloudScaleMin = 1f;
    public float cloudScaleMax = 5f;
    public float cloudSpeedMultiplier = .5f;

    public bool ________________________________;

    public GameObject[] cloudInstances;

    void Awake()
    {
        cloudInstances = new GameObject[numClouds];
        var anchor = GameObject.Find("CloudAnchor");
        GameObject cloud;

        for (int i = 0; i < numClouds; i++)
        {
            int prefabNumber = Random.Range(0, cloudPrefabs.Length);
            cloud = Instantiate(cloudPrefabs[prefabNumber]);

            Vector3 cloudPosition = Vector3.zero;
            cloudPosition.x = Random.Range(cloudPositionMin.x, cloudPositionMax.x);
            cloudPosition.y = Random.Range(cloudPositionMin.y, cloudPositionMax.y);

            float scaleU = Random.value;
            float scaleValue = Mathf.Lerp(cloudScaleMin, cloudScaleMax, scaleU);

            //Smaller clouds go closer to the ground
            cloudPosition.y = Mathf.Lerp(cloudPositionMin.y, cloudPosition.y, scaleU);

            //Smaller clouds are further away
            cloudPosition.z = 100 - 90 * scaleU;

            cloud.transform.position = cloudPosition;
            cloud.transform.localScale = Vector3.one * scaleValue;

            cloud.transform.parent = anchor.transform;

            cloudInstances[i] = cloud;
        }
    }

    void Update()
    {
        foreach (var cloud in cloudInstances)
        {
            float scaleValue = cloud.transform.localScale.x;
            Vector3 cloudPosition = cloud.transform.position;

            cloudPosition.x -= scaleValue * Time.deltaTime * cloudSpeedMultiplier;

            if (cloudPosition.x <= cloudPositionMin.x)
            {
                cloudPosition.x = cloudPositionMax.x;
            }

            cloud.transform.position = cloudPosition;
        }
    }
}
