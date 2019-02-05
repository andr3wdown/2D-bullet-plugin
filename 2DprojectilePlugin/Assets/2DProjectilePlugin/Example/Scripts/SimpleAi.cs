using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//add UpGamesWeapon2D.Weapons and UpGamesWeapon2D.Math namespaces (math is optional and used only for rotation in this case!)
using UpGamesWeapon2D.Math;
using UpGamesWeapon2D.Weapons;

public class SimpleAi : MonoBehaviour
{
    public LayerMask playerLayer;
    public float detectionRadius;
    public float rotationSpeed;
    public Transform target;
    public Weapon2DPreset primaryWeapon;
    public Weapon2DPreset secondaryWeapon;
    public float changeTreshold = 10;
    float stoppingDistance = 2;
    Weapon2DAI aiWeapon;
    public LayerMask obstacles;

    public void Start()
    {

        //get refrence to Weapon2DAI on child object
        aiWeapon = GetComponentInChildren<Weapon2DAI>();
        aiWeapon.InitWeaponPreset(primaryWeapon);
        //Check if the player is in range every 2 seconds!
        StartCoroutine(CheckForPlayer());
        StartCoroutine(DirChange());
    }
    IEnumerator DirChange()
    {
        yield return new WaitForSeconds(5);
        ang = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        StartCoroutine(DirChange());
    }
    IEnumerator CheckForPlayer()
    {
        yield return new WaitForSeconds(2);
        
        Collider2D col = Physics2D.OverlapCircle(transform.position, detectionRadius, playerLayer);
        if(col != null)
        {
            target = col.transform;
            if (Vector2.Distance(transform.position, target.position) < changeTreshold)
            {
                if(aiWeapon.weaponPreset.weaponName != secondaryWeapon.weaponName)
                {
                    aiWeapon.InitWeaponPreset(secondaryWeapon);
                }              
            }
            else
            {
                if (aiWeapon.weaponPreset.weaponName != primaryWeapon.weaponName)
                {
                    aiWeapon.InitWeaponPreset(primaryWeapon);
                }
            }
        }
        else
        {
            
            target = null;
        }

    
        StartCoroutine(CheckForPlayer());
    }
    Vector2 savedVector;
    Vector2 ang;
    private void Update()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        if(target != null)
        {
            
            if(Vector2.Distance(transform.position, target.position) > stoppingDistance)
            {
                transform.position += transform.up * 4 * Time.deltaTime;
            }
            if(Physics2D.Linecast(transform.position, target.position, obstacles))
            {
                if(savedVector == Vector2.zero || Physics2D.Raycast(transform.position, transform.up, stoppingDistance, obstacles))
                {
                    int index = Random.Range(0, 2);
                    switch (index)
                    {
                        case 0:
                            savedVector = transform.right;
                            break;

                        case 1:
                            savedVector = -transform.right;
                            break;
                    }
                    
                }
                transform.rotation = Quaternion.Slerp(transform.rotation, MathOperations.LookAt2D((Vector2)transform.position + savedVector, transform.position, -90), rotationSpeed * Time.deltaTime);
            }
            else
            {
                savedVector = Vector2.zero;
                //rotate the enemy (or just the enemy weapon) towards the player
                transform.rotation = Quaternion.Slerp(transform.rotation, MathOperations.LookAt2D(target.position, transform.position, -90), rotationSpeed * Time.deltaTime);
                //call the ai shoot function on aiWeapon!! //remember to configure a Weapon2DPreset for the enemy!!!
                if(!no)
                    aiWeapon.AIShoot();
            }           
        }
        else
        {
            PatrolRotate();
            transform.position += transform.up * 1 * Time.deltaTime;
        }
    }
    bool no = false;
    private void OnDisable()
    {
        no = true;
    }
    void PatrolRotate()
    {
        transform.rotation = MathOperations.LookAt2D((Vector2)transform.position + ang.normalized, transform.position, -90);
        if (Physics2D.Raycast(transform.position, transform.up, stoppingDistance, obstacles))
        {
            int index = Random.Range(0, 2);
            switch (index)
            {
                case 0:
                    ang = transform.right;
                    break;

                case 1:
                    ang = -transform.right;
                    break;
            }

        }
    }
    private void OnDrawGizmosSelected()
    {
        Color c = Color.white;
        c.a = 0.2f;
        Gizmos.color = c;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
        Gizmos.DrawWireSphere(transform.position, changeTreshold);
    }
}
