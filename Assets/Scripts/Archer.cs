using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : MonoBehaviour
{
    /// <summary>
    /// 方向
    /// </summary>
    enum Direction
    {
        Right,
        Left
    }

    /// <summary>
    /// インプットプロバイダー
    /// </summary>
    private UnityInputProvider inputProvider;

    /// <summary>
    /// 移動速度
    /// </summary>
    private float speed = 10f;

    /// <summary>
    /// ジャンプ力
    /// </summary>
    private const float JUMP_POWER = 300;

    /// <summary>
    /// Rigfidbody
    /// </summary>
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

    /// <summary>
    /// ジャンプ中か？
    /// </summary>
    [SerializeField]
    private bool isJump = false;

    /// <summary>
    /// velocityの最大値
    /// </summary>
    private const int VLIMIT = 5;


    private const int rapid = 120;

    private int coolTime = 0;


    // Start is called before the first frame update
    void Start()
    {
       inputProvider = UnityInputProvider.GetInstance();
    }

    // Update is called once per frame
    void Update()
    {
        // クールタイム減少
        SubCoolTime();

        // 弓矢を射る
        if (inputProvider.GetShoot() && IsPossibleShoot())
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

        // すり抜け床を降りる
        if (inputProvider.GetDownKey())
        {
            LayerName.LayerChange(gameObject, (int)LayerName.Layer_Name.CharacterThroughtFloor);
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


        if (rb.velocity.magnitude < VLIMIT)
        {
            rb.AddForce(moveDirection * speed);
        }
    }

    /// <summary>
    /// 弓矢の残数
    /// </summary>
    /// <returns></returns>
    private bool IsPossibleShoot() => (arrowPossessionCount > 0) && (coolTime <= 0);


    /// <summary>
    /// 矢を射る
    /// </summary>
    private void ArrowShoot()
    {
        GameObject createArrow = Instantiate(arrowPrefab, transform.position, transform.rotation);
        createArrow.GetComponent<Arrow>().Initialized(5, transform.localScale.x < 0);
        coolTime = rapid;
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

    /// <summary>
    /// 当たり判定
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch(collision.gameObject.tag)
        {
            case "Floor":
                isJump = false;
                break;

            case "Arrow":
            // 相手が射った矢に当たるとダメージ
            // 落ちている、壁に刺さった矢に当たると取得

            case "SaftyArrow":
                //Destroy(collision.gameObject);
                arrowPossessionCount++;
                Debug.Log("矢を取得");
                break;
        }
    }


    private void SubCoolTime()
    {
        if (coolTime <= 0) return;

        coolTime--;
    }
}
