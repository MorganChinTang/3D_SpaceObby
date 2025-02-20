using System.Collections;
using UnityEngine;

public class BreakingObstacle : MonoBehaviour
{
    public float fallTime = 0.5f;
    public float comebackTime = 2f;

    private MeshRenderer meshRenderer;
    private Collider collider;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        collider = GetComponent<Collider>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(FallAndComeBack(fallTime, comebackTime));
        }
    }

    IEnumerator FallAndComeBack(float fallDelay, float returnDelay)
    {
        yield return new WaitForSeconds(fallDelay);

        meshRenderer.enabled = false;
        collider.enabled = false;

        yield return new WaitForSeconds(returnDelay);

        meshRenderer.enabled = true;
        collider.enabled = true;
    }
}