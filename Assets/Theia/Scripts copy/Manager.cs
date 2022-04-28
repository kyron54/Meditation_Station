using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.XR.ARFoundation;
using System.Runtime.InteropServices;


public class Manager : MonoBehaviour
{

    public static Manager instance;

    public static string[] GENE_TITLES;

    public Plant plant;
    public static int plantType;

    public GameObject geneUiParent, geneUiPrefab, colorUiParent, colorUiPrefab, materialButtons, selectedMat;
    public Color[] colors;
    public Material[] mats;
    public ARTrackedImageManager arImage;

    public GameObject allUI, editingUI, colorUI, screenshotUI, finishedUI, ARScene;
    public Sprite[] barSprites;
    public Image widthBar, heightBar, growthScaleBar;
    public Button widthPlus, widthMinus, heightPlus, heightMinus, scalePlus, scaleMinus;
    public Slider rotateSlider;
    public Button randomSeedButton, randomizeButton;
    public AudioSource sfxMixer;
    public Animator uiGroup;

    public Transform pictureParent;
    public List<Texture2D> screenshots;
    public GameObject polaroid;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        editingUI.SetActive(false);
        screenshots = new List<Texture2D>();

        for(int i = 0; i < mats.Length; i++)
        {
            mats[i].color = colors[Random.Range(0, colors.Length)];
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (plant == null)
        {
            GameObject[] objs = GameObject.FindGameObjectsWithTag("Plant");
            if (objs.Length > 0)
            {
                plant = objs[0].GetComponent<Plant>();
                plant.plantType = plantType;
                plant.StartPlant();
                SpawnedPlant();
            }
        }
        else
        {
            ARScene.transform.position = plant.transform.position;
        }
    }

    public void SpawnedPlant()
    {
        for (int i = 0; i < plant.genes.Count; i++)
        {
            int temp = i;
            GameObject newButton = Instantiate(geneUiPrefab, geneUiParent.transform);
            Text geneText = newButton.transform.Find("GeneBack").GetComponentInChildren<Text>();
            newButton.transform.Find("LArrow").GetComponent<Button>().onClick.AddListener(delegate { CycleGene(temp, false, geneText); });
            newButton.transform.Find("RArrow").GetComponent<Button>().onClick.AddListener(delegate { CycleGene(temp, true, geneText); });
            newButton.transform.Find("LabelBack").GetComponentInChildren<Text>().text = GENE_TITLES[i];
            geneText.text = plant.genes[i].toString();
        }

        widthPlus.onClick.AddListener(plant.PlusWidth);
        widthMinus.onClick.AddListener(plant.MinusWidth);
        heightPlus.onClick.AddListener(plant.PlusHeight);
        heightMinus.onClick.AddListener(plant.MinusHeight);
        scalePlus.onClick.AddListener(plant.PlusGrowthScale);
        scaleMinus.onClick.AddListener(plant.MinusGrowthScale);

        rotateSlider.onValueChanged.AddListener(plant.RotatePlant);
        randomSeedButton.onClick.AddListener(plant.RandomizeSeed);
        randomizeButton.onClick.AddListener(plant.Randomize);

        editingUI.SetActive(true);

        arImage.enabled = false;

        SelectMat("0");
        //GameObject.FindObjectOfType<ARTrackedImageManager>().enabled = false;
    }


    public void Crossbreed(Plant plant1, Plant plant2, Plant result)
    {
        result.genes = new List<Gene>();
        for (int i = 0; i < plant1.genes.Count; i++)
        {
            result.genes.Add(Genetics.CrossBreed(plant1.genes[i], plant2.genes[i]));
        }

        result.RegeneratePlant();
    }

    public void UpdateBar(Image bar, float amount)
    {
        int index = (int)((amount * 2) - 1);
        bar.sprite = barSprites[index];
    }

    public void CycleGene(int i, bool forward, Text text)
    {
        plant.CycleGene(i, forward);
        text.text = plant.genes[i].toString();
    }

    public void FinishPlant()
    {
        editingUI.SetActive(false);
        colorUI.SetActive(true);
    }

    public void ResumeEditing()
    {
        editingUI.SetActive(true);
        colorUI.SetActive(false);
    }

    public void FinishColors()
    {
        colorUI.SetActive(false);
        ARScene.SetActive(true);
        screenshotUI.SetActive(true);
    }

    public void ResumeColors()
    {
        colorUI.SetActive(true);
        ARScene.SetActive(false);
        screenshotUI.SetActive(false);
    }

    public void Screenshot()
    {
        StartCoroutine(ScreenshotRoutine());
    }

    public IEnumerator ScreenshotRoutine()
    {
        allUI.SetActive(false);
        yield return new WaitForEndOfFrame();
        Texture2D screenshot = ScreenCapture.CaptureScreenshotAsTexture();
        screenshots.Add(screenshot);
        NativeGallery.SaveImageToGallery(screenshot, "Theia", "plant", null);
        byte[] bytes = screenshot.EncodeToPNG();
        //UIPrintInteractionController(bytes);
        yield return new WaitForEndOfFrame();
        allUI.SetActive(true);
    }

