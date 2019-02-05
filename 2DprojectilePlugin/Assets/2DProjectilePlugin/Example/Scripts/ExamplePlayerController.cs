using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UpGamesWeapon2D.Math;

namespace UpGamesWeapon2D.Example
{

        [RequireComponent(typeof(Rigidbody2D))]
        public class ExamplePlayerController : MonoBehaviour
        {
            public float speed;
            public float turningSpeed;
            public float cameraSpeed = 10;
            public float zoomSpeed = 10;
            public float minZoom = 5;
            public float maxZoom = 55;
            Rigidbody2D rb;
            private void Start()
            {
                rb = GetComponent<Rigidbody2D>();
                rb.gravityScale = 0;
            }
            private void FixedUpdate()
            {
                Move();
                Rotate();
                MoveCamera();
            }

            void Move()
            {
                Vector2 moveVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
                if(rb != null)
                {
                    rb.MovePosition((Vector2)transform.position + moveVector * speed * Time.deltaTime);
                }
                else
                {
                    Debug.LogError("Add a Rigidbody2D component!");
                }
                
                
            }
            void Rotate()
            {
                Vector2 rotationVector = transform.position;
                if (Camera.main != null)
                {
                    rotationVector = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                }
                else
                {
                    Debug.LogError("Add a Camera with the tag MainCamera to the scene!");
                }
                
                Quaternion desiredRot = MathOperations.LookAt2D(rotationVector, transform.position, -90);
                transform.rotation = Quaternion.Slerp(transform.rotation, desiredRot, turningSpeed * Time.deltaTime);
            }
            void MoveCamera()
            {
                if(Camera.main != null)
                {
                    Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, new Vector3(transform.position.x, transform.position.y, Camera.main.transform.position.z), cameraSpeed * Time.deltaTime);
                }
                float mouseScroll = Input.GetAxis("Mouse ScrollWheel");
                Camera.main.orthographicSize -= mouseScroll * zoomSpeed;
                Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, minZoom,maxZoom);
            }
        }

  
}


