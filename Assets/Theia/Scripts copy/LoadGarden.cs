using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadGarden : MonoBehaviour
{
    public List<PlantData> allData;
    List<Plant> allPlants;
    public Transform plantParent;

    // Start is called before the first frame update
    void Start()
    {
        allPlants = new List<Plant>();
        foreach(Transform child in plantParent)
        {
            Plant plant = child.GetComponent<Plant>();
            if (plant)
                allPlants.Add(plant);
        }

        StartCoroutine(LoadData());

    }

    public IEnumerator LoadData()
    {
        WWW www = new WWW("https://theiafuse.000webhostapp.com/loadgarden_medstat.php");
        yield return www;
        if(www.text.Length > 0 && www.text[0] == '0')
        {
            Debug.Log(www.text);
            string[] datas = www.text.Split('$');
            for(int i = 1; i < datas.Length - 1; i++)
            {
                PlantData pd = Manager.DataToPlant(datas[i]);
                allData.Add(pd);
            }
            StartCoroutine(LoadAllPlants());
        }
        else if(www.text.Length > 0)
        {
            Debug.LogError(www.text);
        }
        else
        {
            Debug.LogError("LOAD GARDEN NO ECHO");
        }
    }

    public IEnumerator LoadAllPlants()
    {
        foreach(Plant plant in allPlants)
        {
            if (allData.Count > 0)
            {
                int r = UnityEngine.Random.Range(0, allData.Count);
                plant.gameObject.SetActive(true);
                Manager.SetupPlantWithData(plant, allData[r]);
                allData.RemoveAt(r);
                yield return new WaitForEndOfFrame();
                yield return new WaitForEndOfFrame();
                yield return new WaitForEndOfFrame();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReloadGarden()
    {
        //UnityEngine.SceneManagement.SceneManager.LoadScene("JungleWorld");
    }
}
