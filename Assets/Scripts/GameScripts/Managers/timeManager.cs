using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// Clase que gestiona el tiempo que transcurre en el juego. Se colocaría en el gameObject que representa a la escena, ya que quizás se pueda contemplar
/// que cada nivel tenga un tiempo diferente para terminar, ajustable en cada nivel.
/// </summary>
public class timeManager : MonoBehaviour 
{
    //Variable que lleva cuenta de los segundos en los que la cabra empieza a quedarse sin hambre. Cuando llegue a cero la cabra muere
    private float m_seconds;
    //Saber los segundos iniciales
    private float m_secondsInitial;
    public float secondsInitial { get { return m_secondsInitial; } }

    //Variable que indica a partir de cuantos segundos que queden, se establece como alarma para empezar a indicarle al usuario que le queda poco tiempo
    [SerializeField]
    private int m_lastSecondsAlarm = 10;

    public float seconds
    {
        get { return m_seconds; }
        set 
        {
            StopCoroutine("updateTime");
            m_seconds = m_secondsInitial = value; 
            StartCoroutine("updateTime");          
        }
    }

    public int lastSecondsAlarm
    {
        get { return m_lastSecondsAlarm; }
    }

	// Use this for initialization
	void Start () 
    {

	}
	
	// Update is called once per frame
	void Update () {
       
	}

    IEnumerator updateTime()
    {
        while (m_seconds > 0) //Mientras haya tiempo
        {
            m_seconds -= 0.3f;

            //Podemos reproducir un sonido de tic tic tic de reloj, cuando queden 10 segundos para indicarle al jugador que le queda poco tambien
            //por la parte del sonido
            if (m_seconds <= m_lastSecondsAlarm)
            {

            }

            yield return new WaitForSeconds(0.3f); //Esperamos un segundo
        }

        //Si se acaba el tiempo, ejecutaríamos la funcionalidad de que se acaba el tiempo antes de que el jugador consiga el objetivo. 
        Managers.GetInstance.GameMgr.gameOver();
    }

    //Funcion que se llamará cuando cojamos por ejemplo un elemento de Aumento de tiempo. 
    public void addTime(int timeToAdd)
    {
        //Añadimos tiempo, pero no más tiempo del tiempo inicial, para establecer un máximo
        m_seconds = Mathf.Min(timeToAdd + m_seconds, m_secondsInitial);
    }

    public int getPercent()
    {
        return (int)(m_seconds / m_secondsInitial * 100);
    }

}
