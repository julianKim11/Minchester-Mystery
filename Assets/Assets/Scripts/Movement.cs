using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private float speed = 4f;
    private Vector3 finalPosition;
    [SerializeField]private Collider2D squareCollider;

    private void Start()
    {
        finalPosition = this.transform.position;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            clickPosition.z = this.transform.position.z;
            if (squareCollider.OverlapPoint(clickPosition))
            {
                finalPosition = clickPosition;
            }
        }
        this.transform.position = Vector3.MoveTowards(this.transform.position, finalPosition, speed * Time.deltaTime);
    }
}
