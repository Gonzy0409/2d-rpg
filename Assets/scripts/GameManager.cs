using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
    public GameObject playerPrefab;

    private bool gameStarted;
    private TimeManager timeManager;
    private GameObject player;
    private GameObject floor;
    private Spawner spawner;

    void Awake() {
        floor = GameObject.Find("foreground");
        spawner = GameObject.Find("Spawner").GetComponent<Spawner>();
        timeManager = GetComponent<TimeManager>();
    }

	// Use this for initialization
	void Start () {
        var floorHeight = floor.transform.localScale.y;
        var pos = floor.transform.position;
        pos.x = 0;
        pos.y = -((Screen.height / pixelperfectcamera.pixelsToUnits) / 2) + (floorHeight / 2);
        floor.transform.position = pos;
        spawner.active = false;

        Time.timeScale = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (!gameStarted && Time.timeScale == 0) {
            if (Input.anyKeyDown) {
                timeManager.ManipulateTime(1, 1f);
                ResetGame();
            }
        }
	}

    void onPlayerKill() {
        spawner.active = false;

        var playerDestroyScript = player.GetComponent<DestroyOffscreen>();
        playerDestroyScript.DestroyCallback -= onPlayerKill;

        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        timeManager.ManipulateTime(0, 5.5f);
        gameStarted = false;
    }

    void ResetGame() {
        spawner.active = true;
        player = GameObjectUtility.Instantiate(playerPrefab, new Vector3(0, (Screen.height / pixelperfectcamera.pixelsToUnits) / 2 + 100, 0));

        var playerDestroyScript = player.GetComponent<DestroyOffscreen>();
        playerDestroyScript.DestroyCallback += onPlayerKill;
        gameStarted = true;
    }
}
