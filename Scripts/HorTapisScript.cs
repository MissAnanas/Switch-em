using UnityEngine;

public class HorTapisScript : MonoBehaviour
{

    public float SpeedTapis = 100f;
    private void OnCollisionStay2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            if (PlayerMovement.instance.IsWallDetected)
            {
                Physics2DExtensions.AddForce(PlayerMovement.instance.rb, new Vector2(PlayerMovement.instance.IsFacingRight ? 100 : -100, SpeedTapis * 7), ForceMode.Acceleration);
            }
            else if (collision.transform.position.y <= PlayerMovement.instance.transform.position.y)
            {
                Physics2DExtensions.AddForce(PlayerMovement.instance.rb, new Vector2(SpeedTapis, 0), ForceMode.Acceleration);
            }
            
        }
    }
}
