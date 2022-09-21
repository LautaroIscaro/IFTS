using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAnim : MonoBehaviour
{
    private Animator _playerAnim;
    // Start is called before the first frame update
    void Start()
    {
        _playerAnim = GetComponentInChildren<Animator>();
    }

    public void Move(float move)
    {
        _playerAnim.SetFloat("Move", Mathf.Abs(move));
    }

    public void Jump(bool jumping)
    {
        _playerAnim.SetBool("jumping", jumping);
    }

}
