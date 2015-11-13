using UnityEngine;
using System.Collections;

public class movingPlatform : MonoBehaviour {

    // Distancia a la que se considera que la plataforma ha llegado a su destino
    public float m_MinDistance = 2.0f;
    private float m_sqrMinDistance;
    //Velocidad de la plataforma
    public float m_MovementSpeed = 30.0f;

    //Se define por localWaypoints porque así lo hacemos de forma global a todas las plataformas
    public Vector3[] localWaypoints;
    private Vector3[] m_globalWaypoints;

    // Waypoint hacia el que se está moviendo la plataforma
    private int m_CurrentWaypoint;

	// Use this for initialization
	void Start () 
    {
        //Convertimos las posiciones locales de los waypoints a globales.
        m_globalWaypoints = new Vector3[localWaypoints.Length];
        for (int i = 0; i < localWaypoints.Length; ++i)
        {
            m_globalWaypoints[i] = transform.position + localWaypoints[i];
        }

        //Definimos como waypoint 1 al que se dirige inicialmente, dado que inicialmente se encuentra en el 0 0 0 localmente
        m_CurrentWaypoint = 1;

        m_sqrMinDistance = m_MinDistance * m_MinDistance;
	}

    // Update is called once per frame
    void Update()
    {
        doMovement();

        checkArrived();
	}

    void doMovement()
    {
        //Obtenemos la direccion de desplazamiento
        Vector3 dir = (m_globalWaypoints[m_CurrentWaypoint] - transform.position);
        dir.Normalize();

        //Movemos la plataforma segun la direccion al waypoint
        transform.Translate(dir*m_MovementSpeed*Time.deltaTime);
    }

    void checkArrived()
    {
        //Comprobamos si la plataforma está a menos distancia del minimo
        float squareDist = (m_globalWaypoints[m_CurrentWaypoint] - transform.position).sqrMagnitude;

        if (squareDist < m_sqrMinDistance)
        {
            //Cambiamos el waypoint. Mientras no hemos llegado al ultimo waypoint, vamos al siguiente, sino volvemos al principio
            if (m_globalWaypoints.Length - 1 > m_CurrentWaypoint)
            {
                ++m_CurrentWaypoint;
            }
            else
            {
                m_CurrentWaypoint = 0;
            }
        }

    }

    //Esto se ejecuta antes de ejecutar el juego, para saber donde está el waypoint. 
    void OnDrawGizmos()
    {
        if (localWaypoints != null)
        {
            Gizmos.color = Color.red;
            float size = 0.3f;

            for (int i = 0; i < localWaypoints.Length; ++i)
            {
                Vector3 globalWaypointPos = localWaypoints[i] + transform.position;
                Gizmos.DrawLine(globalWaypointPos - Vector3.up*size, globalWaypointPos+Vector3.up*size);
                Gizmos.DrawLine(globalWaypointPos - Vector3.left * size, globalWaypointPos + Vector3.left * size);
            }
        }
    }
}
