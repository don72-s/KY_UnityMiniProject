using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideBlock : Block
{

    [SerializeField]
    Vector3 offset;
    [SerializeField]
    float rotateAngle;
    public override void SettingBlock(CarSpawnPosSO _spawnInfo) {

        base.SettingBlock(_spawnInfo);

        transform.Translate(offset, Space.World);
        transform.Rotate(Vector3.right, rotateAngle);
        nextTileOffset = offset;

    }
}
