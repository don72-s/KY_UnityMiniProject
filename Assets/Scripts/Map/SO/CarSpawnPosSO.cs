using UnityEngine;

[CreateAssetMenu(menuName = "SpawnPoints")]
public class CarSpawnPosSO : ScriptableObject
{

    [Header("half distance of both cars on x")]
    //�¿� ���� �Ÿ��� ���ݰŸ� ������
    [SerializeField]
    float horizontalOffset;
    //�ٴ� Ÿ�� �߽����κ��� ������ �յڰŸ� ���� ������
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
