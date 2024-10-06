using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//drag player to gameobject
public class TankAI : MonoBehaviour
{
    Animator anim;
    public GameObject bullet;
    public Transform turret;
    public bool face = true;
    private GameObject player;
    public GameObject[] creatures;
    public LayerMask enemiLayer;
    public float damage;
    public float heal;
    public int bulletForce;
    public float rangeFrequency;
    public float healFrequency;
    public float meleeFrequency;

    private float distance;
    public float clostDistance;
    private GameObject re;

    public float atRange;
    public Transform atPos;
    
    public Transform pivot;

    public GameObject slash;
    
    private void OnEnable()
    {
        distance = 999999;
        StopFiring();
        StopRange();
    }

    public GameObject GetPlayer() {
        //return the cloest opponent
        return player;
    }

    public GameObject GetClosest()
    {
        if (player)
        {
        distance = Vector2.Distance(transform.position, player.transform.position);
        }
        clostDistance = distance;
                re = player;
        if (re == player)
        {
            creatures = GameObject.FindGameObjectsWithTag("creature");
            clostDistance = 9999;
            foreach (var item in creatures)
            {
                if (item == null)
                {
                    distance = Vector2.Distance(transform.position, player.transform.position);
                    clostDistance = distance;
                    re = player;
                }

                if (item.GetComponent<Enime>()&&item.GetComponent<Enime>().alive) {
                    if (transform.gameObject.GetComponent<Enime>().f != item.GetComponent<Enime>().f)
                    {
                        TankAI ta = item.GetComponent<TankAI>();
                        Transform tr = ta.pivot;
                        //Debug.Log(ta.pivot);
                        distance = Vector3.Distance( pivot.position, tr.position);
                        if (distance < clostDistance)
                        {
                            clostDistance = distance;
                            re = item;
                        }
                    }
                }
            }

        }
        return re;
    }

    void Fire() {
        if (gameObject.name.Length > 7 && gameObject.name.Substring(0, 7) == "wizardK")
        {
            GameManager.getInstance().playSfx("magic");
        }else if ((gameObject.name.Length > 5 && gameObject.name.Substring(0, 4) == "sold")|| gameObject.name.Substring(4, 1) == "7")
        {
            GameManager.getInstance().playSfx("gun");
        }
        GameObject b= poolManager.instance.ReuseObject(bullet, turret.transform.position, turret.transform.rotation);
       
        if (face)
        {
            b.GetComponent<Rigidbody2D>().AddForce(turret.transform.right* bulletForce);
            if (this.gameObject.GetComponent<Enime>())
            {
               b.GetComponent<bullet>().f= this.gameObject.GetComponent<Enime>().f;
              //bullt color change
            }
            
        }
        else
        {
             b.GetComponent<Rigidbody2D>().AddForce(-turret.transform.right* bulletForce);
            if (this.gameObject.GetComponent<Enime>())
            {
                b.GetComponent<bullet>().f = this.gameObject.GetComponent<Enime>().f;
            }
        }
       
   }
    void meleeAttack()
    {
        if(slash){
            Instantiate(slash, turret.transform.position, turret.transform.rotation);
        }
        if (gameObject.name.Substring(0, 5) == "Drago" ||
                    gameObject.name.Substring(4, 1) == "6")
        {
            GameManager.getInstance().playSfx("dragon");
        }
        Collider2D[] enimes = Physics2D.
               OverlapCircleAll(atPos.position, atRange, enemiLayer);
        if (Physics2D.OverlapCircle(atPos.position, atRange, enemiLayer))
        {
            //Camera.main.GetComponent<Animator>().SetTrigger("shake");
            // Debug.Log(enimes[0].name);
            for (int i = 0; i < enimes.Length; i++)
            {
                if (enimes[i].gameObject.GetComponent<Enime>())
                {
                    if (
                enimes[i].gameObject.GetComponent<Enime>().f
                    != this.gameObject.GetComponent<Enime>().f)
                    {
                        if (enimes[i].gameObject.GetComponent<Enime>())
                        {
                           
                            if (enimes[i].gameObject.name.Substring(0, 5) != "Drago" && enimes[i].gameObject.name.Substring(4, 1) != "6")
                            {
                                GameManager.getInstance().playSfx("pounch");
                            }
                           
                            enimes[i].gameObject.GetComponent<Enime>().damage((int)damage);
                            enimes[i].gameObject.GetComponent<TankAI>().clostDistance = 9999;
                            enimes[i].gameObject.GetComponent<TankAI>().GetClosest();
                        }
                    }
                }
            }
           GetComponent<TankAI>().clostDistance = 9999;
           GetComponent<TankAI>().GetClosest();
        }
    }

    void healing()
    {
        if (gameObject.name.Substring(0, 5) == "Drago" ||
                    gameObject.name.Substring(4, 1) == "6")
        {
            GameManager.getInstance().playSfx("dragon");
        }
        Collider2D[] enimes = Physics2D.
               OverlapCircleAll(atPos.position, atRange, enemiLayer);
        if (Physics2D.OverlapCircle(atPos.position, atRange, enemiLayer))
        {
            //Camera.main.GetComponent<Animator>().SetTrigger("shake");
            // Debug.Log(enimes[0].name);
            for (int i = 0; i < enimes.Length; i++)
            {
                if (enimes[i].gameObject.GetComponent<Enime>())
                {
                    if (
                enimes[i].gameObject.GetComponent<Enime>().f
                    == this.gameObject.GetComponent<Enime>().f)
                    {
                        if (enimes[i].gameObject.GetComponent<Enime>())
                        {
                           
                            if (enimes[i].gameObject.name.Substring(0, 5) != "Drago" && enimes[i].gameObject.name.Substring(4, 1) != "6")
                            {
                                GameManager.getInstance().playSfx("pounch");
                            }
                           
                            enimes[i].gameObject.GetComponent<Enime>().healing((int)heal);
                            enimes[i].gameObject.GetComponent<TankAI>().clostDistance = 9999;
                            enimes[i].gameObject.GetComponent<TankAI>().GetClosest();
                        }
                    }
                }
            }
           GetComponent<TankAI>().clostDistance = 9999;
           GetComponent<TankAI>().GetClosest();
        }
    }

    public void StopRange()
    {
        CancelInvoke("Fire");
    }

    public void StartRange()
    {
        InvokeRepeating("Fire", 0.5f, rangeFrequency);
    }
    
    public void StopHeal()
    {
        CancelInvoke("healing");
    }

    public void StartHeal()
    {
        InvokeRepeating("healing", 0.5f, healFrequency);
    }

    public void StopFiring() {
        //CancelInvoke("Fire");
        CancelInvoke("meleeAttack");
    }

    public void StartFiring() {
        //InvokeRepeating("Fire", 0.5f, 0.5f);
        InvokeRepeating("meleeAttack", 0.5f, meleeFrequency);
    }

    private void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        clostDistance = 9999;
        poolManager.instance.CreatePool(bullet, 100);
        //creatures = GameObject.FindGameObjectsWithTag("creature");
    }
    private void Update()
    {
        
        // anim.SetFloat("distance", Vector2.Distance(transform.position, player.transform.position));
        if (re)
        {
            anim.SetFloat("distance", Vector2.Distance(transform.position, re.transform.position));
        }
        else
        {
            anim.SetFloat("distance", 999999);
        }
    }

    public void Flip()
    {
        face = !face;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(atPos.position, atRange);
    }

}
