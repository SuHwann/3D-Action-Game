using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform player; // ī�޶� ���� ���
    public Vector3 offset; // ī�޶�� ��� ������ �Ÿ�

    public float smoothTime = 0.3f; // ī�޶��� �ε巯�� �̵��� �����ϴ� ��
    private Vector3 velocity = Vector3.zero; // �ε巯�� �̵� ��꿡 ���Ǵ� ����
    private void Start()
    {
        player = FindObjectOfType<Player>().transform;
    }
    private void LateUpdate()
    {
        // ī�޶��� ��ġ�� �÷��̾��� ��ġ�� offset�� ���� ������ �����մϴ�.
        Vector3 targetPosition = player.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

        if (Input.GetMouseButton(1)) //��Ŭ���ø�
        {
            Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")); //���콺 ���Ʒ� ��ġ�� vector2�� mouseDelta�� �����Ѵ�.
            Vector3 camAngle = transform.rotation.eulerAngles; //ī�޶� position�� ���� Euler������ ��ȯ�� �д�.
            float x = camAngle.x - mouseDelta.y;
            //���� ����
            if (x < 180f)
            {
                x = Mathf.Clamp(x, -1f, 70f);
            }
            else
            {
                x = Mathf.Clamp(x, 355f, 361f);
            }
            transform.rotation = Quaternion.Euler(x, camAngle.y + mouseDelta.x, camAngle.z);//camAngle�� ���ο�� cameraArm.rotation�� �־��ش�.
        }
    }
}
