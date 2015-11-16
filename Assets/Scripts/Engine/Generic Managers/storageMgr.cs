using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using LitJson;
using System.IO;

public class storageMgr : MonoBehaviour {

    //Datos que almacenará el fichero json
    public class levelsDef
    {
        //Almacenará su nivel con su mejor puntuacion
        public int level;
        public int score;

        public levelsDef(int level_, int bestScore_)
        {
            level = level_;
            score = bestScore_;
        }

        public levelsDef() { }
    }

    private List<levelsDef> m_leveldef = new List<levelsDef>();

    //Almacenamiento volátil
    private Dictionary<int, int> m_dictionaryScore = new Dictionary<int,int>();
    public Dictionary<int, int> dictionaryScore { get { return m_dictionaryScore; } }

    private JsonData fileJson;

	// Use this for initialization
	void Start () {
        readFile();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    //Funcion que lee fichero json
    private void readFile()
    {
        List<levelsDef> dataLevels = JsonMapper.ToObject<List<levelsDef>>(File.ReadAllText(Application.dataPath+ "/Resources/scoreLevels.json"));
        foreach (levelsDef level in dataLevels)
        {
            m_dictionaryScore.Add(level.level, level.score);
        }
    }

    public void setScore(int level, int score)
    {
        Assert.assert(m_dictionaryScore.ContainsKey(level), "The dictionary score doesn't contain the level");
        //Almacenamos la mejor puntuacion
        if (m_dictionaryScore[level] < score)
        {
            m_dictionaryScore[level] = score;
        }
    }

    public void writeFile()
    {
        //Lo que hacemos ahora es almacenar en una lista los niveles definidos segun la clase anteriores
        foreach (int key in m_dictionaryScore.Keys)
        {
            m_leveldef.Add(new levelsDef(key, m_dictionaryScore[key]));
        }

        //Serializamos esa lista y la escribimos en la ruta indicada.
        fileJson = JsonMapper.ToJson(m_leveldef);
        File.WriteAllText(Application.dataPath + "/Resources/scoreLevels.json", fileJson.ToString());
    }


}
