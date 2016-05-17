using UnityEngine;
using System.Collections;

public class MonsterHealth : MonoBehaviour {

    public float health;

    public void ReceiveDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject, 0.75f);
        }
    }
}
