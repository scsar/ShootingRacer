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
        // �ʱ� ��ġ ����
        startPosition = transform.position;

        // �ڷ�ƾ ����
        StartCoroutine(MoveAndReturn());
    }

    IEnumerator MoveAndReturn()
    {
        while (true)
        {
            // ���� �Ÿ� �̵�
            yield return MoveObject(transform.position + Vector3.forward * moveDistance, moveSpeed);

            // ���� �ð� ���
            yield return new WaitForSeconds(returnTime);

            // �ʱ� ��ġ�� ���ƿ�
            yield return MoveObject(startPosition, moveSpeed);
        }
    }

    IEnumerator MoveObject(Vector3 targetPosition, float speed)
    {
        float elapsedTime = 0f;

        while (elapsedTime < 1f)
        {
            //Lerp�� �ε巴�� �����ϼ��ֵ��� ��
            transform.position = Vector3.Lerp(transform.position, targetPosition, elapsedTime);
            elapsedTime += Time.deltaTime * speed;
            yield return null;
        }

        // ��Ȯ�� ��ġ�� ����
        transform.position = targetPosition;
    }
}
