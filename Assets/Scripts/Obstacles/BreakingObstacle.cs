using System.Collections;
using UnityEngine;

public class BreakingObstacle : MonoBehaviour
{
    public float fallTime = 0.5f;
    public float comebackTime = 2f; // Time before the obstacle comes back

    private MeshRenderer meshRenderer;
    private Collider collider;

    void Start()
    {
        // Get the MeshRenderer and Collider components at the start
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
        // First, wait for the fallTime delay
        yield return new WaitForSeconds(fallDelay);

        // Disable the obstacle's renderer and collider
        meshRenderer.enabled = false;
        collider.enabled = false;

        // Wait for the comebackTime delay
        yield return new WaitForSeconds(returnDelay);

        // Re-enable the obstacle's renderer and collider
        meshRenderer.enabled = true;
        collider.enabled = true;
    }
}