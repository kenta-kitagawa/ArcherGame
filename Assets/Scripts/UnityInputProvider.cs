using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityInputProvider : MonoBehaviour , IInputProvider
{

    private static UnityInputProvider instance = null;

    public static UnityInputProvider GetInstance()
    {
        if (instance != null) return instance;

        GameObject singletonObj = new GameObject("UnityInputProvider");
        DontDestroyOnLoad(singletonObj);
        instance = (UnityInputProvider)singletonObj.AddComponent(typeof(UnityInputProvider));
        return instance;
    }

    public bool GetShoot() => Input.GetKeyDown(KeyCode.Z);

    public bool GetJump() => Input.GetKeyDown(KeyCode.J);

    public Vector2 GetMoveDirection()
    {
        Vector2 inputDirectrion = Vector2.zero;
        if(Input.GetKey(KeyCode.D))
        {
            inputDirectrion = Vector2.right;
        }
        else if(Input.GetKey(KeyCode.A))
        {
            inputDirectrion = Vector2.left;
        }


        return inputDirectrion;
        
    }

}
