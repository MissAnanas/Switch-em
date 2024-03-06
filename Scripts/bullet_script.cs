using UnityEngine;


public class bullet_script : MonoBehaviour
{
    // Start is called before the first frame update


    public GameObject Bleu;
    public float LifeTime = 3f;
    public Color color;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (GetComponent<SpriteRenderer>().color == Color.red && PlayerMovement.instance.IsBlue == true)
            {
                PlayerMovement.instance.rb.velocity = Vector3.zero;
                PlayerMovement.instance.CanShoot = false;
                PlayerMovement.instance.Zoomable = true;
                PlayerMovement.instance._Animator.SetTrigger("dead");
                PlayerMovement.instance.CallFadeDeath();
                Destroy(this.gameObject);
                PlayerMovement.instance.rb.transform.position = PlayerMovement.instance.respawnPoint.transform.position;



            }
            if (GetComponent<SpriteRenderer>().color == Color.blue && PlayerMovement.instance.IsBlue == false)
            {
                PlayerMovement.instance.rb.velocity = Vector3.zero;
                PlayerMovement.instance.CanShoot = false;
                PlayerMovement.instance.Zoomable = true;
                PlayerMovement.instance._Animator.SetTrigger("dead");
                PlayerMovement.instance.CallFadeDeath();
                Destroy(this.gameObject);

                PlayerMovement.instance.rb.transform.position = PlayerMovement.instance.respawnPoint.transform.position;


            }
        }
        else if (collision.gameObject.tag == "Ground")
        {
            Destroy (this.gameObject);
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        LifeTime -= Time.deltaTime;
        if (LifeTime < 0)
        {
            Destroy(this.gameObject);
        }

    }
}
