using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New State", menuName = "macklegames/AbilityData/Shooting")]
public class Shoot : StateData
{
    public LineRenderer rope;
    public GameObject bulletPrefab;
    public float bulletForce;

    private GameObject grapple;
    public GameObject grapplePrefab;
    public Transform firePoint;
    private Rigidbody grapplerb;
    public float grappleForce = 10f;

    public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {

    }
    public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {

        PlayerMovement c = characterState.GetCharacterControl(animator);
        if (c.shoot)
        {
            Shoot();
            animator.SetBool(PlayerMovement.transitionParameter.shoot.ToString(), false);
            return;
        }
        else if (!c.shoot)
        {
            animator.SetBool(PlayerMovement.transitionParameter.shoot.ToString(), false);
            return;
        }

        
        void Shoot()
        {
            GameObject bullet = Instantiate(bulletPrefab,firePoint.position, firePoint.rotation);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            rb.AddForce(c.mousePos*bulletForce, ForceMode.Impulse);
        }

    }

    public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {

    }


}

