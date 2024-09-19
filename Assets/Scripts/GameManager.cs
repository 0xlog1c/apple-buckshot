using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager Instance {get; private set;}
    float timeElapsed = 0f;

    void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            DestroyImmediate(gameObject);
        }
    }

    void OnDisable() {
        if (Instance == this) {
            Instance = null;
        }
    }
    
    public void SetTimeElapsed(float time) {
        timeElapsed = time;
    }

    public void NewGame() {
        SceneManager.LoadScene("Game");
    }

    public void ResetGame() {
        timeElapsed += Time.deltaTime;
        if (timeElapsed > 5f) {
            NewGame();
        }
    }

    public void ExitGame() {
        Application.Quit();
    }
}
