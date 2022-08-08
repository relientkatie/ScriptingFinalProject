using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
[Header("Assign Game Objects")]
[SerializeField][Tooltip("Insert target game object here")]
public Transform Target;
[SerializeField][Tooltip("Insert projectile object here")]
public GameObject projectile;

[Header("Enemy Stats")]
public float distanceFromPlayer;
[SerializeField][Range(1,10)]
public float timeBetweenAttacks = 10;
bool alreadyAttacked;
private GameObject newInstance;
float time;
float timeDelay;
private float dist;
public float health = 50f;

public enum State
{
    Idle, Alert
}

public State CurrentState = State.Idle;

private void Update(){

dist = Vector3.Distance(Target.position, transform.position);
    switch(CurrentState)
    {
        case State.Idle:
            DoIdleState();
            break;
        case State.Alert:
            DoAlertState();
            break;
    }
}



private void DoIdleState(){
time = 0f;
timeDelay = 10f;


if(dist < distanceFromPlayer){
ChangeState(State.Alert);
}}

public void DoAlertState(){

transform.LookAt(Target);
AttackPlayer();

if(dist > distanceFromPlayer){
ChangeState(State.Idle);
}}

public void ChangeState(State newState)
{
if (CurrentState == newState){
    return;
}
    CurrentState = newState;
}
  

  public void TakeDamage(float amount){
       
        health -= amount;
        if(health <= 0f){
            Die();
        }
  }

public void AttackPlayer(){
if (alreadyAttacked == false)
{
    newInstance = Instantiate(projectile, transform.position, Quaternion.identity);
    
    Rigidbody rb = newInstance.GetComponent<Rigidbody>();

    rb.AddForce(transform.forward * 32f, ForceMode.Impulse);

    Destroy(newInstance, 2);

    alreadyAttacked = true;
   
    Invoke("ResetAttack", timeBetweenAttacks);
}}

public void ResetAttack(){
    alreadyAttacked = false;
    }

    public void Die(){
    Destroy(gameObject);
    }

    private void OnDrawGizmosSelected(){
    Gizmos.DrawWireSphere(transform.position, distanceFromPlayer);
    }
}
