using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 2;

    [SerializeField]
    float critDamageChance = 0.5f;

    [SerializeField]
    bool randomizeElementalDamage = false;

    enum element {Fire, Ice, Candy, Slime};

    element elem = element.Fire;

    // option to randomize bullet damage?
    void Start() {
        if(Random.value < critDamageChance) {               // 80%
            damage *= 2;
            Debug.Log("Double Damage!");
        }

        if(randomizeElementalDamage) elem = (element)Random.Range(0,3);      // set this bullet to a random range.
    }
}
