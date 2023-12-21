using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public GameObject[] prefabs;
    List<GameObject>[] poolList;

    void Start()
    {
        //리스트 배열 크기, 내용 초기화
        poolList = new List<GameObject>[prefabs.Length];
        
        for(int i = 0;i < poolList.Length; i++) 
        {
            poolList[i] = new List<GameObject>();
        }
    }

    //직접적인 풀링을 담당할 함수
    public GameObject Get(int index)
    {
        GameObject obj = null;
        //오브젝트 풀링으로 생성된 모든 오브젝트를 검사
        foreach (GameObject item in poolList[index])
        {
            // 비활성화 된(재활용 가능한)오브젝트가 있다면
            if (!item.activeSelf)
            {
                obj = item;// 재활용
                obj.SetActive(true); // 활성화
                break;// 함수 종료
            }
        }

        //재활용 가능한 오브젝트가 없다면
        if (!obj)
        {
            //오브젝트를 새롭게 생성 후
            obj = Instantiate(prefabs[index],transform);

            // 풀 리스트에 추가
            poolList[index].Add(obj);
        }

        return obj;
    }
}

