using UnityEngine;
using System.Collections;

/// <summary>
/// MonoBehaviourの拡張
/// </summary>
public class MyMonoBehaviour : MonoBehaviour {

    private Transform mTransform;
    private Rigidbody mRigidbody;
    private Rigidbody2D mRigidbody2D;

    /// <summary>
    /// Transformをキャッシュから返す（高速化
    /// </summary>
    public new Transform transform
    {
        get
        {
            //初めてアクセスされた場合だけGetする
            if (!mTransform)
                mTransform = GetComponent<Transform>();
            return mTransform;
        }
    }
    /// <summary>
    /// Rigidbodyをキャッシュから返す（高速化
    /// </summary>
    public new Rigidbody rigidbody
    {
        get
        {
            if (!mRigidbody)
            {
                mRigidbody = GetComponent<Rigidbody>();
                if (!mRigidbody)
                    Debug.Log("Rigidbodyないんですけど？？？？？？");
            }
            return mRigidbody;
        }
    }

    /// <summary>
    /// Rigidbody2Dをキャッシュから返す（高速化
    /// </summary>
    public new Rigidbody2D rigidbody2D
    {
        get
        {
            if (!mRigidbody2D)
            {
                mRigidbody2D = GetComponent<Rigidbody2D>();
                if (!mRigidbody)
                    Debug.Log("Rigidbody2Dないんですけど？？？？？？");
            }
            return mRigidbody2D;
        }
    }

    //GameObjectを返すInstantiate (初期状態ではObject型を返している
    public GameObject InstantiateGameObject(string path)
    {
        GameObject obj = Instantiate(Resources.Load(path)) as GameObject;

        if (!obj)
            Debug.LogError("なめてんのか");

        return obj;
    }
    public GameObject InstantiateGameObject(string path, Vector3 pos)
    {
        GameObject obj = InstantiateGameObject(path);
        if(obj)
            obj.transform.position = pos;

        return obj;
    }
    public GameObject InstantiateGameObject(Object prefav)
    {
        GameObject obj = Instantiate(prefav) as GameObject;

        if (!obj)
            Debug.LogError("そんなプレハブ知らない");

        return obj;
    }
    public GameObject InstantiateGameObject(Object prefav, Vector3 pos)
    {
        GameObject obj = InstantiateGameObject(prefav);
        if(obj)
            obj.transform.position = pos;
        return obj;
    }
}
