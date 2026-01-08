using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] 


public struct HexCoordinates
{
    // Axial 좌표계
    [SerializeField] private int x, z;

    public int X => x;
    public int Z => z;

    // Cube 좌표계의 Y값
    public int Y => -x - z;

    public HexCoordinates(int x, int z)
    {
        this.x = x;
        this.z = z;
    }
    const float OuterRadius = 5f;
    const float InnerRadius = OuterRadius * 0.866025404f;

    //좌표 보정용 팩토리
    public static HexCoordinates FromOffsetCoordinates(int x, int z)
    {
        return new HexCoordinates(x - z / 2, z);
    }

    public static HexCoordinates FromPosition(Vector3 position)
    {
        // 1. 월드 좌표를 육각형 그리드 기준(1칸 크기)으로 정규화
        // x축: 내접원 지름(Inner * 2)으로 나눔
        float x = position.x / (InnerRadius * 2f);

        // y축(가상의 축): -x
        float y = -x;

        // z축(줄): 외접원 반지름 * 1.5로 나눔 (육각형의 세로 겹침 고려)
        float offset = position.z / (OuterRadius * 1.5f);

        // 2. 지그재그(Shift) 보정 역산
        x -= offset;
        y -= offset;

        // 3. 반올림해서 정수 좌표 찾기
        int iX = Mathf.RoundToInt(x);
        int iY = Mathf.RoundToInt(y);
        int iZ = Mathf.RoundToInt(-x - y);

        // 4. 반올림 오차 보정 (x + y + z = 0 규칙이 깨졌을 때 가장 오차가 큰 놈을 수정)
        if (iX + iY + iZ != 0)
        {
            float dX = Mathf.Abs(x - iX);
            float dY = Mathf.Abs(y - iY);
            float dZ = Mathf.Abs(-x - y - iZ);

            if (dX > dY && dX > dZ)
            {
                iX = -iY - iZ;
            }
            else if (dZ > dY)
            {
                iZ = -iX - iY;
            }
        }

        return new HexCoordinates(iX, iZ);
    }

    public override string ToString()
    {
        return "(" + X.ToString() + ", " + Y.ToString() + ", " + Z.ToString() + ")";
    }

    public string ToStringOnSeparateLines()
    {
        return X.ToString() + "\n" + Y.ToString() + "\n" + Z.ToString();
    }


}

