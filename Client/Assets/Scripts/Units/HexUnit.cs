using UnityEngine;

public class HexUnit : MonoBehaviour
{
    public HexCell Location { get; private set; }

    // 원래 색상 저장용
    private Color defaultColor;
    private MeshRenderer meshRenderer;

    void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        // 나중에 색을 되돌리기 위해 저장 (Material 인스턴스 문제 방지)
        defaultColor = meshRenderer.material.color;
    }

    // 초기화 함수 (소환될 때 호출)
    public void Initialize()
    {
        defaultColor = meshRenderer.material.color;
    }

    public void SetLocation(HexCell cell)
    {
        // 원래 있던 타일에서 나를 지움
        if (Location)
        {
            Location.Unit = null;
        }

        Location = cell;
        cell.Unit = this;
        transform.position = cell.transform.position;
    }

    // 선택됐을 때 (빨간색)
    public void EnableHighlight()
    {
        meshRenderer.material.color = Color.red;
    }

    // 선택 해제됐을 때 (원래 색)
    public void DisableHighlight()
    {
        meshRenderer.material.color = defaultColor;
    }
}