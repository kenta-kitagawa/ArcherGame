using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : MonoBehaviour
{
    enum Direction
    {
        Right,
        Left
    }

    private UnityInputProvider inputProvider;

    private float speed = 3f;

    private const float JUMP_POWER = 300;


    [SerializeField]
    private Rigidbody2D rb;

    /// <summary>
    /// キャラクターの向き
    /// </summary>
    private Direction direction = Direction.Right;

    /// <summary>
    /// 弓所持数
    /// </summary>
    private int arrowPossessionCount = 10;

    /// <summary>
    /// 矢のプレハブ
    /// </summary>
    [SerializeField]
    private GameObject arrowPrefab;

    [SerializeField]
    private bool isJump = false;


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

        // 着地時にすり抜けないように
        if (rb.velocity.y < 0)
        {
            LayerName.LayerChange(gameObject, (int)LayerName.Layer_Name.Character);
        }

        // ジャンプ
        if (inputProvider.GetJump() && !isJump)
        {
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
        if (direction == Direction.Right && moveDirection.x < 0)
        {
            Vector3 scale = transform.localScale;
            scale.x = 1;
            direction = Direction.Left;
            transform.localScale = scale;
        }
        else if (direction == Direction.Left && moveDirection.x > 0)
        {
            Vector3 scale = transform.localScale;
            scale.x = -1;
            direction = Direction.Right;
            transform.localScale = scale;
        }
    }

    /// <summary>
    /// ジャンプ
    /// </summary>
    private void Jump()
    {
        //上方向に力を加える
        rb.AddForce(new Vector2(0, JUMP_POWER));
        isJump = true;

        LayerName.LayerChange(gameObject, (int)LayerName.Layer_Name.CharacterThroughtFloor);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Floor"))
        {
            isJump = false;
        }
    }

}
