using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// マップを生成するクラス
/// </summary>

public class MapMgr : MonoBehaviour
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

    // マップのタイプを取得する(操作キャラの生成に使いたい)
    public Vector3 propFloorPos
    {
        get
        { return Vector3.zero; }
    }

    // プレハブを保存するリスト
    public List<GameObject> mStageObject = new List<GameObject>();
    public int mRows = 10;       // マップの横
    public int mColumns = 10;    // マップの縦

    private sChipData[] mMapData = null;  //マップの管理
    private int mRect = 0;                // フロアにする範囲

    // Use this for initialization
    void Start()
    {
        // フロアにする範囲の計算(line 37, 38)
        mRect = mRows * mColumns;
        mMapData = new sChipData[mRect];

        // マップタイプを床に初期化
        for (int i = 0; i < mRect; ++i)
        {
            mMapData[i].Type = eStageObject.Floor;
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
                // マップの端以外は生成するオブジェクトのタイプを壁に(要改善)
                if (column == 0 || column == mColumns - 1 || row == 0 || row == mRows - 1)
                    mMapData[rect].Type = eStageObject.Wall;

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
