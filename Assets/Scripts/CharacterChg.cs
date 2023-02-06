using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterChg : MonoBehaviour
{

	private int index = 0;
	private int o_max = 0;
	GameObject[] childObject;

	///-------------------------------------------
	Vector3 prevPos;    // �֑ؑO�̍��W���擾���邽�߂̕ϐ�
	Quaternion prevRot;
	///-------------------------------------------

	void Start()
	{
		o_max = this.transform.childCount;//�q�I�u�W�F�N�g�̌��擾
		childObject = new GameObject[o_max];//�C���X�^���X�쐬

		for (int i = 0; i < o_max; i++)
		{
			childObject[i] = transform.GetChild(i).gameObject;//���ׂĂ̎q�I�u�W�F�N�g�擾

			///-------------------------------------------
			// ���ׂẴL�����N�^�[�̍��W���擾
			prevPos = childObject[i].transform.position;
			prevRot = childObject[i].transform.rotation;
			///���̕���������Update()�ɂ���Ȃ����񂩂�
			///-------------------------------------------
		}
		//���ׂĂ̎q�I�u�W�F�N�g���A�N�e�B�u
		foreach (GameObject gamObj in childObject)
		{
			gamObj.SetActive(false);
		}
		//�ŏ��͂ЂƂ����A�N�e�B�u�����Ă���
		childObject[index].SetActive(true);
	}

	void Update()

	{
		prevPos = childObject[index].transform.position;
		prevRot = childObject[index].transform.rotation;


		if (Input.GetKeyDown("c"))
		{
			//���݂̃A�N�e�B�u�Ȏq�I�u�W�F�N�g���A�N�e�B�u
			childObject[index].SetActive(false);
			index++;
			
			//�q�I�u�W�F�N�g�����ׂĐ؂�ւ�����܂��ŏ��̃I�u�W�F�N�g�ɖ߂�
			if (index == o_max) { index = 0; }

			///-------------------------------------------
			// �L�����N�^�[�̍��W��K��������
			childObject[index].transform.position = prevPos;
			childObject[index].transform.rotation = prevRot;


			///-------------------------------------------

			//���̃I�u�W�F�N�g���A�N�e�B�u��
			childObject[index].SetActive(true);
			
		}
	}
}