using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MoveManager : MonoSingleton<MoveManager>
{
    public struct GameObjectMoveData
    {
        public bool loop;
        public float currDistance;
        public float speed;
        public float time;
        public float passedTime;
        public Vector3 begin;
        public Vector3 end;
        public MoveType moveType;
    }

    public enum MoveType
    {
        UniformSpeed,
        EaseIn,
        EaseOut,
        EaseInOut
    }

    public void Init()
    {
        GameobjectMoves = new Dictionary<GameObject, GameObjectMoveData>();
    }

    Dictionary<GameObject, GameObjectMoveData> GameobjectMoves;

    public void Move(GameObject gameObject, Vector3 begin, Vector3 end, float time, bool pingpong, MoveType moveType)
    {
        GameObjectMoveData gameObjectMoveData = new GameObjectMoveData() { 
            begin = begin, 
            end = end, 
            loop = pingpong, 
            moveType = moveType, 
            time = time, 
            passedTime = 0.0f 
        };
        gameObjectMoveData.currDistance = Vector3.Distance(begin, end);
        gameObjectMoveData.speed = gameObjectMoveData.currDistance / time;

        gameObject.transform.position = begin;
        if (GameobjectMoves.ContainsKey(gameObject))
        {
            //如果已经有了，更新他的move数据
            GameobjectMoves[gameObject] = gameObjectMoveData;
        }
        else
        {
            //还没有就新建一个加入move管理
            GameobjectMoves.Add(gameObject, gameObjectMoveData);
        }
    }

    private void Update()
    {
        if(GameobjectMoves.Count != 0)
        {
            for(int i = 0; i < GameobjectMoves.Count; i++)
            {
                var objMove = GameobjectMoves.ElementAt(i);
                GameObject gameObject = objMove.Key;
                GameObjectMoveData gameObjectMoveData = objMove.Value;
                if (Vector3.Distance(gameObject.transform.position, gameObjectMoveData.end) < 0.001f || gameObjectMoveData.passedTime >= gameObjectMoveData.time)
                {
                    if (gameObjectMoveData.loop == false)
                    {
                        GameobjectMoves.Remove(gameObject);
                        return;
                    }
                    else
                    {
                        Debug.Log("切换");
                        Vector3 temp = gameObjectMoveData.begin;
                        gameObjectMoveData.begin = gameObjectMoveData.end;
                        gameObjectMoveData.end = temp;
                        gameObjectMoveData.passedTime = 0.0f;
                        GameobjectMoves[gameObject] = gameObjectMoveData;
                    }
                }

                //  全部都准备好了，可以开始移动了
                //  现在多了几种移动方式
                switch (gameObjectMoveData.moveType)
                {
                    case MoveType.UniformSpeed:
                        float step = gameObjectMoveData.speed * Time.deltaTime;
                        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, gameObjectMoveData.end, step);
                        break;
                    case MoveType.EaseIn:
                        float velocity = Mathf.Lerp(0, gameObjectMoveData.speed, gameObjectMoveData.passedTime / gameObjectMoveData.time) * 2;
                        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, gameObjectMoveData.end, velocity * Time.deltaTime);
                        break;
                    case MoveType.EaseOut:
                        velocity = Mathf.Lerp(0, gameObjectMoveData.speed, 1-(gameObjectMoveData.passedTime/ gameObjectMoveData.time)) * 2;
                        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, gameObjectMoveData.end, velocity * Time.deltaTime);
                        break;
                    case MoveType.EaseInOut:
                        if(Vector3.Distance(gameObject.transform.position, gameObjectMoveData.end) * 2 > Vector3.Distance(gameObjectMoveData.begin, gameObjectMoveData.end))
                        {
                            velocity = Mathf.Lerp(0, gameObjectMoveData.speed, gameObjectMoveData.passedTime / gameObjectMoveData.time) * 4;
                            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, gameObjectMoveData.end, velocity * Time.deltaTime);
                        }
                        else
                        {
                            velocity = Mathf.Lerp(0, gameObjectMoveData.speed, 1 - (gameObjectMoveData.passedTime / gameObjectMoveData.time)) * 4;
                            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, gameObjectMoveData.end, velocity * Time.deltaTime);
                        }
                        break;
                }
                gameObjectMoveData.passedTime = gameObjectMoveData.passedTime + Time.deltaTime;
                gameObjectMoveData.currDistance = Vector3.Distance(gameObject.transform.position, gameObjectMoveData.end);
                GameobjectMoves[gameObject] = gameObjectMoveData;
            }
        }


    }

}
