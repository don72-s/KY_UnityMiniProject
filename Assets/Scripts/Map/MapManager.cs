using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class MapManager : MonoBehaviour {

    PoolManager poolManager;

    [SerializeField]
    Transform maps;

    [SerializeField]
    float blockLength;
    [SerializeField]
    Transform blockParent;

    [SerializeField]
    CarSpawnPosSO spawnInfoSO;

    float removeDistance;

    List<Block> mapBlocks = new List<Block>(10);
    Transform oldestBlock;

    Vector3 startPos;
    Vector3 EndPos = Vector3.zero;
    Transform slideBlock;

    //초기 블록 생성.
    private void Start() {

        poolManager = PoolManager.GetInstance();

        removeDistance = blockLength * 1.5f;

        CreateEmptyFlatTile();
        CreateEmptyFlatTile();
        CreateRandomTile();
        CreateRandomTile();
        CreateRandomTile();
        CreateRandomTile();
        CreateRandomTile();
        CreateRandomTile();
        CreateRandomTile();
        CreateRandomTile();


    }



    #region 생성 테스트용 디버그 코드

    private void FixedUpdate() {

        foreach (var block in mapBlocks) {

            block.transform.Translate(10 * Time.fixedDeltaTime * Vector3.back, Space.World);

        }

    }




    private void Update() {

        if (oldestBlock.transform.position.z <= -removeDistance) {

            RemoveOldestTile();
            CreateRandomTile();
            oldestBlock = mapBlocks[0].transform;
            if (mapBlocks[1].GetType() == typeof(SlideBlock)) {

                slideBlock = mapBlocks[1].transform;
                startPos = EndPos;
                EndPos = startPos + new Vector3(0, mapBlocks[1].heightOffset, 0);

            } else {
                startPos = EndPos;
                maps.position = startPos;

            }
        }

        if (startPos != EndPos) {

            float val = (-slideBlock.position.z + (blockLength) / 2) / blockLength;
            maps.position = Vector3.LerpUnclamped(startPos, EndPos, val);

        }


    }

    #endregion

    public Block CreateEmptyFlatTile() {

        Block block = poolManager.GetObj<Block>();
        CreateTile(block);
        return block;

    }
    public void CreateFlatTile() {

        Block block = CreateEmptyFlatTile();
        SetupObstacle(block);

    }
    void CreateTile(Block _block) {
       
        if (mapBlocks.Count == 0) {

            mapBlocks.Add(_block);
            mapBlocks[0].transform.SetParent(blockParent);
            mapBlocks[0].transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
            oldestBlock = mapBlocks[0].transform;

        } else {

            _block.transform.SetPositionAndRotation(mapBlocks[mapBlocks.Count - 1].transform.position + Vector3.forward * blockLength + mapBlocks[mapBlocks.Count - 1].nextTileOffset, Quaternion.identity);
            _block.transform.SetParent(blockParent);
            mapBlocks.Add(_block);

        }

    }
    void SetupObstacle(Block _block) { 
    
        _block.SettingBlock(spawnInfoSO);

    }


    /// <summary>
    /// 가장 뒤에 랜덤으로 블록 하나 추가.
    /// </summary>
    public void CreateRandomTile() {

        int v = UnityEngine.Random.Range(0, 3);
        Block block = null;

        if (v == 0) block = poolManager.GetObj<Block>();
        else if (v == 1) block = poolManager.GetObj<SlideBlock>("Up");
        else if (v == 2) block = poolManager.GetObj<SlideBlock>("Down");

        CreateTile(block);
        SetupObstacle(block);

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
