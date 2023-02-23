using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform player; // 카메라가 따라갈 대상
    public Vector3 offset; // 카메라와 대상 사이의 거리

    public float smoothTime = 0.3f; // 카메라의 부드러운 이동을 제어하는 값
    private Vector3 velocity = Vector3.zero; // 부드러운 이동 계산에 사용되는 변수
    private void Start()
    {
        player = FindObjectOfType<Player>().transform;
    }
    private void LateUpdate()
    {
        // 카메라의 위치를 플레이어의 위치와 offset을 더한 값으로 설정합니다.
        Vector3 targetPosition = player.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

        if (Input.GetMouseButton(1)) //우클릭시만
        {
            Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")); //마우스 위아래 수치를 vector2로 mouseDelta에 저장한다.
            Vector3 camAngle = transform.rotation.eulerAngles; //카메라 position을 값을 Euler값으로 변환해 둔다.
            float x = camAngle.x - mouseDelta.y;
            //각도 제한
            if (x < 180f)
            {
                x = Mathf.Clamp(x, -1f, 70f);
            }
            else
            {
                x = Mathf.Clamp(x, 355f, 361f);
            }
            transform.rotation = Quaternion.Euler(x, camAngle.y + mouseDelta.x, camAngle.z);//camAngle의 새로운값을 cameraArm.rotation에 넣어준다.
        }
    }
}
