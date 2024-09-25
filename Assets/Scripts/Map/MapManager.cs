using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MapManager : MonoBehaviour
{
   
    //todo : 여러 종류의 맵을 so등으로 가지고있기.
    [SerializeField]
    Block blockPrefab;
    [SerializeField]
    float blockLength;
    [SerializeField]
    Transform blockParent;

    [SerializeField]
    CarSpawnPosSO spawnInfoSO;
    [SerializeField]
    Car carPrefab;

    float removeDistance;

    List<Block> mapBlocks = new List<Block>(10);
    Transform oldestBlock;

    //초기 블록 생성.
    private void Start() {

        removeDistance = blockLength;

        CreateNewTile();
        CreateNewTile();
        CreateNewTile();
        CreateNewTile();
        CreateNewTile();

    }

    #region 생성 테스트용 디버그 코드
    float time = 0;

    [Header("Debug")]
    [SerializeField]
    float aliveTime = 4;

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
            mapBlocks.Add(Instantiate(blockPrefab, blockParent));
            oldestBlock = mapBlocks[0].transform;
        } else {
            mapBlocks.Add(Instantiate(blockPrefab, mapBlocks[mapBlocks.Count - 1].transform.position + Vector3.forward * blockLength, Quaternion.identity, blockParent));        
        }

        mapBlocks[mapBlocks.Count - 1].ResetBlock(spawnInfoSO, carPrefab);

        //todo : instantiate를 오브젝트 풀로부터 빌려오는걸로 전환.

    }

    /// <summary>
    /// 가장 오래된 타일 제거
    /// </summary>
    public void RemoveOldestTile() {

        Block blockObj = mapBlocks[0];
        mapBlocks.RemoveAt(0);
        Destroy(blockObj.gameObject);

        //todo : 개발 후 주석해제
        //blockObj.ResetBlock(spawnInfoSO, carPrefab);

    }

}
