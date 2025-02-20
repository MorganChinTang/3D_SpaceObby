using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other != null)
        {
            if (other.tag == "Player")
            {
                GameController.Instance.PlayerHitCheckPoint(transform);
            }
            else if(other.tag == "Checkpoint" && tag == "Player")
            {
                GameController.Instance.PlayerHitCheckPoint(other.transform);
            }
        }
    }
}
