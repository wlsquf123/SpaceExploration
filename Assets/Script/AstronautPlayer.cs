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

        void Update()
        {
            float xInput = 0f;
            float zInput = 0f;
            float yInput = 0f;

            if (Input.GetKey(KeyCode.W)) zInput = 1f;
            if (Input.GetKey(KeyCode.S)) zInput = -1f;
            if (Input.GetKey(KeyCode.A)) xInput = -1f;
            if (Input.GetKey(KeyCode.D)) xInput = 1f;
            if (Input.GetKey(KeyCode.LeftShift)) yInput = -1f;
            if (Input.GetKey(KeyCode.Space)) yInput = 1f;

            Vector3 move = (transform.forward * zInput) + (transform.right * xInput) + (transform.up * yInput);
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

            if (Input.GetMouseButton(1))
            {
                // 유니티 자체 설정에 매핑된 마우스 이동 값을 가져옴
                float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
                float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

                // 좌우 회전: 플레이어 오브젝트 자체를 Y축(Vector3.up) 기준으로 회전
                transform.Rotate(Vector3.up * mouseX);
                firstPersonCamera.transform.Rotate(Vector3.left * mouseY);

                return;
            }
        }



    }
}