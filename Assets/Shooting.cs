using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;


/**
 * Our Handler for projectiles fired such as grappling hooks or bullets
 */


public class Shooting : MonoBehaviour
{





    bool grappling;
    float grapplingtime = 0.5f;
    float grapplingtimespent;
    float mingrapplingtime = 0.05f;
    bool collided;


    
        
     // 

    }

    










   // private void Start()
    //{
      //  collided = false;
   // }
    // Update is called once per frame
  //  void Update()
  //  {

   //     else if (Input.GetButtonDown("Fire2") && !grappling)
  //      {
   //         grappling = true;
        //    Grapple();
   //     }

    ///    if (grappling)
    //    {
      //      rope = GetComponent<LineRenderer>();
        //    rope.positionCount = 2;
          //  rope.SetPosition(0, transform.position);
            //rope.SetPosition(1, grapplerb.transform.position);
          //  grapplingtimespent += Time.deltaTime;
            //after enough time passes delete/return our grappling hook
     //       if(grapplingtimespent > grapplingtime)
       //     {
         //       GrappleEnd();
          //  }
            //else if (grapplingtimespent < mingrapplingtime && collided)
        //    {
       //         GrappleEnd();
   //         }
    //    }
 //   }

    //Our function that fires a bullet from our main character
    



    //public void GrappleEnd()
    //{
      //  rope.positionCount = 0;
        //grappling = false;
 //       grapplingtimespent = 0;
   //     collided = false;
     //   Destroy(grapple);
        
    //}

//    public void GrappleHit()
  //  {
    //    collided = true;
   // }
//}
