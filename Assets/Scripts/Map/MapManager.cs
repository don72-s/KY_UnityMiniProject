using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MapManager : MonoBehaviour
{
   
    //todo : ���� ������ ���� so������ �������ֱ�.
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

    //�ʱ� ��� ����.
    private void Start() {

        removeDistance = blockLength;

        CreateNewTile();
        CreateNewTile();
        CreateNewTile();
        CreateNewTile();
        CreateNewTile();

    }

    #region ���� �׽�Ʈ�� ����� �ڵ�
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
    /// ���� �ڿ� ��� �ϳ� �߰�.
    /// </summary>
    public void CreateNewTile() {

        if (mapBlocks.Count == 0) {
            mapBlocks.Add(Instantiate(blockPrefab, blockParent));
            oldestBlock = mapBlocks[0].transform;
        } else {
            mapBlocks.Add(Instantiate(blockPrefab, mapBlocks[mapBlocks.Count - 1].transform.position + Vector3.forward * blockLength, Quaternion.identity, blockParent));        
        }

        mapBlocks[mapBlocks.Count - 1].ResetBlock(spawnInfoSO, carPrefab);

        //todo : instantiate�� ������Ʈ Ǯ�κ��� �������°ɷ� ��ȯ.

    }

    /// <summary>
    /// ���� ������ Ÿ�� ����
    /// </summary>
    public void RemoveOldestTile() {

        Block blockObj = mapBlocks[0];
        mapBlocks.RemoveAt(0);
        Destroy(blockObj.gameObject);

        //todo : ���� �� �ּ�����
        //blockObj.ResetBlock(spawnInfoSO, carPrefab);

    }

}
