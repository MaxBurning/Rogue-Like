using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// マップを生成するクラス
/// </summary>

public class MapManager : MonoBehaviour
{

    // マス毎のデータ
    struct sChipData
    {
        public eStageObject Type;  // 壁か床を判別する
        public GameObject go;      // マス毎のオブジェクトデータ
    };

    // マス毎のタイプ
    enum eStageObject : short
    {
        //壁
        Wall = 0,
        //床(=1)
        Floor
    };

    // プレハブを保存するリスト
    public List<GameObject> mStageObject = new List<GameObject>();
    public int mRows = 5;       // マップの横
    public int mColumns = 5;    // マップの縦

    private sChipData[] mMapData = null;  //マップの管理
    private int mRect = 0;                // フロアにする範囲

    // Use this for initialization
    void Start()
    {
        // フロアにする範囲の計算(line 37, 38)
        mRect = mRows * mColumns;
        mMapData = new sChipData[mRect];

        // マップタイプを壁に初期化
        for (int i = 0; i < mRect; ++i)
        {
            mMapData[i].Type = eStageObject.Wall;
        }

        // マップの配置
        MapSetting();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // マップの生成
    void MapSetting()
    {
        int rect = 0;
        Vector3 pos = Vector3.zero;
        GameObject work = new GameObject();
        work.name = "Stage";

        for (int column = 0; column < mColumns; ++column)
        {
            for (int row = 0; row < mRows; ++row)
            {
                // 指定したタイプのデータをインスタンス化
                mMapData[rect].go = Instantiate(mStageObject[(int)mMapData[rect].Type], pos,
                                        Quaternion.identity) as GameObject;
                // クローンの削除
                mMapData[rect].go.name = mStageObject[(int)mMapData[rect].Type].name;
                // 親要素の指定
                mMapData[rect].go.transform.parent = work.transform;
                ++pos.x;
                ++rect;
            }
            pos.x = 0;
            ++pos.z;
        }
    }
}
