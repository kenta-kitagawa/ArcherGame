using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{

    /// <summary>
    /// 弓の移動速度
    /// </summary>
    private float speed;


    private float attack;


    [SerializeField]
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Initialized(float _speed, bool _isRight)
    {
        speed = _speed;
        if(_isRight)
        {
            Vector3 scale = transform.localScale;
            scale.x = -scale.x;
            transform.localScale = scale;
        }
        else
        {
            speed = -speed;
        }
        rb.velocity = new Vector2(speed, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch(collision.gameObject.tag)
        {
            case "Floor":
            case "Wall":
                rb.velocity = Vector2.zero;
                rb.bodyType = RigidbodyType2D.Static;
                break;
        }
    }
}
