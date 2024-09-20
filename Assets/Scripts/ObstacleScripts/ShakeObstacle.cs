using UnityEngine;

public class ShakeObstacle : MonoBehaviour
{
    // ��鸲 �ӵ� �� ���� ����
    public float ShakeSpeed = 110f;           // ��鸲 �ӵ�
    public float MinShakeSpeed = 20f;        // �ּ� �ӵ�
    private int ShakeDirection = 1;          // ��鸲 ����

    // ���� ����
    public float MaxZPosition = 10f;         // �ִ� Z ��ġ

    // Update �޼ҵ�� �� �����Ӹ��� ȣ���
    void Update()
    {
        // ���� ��ġ�� ������� ���ο� Z ��ġ ���
        float newPosition = transform.position.z + ShakeDirection * ShakeSpeed * Time.deltaTime;

        // Z ��ġ�� �ִ� �� �ּ� ������ ����
        newPosition = Mathf.Clamp(newPosition, 0f, MaxZPosition);

        // ���� ���� Z ��ġ�� ������Ʈ�� ��ġ ������Ʈ
        transform.position = new Vector3(transform.position.x, transform.position.y, newPosition);

        // ���� �ִ� �Ǵ� �ּ� ���� �����ϸ�
        if (newPosition >= MaxZPosition || newPosition <= 0f)
        {
            // ��鸲 ���� ��ȯ
            ShakeDirection *= -1;

            // ��鸲 �ӵ� ����
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
