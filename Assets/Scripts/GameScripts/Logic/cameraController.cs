using UnityEngine;
using System.Collections;

public class cameraController : MonoBehaviour {

    public Transform target;

    private float m_posYCameraInitial;
	// Use this for initialization
	void Start () {
        m_posYCameraInitial = -1;
	}
	
	//Llamamos con lateUpdate, porque asi garantizamos que la cámara cambia con la nueva posicion del player del frame determinado
	void LateUpdate () {

        //En cada tick, lo que hacemos es actualizar la posición de la cámara según el player. El parámetro Y nunca bajará de la Y de la posición
        //inicial de la cámara, para que no baje más del campo de visión inicial, que es donde empieza el escenario. 
        transform.position = new Vector3(transform.position.x, Mathf.Max(target.position.y, m_posYCameraInitial), transform.position.z);
	}
}
