using System.Collections.Generic;
using UnityEngine;

public class shoot_player : MonoBehaviour
{
    public Transform target; //where we want to shoot(player? mouse?)
    public Transform weaponMuzzle; //The empty game object which will be our weapon muzzle to shoot from
    public float fireRate = 3000f; //Fire every 3 seconds
    public float shootingPower = 0.1f; //force of projection
    private Vector2 normalizedVector;
    public LineRenderer rope;
    public float length;

    public AudioSource ShotSound;






    public List<GameObject> bullet;



    private float shootingTime; //local to store last time we shot so we can make sure its done every 3s

    private void Start()
    {
        rope.SetPosition(0, transform.position);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
        }
    }

    private void Update()
    {
        rope.SetPosition(1, transform.position + (PlayerMovement.instance.transform.position - transform.position).normalized * length );
        Fire(); //Constantly fire
    }

    private void Fire()
    {
        if (Time.time > shootingTime)
        {
            int index = Random.Range(0, bullet.Count);
            shootingTime = Time.time + fireRate / 1000; //set the local var. to current time of shooting
            Vector2 myPos = new Vector2(weaponMuzzle.position.x, weaponMuzzle.position.y); //our curr position is where our muzzle points

            if (PlayerMovement.instance.CanShoot == true)
            {
                GameObject projectile = Instantiate(bullet[index], myPos, Quaternion.identity); //create our bullet


                Vector2 direction = (Vector2)target.position - myPos; //get the direction to the target
                normalizedVector = direction.normalized;


                ShotSound = GetComponent<AudioSource>();
                ShotSound.Play();
                projectile.GetComponent<Rigidbody2D>().velocity = normalizedVector * shootingPower; //shoot the bullet
            }
        }
    }
}
