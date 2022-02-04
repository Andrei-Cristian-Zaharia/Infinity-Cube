using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonScript : TrapScript
{
    public Transform shootPosition;
    public float range;
    public float reload;
    private float reloadTime;
    public float bulletSpeed;
    public GameObject bulletPrefab;
    public bool canShoot;
    public float offsetY;

    private bool inRange;
    private GameObject player;
    private new CircleCollider2D collider;

    private void Start()
    {
        collider = this.GetComponent < CircleCollider2D > ();
        AudioManager audioManager = GameObject.FindGameObjectWithTag("MainMenu").GetComponent<AudioManager>();
        audioSource = this.GetComponent<AudioSource>();

        if (audioManager.GetComponent<AudioSource>())
            audioSource.volume = 0.5f;
        else audioSource.volume = 0f;

        collider.radius = range;
    }

    private void Update()
    {
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player");

        if (inRange)
        {
            Vector3 dir = player.transform.position - this.transform.position;
            float angle = Mathf.Atan2(-dir.y, -dir.x) * Mathf.Rad2Deg;
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.AngleAxis(angle, Vector3.forward), 5 / SlowMotionTime);

            if (canShoot)
            {
                canShoot = false;

                GameObject bullet = Instantiate(bulletPrefab);
                bullet.transform.position = shootPosition.position;
                bullet.GetComponent<Rigidbody2D>().AddForce((new Vector3(dir.x, dir.y + offsetY, dir.z) * bulletSpeed * 10) * SlowMotionTime);
                PlaySoundEffect();

                StartCoroutine(Reload());
                Destroy(bullet, 4f);
            }
        }
    }

    IEnumerator Reload()
    {
        if (SlowMotionTime < 0.7f) reloadTime = reload * 3;
        else reloadTime = reload;

        yield return new WaitForSeconds(reload);

        canShoot = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inRange = false;
        }
    }
}
