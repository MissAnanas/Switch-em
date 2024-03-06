using UnityEngine;

public class CameraController : MonoBehaviour
{
    //Room camera
    public GameObject player;
    public GameObject right;
    public GameObject left;
    public GameObject up;



    private void Update()
    {
        if(player.transform.position.x > right.transform.position.x)
        {
            transform.position += new Vector3(35, 0, 0);
        }
        if (player.transform.position.x < left.transform.position.x)
        {
           transform.position += new Vector3(-35, 0, 0);
        }
        if (player.transform.position.y > up.transform.position.y)
        {
            transform.position += new Vector3(0, 23, 0);
        }

    }

}