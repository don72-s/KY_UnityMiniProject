using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.EditorTools;
using UnityEngine;

public class MapManager : MonoBehaviour
{

    PoolManager poolManager;
   
    //todo : ���� ������ ���� so������ �������ֱ�.
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

    //�ʱ� ��� ����.
    private void Start() {

        poolManager = PoolManager.GetInstance();

        removeDistance = blockLength;

        CreateNewTile();
        CreateNewTile();
        CreateNewTile();
        CreateNewTile();
        CreateNewTile();

    }

    #region ���� �׽�Ʈ�� ����� �ڵ�

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
    /// ���� ������ Ÿ�� ����
    /// </summary>
    public void RemoveOldestTile() {

        Block blockObj = mapBlocks[0];
        mapBlocks.RemoveAt(0);

        blockObj.RemoveBlock();

    }

}
