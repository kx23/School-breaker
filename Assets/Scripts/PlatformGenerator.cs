using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    [SerializeField]
    private int currentPlatformsNumber;
    public int CurrentPlatformsNumber
    {
        get { return currentPlatformsNumber; }
        set { currentPlatformsNumber = value; }
    }

    public static bool nextChanging = false;

    [SerializeField] public List<GameObject> ActivePlatforms = new List<GameObject>();
    [SerializeField] private List<GameObject> ChangingPlatforms = new List<GameObject>();
    private Dictionary<int, List<GameObject>> spawnPool = new Dictionary<int, List<GameObject>>();
    private Vector3 initialStartPosition = new Vector3(0f, 0f, -92f);
    [SerializeField] private List<GameObject> spawnPoolZero = new List<GameObject>();
    [SerializeField] private List<GameObject> spawnPoolOne = new List<GameObject>();
    [SerializeField] private List<GameObject> spawnPoolTwo = new List<GameObject>();

    public static int currentId = 0;

    public int GetSpawnPoolLenght() => spawnPool.Count;
    private void Awake()
    {
        currentId = 2;
        spawnPool[0] = spawnPoolZero;
        spawnPool[1] = spawnPoolOne;
        spawnPool[2] = spawnPoolTwo;
        Tutorial.OnTutorialEnded.AddListener(ActivateObstaclesAfterTutorial);
    }

    public void Generate(Vector3 pos)
    {

        if (spawnPool.Count == 0) return;
        GameObject platform;
        if (nextChanging)
        {
            platform = ChangingPlatforms[currentId];
            platform.transform.position = pos;
            platform.SetActive(true);
            for (int i = 0; i < platform.transform.childCount; i++)
            {
                platform.transform.GetChild(i).gameObject.SetActive(true);
                ActivePlatforms.Add(platform.transform.GetChild(i).gameObject);
            }
            nextChanging = false;
            return;
        }
        int index = Random.Range(0, spawnPool[currentId].Count);
        platform = spawnPool[currentId][index];
        spawnPool[currentId].RemoveAt(index);
       
        platform.transform.position = pos;
        platform.SetActive(true);
        for (int i = 0; i < platform.transform.childCount; i++) 
        {
            platform.transform.GetChild(i).gameObject.SetActive(true);
            if (PlayerPrefs.GetInt("Tutorial") == 0)
            {
                platform.transform.GetChild(i).gameObject.GetComponent<Platform>().SetActiveObstacles(false);
                platform.transform.GetChild(i).gameObject.GetComponent<Platform>().SetActiveItems(false);
            }
            ActivePlatforms.Add(platform.transform.GetChild(i).gameObject);
            
        }
        /*foreach (PlatformMovement miniPlatform in platform.GetComponentsInChildren<PlatformMovement>()) 
        {
            miniPlatform.gameObject.SetActive(true);
        }*/

        //ActivePlatforms.Add(platform);
    }

    public void AddToPool(GameObject platform, int id)
    {
        spawnPool[id].Add(platform);
    }

    public void GenerateInitialPath()
    {
        //Вытаскиваем индекс дороги из списка спавн пула
        int index = Random.Range(0, spawnPool[currentId].Count);
        GameObject platform = spawnPool[currentId][index];
        //Очищаем из стека дорогу 
        spawnPool[currentId].RemoveAt(index);

        //Генерация трассы
        platform.transform.position = new Vector3(initialStartPosition.x, initialStartPosition.y, initialStartPosition.z);
        for (int i = 0; i < platform.transform.childCount; i++)
        {
            platform.transform.GetChild(i).gameObject.SetActive(true);
            ActivePlatforms.Add(platform.transform.GetChild(i).gameObject);

        }
        platform.SetActive(true);
        //ActivePlatforms.Add(platform);
        
        //Отключаем у дочерних минитрасс препятствия
        foreach (Platform platformMovement in platform.GetComponentsInChildren<Platform>())
        {
            platformMovement.SetActiveObstacles(false);
            platformMovement.SetActiveItems(false);
            //ActivePlatforms.Add(platformMovement.gameObject);
        }

        Generate(new Vector3(initialStartPosition.x, initialStartPosition.y, initialStartPosition.z + 180.4f));
        //PlatformMovement.isMove = true;


    }

    public void ClearActivePlatforms()
    {
        
        foreach (GameObject plt in ActivePlatforms)
        {
            if (!plt.gameObject.transform.parent.gameObject.CompareTag("BH")) 
            {
                plt.SetActive(false);
                if (!spawnPool[currentId].Contains(plt.gameObject.transform.parent.gameObject))
                    spawnPool[currentId].Add(plt.gameObject.transform.parent.gameObject);
                plt.gameObject.transform.parent.gameObject.SetActive(false);
            }

        }

        ActivePlatforms.Clear();

    }
    private void Start()
    {

        GenerateInitialPath();
    }
    private void ActivateObstaclesAfterTutorial ()
    {
        for (int i = 0; i < ActivePlatforms.Count-4; ++i)
        {
            Platform platform = ActivePlatforms[ActivePlatforms.Count - 1 - i].GetComponent<Platform>();
            platform.SetActiveObstacles(true);
            platform.SetActiveItems(true);
        }
            
    }
    public void ClearAllObstacles()
    {
        for (int i = 0; i < ActivePlatforms.Count; ++i)
        {
            ActivePlatforms[i].GetComponent<Platform>().SetActiveObstacles(false);
        }

    }
    public void SwitchObstaclesAndItems()
    {

        List<Platform> platfromsComps = new List<Platform>();
        for (int i = 0; i < ActivePlatforms.Count; ++i)
        {
            
            if (currentId == 2)
            {
                 ActivePlatforms[i].transform.parent.gameObject.GetComponent<PlatformsRemover>().SetPlatforms();
            }
            
            Platform platform = ActivePlatforms[i].GetComponent<Platform>();
            platform.ChangeObstaclesAndItems();
            platfromsComps.Add(platform);
        }
        for (int i = 0; i < 3; ++i)
        {
            platfromsComps[i].SetActiveObstacles(false);
            platfromsComps[i].SetActiveItems(false);
        }
    }

}
