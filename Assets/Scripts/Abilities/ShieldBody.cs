using UnityEngine;

public class ShieldBody : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        { Destroy(collision.gameObject); Debug.Log(collision.name + "destroyed.");  }

        if (collision.CompareTag("Ground") || collision.CompareTag("Goal") || collision.CompareTag("MovingPlatform"))
        {
            Physics2D.IgnoreCollision(collision.GetComponent<Collider2D>(), this.GetComponent<Collider2D>());
        }
    }
}
