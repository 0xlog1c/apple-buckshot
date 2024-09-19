using UnityEngine;

public class ScreenOffset : MonoBehaviour {
    private float screenLeft;
    private float screenRight;
    private float screenTop;
    private float screenBottom;
    
    private float playerWidth;
    private float playerHeight;

    void Start() {
        Vector2 screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        screenRight = screenBounds.x;
        screenLeft = -screenBounds.x;
        screenTop = screenBounds.y;
        screenBottom = -screenBounds.y;

        playerWidth = GetComponent<SpriteRenderer>().bounds.size.x / 2;
        playerHeight = GetComponent<SpriteRenderer>().bounds.size.y / 2;
    }

    void Update() {
        Vector3 newPosition = transform.position;

        if (newPosition.x > screenRight + playerWidth) {
            newPosition.x = screenLeft - playerWidth;
        }
        else if (newPosition.x < screenLeft - playerWidth) {
            newPosition.x = screenRight + playerWidth;
        }

        if (newPosition.y > screenTop + playerHeight) {
            newPosition.y = screenBottom - playerHeight;
        }
        else if (newPosition.y < screenBottom - playerHeight) {
            newPosition.y = screenTop + playerHeight;
        }

        transform.position = newPosition;
    }
}
