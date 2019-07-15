using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : MonoBehaviour
{
    enum DIRECTION
    {
        Right,
        Left
    }

    private UnityInputProvider inputProvider;

    private float speed = 3f;

    private float jumpPower = 500;

    private float JUMP_POWER = 2f;


    [SerializeField]
    private Rigidbody2D rb;

    private DIRECTION direction = DIRECTION.Right;

    /// <summary>
    /// 弓所持数
    /// </summary>
    private int arrowPossessionCount = 10;

    [SerializeField]
    private GameObject arrowPrefab;


    // Start is called before the first frame update
    void Start()
    {
       inputProvider = UnityInputProvider.GetInstance();
    }

    // Update is called once per frame
    void Update()
    {

        // 弓矢を射る　残数チェックを入れる
        if(inputProvider.GetShoot() && IsPossibleShoot())
        {
            ArrowShoot();
        }

        // 移動
        Move();

        
        if(inputProvider.GetJump())
        {
            Debug.Log("jump");
            Jump();
        }
        


    }

    /// <summary>
    /// 移動
    /// </summary>
    private void Move()
    {
        Vector2 moveDirection = inputProvider.GetMoveDirection();
        // 向きを変える
        DirectionChange(moveDirection);

        if(rb.velocity.magnitude<5)
        {
            rb.AddForce(moveDirection * 10);
        }
       
    }

    /// <summary>
    /// 弓矢の残数
    /// </summary>
    /// <returns></returns>
    private bool IsPossibleShoot() => arrowPossessionCount > 0;


    /// <summary>
    /// 矢を射る
    /// </summary>
    private void ArrowShoot()
    {

        GameObject createArrow = Instantiate(arrowPrefab, transform.position, transform.rotation);
        createArrow.GetComponent<Arrow>().Initialized(5, transform.localScale.x < 0);

    }

    /// <summary>
    /// 向きを変える
    /// </summary>
    /// <param name="moveDirection"></param>
    private void DirectionChange(Vector2 moveDirection)
    {
        if (direction == DIRECTION.Right && moveDirection.x < 0)
        {
            Vector3 scale = transform.localScale;
            scale.x = 1;
            direction = DIRECTION.Left;
            transform.localScale = scale;
        }
        else if (direction == DIRECTION.Left && moveDirection.x > 0)
        {
            Vector3 scale = transform.localScale;
            scale.x = -1;
            direction = DIRECTION.Right;
            transform.localScale = scale;
        }
    }

    private void Jump()
    {
        rb.AddForce(new Vector2(0, 300f));
    }
}
