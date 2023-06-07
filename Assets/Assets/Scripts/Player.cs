using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float movimientoHorizontal = 0f;
    [SerializeField] private float velocidadDeMovimiento;
    private bool mirandoDerecha = true;
    [Range(0, 0.3f)] [SerializeField] private float suavidadDeMovimiento;
    private Vector3 velocidad = Vector3.zero;
    private Rigidbody2D rb2D;
    public MainCamera mainCamera;
    //public bool isDoorOpen = false;
    //public bool isMoving = false;

    public bool firstDoor = false;
    public bool secondDoor = false;
    public bool thirdDoor = false;
    public bool fourthDoor = false;
    public bool fifthDoor = false;
    public bool sixthDoor = false;

    public Inventory inventory;
    public GameObject nota;

    private Vector3 newPlayerPosition;
    private Transform newCameraPosition;
    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        mainCamera = FindObjectOfType<MainCamera>();
    }
    private void Update()
    {
        //if (!isMoving)
        //{
        //    movimientoHorizontal = Input.GetAxisRaw("Horizontal") * velocidadDeMovimiento;
        //}
        movimientoHorizontal = Input.GetAxisRaw("Horizontal") * velocidadDeMovimiento;
    }
    private void FixedUpdate()
    {
        //if (!isMoving)
        //{
        //    Mover(movimientoHorizontal * Time.fixedDeltaTime);
        //}
        Mover(movimientoHorizontal * Time.fixedDeltaTime);
        if (firstDoor)
        {
            transform.position += new Vector3(6f, 0f, 0f);
            mainCamera.ChangeCameraPosition(mainCamera.livingRoomCameraPosition);
            firstDoor = false;
        }
        if (secondDoor)
        {
            transform.position += new Vector3(-6f, 0f, 0f);
            mainCamera.ChangeCameraPosition(mainCamera.outsideCameraPosition);
            secondDoor = false;
        }
        if (thirdDoor)
        {
            transform.position += new Vector3(3f, 0f, 0f);
            mainCamera.ChangeCameraPosition(mainCamera.kitchenCameraPosition);
            thirdDoor = false;
        }
        if (fourthDoor)
        {
            transform.position += new Vector3(-3f, 0f, 0f);
            mainCamera.ChangeCameraPosition(mainCamera.livingRoomCameraPosition);
            fourthDoor = false;
        }
        if (fifthDoor)
        {
            transform.position += new Vector3(3f, 0f, 0f);
            mainCamera.ChangeCameraPosition(mainCamera.patioCameraPosition);
            fifthDoor = false;
        }
        if (sixthDoor)
        {
            transform.position += new Vector3(-3f, 0f, 0f);
            mainCamera.ChangeCameraPosition(mainCamera.kitchenCameraPosition);
            sixthDoor = false;
        }
    }
    public void Note()
    {
        if (inventory.HasItem("Nota"))
        {
            nota.SetActive(true);
        }
    }
    private void Mover(float mover)
    {
        //if (!isMoving)
        //{
        //    Vector3 velocidadObjetivo = new Vector2(mover, rb2D.velocity.y);
        //    rb2D.velocity = Vector3.SmoothDamp(rb2D.velocity, velocidadObjetivo, ref velocidad, suavidadDeMovimiento);

        //    if (mover > 0 && !mirandoDerecha)
        //    {
        //        Girar();
        //    }
        //    else if (mover < 0 && mirandoDerecha)
        //    {
        //        Girar();
        //    }
        //}
        Vector3 velocidadObjetivo = new Vector2(mover, rb2D.velocity.y);
        rb2D.velocity = Vector3.SmoothDamp(rb2D.velocity, velocidadObjetivo, ref velocidad, suavidadDeMovimiento);

        if (mover > 0 && !mirandoDerecha)
        {
            Girar();
        }
        else if (mover < 0 && mirandoDerecha)
        {
            Girar();
        }
    }
    public void Girar()
    {
        mirandoDerecha = !mirandoDerecha;
        Vector3 escala = transform.localScale;
        escala.x *= -1;
        transform.localScale = escala;
    }
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("FirstDoor"))
    //    {
    //        firstDoor = true;
    //    }
    //    if (collision.CompareTag("SecondDoor"))
    //    {
    //        secondDoor = true;
    //    }
    //    if (collision.CompareTag("ThirdDoor"))
    //    {
    //        thirdDoor = true;
    //    }
    //    if (collision.CompareTag("FourthDoor"))
    //    {
    //        fourthDoor = true;
    //    }
    //    if (collision.CompareTag("FifthDoor"))
    //    {
    //        fifthDoor = true;
    //    }
    //    if (collision.CompareTag("SixthDoor"))
    //    {
    //        sixthDoor = true;
    //    }
    //}
}
