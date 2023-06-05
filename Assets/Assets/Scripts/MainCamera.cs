using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public Transform outsideCameraPosition;
    public Transform livingRoomCameraPosition;
    public Transform kitchenCameraPosition;
    public Transform patioCameraPosition;

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
}
