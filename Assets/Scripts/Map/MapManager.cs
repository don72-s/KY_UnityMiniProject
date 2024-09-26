using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.EditorTools;
using UnityEngine;

public class MapManager : MonoBehaviour
{

    PoolManager poolManager;
   
    //todo : 여러 종류의 맵을 so등으로 가지고있기.
    [SerializeField]
    Block blockPrefab;
    [SerializeField]
    float blockLength;
    [SerializeField]
    Transform blockParent;

    [SerializeField]
    CarSpawnPosSO spawnInfoSO;

    float removeDistance;

    List<Block> mapBlocks = new List<Block>(10);
    Transform oldestBlock;

    //초기 블록 생성.
    private void Start() {

        poolManager = PoolManager.GetInstance();

        removeDistance = blockLength;

        CreateNewTile();
        CreateNewTile();
        CreateNewTile();
        CreateNewTile();
        CreateNewTile();

    }

    #region 생성 테스트용 디버그 코드

    private void FixedUpdate() {

        foreach (var block in mapBlocks) {

            block.transform.Translate(10 * Time.fixedDeltaTime * Vector3.back);

        }

    }


    private void Update() {

        if (oldestBlock.transform.position.z <= -removeDistance) { 
        
            RemoveOldestTile();
            CreateNewTile();
            oldestBlock = mapBlocks[0].transform;

        }

    }

    #endregion


    /// <summary>
    /// 가장 뒤에 블록 하나 추가.
    /// </summary>
    public void CreateNewTile() {

        if (mapBlocks.Count == 0) {
            mapBlocks.Add(poolManager.GetObj<Block>());
            mapBlocks[0].transform.SetParent(blockParent);
            mapBlocks[0].transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
            oldestBlock = mapBlocks[0].transform;
        } else {

            Block tmp = poolManager.GetObj<Block>();
            tmp.transform.SetPositionAndRotation(mapBlocks[mapBlocks.Count - 1].transform.position + Vector3.forward * blockLength, Quaternion.identity);
            tmp.transform.SetParent(blockParent);
            mapBlocks.Add(tmp);
        }

        mapBlocks[mapBlocks.Count - 1].SettingBlock(spawnInfoSO);

    }

    /// <summary>
    /// 가장 오래된 타일 제거
    /// </summary>
    public void RemoveOldestTile() {

        Block blockObj = mapBlocks[0];
        mapBlocks.RemoveAt(0);

        blockObj.RemoveBlock();

    }

}
