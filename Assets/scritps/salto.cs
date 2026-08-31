using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class SaltoJugador : MonoBehaviour
{
    public float fuerzaSalto = 50f;
    public LayerMask capaSuelo;
    public Transform verificadorSuelo;
    public float radioVerificacion = 0.2f;

    private Rigidbody rb;
    private bool enElSuelo;

    // Variables para el temporizador antibug
    private float tiempoUltimoSalto = 0f;
    public float cooldownSalto = 0.5f; // Medio segundo de espera obligatoria

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Comprueba si el jugador está tocando el suelo
        enElSuelo = Physics.CheckSphere(verificadorSuelo.position, radioVerificacion, capaSuelo);

        // Detecta la barra espaciadora
        if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            EjecutarSalto();
        }
    }

    public void EjecutarSalto()
    {
        // Solo salta si toca el suelo Y el reloj del juego superó el tiempo de espera
        if (enElSuelo && Time.time >= tiempoUltimoSalto + cooldownSalto)
        {
            // Registra el momento exacto en el que saltaste
            tiempoUltimoSalto = Time.time;

            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
            rb.AddForce(Vector3.up * fuerzaSalto, ForceMode.Impulse);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (verificadorSuelo != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(verificadorSuelo.position, radioVerificacion);
        }
    }
}