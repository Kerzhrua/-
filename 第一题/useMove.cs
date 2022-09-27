using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class useMove : MonoBehaviour
{
    //  在编辑器上挂载物体
    public GameObject gameObject1;
    public GameObject gameObject2;
    public GameObject gameObject3;
    public GameObject gameObject4;

    private void Awake()
    {
        MoveManager.Instance.Init();
    }

    void Start()
    {
        //  调用方法将物体加入移动管理
        MoveManager.Instance.Move(gameObject1, new Vector3(0, 0, 0), new Vector3(4, 4, 4), 2.0f, true, MoveManager.MoveType.UniformSpeed);
        MoveManager.Instance.Move(gameObject2, new Vector3(0, 0, 0), new Vector3(24, -5, 4), 5.0f, true, MoveManager.MoveType.EaseIn);
        MoveManager.Instance.Move(gameObject3, new Vector3(0, 0, 0), new Vector3(4, 14, 4), 3.0f, true, MoveManager.MoveType.EaseOut);
        MoveManager.Instance.Move(gameObject4, new Vector3(0, 0, 0), new Vector3(-4, 4, 14), 6.0f, true, MoveManager.MoveType.EaseInOut);
    }

}
