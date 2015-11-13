using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class showFinalScore : MonoBehaviour
{

	// Use this for initialization
	void Start () {
	
	}

    void OnEnable()
    {
        gameObject.GetSafeComponent<Text>().text = Managers.GetInstance.TimeMgr.percent.ToString() + "%";
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
