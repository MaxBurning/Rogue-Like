﻿using UnityEngine;

/// <summary>
/// マップを生成するクラス
/// </summary>

public class MapManager : MonoBehaviour {

    private int mRows = 5;
    private int mColumns = 5;
    private Transform mBoardHolder;

    public GameObject mFloor;

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

       for(int column = 1; column < mColumns; column++)
        {
            for(int row = 1; row < mRows; row++)
            {
                GameObject instance = Instantiate(mFloor, new Vector3(column, 0.0f, row),
                                      Quaternion.identity) as GameObject;

                instance.transform.SetParent(mBoardHolder);
            }
        }
    }

}