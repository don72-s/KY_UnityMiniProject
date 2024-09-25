using UnityEngine;

[CreateAssetMenu(menuName = "SpawnPoints")]
public class CarSpawnPosSO : ScriptableObject
{

    [Header("half distance of both cars on x")]
    //좌우 차간 거리의 절반거리 오프셋
    [SerializeField]
    float horizontalOffset;
    //바닥 타일 중심으로부터 생성될 앞뒤거리 단위 오프셋
    [Header("half distance of both cars on y")]
    [SerializeField]
    float verticalOffset;

    [Space(5)]
    [SerializeField]
    int lineCount;
    [SerializeField]
    int carCount;

    public float VerticalOffset { get { return verticalOffset; } private set { } }
    public float HorizontalOffset { get { return horizontalOffset; } private set { } }
    public int LineCount { get { return lineCount; } private set { } }
    public int CarCount { get { return carCount; } private set { } }

}
