using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IInputProvider
{
    /// <summary>
    /// 移動しているか？
    /// </summary>
    /// <returns>The move.</returns>
    Vector2 GetMoveDirection();

    /// <summary>
    /// 弾を発射するか？
    /// </summary>
    /// <returns><c>true</c>, if fire was gotten, <c>false</c> otherwise.</returns>
    bool GetShoot();

    bool GetJump();
}
