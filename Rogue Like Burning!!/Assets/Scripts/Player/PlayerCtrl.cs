using UnityEngine;
using System.Collections.Generic;


public class PlayerCtrl : MonoBehaviour {

	public GameObject mPlayer;
	private List<Vector3> mFloorList = new List<Vector3>();
	

	// Use this for initialization
	void Start () 
	{
		this.mFloorList = MapMgr.FloorList;
		GameObject player = new GameObject();
		player.name = "Player";
		this.mPlayer = Instantiate(
							this.mPlayer,
							this.mFloorList[Random.Range(0, mFloorList.Count - 1)],
							Quaternion.identity
							) as GameObject;
		this.mPlayer.transform.parent = player.transform;
		// this.mPlayer = Instantiate(this.mPlayer, 
		// 				this.SetPlayerStartPos(), 
		// 				Quaternion.identity) as GameObject;
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

    // 自キャラの開始位置を設定
    Vector3 SetPlayerStartPos()
    {
		return this.mFloorList[Random.Range(0, mFloorList.Count - 1)];
    }
}
