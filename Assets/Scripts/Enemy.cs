using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform player;                // for the location of the player
    public float attackInterval = 2;
    public int attackDamage = 5;

    bool canAttack = true;
    Health playerHealth;  

    void Start()
    {
        if(player == null) player = GameObject.Find("PlayerCapsule").transform;
        playerHealth = player.gameObject.GetComponent<Health>();
    }

    void Update()
    {
        if(Vector3.Distance(this.transform.position, player.position) < 3) {
            if(canAttack) {
                playerHealth.health -= attackDamage;
                Debug.Log("Attack!");
                StartCoroutine(ResetAttack());
            }
        }
    }

    IEnumerator ResetAttack() {
        canAttack = false;
        yield return new WaitForSeconds(attackInterval);
        canAttack = true;
    }
}
