using UnityEngine;
using System.Collections;

public class playerController : MonoBehaviour {

    //Atributos del Inspector
    [SerializeField]
    private float m_speedX; //Cantidad de aceleracion en X a aumentar en cada fixedUpdate
    [SerializeField]
    private float m_speedY; //Cantidad de aceleracion en Y a aumentar en cada fixedUpdate
    [SerializeField]
    private float m_maxSpeedX; //Velocidad maxima en X
    [SerializeField]
    private float m_maxSpeedY; //Velocidad maxima en Y
    public Rigidbody2D m_rigi; //Componente RigidBody del player. 
    public GameObject groundCheckedLeft;
    public GameObject groundCheckedRight;
    public GameObject topCheckedRight;
    public GameObject topCheckedLeft;

    //Atributos privados
    private bool m_lookAtRight;//Indica si esta mirando hacia la derecha o izquierda. 
    private bool m_isJumping; //Indica si esta saltando
    private bool m_completeJump;
    private bool m_isGround;
    private bool m_isTouchingTop;

    public bool isDistanceJoint;

	// Use this for initialization
	void Start () 
    {
        isDistanceJoint = false;
        m_lookAtRight = true;
        m_isJumping = false;
        m_completeJump = false;
        m_isGround = false;
        m_isTouchingTop = false;

        //Animacion de Idle si tiene


	}
	
	// Update is called once per frame
	void Update () 
    {
        //Si estamos pulsando la tecla, y ademas no estamos saltando y ademas estamos en el suelo, salto
        if (Input.GetButton("Jump") && !m_isJumping && (m_isGround || isDistanceJoint))
        {
            //Debug.Log("Entra");
            m_isJumping = true;
            m_completeJump = false;
        }

        //Si suelto el boton paro el salto
        if (Input.GetButtonUp("Jump"))
        {
            m_isJumping = false;
        }

	}

    //El movimiento lo hace la física, para que sea el encargado de detectar colisiones mientras el player se mueve. Si no interviniese la fisica
    //como por ejemplo, algun objeto que no tenga física, que se mueva sólo por decoración, sería mejor con Translate. CREO. 
    void FixedUpdate()
    {
        //ESTO DEL LAYER GROUND SERÁ CAMBIADO POSTERIORMENTE POR LAYER A COLISIONABLE. CUANDO EMPECEMOS CON CAPAS.
        //Indica si estamos tocando o no el suelo
        m_isGround = Physics2D.Linecast(transform.position, groundCheckedLeft.transform.position, 1 << LayerMask.NameToLayer("Ground"))
            || Physics2D.Linecast(transform.position, groundCheckedRight.transform.position, 1 << LayerMask.NameToLayer("Ground"));
        //
        m_isTouchingTop = Physics2D.Linecast(transform.position, topCheckedLeft.transform.position, 1 << LayerMask.NameToLayer("Ground"))
            || Physics2D.Linecast(transform.position, topCheckedRight.transform.position, 1 << LayerMask.NameToLayer("Ground"));
        
        //Si tocamos con la cabeza con alguna plataforma, paramos el salto. Si no hago esto, se quedaría pegado arriba, dado que
        //se considera que el salto todavia no se acabo, y por tanto, la gravedad no bajaría al objeto.
        if (m_isTouchingTop)
        {
            //Debug.Log("touchingTop");
            m_isJumping = false;
        }



        //Obtenemos hacia donde esta caminando en X. Si el resultado es negativo significa que vamos a la izquierda, si es positivo es que vamos a la derecha
        float horizontal = Input.GetAxisRaw("Horizontal");
        if (horizontal != 0)
        {
            //Animacion de correr si tiene

        }
        else
        {
            //Dejamos de ejecutar la antigua animacion y ejecutamos la animacion de Idle
        }

        


        //Si todavia no hemos alcanzado la velocidad maxima, movemos con la física. 
        if (horizontal * m_rigi.velocity.x < m_maxSpeedX)
        {
            m_rigi.AddForce(Vector2.right * horizontal * m_speedX);
        }

        //Si el valor absoluto de la velocidad, supera a la velocidad maxima, entonces
        if (Mathf.Abs(m_rigi.velocity.x) > m_maxSpeedX)
        {
            //Establecemos en X la velocidad maxima. Mathf.Sign solo obtiene el signo, negativo si vamos a la izq y positivo si vamos a la derecha
            m_rigi.velocity = new Vector2(Mathf.Sign(m_rigi.velocity.x) * m_maxSpeedX, m_rigi.velocity.y);
        }

        //Si esta mirando para la derecha y estaba mirando hacia la izquierda, cambiamos
        if (horizontal > 0 && !m_lookAtRight || horizontal < 0 && m_lookAtRight)
        {
            m_lookAtRight = !m_lookAtRight;

            Vector2 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }

        //Si tenemos la orden de saltar o estamos saltando
        if (m_isJumping)
        {            
            //Animacion de salto si tiene


            //Mientras no lleguemos a la velocidad maxima en Y, añadimos fuerza
            if (m_rigi.velocity.y < m_maxSpeedY && !m_completeJump)
            {
                m_rigi.AddForce(Vector2.up * m_speedY);
            }
            else
            {
                m_completeJump = true;
                m_isJumping = false;
            }
        }
    }


    /// <summary>
    /// Este juego ha sido planteado en la misma escena, cargando un nuevo nivel mediante archivos XML, pero todo en la misma escena con el mismo personaje
    /// Dado que el movimiento del personaje lo movemos con la física, debemos de setear a 0 las velocidades, debido a que comenzamos un nuevo nivel
    /// y no queremos que comience con la velocidad con la que se terminó el anterior nivel
    /// </summary>
    public void newLevel()
    {
        m_rigi.velocity = Vector2.zero;
        this.gameObject.GetSafeComponent<DistanceJoint2D>().enabled = false;
        isDistanceJoint = false;
    }
}
