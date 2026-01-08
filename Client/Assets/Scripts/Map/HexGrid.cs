using UnityEngine;
using UnityEngine.EventSystems; // UI 클릭 무시용

public class HexGrid : MonoBehaviour
{
    public int width = 6;
    public int height = 6;

    public HexCell cellPrefab;

    private HexCell currentCell;

    public HexUnit unitPrefab;

    private HexUnit selectedUnit;

    private const float OuterRadius = 5f;
    private const float InnerRadius = OuterRadius * 0.866025404f;

    void Awake()
    {
        for (int z = 0, i = 0; z < height; z++)
        {
            for (int x = 0; x < width; x++)
            {
                CreateCell(x, z, i++);
            }
        }
    }

    void Update()
    {
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            HandleInput(0);
        }
        else if (Input.GetMouseButtonDown(1))
        {
            HandleInput(1);
        }
    }

    void HandleInput(int mouseButton)
    {
        Ray inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(inputRay, out hit))
        {
            HexCell cell = hit.collider.GetComponent<HexCell>();

            if (cell != null)
            {
                if (mouseButton == 0)
                    HandleLeftClick(cell);
                else
                    HandleRightClick(cell);
            }
        }
    }
    void HandleLeftClick(HexCell cell)
    {
        if (cell.Unit != null)
        {
            SelectUnit(cell.Unit);
        }
        else
        {
            AddUnit(cell);
        }
    }

    void HandleRightClick(HexCell cell)
    {
        if (selectedUnit != null && cell.Unit == null)
        {
            selectedUnit.SetLocation(cell);
            ClearSelection(); 
        }
    }

    void SelectUnit(HexUnit unit)
    {
        
        if (selectedUnit != null)
        {
            selectedUnit.DisableHighlight();
        }

        
        selectedUnit = unit;
        selectedUnit.EnableHighlight();

        Debug.Log("Unit Selected!");
    }
    void TouchCell(HexCell cell)
    {
        if (currentCell != null) currentCell.DisableHighlight();
        currentCell = cell;
        currentCell.EnableHighlight();

        if (cell.Unit == null)
        {
            AddUnit(cell);
        }
        else
        {
            Debug.Log("이미 유닛이 있는 자리입니다!");
        }
    }
    void AddUnit(HexCell cell)
    {
        HexUnit unit = Instantiate(unitPrefab);

        unit.transform.SetParent(transform, false);
        unit.Initialize();

        unit.SetLocation(cell);
    }
    void CreateCell(int x, int z, int i)
    {
        Vector3 position;
        position.x = (x + z * 0.5f - z / 2) * (InnerRadius * 2f);
        position.y = 0f;
        position.z = z * (OuterRadius * 1.5f);

        HexCell cell = Instantiate(cellPrefab);
        cell.transform.SetParent(transform, false);
        cell.transform.localPosition = position;

        cell.SetCoordinates(HexCoordinates.FromOffsetCoordinates(x, z));
    }

    void ClearSelection()
    {
        if (selectedUnit != null)
        {
            selectedUnit.DisableHighlight();
            selectedUnit = null;
        }
    }
}