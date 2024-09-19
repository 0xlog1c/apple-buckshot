using UnityEngine;
using TMPro;

public class PauseManager : MonoBehaviour {

    [SerializeField] GameObject player;
    [SerializeField] GameObject appleSpawner;
    [SerializeField] GameObject gameCanvas;
    [SerializeField] GameObject pauseCanvas;

    [SerializeField] TextMeshProUGUI amountOfApples;

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            PauseGame();
        }
    }

    public void PauseGame() {
        amountOfApples.text = FindObjectOfType<GunRecoilJump>().GetNumberOfApplesCollected().ToString();

        player.SetActive(false);
        gameCanvas.SetActive(false);

        for (int i = 0; i < appleSpawner.transform.childCount; i++) {
            appleSpawner.transform.GetChild(i).GetComponent<SpriteRenderer>().enabled = false;
        }

        pauseCanvas.SetActive(true);
    }

    public void ResumeGame() {
        player.SetActive(true);
        gameCanvas.SetActive(true);

        for (int i = 0; i < appleSpawner.transform.childCount; i++) {
            appleSpawner.transform.GetChild(i).GetComponent<SpriteRenderer>().enabled = true;
        }

        pauseCanvas.SetActive(false);
    }
}
