using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// マップの生成を行います。
/// </summary>
public class MapMgr : MonoBehaviour
{

#region Struct & Enum

    // マス毎のデータ
    struct sChipData
    {
        public eStageObject Type;  // 壁か床を判別する
        public GameObject go;      // マス毎のオブジェクトデータ
    };

    // マス毎のタイプ
    enum eStageObject : short
    {
        // 壁
        Wall = 0,
        // 床(=1)
        Floor,
        // 階段(=2)
        Stairs
    };   

#endregion Struct & Enum

#region Field

    /// <summary>
    /// プレハブを格納します。
    /// </summary>
    public List<GameObject> mStageObject = new List<GameObject>();
    
    /// <summary>
    /// マップの行数を設定します。
    /// </summary>
    public int mRows = 10;  
    
    /// <summary>
    /// マップの列数を設定します。
    /// </summary>    
    public int mColumns = 10;    

    private sChipData[] mMapData = null;
    private int mRect = 0;
    private static List<Vector3> mFloorList = new List<Vector3>(); 
    private Vector3 mStairPos;

#endregion Field

#region Property

    /// <summary>
    /// 床オブジェクトの座標リストを取得します。
    /// </summary>
    /// <returns>床オブジェクトの座標リスト</returns>
    public static List<Vector3> FloorList
    {
        get{return mFloorList;}
    }

#endregion Property

#region Method

    /// <summary>
    /// シーン開始時に一度だけ実行されます。
    /// </summary>
    private void Awake()
    {
        // フロアにする範囲の計算
        mRect = mRows * mColumns;
        mMapData = new sChipData[mRect];

        // マップタイプを床に初期化
        for (int i = 0; i < mRect; ++i)
        {
            mMapData[i].Type = eStageObject.Floor;
        }

        // マップの配置
        this.MapSetting();
        this.SetStairs();

    }

    // Update is called once per frame
    private void Update()
    {

    }

    /// <summary>
    /// 床と壁を設置します。
    /// </summary>
    private void MapSetting()
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
                mMapData[rect].go = Instantiate(mStageObject[(int)mMapData[rect].Type]
                                    , pos
                                    , Quaternion.identity) as GameObject;

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

    /// <summary>
    /// 階段を設置します。
    /// </summary>
    private void SetStairs()
    {
        this.AddFloorsPos();

        // フロアマスの座標を格納したリストからランダムで座標を取得し、
        // 取得した座標マスのタイプをStairに変更
        this.mStairPos = mFloorList[Random.Range(0, mFloorList.Count - 1)];
        
        // 階段の座標をリストから削除しておく
        for(int i = 0; i < mFloorList.Count - 1; ++i)
        {
            if(mFloorList[i] == this.mStairPos)
            {
                mFloorList.RemoveAt(i);
            }
        }

    }

    /// <summary>
    /// 床オブジェクトの座標を追加します。
    /// </summary>
    private void AddFloorsPos()
    {
        // マス毎のデータタイプを判別
        foreach(sChipData s in mMapData)
        {
            // フロアタイプのマスはリストに追加
            if (s.Type == eStageObject.Floor)
            {
                mFloorList.Add(s.go.transform.position);
            }
        }
    }

#endregion Method

}
