
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class SpinningObstacle : MonoBehaviour
{
    [SerializeField]
    private Vector3 spinSpeed = Vector3.zero;

    [SerializeField]
    private Transform spinningObject;

    struct TrackerData
    {
        public Vector3 previousPosition;
        public Vector3 velocity;
    }
    private List<TrackerData> trackerData = new List<TrackerData>();

    private void Start()
    {
        int childCount = transform.childCount;
        for (int i = 0; i < childCount; ++i)
        {
            TrackerData data = new TrackerData();
            data.previousPosition = transform.GetChild(i).transform.position;
            data.velocity = Vector3.zero;
            trackerData.Add(data);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 spinAmount = spinSpeed * Time.deltaTime;
        spinningObject.Rotate(spinAmount.x, spinAmount.y, spinAmount.z);

        int childCount = transform.childCount;
        for (int i = 0; i < childCount; ++i)
        {
            TrackerData data = trackerData[i];
            data.velocity = (transform.GetChild(i).transform.position - data.previousPosition) / Time.deltaTime;
            data.previousPosition = transform.GetChild(i).transform.position;
            trackerData[i] = data;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Vector3 playerPos = collision.transform.position;
            int closestTracker = -1;
            float closestDistanceSq = float.MaxValue;
            int childCount = transform.childCount;
            for (int i = 0; i < childCount; ++i)
            {
                Vector3 trackerPos = transform.GetChild(i).transform.position;
                float distSq = Vector3.SqrMagnitude(playerPos - trackerPos);
                if (distSq < closestDistanceSq)
                {
                    closestDistanceSq = distSq;
                    closestTracker = i;
                }
            }
            if (closestTracker >= 0)
            {
                TrackerData data = trackerData[closestTracker];
                collision.rigidbody.AddForce(data.velocity * 500.0f);
            }
        }
    }
}
