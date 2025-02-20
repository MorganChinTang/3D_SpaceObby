using UnityEngine;

public class KillVolume : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other != null && other.tag == "Player")
        {
            GameController.Instance.PlayerDied(other.gameObject);
        }
    }
}
