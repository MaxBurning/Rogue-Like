using UnityEngine;
using System.Collections;

/// <summary>
/// これ継承したらシングルトンになります。
/// 
/// </summary>
public class SingletonMonoBehaviour<T> : MyMonoBehaviour where T : MyMonoBehaviour{
    private static T mInstance;
    
    /// <summary>
    /// 起動時に他に自分が居ないかチェック
    /// </summary>
    public virtual void Awake()
    {
        if (this != instance)
        {
            Destroy(this);
            return;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public static T instance
    {
        get
        {
            //インスタンスが無ければ生成
            if (!mInstance)
            {
                mInstance = FindObjectOfType(typeof(T)) as T;
                //それでもない場合はエラー
                if (!mInstance)
                    Debug.LogError(typeof(T) + "がないんやで");
            }
            return mInstance;
        }
    }
}