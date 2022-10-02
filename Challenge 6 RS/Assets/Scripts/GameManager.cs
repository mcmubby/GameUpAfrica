using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager singleton;
    public TileController[] allTilePiece;

    void Start() 
    {
        SetupNewLevel();
    }

    void SetupNewLevel()
    {
        allTilePiece = FindObjectsOfType<TileController>();
    }

    private void Awake() 
    {
        if(singleton == null)
        {
            singleton = this;
        }
        else if(singleton != this)
        {
            Destroy(gameObject);
            DontDestroyOnLoad(gameObject);
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    private void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        SetupNewLevel();
    }

    public void CheckComplete()
    {
        bool isFinished = !allTilePiece.Any(x => x.isColored == false);

        if(isFinished)
        {
            NextLevel();
        }
    }

    private void NextLevel()
    {
        if(SceneManager.GetActiveScene().buildIndex == 4)
        {
            SceneManager.LoadScene(0);
        }
        else{
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        
    }
}
