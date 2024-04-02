using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    // simple counter that is used for health, but can be used for anything that needs counting

    public float health {private set; get;}
    public float maxHealth {private set; get;}
    public bool dead = true;
    public event Action onDeath;
    public event Action onRevive;

    public void InitializeHealth(float amount){
        maxHealth = amount;
        health = amount;
        dead = false;
    }

    public float TakeDamage(float damage){
        // applies damage to health
        // returns extra damage if amount is larger than current health
        float overkill = 0;
        health -= damage;
        if(health <= damage){
            overkill = damage - overkill;
            Death();
        }

        return overkill;
    }

    public float Heal(float heal){
        // applies healing to health
        // returns extra health if current health is large than max health
        float overheal = 0;
        health += heal;
        if(health > maxHealth){
            overheal = health - maxHealth;
            health = maxHealth;
        }
        if(dead){ Revive(); }

        return overheal;
    }

    public void ModfiyMaxHealth(float newMaxHealth){
        maxHealth = newMaxHealth;
        if(health > maxHealth){ health = maxHealth; }
        if(health <= 0){ Death(); }
    }

    void Death(){
        health = 0;
        if(dead == false){
            dead = true;
            onDeath?.Invoke();
        }
    }

    void Revive(){
        dead = false;
        onRevive?.Invoke();
    }
}
