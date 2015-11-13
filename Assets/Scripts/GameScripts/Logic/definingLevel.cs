using UnityEngine;
using System.Collections;

//Este componente simplemente se colocara en el prefab del nivel en cuestion que queremos definir. Sirve para definir tanto
//el tiempo maximo en ese nivel como donde se spawneara el player
public class definingLevel : MonoBehaviour {

    [SerializeField]
    private int m_seconds;

    [SerializeField]
    private Vector3 m_spawnPlayerPoint;

    public int seconds
    {
        get { return m_seconds; }
    }

    public Vector3 spawnPoint
    {
        get { return m_spawnPlayerPoint; }
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        float size = 0.3f;

        Gizmos.DrawLine(m_spawnPlayerPoint - Vector3.up * size, m_spawnPlayerPoint + Vector3.up * size);
        Gizmos.DrawLine(m_spawnPlayerPoint - Vector3.left * size, m_spawnPlayerPoint + Vector3.left * size);
        
    }
}
