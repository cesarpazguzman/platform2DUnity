using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class scoreLevels : MonoBehaviour {

    [SerializeField]
    public List<Levels> levels;

    [System.Serializable]
    public class Levels
    {
        public GameObject buttonLevel;
        public Text textScore;
    }

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void showScore()
    {
        Dictionary<int, int> levelDef = Managers.GetInstance.StorageMgr.dictionaryScore;
        int maxLevel = levels.Count;
        foreach (int key in levelDef.Keys)
        {
            if (maxLevel == levels.Count && levelDef[key] == 0)
            {
                maxLevel = key;
            }

            levels[key - 1].textScore.text = levelDef[key].ToString() + "%";
            levels[key - 1].buttonLevel.GetComponent<Button>().interactable = true;
        }

        for (int i = maxLevel; i < levels.Count; ++i)
        {
            levels[i].buttonLevel.GetComponent<Button>().interactable = false;
        }
    }
}
