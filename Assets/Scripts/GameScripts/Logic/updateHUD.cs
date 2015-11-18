using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class updateHUD : MonoBehaviour {

    public Text m_score;
    public Text m_level;
    public Text m_record;
	// Use this for initialization
	void Start () {

	}

    void OnEnable()
    {
        sceneMgr.newLevel += changeLevel;
    }

    void OnDisable()
    {
        sceneMgr.newLevel -= changeLevel;
    }
	
	// Update is called once per frame
	void Update () {
        m_score.text = Managers.GetInstance.TimeMgr.getPercent().ToString() + "%";
	}

    public void changeLevel()
    {
        m_level.text = "LEVEL "+(Managers.GetInstance.SceneMgr.currentLevel +1).ToString();

        m_record.text = "Record: " + Managers.GetInstance.StorageMgr.dictionaryScore[Managers.GetInstance.SceneMgr.currentLevel + 1].ToString();
    }
}
