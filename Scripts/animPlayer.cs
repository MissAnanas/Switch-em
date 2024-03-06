using Unity.VisualScripting;
using UnityEngine;

public class animPlayer : MonoBehaviour
{
    public Animator anim;
    public PlayerMovement die;

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("vertical", Input.GetAxis("Vertical"));
        anim.SetFloat("horizontal", Input.GetAxis("Horizontal"));
    }
}