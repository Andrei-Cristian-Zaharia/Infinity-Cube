using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineCannonScript : TrapScript
{
    public float bulletSpeed;
    public float reload;
    private float reloadTime;
    public float bulletTimeAlive = 2f;
    public GameObject bulletPrefab;
    public Transform shootPosition;
    public bool canShoot;
    public bool shootRight;
    public Vector3 dir;

    private GameObject player;

    private void Start()
    {
        AudioManager audioManager = GameObject.FindGameObjectWithTag("MainMenu").GetComponent<AudioManager>();

        audioSource = this.GetComponent<AudioSource>();

        if (audioManager.GetComponent<AudioSource>())
            audioSource.volume = 0.5f;
        else audioSource.volume = 0f;
    }

    private void Update()
    {
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player");

        if (canShoot)
        {
            if (Mathf.Abs(this.transform.position.y - player.transform.position.y) < 20f)
            { canShoot = false;

                GameObject bullet = Instantiate(bulletPrefab);
                bullet.transform.position = shootPosition.position;
                bullet.GetComponent<LineBullet>().cannon = this.gameObject;
                dir = shootPosition.transform.position - this.transform.position;
                bullet.GetComponent<Rigidbody2D>().velocity = dir * bulletSpeed * SlowMotionTime;
                PlaySoundEffect();

                StartCoroutine(Reload());
                Destroy(bullet, bulletTimeAlive);
            } 
        }
    }

    IEnumerator Reload()
    {
        if (SlowMotionTime < 0.7f) reloadTime = reload * 3;
        else reloadTime = reload;

        yield return new WaitForSeconds(reloadTime);

        canShoot = true;
    }
}
