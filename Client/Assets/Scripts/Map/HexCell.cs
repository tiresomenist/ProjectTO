using UnityEngine;
using UnityEngine.UI;

public class HexCell : MonoBehaviour
{
    public HexCoordinates coordinates;
    public Text label;

    public Color defaultColor = Color.white;    //시작 색깔, 임시용
    public Color touchedColor = Color.magenta; // 선택 시 바뀔 색
    private MeshRenderer meshRenderer;
    public HexUnit Unit { get; set; }
    void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        if (meshRenderer != null)
        {
            //시작 색깔 초기화
            defaultColor = meshRenderer.material.color;
        }
    }

    public void SetCoordinates(HexCoordinates coords)
    {
        coordinates = coords;

        if (label != null)
        {
            label.text = coords.ToStringOnSeparateLines();
        }
    }

    public void EnableHighlight()
    {
        if (meshRenderer != null)
        {
            meshRenderer.material.color = touchedColor;
        }
    }

    public void DisableHighlight()
    {
        if (meshRenderer != null)
        {
            meshRenderer.material.color = defaultColor;
        }
    }
}