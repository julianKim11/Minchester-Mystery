using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public Transform outsideCameraPosition;
    public Transform livingRoomCameraPosition;
    public Transform kitchenCameraPosition;
    public Transform patioCameraPosition;
    public float cameraTransitionSpeed = 1f;

    private int currentZoneIndex = 0;
    private Transform[] zonePositions;

    private void Start()
    {
        // Inicializar el arreglo de posiciones de zona
        zonePositions = new Transform[4];
        zonePositions[0] = outsideCameraPosition;
        zonePositions[1] = livingRoomCameraPosition;
        zonePositions[2] = kitchenCameraPosition;
        zonePositions[3] = patioCameraPosition;

        // Posicionar la cámara en la zona inicial
        transform.position = zonePositions[currentZoneIndex].position;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            // Mover hacia la izquierda
            if (currentZoneIndex > 0)
            {
                currentZoneIndex--;
                transform.position = zonePositions[currentZoneIndex].position;
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            // Mover hacia la derecha
            if (currentZoneIndex < zonePositions.Length - 1)
            {
                currentZoneIndex++;
                transform.position = zonePositions[currentZoneIndex].position;
            }
        }
    }

    public void ChangeCameraPosition(Transform newPosition)
    {
        //transform.position = newPosition.position;
        StartCoroutine(TransitionToPosition(newPosition));
    }
    private IEnumerator TransitionToPosition(Transform newPosition)
    {
        Vector3 startPosition = transform.position;
        Vector3 targetPosition = newPosition.position;

        //Player player = FindObjectOfType<Player>();

        //if(player != null)
        //{
        //    player.isMoving = true;
        //}

        float elapsedTime = 0f;
        while(elapsedTime < 1f)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime);
            elapsedTime += Time.deltaTime * cameraTransitionSpeed;
            yield return null;
        }
        transform.position = targetPosition;
        //if(player != null)
        //{
        //    player.isMoving = false;
        //}
    }
}