    public void FinishPics()
    {
        screenshotUI.SetActive(false);
        finishedUI.SetActive(true);
        StartCoroutine(ShowPics());
    }

    public IEnumerator ShowPics()
    {
        StartCoroutine(UploadPlant(plant));

        foreach (Texture2D pic in screenshots)
        {
            GameObject newPic = Instantiate(polaroid, pictureParent);
            newPic.GetComponentInChildren<RawImage>().texture = pic;
            yield return new WaitForSeconds(2f);
        }
    }

    public IEnumerator UploadPlant(Plant plant)
    {
        WWWForm form = new WWWForm();
        form.AddField("plantData", PlantToData(plant));
        WWW www = new WWW("https://theiafuse.000webhostapp.com/addplant.php", form);
        yield return www;
        if(www.text == "0")
        {
            Debug.Log("UPLOADED THE PLANT!!!");
        }
        else
        {
            Debug.LogError("ERROR UPLOADING: " + www.text);
        }
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void SFX(AudioClip sfx)
    {
        sfxMixer.PlayOneShot(sfx);
    }

    public void NextScene(int scene)
    {
        uiGroup.SetBool("isChanging", true);
        StartCoroutine("Transition", scene);
    }

    public IEnumerator Transition(int scene)
    {
        yield return new WaitForSeconds(0.8f);
        uiGroup.SetBool("isChanging", false);
        SceneManager.LoadScene(scene);
    }

    /*
    [DllImport("__Internal")]
    private static extern float UIPrintInteractionController.print();
    */

    public static void SetPlantType(int plantType)
    {
        Manager.plantType = plantType;

        if(plantType == 0)
        {
            GENE_TITLES = new string[] { "Trunk Class", "Trunk Type", "Growths Class", "Growths Type", "Accessory Class", "Accessory Type" };
        }
        if (plantType == 1)
        {
            GENE_TITLES = new string[] { "Stem Class", "Stem Type", "Head Class", "Head Type", "Accessory Class", "Accessory Type" };
        }
        if(plantType == 2)
        {
            GENE_TITLES = new string[] { "Blade", "Head Class", "Head Type", "Accessory Class", "Accessory Type" };
        }
    }

    public void SelectMat(string matsString)
    {
        foreach(Transform child in colorUiParent.transform)
        {
            Destroy(child.gameObject);
        }

        string[] matsStrings = matsString.Split(',');
        int[] mats = new int[matsStrings.Length];
        for (int i = 0; i < mats.Length; i++)
        {
            mats[i] = int.Parse(matsStrings[i]);
        }

        foreach(Color color in colors)
        {
            GameObject newColorUI = Instantiate(colorUiPrefab, colorUiParent.transform);
            Color tempColor = color;
            foreach (int i in mats)
            {
                Material tempMat = this.mats[i];
                newColorUI.GetComponent<Button>().onClick.AddListener(delegate { ChooseColor(tempMat, tempColor); });
            }
            newColorUI.GetComponent<Image>().color = tempColor;
        }

        SwitchMatButtonSprite(selectedMat);

        selectedMat = materialButtons.transform.GetChild(mats[0]).gameObject;
        SwitchMatButtonSprite(selectedMat);
    }

    public void ChooseColor(Material mat, Color color)
    {
        Color newColor = color;
        newColor.a = mat.color.a;
        mat.color = newColor;
    }

    public void SwitchMatButtonSprite(GameObject button)
    {
        if (button == null)
            return;

        Image childImg = button.transform.GetChild(0).GetComponent<Image>();
        Sprite newSprite = childImg.sprite;
        childImg.sprite = button.GetComponent<Image>().sprite;
        button.GetComponent<Image>().sprite = newSprite;
    }

    public void RandomizeColors()
    {
        foreach (Material mat in mats)
        {
            ChooseColor(mat, colors[Random.Range(0, colors.Length)]);
        }
    }

    public static string PlantToData(Plant plant)
    {
        PlantData pd = new PlantData();
        pd.genes = plant.genes;
        pd.seed = plant.seed;
        pd.plantType = plant.plantType;
        pd.width = plant.width;
        pd.height = plant.height;
        pd.growthScale = plant.growthScale;
        pd.colors = new List<Color>();
        foreach(Material mat in plant.mats)
        {
            pd.colors.Add(mat.color);
        }
        return JsonUtility.ToJson(pd);
    }

    public static PlantData DataToPlant(string data)
    {
        Debug.Log(data);
        return JsonUtility.FromJson<PlantData>(data);
    }

    public static void SetupPlantWithData(Plant plant, PlantData data)
    {
        plant.genes = data.genes;
        plant.seed = data.seed;
        plant.plantType = data.plantType;
        plant.width = data.width;
        plant.height = data.height;
        plant.growthScale = data.growthScale;
        for(int i = 0; i < data.colors.Count; i++)
        {
            plant.mats[i].color = data.colors[i];
        }
        plant.RegeneratePlant();
    }
}
