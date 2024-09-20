using UnityEngine;

public class ShakeObstacle : MonoBehaviour
{
    // 흔들림 속도 및 관련 변수
    public float ShakeSpeed = 110f;           // 흔들림 속도
    public float MinShakeSpeed = 20f;        // 최소 속도
    private int ShakeDirection = 1;          // 흔들림 방향

    // 제한 변수
    public float MaxZPosition = 10f;         // 최대 Z 위치

    // Update 메소드는 매 프레임마다 호출됨
    void Update()
    {
        // 현재 위치를 기반으로 새로운 Z 위치 계산
        float newPosition = transform.position.z + ShakeDirection * ShakeSpeed * Time.deltaTime;

        // Z 위치를 최대 및 최소 값으로 제한
        newPosition = Mathf.Clamp(newPosition, 0f, MaxZPosition);

        // 새로 계산된 Z 위치로 오브젝트의 위치 업데이트
        transform.position = new Vector3(transform.position.x, transform.position.y, newPosition);

        // 만약 최대 또는 최소 값에 도달하면
        if (newPosition >= MaxZPosition || newPosition <= 0f)
        {
            // 흔들림 방향 전환
            ShakeDirection *= -1;

            // 흔들림 속도 감소
            if (ShakeSpeed > MinShakeSpeed)
            {
                ShakeSpeed -= 10f;
            }
            else
            {
                ShakeSpeed = MinShakeSpeed;
            }
        }
    }
}
