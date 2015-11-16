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
        gameObject.GetSafeComponent<Text>().color = (Managers.GetInstance.StorageMgr.dictionaryScore[Managers.GetInstance.SceneMgr.currentLevel + 1] < Managers.GetInstance.TimeMgr.getPercent())
            ? Color.green : Color.black;

        gameObject.GetSafeComponent<Text>().text = Managers.GetInstance.TimeMgr.getPercent().ToString() + "%";
        Managers.GetInstance.StorageMgr.setScore(Managers.GetInstance.SceneMgr.currentLevel + 1, Managers.GetInstance.TimeMgr.getPercent());
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
