using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    
    enum healthType {Player, Enemy, Object};

    [SerializeField]
    healthType hType = healthType.Object;

    public int health = 10;

    public AudioClip death;
    private AudioSource aud;

    private bool isDying = false;

    // [Tooltip("Check this box if this object is just an object (like a crate), not an enemy.")]
    // public bool isObject = false;

    void Start() {
        if(GetComponent<AudioSource>() != null) {
            aud = this.gameObject.GetComponent<AudioSource>();
            aud.spatialBlend = 1;       // make sure the sound is positional.
        } 
    }

    // todo - randomize starting health
    // regenerate health for enemies and player?
    // for objects, break objects into smaller pieces upon death
    // for death, if enemy, give XP back to the player.

    void Update() {
        if(health <= 0 && !isDying) {
            Death();
        }
        if(hType == healthType.Player) {
            UIManager.playerHealthText.text = "Health: " + health.ToString();
        }
    }

    void OnCollisionEnter(Collision other) {
        if(other.gameObject.CompareTag("Bullet")) {     // don't forget to tag your bullet.
            // health -= 2;         

            // let the velocity of the bullet define that amount                        
            // Debug.Log("Magnitude: " + other.relativeVelocity.magnitude);
            // health -= (int)(other.relativeVelocity.magnitude * 0.05f);

            // let the bullet define that amount
            health -= other.gameObject.GetComponent<Bullet>().damage;

            
        }
    }

    void Death() {
        isDying = true;

        switch(hType) {
            case healthType.Object: ObjectDeath();
                break;
            case healthType.Enemy: EnemyDeath();
                break;
            case healthType.Player: PlayerDeath();
                break;
            default: Debug.Log("How did you get here?");
                break;
        }
    }

    void ObjectDeath() {
        Destroy(this.GetComponent<Collider>());     // keep it from colliding with it's parts.
        Destroy(this.GetComponent<Renderer>());          // make it disappear
        for(int i = 0; i < 4; i++) {
            // randomize color of each part
            // have each part inherit bullet velocity
            // change the number of parts based on the size of the object.
            // have each part be randomly placed inside of the object, instead of centered.
            GameObject part = GameObject.CreatePrimitive(PrimitiveType.Cube);
            part.transform.localScale = Vector3.one * Random.Range(0.3f, 0.6f);
            part.transform.position = this.transform.position;
            part.transform.Translate(Random.Range(-.5f, .5f), Random.Range(-.5f, .5f), Random.Range(-.5f, .5f));
            part.AddComponent<Rigidbody>();     // make it fall down.
        }
        Destroy(this.gameObject, 1);
        aud.PlayOneShot(death);
    }

    void EnemyDeath() {
        this.gameObject.AddComponent<Rigidbody>();
        UIManager.KilledEnemy();
        StartCoroutine(GetSmallAndDie());
    }

    void PlayerDeath() {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    IEnumerator GetSmallAndDie() {
        float time = 4;
        float ObjStartSize = this.transform.localScale.y;
        float objectSize = this.transform.localScale.y;

        Debug.Log("Time: " + time + " ObjStartSize: " + ObjStartSize);
        Debug.Log("Making things smaller by " + (ObjStartSize / time) * Time.deltaTime);
        while(objectSize > 0.1f) {
            this.transform.localScale -= Vector3.one * (ObjStartSize / time) * Time.deltaTime;       // change 0.01 to public variable later.
            yield return new WaitForEndOfFrame();
            objectSize = this.transform.localScale.y;
        }
        Destroy(this.gameObject);
    }
}
