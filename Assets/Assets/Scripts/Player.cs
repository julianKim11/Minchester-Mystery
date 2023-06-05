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

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        movimientoHorizontal = Input.GetAxisRaw("Horizontal") * velocidadDeMovimiento;
    }
    private void FixedUpdate()
    {
        Mover(movimientoHorizontal * Time.fixedDeltaTime);
    }
    private void Mover(float mover)
    {
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
}
