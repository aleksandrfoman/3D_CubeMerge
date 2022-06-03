using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private Cube cubePrefab;
    [SerializeField]
    private Transform spawnPos;
    [SerializeField]
    private int startSpawnIndex = 0;
    private int maxIndex;
    public int MaxIndex => maxIndex;
    private Cube currentDraggableCube;
    private bool isMoveCube;
    [SerializeField]
    private float speedTranslateCube;
    [SerializeField]
    private float maxX;
    private float posX;
    private int currentScore;
    [SerializeField]
    private float finishCoord;
    public float FinishCoord => finishCoord;
    [SerializeField]
    private float timeToSapwnCube;
    [SerializeField]
    private MobAdsInterstitial mobAdsInterstitial;
    [SerializeField]
    private int everyShotsDisplay = 10;
    private int tempShoots = 0;
    [Header("Ui")]
    [SerializeField]
    private TMP_Text scoreText;
    [SerializeField]
    private GameObject startPanel;
    [SerializeField]
    private GameObject losePanel;
    [SerializeField]
    private GameObject winPanel;
    [SerializeField]
    private GameObject gamePanel;
    [SerializeField]
    private GraphicRaycaster graphicRaycaster;
    private bool isStartGame = true;
    public static GameController Instance { get; private set; }

    private void Awake()
    {
        Application.targetFrameRate = 60;

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        SetMaxIndex(startSpawnIndex);
        CreateCube();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && isStartGame)
        {
            List<RaycastResult> raycastResults = new List<RaycastResult>();
            graphicRaycaster.Raycast(new UnityEngine.EventSystems.PointerEventData(EventSystem.current) { position = Input.mousePosition },raycastResults);
            
            if (raycastResults.Count == 0)
            {
                if (currentDraggableCube != null && !isMoveCube)
                {
                    isMoveCube = true;
                }
                posX = 0;
                DeactiveStartPanel();
            }
        }
        else if (Input.GetKey(KeyCode.Mouse0))
        {
            if (isMoveCube)
            {
                float mouseX = Input.GetAxis("Mouse X")*Time.deltaTime*speedTranslateCube;
                posX += mouseX;
                posX = Mathf.Clamp(posX, -1, 1);
                Vector3 startPos = currentDraggableCube.transform.position;
                Vector3 endPos = new Vector3(posX * maxX, currentDraggableCube.transform.position.y, currentDraggableCube.transform.position.z);
                currentDraggableCube.transform.position = Vector3.LerpUnclamped(startPos, endPos, 10f * Time.deltaTime);
                    
            }
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            if (isMoveCube)
            {
                isMoveCube = false;
                currentDraggableCube.ShootCube();
                currentDraggableCube = null;
                Invoke(nameof(CreateCube), timeToSapwnCube);

                tempShoots++;
                if (tempShoots % everyShotsDisplay == 0)
                {
                    mobAdsInterstitial.ShowAd();
                }
            }
        }
    }
    public void UpdateScore(int value)
    {
        currentScore += value;
        scoreText.text = currentScore.ToString();
    }

    public void SetMaxIndex(int value)
    {
        maxIndex = value;
    }
    public void CreateCube()
    {
        int rndIndex = Random.Range(startSpawnIndex, maxIndex);
        Cube createCube = Instantiate(cubePrefab, spawnPos.position, Quaternion.identity);
        createCube.SetSettings(rndIndex);
        currentDraggableCube = createCube;
    }
    public void ActivaeLosePanel()
    { 
        losePanel.SetActive(true);
        gamePanel.SetActive(false);
        isStartGame = false;
    }

    public void ActiveWinPanel()
    {
        winPanel.SetActive(true);
        gamePanel.SetActive(false);
        isStartGame = false;
    }

    public void DeactiveStartPanel()
    {
        startPanel.SetActive(false);
        gamePanel.SetActive(true);
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
