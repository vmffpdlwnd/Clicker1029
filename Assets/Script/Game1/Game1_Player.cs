using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game1_Player : MonoBehaviour
{
    Rigidbody rigid;
    Animator anim;

    public FixedJoystick joystick;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal") + joystick.Horizontal;

        if (h < 0)
            transform.localScale = new Vector3(2, 2, -2); // 스케일을 반전시킴
        else if (h > 0)
            transform.localScale = new Vector3(2, 2, 2); // 스케일을 원래대로 복원

        rigid.AddForce(Vector2.right * h * 10, ForceMode.Impulse);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Horizontal"))
        {
            anim.SetBool("isMove", true);
        }
        else anim.SetBool("isMove", false);
        //anim.SetBool("isMove", Mathf.Abs(rigid.velocity.x) >= 0.1f );

        if (Input.GetButtonDown("Fire1"))
        {
            anim.SetBool("isAttack", true);
        }
        else
            anim.SetBool("isAttack", false);

        if (Input.GetButtonDown("Fire2"))
        {
            anim.SetBool("isSkill", true);
        }
        else
            anim.SetBool("isSkill", false);
    }
}