using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterChg : MonoBehaviour
{

	private int index = 0;
	private int o_max = 0;
	GameObject[] childObject;

	///-------------------------------------------
	Vector3 prevPos;    // 切替前の座標を取得するための変数
	Quaternion prevRot;
	///-------------------------------------------

	void Start()
	{
		o_max = this.transform.childCount;//子オブジェクトの個数取得
		childObject = new GameObject[o_max];//インスタンス作成

		for (int i = 0; i < o_max; i++)
		{
			childObject[i] = transform.GetChild(i).gameObject;//すべての子オブジェクト取得

			///-------------------------------------------
			// すべてのキャラクターの座標を取得
			prevPos = childObject[i].transform.position;
			prevRot = childObject[i].transform.rotation;
			///この部分だけはUpdate()にいれないけんかも
			///-------------------------------------------
		}
		//すべての子オブジェクトを非アクティブ
		foreach (GameObject gamObj in childObject)
		{
			gamObj.SetActive(false);
		}
		//最初はひとつだけアクティブ化しておく
		childObject[index].SetActive(true);
	}

	void Update()

	{
		prevPos = childObject[index].transform.position;
		prevRot = childObject[index].transform.rotation;


		if (Input.GetKeyDown("c"))
		{
			//現在のアクティブな子オブジェクトを非アクティブ
			childObject[index].SetActive(false);
			index++;
			
			//子オブジェクトをすべて切り替えたらまた最初のオブジェクトに戻る
			if (index == o_max) { index = 0; }

			///-------------------------------------------
			// キャラクターの座標を適応させる
			childObject[index].transform.position = prevPos;
			childObject[index].transform.rotation = prevRot;


			///-------------------------------------------

			//次のオブジェクトをアクティブ化
			childObject[index].SetActive(true);
			
		}
	}
}