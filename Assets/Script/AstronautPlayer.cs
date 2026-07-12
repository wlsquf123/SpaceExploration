using UnityEngine;

namespace AstronautPlayer
{
    public class AstronautPlayer : MonoBehaviour
    {
        public Animator an;

        public float speed = 6.0f; // 속도
        public float mouseSensitivity = 2.0f; // 감도

        private Vector3 moveDirection = Vector3.zero;
        public float gravity = 20.0f;

        public Transform firstPersonCamera;

        // 카메라의 상하 회전각을 누적해서 저장할 변수
        private float currentXRotation = 0f;

        void Update()
        {
            float xInput = 0f;
            float zInput = 0f;

            if (Input.GetKey(KeyCode.W)) zInput = 1f;
            if (Input.GetKey(KeyCode.S)) zInput = -1f;
            if (Input.GetKey(KeyCode.A)) xInput = -1f;
            if (Input.GetKey(KeyCode.D)) xInput = 1f;

            Vector3 move = (transform.forward * zInput) + (transform.right * xInput);

            moveDirection = move * speed * Time.deltaTime;

            transform.position += moveDirection;


            if (xInput == 0 && zInput == 0)
            {
                an.SetInteger("AnimationPar", 0);
            }
            else
            {
                an.SetInteger("AnimationPar", 1);
            }


            // ---------------- [3. 마우스 회전 로직 (우클릭)] ----------------
            // Input.GetMouseButton(1)은 마우스 우클릭을 '누르고 있는 동안' 계속 true를 반환합니다.
            if (Input.GetMouseButton(1))
            {
                // 유니티 자체 설정에 매핑된 마우스 이동 값을 가져옴
                float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
                float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

                // 좌우 회전: 플레이어 오브젝트 자체를 Y축(Vector3.up) 기준으로 회전
                transform.Rotate(Vector3.up * mouseX);
                firstPersonCamera.transform.Rotate(Vector3.left * mouseY);

                return;
                // 상하 회전: 카메라만 X축 기준으로 회전
                // 마우스를 위로 올리면(양수) 고개를 들어야 하므로 값을 빼줍니다.
                currentXRotation -= mouseY;

                // 고개가 뒤로 완전히 꺾이는 것을 방지 (-90도는 천장, 90도는 바닥)
                currentXRotation = Mathf.Clamp(currentXRotation, -90f, 90f);

                // 카메라의 로컬 회전값(부모인 플레이어 기준의 회전)을 적용
                firstPersonCamera.localRotation = Quaternion.Euler(currentXRotation, 0f, 0f);
            }
        }


        
    }
}