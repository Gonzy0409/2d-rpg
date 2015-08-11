using UnityEngine;
using System.Collections;

public class DestroyOffscreen : MonoBehaviour {
    public float offset = 16f;
    public delegate void onDestroy();
    public event onDestroy DestroyCallback;
    private bool offscreen;
    private float offscreenx = 0;
    private Rigidbody2D body2d;

    void Awake() {
        body2d = GetComponent<Rigidbody2D>();
    }

    // Use this for initialization
    void Start() {
        offscreenx = (Screen.width / pixelperfectcamera.pixelsToUnits) / 2 + offset;
    }

    // Update is called once per frame
    void Update() {
        var posX = transform.position.x;
        var dirX = body2d.velocity.x;

        if (Mathf.Abs(posX) > offscreenx) {
            if (dirX < 0 && posX < -offscreenx) {
                offscreen = true;
            }
            else if (dirX > 0 && posX > offscreenx) {
                offscreen = true;
            }
        }
        else {
            offscreen = false;
        }

        if (offscreen) {
            onOutOfBounds();
        }
    }

    public void onOutOfBounds() {
        offscreen = false;
        GameObjectUtility.Destroy(gameObject);

        if (DestroyCallback != null) {
            DestroyCallback();
        }
    }
}
