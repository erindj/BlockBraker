using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour {
    public AudioClip crack;
    public Sprite[] hitSprites;
    public static int breakableCount = 0;
    public GameObject smoke;


    private int timesHit;
    private LevelManager levelManager;
    private bool isBreakable; 

    // Use this for initialization
    void Start () {
     isBreakable = (this.tag == "Breakable");
        //Keep track of breakable bricks
        if (isBreakable) {
            breakableCount++;
        }
     timesHit = 0;
     levelManager = LevelManager.FindObjectOfType<LevelManager> ();
}

    // Update is called once per frame
    void Update () {

    }

    void OnCollisionEnter2D(Collision2D col) {
        AudioSource.PlayClipAtPoint(crack, new Vector3(8f, 6f, 0.8f));
        if (isBreakable) {
            HandleHits();
        }
    }
    void HandleHits() {
        timesHit++;
        int maxHits = hitSprites.Length + 1;
        if (timesHit >= maxHits) {
            breakableCount--;
            levelManager.BrickDestroyed();
            PuffSmoke();
            Destroy(gameObject);
        } else {
            LoadSprites();
        }

    }

    void LoadSprites()
    {
        int spriteIndex = timesHit - 1;

        if (hitSprites[spriteIndex]) {
            this.GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
        } else {
            Debug.LogError("Brick sprite missing");
        }
    }

    void PuffSmoke () {
        GameObject smokePuff = Instantiate(smoke, transform.position, Quaternion.identity) as GameObject;
        smokePuff.GetComponent<ParticleSystem>().startColor = gameObject.GetComponent<SpriteRenderer>().color;
    }
    //TODO Remove this methed when we actually win!

    void SimulateWin () {
        levelManager.LoadNextLevel();
    }
}
