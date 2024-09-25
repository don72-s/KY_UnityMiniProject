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

    List<Block> mapBlocks = new List<Block>(10);

    //�ʱ� ��� ����.
    private void Start() {

        CreateEndTile();
        CreateEndTile();
        CreateEndTile();
        CreateEndTile();
        CreateEndTile();

    }

    #region ���� �׽�Ʈ�� ����� �ڵ�
    float time = 0;

    [Header("Debug")]
    [SerializeField]
    float aliveTime = 4;

    private void FixedUpdate() {

        foreach (var block in mapBlocks) {

            block.transform.Translate(Vector3.back * 10 * Time.fixedDeltaTime);

        }

    }



    private void Update() {

        if (time > aliveTime) {
            time = 0;
            RemoveFirstTile();
            CreateEndTile();
        } else {
            time += Time.deltaTime;
        }

    }

    #endregion


    /// <summary>
    /// ���� �ڿ� ��� �ϳ� �߰�.
    /// </summary>
    public void CreateEndTile() {

        if (mapBlocks.Count == 0) {
            mapBlocks.Add(Instantiate(blockPrefab, blockParent));
        } else {
            mapBlocks.Add(Instantiate(blockPrefab, mapBlocks[mapBlocks.Count - 1].transform.position + Vector3.forward * blockLength, Quaternion.identity, blockParent));        
        }

        //todo : instantiate�� ������Ʈ Ǯ�κ��� �������°ɷ� ��ȯ.

    }

    /// <summary>
    /// ���� ������ Ÿ�� ����
    /// </summary>
    public void RemoveFirstTile() {

        Block blockObj = mapBlocks[0];
        mapBlocks.RemoveAt(0);
        Destroy(blockObj.gameObject);

        //todo : ���� �� �ּ�����
        //blockObj.ResetBlock();

    }

}
