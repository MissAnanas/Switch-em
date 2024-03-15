using UnityEngine;
public static class Physics2DExtensions
{
    public static void AddForce(this Rigidbody2D rigidbody2D, Vector2 force, ForceMode mode = ForceMode.Force)
    {
        switch (mode)
        {
            case ForceMode.Force:
                rigidbody2D.AddForce(force);
                break;
            case ForceMode.Impulse:
                rigidbody2D.AddForce(force / Time.fixedDeltaTime);
                break;
            case ForceMode.Acceleration:
                rigidbody2D.AddForce(force * rigidbody2D.mass);
                break;
            case ForceMode.VelocityChange:
                rigidbody2D.AddForce(force * rigidbody2D.mass / Time.fixedDeltaTime);
                break;
        }
    }

    public static void AddForce(this Rigidbody2D rigidbody2D, float x, float y, ForceMode mode = ForceMode.Force)
    {
        rigidbody2D.AddForce(new Vector2(x, y), mode);
    }
}

public class ScriptBumper : MonoBehaviour
{

    public AudioSource BumpSound;
    private Animator[] _isBumping;

    private void Start()
    {
        _isBumping = new Animator[transform.childCount];
        for(int i = 0; i < transform.childCount; i++)
        {
            _isBumping[i] = transform.GetChild(i).GetComponent<Animator>();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            BumpSound = GetComponent<AudioSource>();
            //BumpSound.Play();
            Physics2DExtensions.AddForce(PlayerMovement.instance.rb, new Vector2(PlayerMovement.instance.IsFacingRight ? 750 : -750, 472), ForceMode.Force);
            foreach (Animator animator in _isBumping)
            {
                animator.SetTrigger("isBumping");
            }
        }
    }
}



