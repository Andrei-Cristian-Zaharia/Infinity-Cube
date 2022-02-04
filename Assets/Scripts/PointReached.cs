using UnityEngine;

public class PointReached : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) { 
            GameManager GM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>(); 
            GM.PlayerReachedTheEndPoint();
        }
    }
}
