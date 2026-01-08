using UnityEngine;

public class HexMapCamera : MonoBehaviour
{
    float moveSpeed = 20f;   // 이동 속도
    float zoomSpeed = 10f;   // 줌 속도
    float minZoom = 5f;      // 최대 줌인
    float maxZoom = 40f;     // 최대 줌아웃

    void Update()
    {
        // 1. WASD 이동
        float xDelta = Input.GetAxis("Horizontal"); // A, D, Left, Right
        float zDelta = Input.GetAxis("Vertical");   // W, S, Up, Down

        if (xDelta != 0 || zDelta != 0)
        {
            AdjustPosition(xDelta, zDelta);
        }

        // 2. 마우스 휠 줌
        float zoomDelta = Input.GetAxis("Mouse ScrollWheel");
        if (zoomDelta != 0f)
        {
            AdjustZoom(zoomDelta);
        }
    }

    void AdjustPosition(float xDelta, float zDelta)
    {
        Vector3 direction = new Vector3(xDelta, 0f, zDelta).normalized;
        float distance = moveSpeed * Time.deltaTime;

        // 카메라의 현재 높이(줌)에 따라 이동 속도 보정 (멀리 볼수록 빨리 이동)
        // transform.position += direction * distance; 

        // x, z 축만 이동
        Vector3 pos = transform.position;
        pos += direction * distance;
        transform.position = pos;
    }

    void AdjustZoom(float delta)
    {
        // 줌은 Y축 이동으로 처리
        Vector3 pos = transform.position;
        pos.y -= delta * zoomSpeed;

        // 줌 제한 (너무 가까이 가거나 너무 멀리 못 가게)
        pos.y = Mathf.Clamp(pos.y, minZoom, maxZoom);

        transform.position = pos;
    }
}