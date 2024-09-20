using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnObstacle : MonoBehaviour
{
    public float moveDistance = 5f;
    public float moveSpeed = 2f;
    public float returnTime = 2f;

    private Vector3 startPosition;

    void Start()
    {
        // 초기 위치 저장
        startPosition = transform.position;

        // 코루틴 시작
        StartCoroutine(MoveAndReturn());
    }

    IEnumerator MoveAndReturn()
    {
        while (true)
        {
            // 일정 거리 이동
            yield return MoveObject(transform.position + Vector3.forward * moveDistance, moveSpeed);

            // 일정 시간 대기
            yield return new WaitForSeconds(returnTime);

            // 초기 위치로 돌아옴
            yield return MoveObject(startPosition, moveSpeed);
        }
    }

    IEnumerator MoveObject(Vector3 targetPosition, float speed)
    {
        float elapsedTime = 0f;

        while (elapsedTime < 1f)
        {
            //Lerp로 부드럽게 움직일수있도록 함
            transform.position = Vector3.Lerp(transform.position, targetPosition, elapsedTime);
            elapsedTime += Time.deltaTime * speed;
            yield return null;
        }

        // 정확한 위치로 보정
        transform.position = targetPosition;
    }
}
