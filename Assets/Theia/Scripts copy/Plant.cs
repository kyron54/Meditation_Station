
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlantData
{
    public List<Gene> genes;
    public List<Color> colors;
    public int seed, plantType;
    public float width = 1, height = 1, growthScale = 1;
}


public class Plant : MonoBehaviour
{
    public LayerMask currentLayerMask;
    bool ignoreChanges;

    public int seed;

    //0 for tree, 1 for flower
    public int plantType;

    public float width, height, growthScale = 1;

    public List<Gene> genes;

    public GameObject[] trunkPrefabs, stemPrefabs, bladePrefabs, flowerHeadPrefabs;
    List<GameObject> bushSpawnpoints, accSpawns;
    public GameObject BushPrefab;
    List<GameObject> bushes;
    public GameObject[] fruitPrefabs;
    public GameObject[] accPrefabs;
    public Material[] mats, newMats;

    public void Start()
    {

    }

    public void StartPlant()
    {
        genes = new List<Gene>();
        if (plantType == 0)
        {
            genes.Add(new Gene("Tt"));
            genes.Add(new Gene("Ii"));
            genes.Add(new Gene("Ff"));
            genes.Add(new Gene("Bb"));
            genes.Add(new Gene("Aa"));
            genes.Add(new Gene("Cc"));
        }
        if (plantType == 1)
        {
            genes.Add(new Gene("Tt"));
            genes.Add(new Gene("Ii"));
            genes.Add(new Gene("Bb"));
            genes.Add(new Gene("Ff"));
            genes.Add(new Gene("Aa"));
            genes.Add(new Gene("Cc"));
        }
        if (plantType == 2)
        {
            genes.Add(new Gene("Tt"));
            genes.Add(new Gene("Bb"));
            genes.Add(new Gene("Ff"));
            genes.Add(new Gene("Aa"));
            genes.Add(new Gene("Cc"));
        }
        seed = UnityEngine.Random.Range(100000, 999999);
        StartCoroutine(SetupPlant(genes));
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Randomize()
    {
        seed = UnityEngine.Random.Range(100000, 999999);

        float minSize = 0.5f, maxSize = 1.5f;
        ignoreChanges = true;
        SetWidth(UnityEngine.Random.Range(5, 30) / 10f);
        SetHeight(UnityEngine.Random.Range(5, 30) / 10f);
        SetGrowthScale(UnityEngine.Random.Range(5, 30) / 10f);
        ignoreChanges = false;


        foreach (Gene gene in genes)
        {
            if (UnityEngine.Random.Range(0, 2) == 0)
            {
                gene.chromo1 = gene.chromo1.ToUpper();
            }
            else
            {
                gene.chromo1 = gene.chromo1.ToLower();
            }

            if (UnityEngine.Random.Range(0, 2) == 0)
            {
                gene.chromo2 = gene.chromo2.ToUpper();
            }
            else
            {
                gene.chromo2 = gene.chromo2.ToLower();
            }
        }

        RegeneratePlant();
    }

    public void RandomizeSeed()
    {
        seed = UnityEngine.Random.Range(100000, 999999);
        RegeneratePlant();
    }

    public void RegeneratePlant()
    {
        StartCoroutine(SetupPlant(genes));
    }


    public void CycleGene(int i, bool forward)
    {
        if (forward)
            genes[i].CycleGeneForward();
        else
            genes[i].CycleGeneBackward();

        StartCoroutine(SetupPlant(genes));
    }

    public Gene FindGene(string geneChr)
    {
        foreach (Gene gene in genes)
        {
            if (gene.toString().ToLower().Contains(geneChr.ToLower()))
            {
                return gene;
            }
        }

        return null;
    }


    public IEnumerator SetupPlant(List<Gene> genes)
    {
        UnityEngine.Random.InitState(seed);

        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        yield return new WaitForEndOfFrame();

        bushes = new List<GameObject>();


        /*
        foreach (string c in new string[] { "t", "b", "f" })
        {
            Gene gene = FindGene(c);
            if (gene == null)
            {
                Debug.LogError("GENE " + c + " DOESNT EXIST");
                continue;
            }

            if (gene.toString().ToLower() == "tt")
            {
                GenerateTrunk(gene);
            }
            else if (gene.toString().ToLower() == "bb")
            {
                GenerateBush(gene);
            }
            else if (gene.toString().ToLower() == "ff")
            {
                GenerateFruit(gene);
            }
        }
        */

        GenerateTrunk();
        GenerateBush();
        yield return new WaitForEndOfFrame();
        GenerateFruit();
        GenerateAcc();

        if (Manager.instance == null)
            FinishMats();
    }


    public void GenerateTrunk()
    {
        //Generates Trunk or Stem
        Gene gene = FindGene("t");
        Gene accGene = FindGene("i");
        bushSpawnpoints = new List<GameObject>();
        accSpawns = new List<GameObject>();

        if (plantType == 0)
        {
            GameObject trunk = null;
            GameObject trunkPrefab;
            //Spooky
            if (gene.chromo1 == "t" && gene.chromo2 == "t")
            {
                if (accGene.chromo1 == "i" && accGene.chromo2 == "i")
                {
                    trunkPrefab = trunkPrefabs[0];
                }
                else if (accGene.chromo1 == "I" && accGene.chromo2 == "I")
                {
                    trunkPrefab = trunkPrefabs[1];
                }
                else
                {
                    trunkPrefab = trunkPrefabs[2];
                }
            }
            //Funky
            else if (gene.chromo1 == "T" && gene.chromo2 == "T")
            {
                if (accGene.chromo1 == "i" && accGene.chromo2 == "i")
                {
                    trunkPrefab = trunkPrefabs[3];
                }
                else if (accGene.chromo1 == "I" && accGene.chromo2 == "I")
                {
                    trunkPrefab = trunkPrefabs[4];
                }
                else
                {
                    trunkPrefab = trunkPrefabs[5];
                }
            }
            //Normal
            else
            {
                if (accGene.chromo1 == "i" && accGene.chromo2 == "i")
                {
                    trunkPrefab = trunkPrefabs[6];
                }
                else if (accGene.chromo1 == "I" && accGene.chromo2 == "I")
                {
                    trunkPrefab = trunkPrefabs[7];
                }
                else
                {
                    trunkPrefab = trunkPrefabs[8];
                }
            }
            trunk = Instantiate(trunkPrefab, transform);
            trunk.transform.localScale = Vector3.Scale(trunk.transform.localScale, new Vector3(width, height, width));
            trunk.transform.Rotate(new Vector3(0, UnityEngine.Random.Range(0, 360), 0));

            foreach (Transform child in trunk.transform)
            {
                if (child.tag == "BushSpawn")
                {
                    bushSpawnpoints.Add(child.gameObject);
                }
                if (child.tag == "AccSpawn")
                {
                    accSpawns.Add(child.gameObject);
                }
            }
        }
        else if (plantType == 1)
        {
            for (int i = 0; i < 4; i++)
            {
                GameObject stemPrefab;
                //Barren
                if (gene.chromo1 == "t" && gene.chromo2 == "t")
                {
                    if (accGene.chromo1 == "i" && accGene.chromo2 == "i")
                    {
                        stemPrefab = stemPrefabs[0];
                    }
                    else if (accGene.chromo1 == "I" && accGene.chromo2 == "I")
                    {
                        stemPrefab = stemPrefabs[1];
                    }
                    else
                    {
                        stemPrefab = stemPrefabs[2];
                    }
                }
                //Funky
                else if (gene.chromo1 == "T" && gene.chromo2 == "T")
                {
                    if (accGene.chromo1 == "i" && accGene.chromo2 == "i")
                    {
                        stemPrefab = stemPrefabs[3];
                    }
                    else if (accGene.chromo1 == "I" && accGene.chromo2 == "I")
                    {
                        stemPrefab = stemPrefabs[4];
                    }
                    else
                    {
                        stemPrefab = stemPrefabs[5];
                    }
                }
                //Leafy
                else
                {
                    if (accGene.chromo1 == "i" && accGene.chromo2 == "i")
                    {
                        stemPrefab = stemPrefabs[6];
                    }
                    else if (accGene.chromo1 == "I" && accGene.chromo2 == "I")
                    {
                        stemPrefab = stemPrefabs[7];
                    }
                    else
                    {
                        stemPrefab = stemPrefabs[8];
                    }
                }

                GameObject trunk = Instantiate(stemPrefab, transform);
                trunk.transform.localScale = Vector3.Scale(trunk.transform.localScale, new Vector3(width, height, width));
                trunk.transform.Rotate(new Vector3(0, UnityEngine.Random.Range(0, 360), 0));

                float moveRange = 0.2f;
                float xRange = moveRange;
                float zRange = moveRange;
                if (i == 0)
                {
                    xRange = moveRange;
                    zRange = moveRange;
                }
                else if (i == 1)
                {
                    xRange = -moveRange;
                    zRange = moveRange;
                }
                else if (i == 2)
                {
                    xRange = moveRange;
                    zRange = -moveRange;
                }
                else if (i == 3)
                {
                    xRange = -moveRange;
                    zRange = -moveRange;
                }
                trunk.transform.Translate(new Vector3(UnityEngine.Random.Range(Mathf.Min(0, xRange), Mathf.Max(0,xRange)), 0, UnityEngine.Random.Range(Mathf.Min(0,zRange), Mathf.Max(0,zRange))));

                foreach (Transform child in trunk.transform)
                {
                    if (child.tag == "BushSpawn")
                    {
                        bushSpawnpoints.Add(child.gameObject);
                    }
                    if (child.tag == "AccSpawn")
                    {
                        accSpawns.Add(child.gameObject);
                    }
                }
            }
        }
        else if (plantType == 2)
        {
            int amount = 1;
            GameObject chosenBlade;

            if (gene.chromo1 == "t" && gene.chromo2 == "t")
            {
                chosenBlade = bladePrefabs[0];
                amount = 3;
            }
            else if (gene.chromo1 == "T" && gene.chromo2 == "T")
            {
                chosenBlade = bladePrefabs[1];
                amount = 3;
            }
            else
            {
                chosenBlade = bladePrefabs[2];
                amount = 3;
            }

            for (int i = 0; i < amount; i++)
            {
                GameObject trunk = Instantiate(chosenBlade, transform);
                trunk.transform.localScale = Vector3.Scale(trunk.transform.localScale, new Vector3(width, height, width));
                trunk.transform.Rotate(new Vector3(0, UnityEngine.Random.Range((360 / amount) * i, (360 / amount) * (i + 1)), 20));
                foreach (Transform child in trunk.transform)
                {
                    if (child.tag == "BushSpawn")
                    {
                        bushSpawnpoints.Add(child.gameObject);
                    }
                    if (child.tag == "AccSpawn")
                    {
                        accSpawns.Add(child.gameObject);
                    }
                }
            }
        }
    }

    public void GenerateBush()
    {
        Gene gene = FindGene("b");

        foreach (GameObject spawnpoint in bushSpawnpoints)
        {
            if (plantType == 0)
            {
                int bushCount = UnityEngine.Random.Range(3, 6);
                for (int i = 0; i < bushCount; i++)
                {
                    Vector3 spawnPos = spawnpoint.transform.position + (UnityEngine.Random.onUnitSphere * gameObject.transform.localScale.x * 0.9f);
                    GameObject newBush = Instantiate(BushPrefab, spawnPos, spawnpoint.transform.rotation, transform);
                    newBush.transform.rotation = Quaternion.Euler(new Vector3(UnityEngine.Random.Range(0, 360), UnityEngine.Random.Range(0, 360), UnityEngine.Random.Range(0, 360)));
                    bushes.Add(newBush);
                    bushes[bushes.Count - 1].transform.localScale *= UnityEngine.Random.Range(0.5f, 1.2f);
                }
            }
            else if (plantType == 1 || plantType == 2)
            {
                Gene assistGene = FindGene("f");
                GameObject chosenPrefab;
                Quaternion rot = spawnpoint.transform.rotation;
                float rotRange = 20f;
                rot *= Quaternion.Euler(new Vector3(UnityEngine.Random.Range(-rotRange, rotRange), UnityEngine.Random.Range(-rotRange, rotRange), UnityEngine.Random.Range(-rotRange, rotRange)));
                GameObject newHead;
                if (gene.chromo1 == "b" && gene.chromo2 == "b")
                {
                    //Funky
                    if (assistGene.chromo1 == "f" && assistGene.chromo2 == "f")
                    {
                        chosenPrefab = flowerHeadPrefabs[0];
                    }
                    else if (assistGene.chromo1 == "F" && assistGene.chromo2 == "F")
                    {
                        chosenPrefab = flowerHeadPrefabs[1];
                    }
                    else
                    {
                        chosenPrefab = flowerHeadPrefabs[2];
                    }
                }
                else if (gene.chromo1 == "B" && gene.chromo2 == "B")
                {
                    //Dangerous
                    if (assistGene.chromo1 == "f" && assistGene.chromo2 == "f")
                    {
                        chosenPrefab = flowerHeadPrefabs[6];
                    }
                    else if (assistGene.chromo1 == "F" && assistGene.chromo2 == "F")
                    {
                        chosenPrefab = flowerHeadPrefabs[7];
                    }
                    else
                    {
                        chosenPrefab = flowerHeadPrefabs[8];
                    }
                }
                else
                {
                    //Normal
                    if (assistGene.chromo1 == "f" && assistGene.chromo2 == "f")
                    {
                        chosenPrefab = flowerHeadPrefabs[3];
                    }
                    else if (assistGene.chromo1 == "F" && assistGene.chromo2 == "F")
                    {
                        chosenPrefab = flowerHeadPrefabs[4];
                    }
                    else
                    {
                        chosenPrefab = flowerHeadPrefabs[5];
                    }
                }
                newHead = Instantiate(chosenPrefab, spawnpoint.transform.position, rot, transform);
                newHead.transform.localScale *= growthScale;
                if (plantType == 2)
                    newHead.transform.localScale *= 0.5f;
                bushes.Add(newHead);
            }
        }
    }

    public void GenerateFruit()
    {
        Gene gene = FindGene("f");
        Gene accGene = FindGene("b");

        if (plantType == 0)
        {
            foreach (GameObject bush in bushes)
            {

                GameObject prefab;
                //Plant Class
                if (gene.chromo1 == "f" && gene.chromo2 == "f")
                {
                    if (accGene.chromo1 == "b" && accGene.chromo2 == "b")
                    {
                        prefab = fruitPrefabs[0];
                    }
                    else if (accGene.chromo1 == "B" && accGene.chromo2 == "B")
                    {
                        prefab = fruitPrefabs[1];
                    }
                    else
                    {
                        prefab = fruitPrefabs[2];
                    }
                }
                //Alien Class
                else if (gene.chromo1 == "F" && gene.chromo2 == "F")
                {
                    if (accGene.chromo1 == "b" && accGene.chromo2 == "b")
                    {
                        prefab = fruitPrefabs[3];
                    }
                    else if (accGene.chromo1 == "B" && accGene.chromo2 == "B")
                    {
                        prefab = fruitPrefabs[4];
                    }
                    else
                    {
                        prefab = fruitPrefabs[5];
                    }
                }
                //Fruit Class
                else
                {
                    if (accGene.chromo1 == "b" && accGene.chromo2 == "b")
                    {
                        prefab = fruitPrefabs[6];
                    }
                    else if (accGene.chromo1 == "B" && accGene.chromo2 == "B")
                    {
                        prefab = fruitPrefabs[7];
                    }
                    else
                    {
                        prefab = fruitPrefabs[8];
                    }
                }

                SetLayerRecursively(bush, 6);
                for (int i = 0; i < UnityEngine.Random.Range(7, 15); i++)
                {
                    Vector3 randPos = UnityEngine.Random.onUnitSphere;
                    randPos.x *= bush.transform.localScale.x / 2;
                    randPos.y *= bush.transform.localScale.y / 2;
                    randPos.z *= bush.transform.localScale.z / 2;
                    randPos *= 3f;
                    randPos += bush.transform.GetChild(0).position;

                    RaycastHit hit;
                    if (Physics.Raycast(new Ray(randPos, (bush.transform.position - randPos)), out hit, 100f, currentLayerMask))
                    {
                        Vector3 spawnpoint = hit.point;
                        //spawnpoint += (bush.transform.position - randPos) * 0.1f;
                        GameObject newFruit = Instantiate(prefab, spawnpoint, Quaternion.identity, bush.transform);
                        newFruit.transform.localScale *= growthScale;
                        newFruit.transform.LookAt(randPos);
                    }
                }
                SetLayerRecursively(bush, 0);
            }
        }
        else if (plantType == 1)
        {
            /*
            foreach (GameObject spawnpoint in bushSpawnpoints)
            {
                Quaternion rot = spawnpoint.transform.rotation;
                float rotRange = 20f;
                rot *= Quaternion.Euler(new Vector3(UnityEngine.Random.Range(-rotRange, rotRange), UnityEngine.Random.Range(-rotRange, rotRange), UnityEngine.Random.Range(-rotRange, rotRange)));
                if (gene.chromo1 == "f" && gene.chromo2 == "f")
                {
                    Instantiate(fruitPrefabs[0], spawnpoint.transform.position, rot, transform);
                }
                else if (gene.chromo1 == "F" && gene.chromo2 == "F")
                {
                    Instantiate(fruitPrefabs[1], spawnpoint.transform.position, rot, transform);
                }
                else
                {
                    Instantiate(fruitPrefabs[2], spawnpoint.transform.position, rot, transform);
                }

            }
            */
        }
    }

    public void GenerateAcc()
    {
        Gene gene = FindGene("a");
        Gene accGene = FindGene("c");

        foreach (GameObject spawn in accSpawns)
        {
            GameObject accPrefab;
            //Alien
            if (gene.chromo1 == "A" && gene.chromo2 == "A")
            {
                if (accGene.chromo1 == "C" && accGene.chromo2 == "C")
                {
                    accPrefab = accPrefabs[0];
                }
                else if (accGene.chromo1 == "c" && accGene.chromo2 == "c")
                {
                    accPrefab = accPrefabs[1];
                }
                else
                {
                    accPrefab = accPrefabs[2];
                }
            }
            //Stuff
            else if (gene.chromo1 == "a" && gene.chromo2 == "a")
            {
                if (accGene.chromo1 == "C" && accGene.chromo2 == "C")
                {
                    accPrefab = accPrefabs[3];
                }
                else if (accGene.chromo1 == "c" && accGene.chromo2 == "c")
                {
                    accPrefab = accPrefabs[4];
                }
                else
                {
                    accPrefab = accPrefabs[5];
                }
            }
            //Natural
            else
            {
                if (accGene.chromo1 == "C" && accGene.chromo2 == "C")
                {
                    accPrefab = accPrefabs[6];
                }
                else if (accGene.chromo1 == "c" && accGene.chromo2 == "c")
                {
                    accPrefab = accPrefabs[7];
                }
                else
                {
                    accPrefab = accPrefabs[8];
                }
            }

            Instantiate(accPrefab, spawn.transform);
        }
    }

    public static void SetLayerRecursively(GameObject obj, int layer)
    {
        if (obj)
        {
            obj.layer = layer;
            foreach (Transform child in obj.transform)
            {
                SetLayerRecursively(child.gameObject, layer);
            }
        }
    }

    public void PlusWidth()
    {
        SetWidth(Mathf.Clamp(width + 0.1f, 0.5f, 3f));
    }

    public void MinusWidth()
    {
        SetWidth(Mathf.Clamp(width - 0.1f, 0.5f, 3f));
    }

    public void PlusHeight()
    {
        SetHeight(Mathf.Clamp(height + 0.1f, 0.5f, 3f));
    }

    public void MinusHeight()
    {
        SetHeight(Mathf.Clamp(height - 0.1f, 0.5f, 3f));
    }

    public void PlusGrowthScale()
    {
        SetGrowthScale(Mathf.Clamp(growthScale + 0.1f, 0.5f, 3f));
    }

    public void MinusGrowthScale()
    {
        SetGrowthScale(Mathf.Clamp(growthScale - 0.1f, 0.5f, 3f));
    }


    public void SetWidth(float w)
    {
        width = w;
        if (!ignoreChanges)
            RegeneratePlant();
        Manager.instance.UpdateBar(Manager.instance.widthBar, width);

    }

    public void SetHeight(float h)
    {

        height = h;
        if (!ignoreChanges)
            RegeneratePlant();
        Manager.instance.UpdateBar(Manager.instance.heightBar, height);

    }

    public void SetGrowthScale(float s)
    {
        growthScale = s;
        if (!ignoreChanges)
            RegeneratePlant();
        Manager.instance.UpdateBar(Manager.instance.growthScaleBar, growthScale);

    }

    public void SetPlantType(int type)
    {
        plantType = type;
        RegeneratePlant();
    }

    public void RotatePlant(float rotate)
    {
        transform.localRotation = Quaternion.Euler(0, rotate, 0);
    }

    public void FinishMats()
    {
        Material[] tempMats = new Material[mats.Length];
        for (int i = 0; i < mats.Length; i++)
        {
            tempMats[i] = new Material(mats[i]);
        }
        newMats = tempMats;

        RecursiveMakeMatsNew(gameObject);
    }

    public void RecursiveMakeMatsNew(GameObject obj)
    {
        MeshRenderer mr = obj.GetComponent<MeshRenderer>();
        ParticleMatchMatColor pmmc = obj.GetComponent<ParticleMatchMatColor>();
        for (int i = 0; i < mats.Length; i++)
        {
            if (mr)
            {
                for (int x = 0; x < mr.materials.Length; x++)
                {
                    if (mr.materials[x] == mats[i])
                    {
                        mr.materials[x] = newMats[i];
                    }
                }
            }
            if (pmmc)
            {
                if (pmmc.mat == mats[i])
                    pmmc.mat = newMats[i];
            }
        }


        foreach (Transform child in obj.transform)
        {
            RecursiveMakeMatsNew(child.gameObject);
        }
    }
}

/*
public void GenerateThorns()
{
    //Run a simple for loop
    //  Choose a UnityEngine.Random point kinda far away
    //  Shoot a raycast from point towards the plant
    //  If hit stem, place thorn
}

public void GenerateBranches(int branches, int perBranch, float UnityEngine.RandomRot)
{
    Transform mainBranch = stem.GetChild(0).Find("BranchEnd");
    Transform currentBranch = mainBranch;
    Transform oldBranch = currentBranch;
    GameObject newBranch = currentBranch.gameObject;
    for(int n = 0; n < branches; n++)
    {
        oldBranch = currentBranch;
        for (int i = 0; i < perBranch; i++)
        {
            newBranch = Instantiate(branchPrefab, currentBranch.position, currentBranch.rotation, currentBranch);
            newBranch.transform.Rotate(new Vector3(UnityEngine.Random.Range(-UnityEngine.RandomRot, UnityEngine.RandomRot), UnityEngine.Random.Range(-UnityEngine.RandomRot, UnityEngine.RandomRot), UnityEngine.Random.Range(-UnityEngine.RandomRot, UnityEngine.RandomRot)));
            newBranch.transform.localScale = currentBranch.parent.localScale / 6 * 5;
            newBranch.transform.position = currentBranch.position;
        }
        currentBranch = newBranch.transform.Find("BranchEnd");
    }
}
*/
