using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New State", menuName = "macklegames/AbilityData/Shooting")]
public class Shoot : StateData
{
    public LineRenderer rope;
    public GameObject bulletPrefab;
    public float bulletForce;

    private float shootcooldown = 0.25f;
    private float shoottime = 0;

    public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
        shoottime = 1f;
        shootcooldown = 0.25f;
    }
    public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {

        Player c = characterState.GetCharacterControl(animator);

        if (c.jump)
        {
            animator.SetBool(Player.TransitionParameter.jump.ToString(), true);
        }
        
        if (c.shoot)
        {
            if(shoottime > shootcooldown)
            {
                Shoot();
                shoottime = 0;
                shootcooldown = 0.25f;
            }
            shoottime += Time.deltaTime;
            
            return;
        }
        else if (!c.shoot)
        {
            animator.SetBool(Player.TransitionParameter.shoot.ToString(), false);
            return;
        }

        
        void Shoot()
        {
            GameObject bullet = Instantiate(bulletPrefab, c.aimingposition+c.transform.position, c.transform.rotation); //firePoint.position, firePoint.rotation);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            rb.AddForce(c.aimingposition*bulletForce, ForceMode.Impulse);
        }

    }

    public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
        shoottime = 1;
        shootcooldown = 0.25f;
    }


}

