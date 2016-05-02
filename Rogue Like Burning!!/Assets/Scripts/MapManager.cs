using UnityEngine;

/// <summary>
/// マップを生成するクラス
/// </summary>

public class MapManager : MonoBehaviour {

    
    private Transform mBoardHolder;

    public GameObject mFloor;
    public int mRows = 5;
    public int mColumns = 5;

    // Use this for initialization
    void Start ()
    {
        MapSetting();
    }

    // Update is called once per frame
    void Update() {}

    // 床を生成する
    void MapSetting()
    {
        mBoardHolder = new GameObject("Board").transform;

       for(int column = 0; column < mColumns; column++)
        {
            for(int row = 0; row < mRows; row++)
            {
                GameObject instance = Instantiate(mFloor, new Vector3(column, 0.0f, row),
                                      Quaternion.identity) as GameObject;

                instance.transform.SetParent(mBoardHolder);
            }
        }
    }

}
