using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public enum State
{
    FALSE = 0,
    TRUE = 1,
    NONE = -1
}

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    public float speed = 20f;
    public float JumpPower = 6f;
    public bool IsFacingRight = true;
    public BoundsInt area;
    public Tilemap Bleu;
    public Tilemap Rouge;
    public Tilemap Fondation;
    private bool IsGrounded;
    private bool CanGrabWall = true;
    public State IsBlue = State.NONE;
    public static PlayerMovement instance;
    public GameObject respawnPoint;
    //public bool IsTouched = false;
    public bool CanShoot = false;
    public bool IsDead = false;
    public bool Zoomable = true;
    AudioSource DeathSound;
    AudioSource JumpSound;


    [SerializeField] public Rigidbody2D rb;
    [SerializeField] private Transform GroundCheck;
    [SerializeField] private LayerMask GroundLayer;

    [SerializeField] private Transform WallCheck;
    [SerializeField] private float WallCheckDistance;
    public bool IsWallDetected;
    private bool CanWallSlide;
    public bool IsWallSliding;

    public Image Imagepourlebiz;
    float alphaFadeValue = 0.0f;

    public Animator _Animator;

    IEnumerator WaitOneFrame()
    {
        Physics2DExtensions.AddForce(rb, new Vector2(!IsFacingRight ? 400 : -400, 300), ForceMode.Force);
        CanGrabWall = false;
        yield return new WaitForSeconds(0.3f);
        CanGrabWall = true;
    }
     
    public State ChangeState(State On) 
    {
        if(On == State.FALSE)
        {
            return State.TRUE;
        }
        else
        {
            return State.FALSE;
        }
    }

    private void Awake()
    {
        instance = this;
        
    }

    void Start()
    {
        AudioSource[] audios = GetComponents<AudioSource>();
        DeathSound = audios[0];
        JumpSound = audios[1];

    }

    private void OnTriggerEnter2D(Collider2D collisionbis)
    {
        
        if (collisionbis.gameObject.CompareTag("DieZone"))
        {
            rb.velocity = Vector3.zero;
            CanShoot = false;
            Zoomable = true;
            _Animator.SetTrigger("dead");
            //StartCoroutine(DeathFrame());
            StartCoroutine(FadeDeath());
            rb.transform.position = respawnPoint.transform.position;
            
        }
    }

    public void CallFadeDeath()
    {
        StartCoroutine(FadeDeath());
    }

    public IEnumerator FadeDeath()
    {
        DeathSound.Play();
        IsDead = true;
        IsBlue = State.NONE;
        Rouge.gameObject.SetActive(true);
        Bleu.gameObject.SetActive(true);
        Imagepourlebiz.enabled = true;
        for (int i = 0; i < 101; i++)
        {
            alphaFadeValue = ((float)i / 100);
            Imagepourlebiz.color = new Color(0, 0, 0, alphaFadeValue);
            yield return new WaitForSeconds(0.005f);//0.005f
        }
        yield return new WaitForSeconds(0.4f);//0.4f
        for (int i = 0; i < 101; i++)
        {
            alphaFadeValue = 1 - ((float)i / 100);
            Imagepourlebiz.color = new Color(0, 0, 0, alphaFadeValue);
            yield return new WaitForSeconds(0.005f);//0.005f
        }
        Imagepourlebiz.enabled = false;
        IsDead = false;
    }

    // Update is called once per frame
    void Update()
    {

        CollisionCheck();
        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && IsWallDetected && IsDead == false)
        {
            JumpSound.Play();
            if (IsBlue == State.NONE)
            {
                ToggleColor();
            }
            CanShoot = true;
            Zoomable = false;
            StartCoroutine(WaitOneFrame());
            

        } else if (Input.GetButtonDown("Jump") && IsGrounded && IsDead == false)
        {
            JumpSound.Play();
            if (IsBlue == State.NONE)
            {
                ToggleColor();
            }
            CanShoot = true;
            Zoomable = false;
            rb.velocity = new Vector2(rb.velocity.x, JumpPower);
        }


        /*if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }*/

        Flip();

        if (Input.GetKeyDown(KeyCode.E))
        {
            ToggleColor(); 
        }
       
    }

    public void ToggleColor()
    {
        if (IsBlue == State.NONE)
        {
            IsBlue = State.TRUE;
            ToggleColor();
        }
        if (IsBlue == State.TRUE)
        {
            IsBlue = State.FALSE;
            _Animator.SetBool("isRed", true);
            Rouge.gameObject.SetActive(true);
            Bleu.gameObject.SetActive(false);
        }
        else
        {
            IsBlue = State.TRUE;
            _Animator.SetBool("isRed", false);
            Rouge.gameObject.SetActive(false);
            Bleu.gameObject.SetActive(true);
        }
    }

    private void FixedUpdate()
    {
        if(horizontal != 0 && IsDead == false)
        {
            if (IsBlue == State.NONE)
            {
                ToggleColor();
            }
            CanShoot = true;
            Zoomable = false;
        }

        if (IsWallDetected && CanWallSlide)
        {
            IsWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.2f);
        } else
        {
            IsWallSliding = false;
            if (IsDead == false)
            {
                rb.velocity = Vector2.Lerp(rb.velocity, new Vector2(horizontal * speed, rb.velocity.y), Time.deltaTime);
            }
            
        }
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(WallCheck.position, new Vector3(WallCheck.position.x + WallCheckDistance * transform.localScale.x, WallCheck.position.y, WallCheck.position.z)); 
    }

    private void CollisionCheck()
    {
        IsGrounded = Physics2D.OverlapCircle(GroundCheck.position, 0.2f, GroundLayer);


        IsWallDetected = CanGrabWall && Physics2D.Raycast(WallCheck.position, Vector2.right * transform.localScale.x, WallCheckDistance, GroundLayer);


        if (!IsGrounded && rb.velocity.y < 0)
        {
            CanWallSlide = true;
        }
    }


    private void Flip()
    {
        if (IsFacingRight && horizontal < 0f || !IsFacingRight && horizontal > 0f)
        {
            IsFacingRight = !IsFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        
        }
    }

}


