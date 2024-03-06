using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;



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
    public bool IsBlue;
    public static PlayerMovement instance;
    public GameObject respawnPoint;
    //public bool IsTouched = false;
    public bool CanShoot = false;
    public bool IsDead = false;
    public bool Zoomable = true;


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
        Physics2DExtensions.AddForce(rb, new Vector2(!IsFacingRight ? 300 : -300, 698), ForceMode.Force);
        CanGrabWall = false;
        yield return new WaitForSeconds(0.3f);
        CanGrabWall = true;
    }



    private void Awake()
    {
        instance = this;
        _Animator = GetComponent<Animator>();
    }

    void Start()
    {
       

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
        IsDead = true;
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
            CanShoot = true;
            Zoomable = false;
            StartCoroutine(WaitOneFrame());
            

        } else if (Input.GetButtonDown("Jump") && IsGrounded && IsDead == false)
        {
            CanShoot = true;
            Zoomable = false;
            rb.velocity = new Vector2(rb.velocity.x, JumpPower);
        }


        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        Flip();

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Rouge.gameObject.active == false)
            {
                IsBlue = false;
                _Animator.SetBool("isRed", !IsBlue);
                Rouge.gameObject.SetActive(true);
                Bleu.gameObject.SetActive(false);
            } else
            {
                IsBlue = true;
                _Animator.SetBool("isRed", !IsBlue);
                Rouge.gameObject.SetActive(false);
                Bleu.gameObject.SetActive(true);
            }
        }
       
    }

    private void FixedUpdate()
    {
        if(horizontal != 0 && IsDead == false)
        {
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


