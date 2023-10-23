using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enime : MonoBehaviour
{
    public int health=1;
    public float energy;
    public float speed;
    public float dazedTime;
    public float startDazed;
    public float knockBackForce = 20f;
    public float dieForce = 500f;

    public float slowDownFactor = 0.05f;
    public float slowDownLength = 2f;
    private bool slowOn = false;

    public bool alive;
    public GameObject gost;
    public GameObject gostFlip;
    public GameObject gainText;
    private Canvas canvasSp;

    private GameObject foeSpawner;

    private LayerMask layer;

    private TankAI tank;
    private GameObject player;
    //    private CoinQuest cq;
    //  private QuestManager qm;
    //  public RandomLootSystem rls;
    private Rigidbody2D rb2d;

    public Factions f = Factions.white;

    //on enable dont have enough time to up date a lot of component add in update function
    private void OnEnable()
    {
        alive = true;
        Destroy(GetComponent<Rigidbody>());
        Destroy(GetComponent<BoxCollider>());
        GetComponent<Animator>().enabled = true;
        //health = 3;
        this.gameObject.layer = LayerMask.NameToLayer("Default");
        if (gameObject.name.Substring(0, 4) == "base")
        {
            health += 7;
        }
        //Debug.Log(gameObject.name.Substring(4, 1));
        else if (gameObject.name.Substring(4, 1) == "2")
        {
            health += 2;
        }
        else if (gameObject.name.Substring(4, 1) == "4"|| gameObject.name.Substring(0, 4) == "shie")
        {
            health += 10;
        }
        else if (gameObject.name.Substring(0, 5) == "Drago" || gameObject.name.Substring(4, 1) == "6")
        {
            health += 50;
        } else if ((gameObject.name.Length>7&&gameObject.name.Substring(0,7)=="wizardK")|| gameObject.name.Substring(4, 1) == "9") {
            health += 50;
        }
        else
        {
            health = 3;
        }
    }

    private void Start()
    {
        tank = GetComponent<TankAI>();
        player = GameObject.FindGameObjectWithTag("Player");
        //cq =player.GetComponent<CoinQuest>();
        //qm = player.GetComponent<QuestManager>();
        rb2d = GetComponent<Rigidbody2D>();
        poolManager.instance.CreatePool(gost, 10);
        poolManager.instance.CreatePool(gostFlip, 10);
        poolManager.instance.CreatePool(gainText, 10);

        foeSpawner = GameObject.Find("foeSpawnManager");
    }

    private void changeLayer()
    {
        this.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
        GetComponent<Animator>().enabled = false;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Submit"))
        {
            slowMotion();
            slowOn = true;
        }
        if (Input.GetButtonUp("Submit"))
        {
            slowOn = false;
        }

        if (canvasSp == null)
        {
            canvasSp = FindObjectOfType<Canvas>();
        }


        if (health <= 0)
        {
            alive = false;
            //Debug.Log("died");
            //killing quest
            //rls.loot();
            //when it died remove 2d rigdbody and 2d colidder add 3d rig and collider let is fell in to water
            //Destroy(this.gameObject);

            //instan gost
            this.gameObject.GetComponent<TankAI>().StopRange();
            this.gameObject.GetComponent<TankAI>().StopFiring();

            if (gameObject.name.Substring(0, 4) == "base")
            {
                if (foeSpawner)
                {
                    foeSpawner.SetActive(false);
                }
            }

        }
        else
        {
            //check and add collider and rigidbody while alive
            if(!GetComponent<BoxCollider2D>() && !GetComponent<Rigidbody2D>())
            {
                gameObject.AddComponent<Rigidbody2D>();
                gameObject.AddComponent<BoxCollider2D>();
                if (gameObject.name.Substring(0, 5) == "Drago" || 
                    gameObject.name.Substring(4, 1) == "6"|| 
                    gameObject.name.Substring(4, 1) == "4"||
                    gameObject.name.Substring(0,4)=="shie")
                {
                    gameObject.GetComponent<Rigidbody2D>().mass = 10;
                }
            }
            if (GetComponent<Rigidbody2D>())
            {
                if (gameObject.name.Substring(0, 5) == "Drago" ||
                   gameObject.name.Substring(4, 1) == "6" ||
                   gameObject.name.Substring(4, 1) == "4" ||
                   gameObject.name.Substring(0, 4) == "shie"||
                   gameObject.name.Substring(4, 1) == "9"||
                   (gameObject.name.Length > 7 && gameObject.name.Substring(0, 7) == "wizardK"))
                {
                    gameObject.GetComponent<Rigidbody2D>().mass = 10;
                }
            }
        }

        if (dazedTime <= 0)
        {
            if (gameObject.name.Substring(4, 1) == "3"|| (gameObject.name.Length >= 5&&gameObject.name.Substring(0,5) =="horse"))
            {
                speed = 0.4f;
            } else if (gameObject.name.Substring(4, 1) == "4"|| (gameObject.name.Length>=6 && gameObject.name.Substring(0, 6) == "shield"))
            {
                speed = 0.1f;
            }
            else if(gameObject.name.Substring(0, 5) == "Drago"|| gameObject.name.Substring(4, 1) == "6")//because the shortest is only 5 long it will error
            {
                speed = 0.03f;
            }else if (gameObject.name.Substring(4, 1) == "9" ||
                   (gameObject.name.Length > 7 && gameObject.name.Substring(0, 7) == "wizardK"))
            {
                speed = 0.05f;
            }
            else { 
            speed = 0.2f;
            }
        }
        else
        { 
            speed = 0;
            dazedTime -= Time.deltaTime;
        }
        
      
    }

    private void FixedUpdate()
    {

        if (rb2d&&(rb2d.velocity.sqrMagnitude > 2))
        {
            //smoothness of the slowdown is controlled by the 0.99f, 
            //0.5f is less smooth, 0.9999f is more smooth
            rb2d.velocity *= 0.99f;
        }

        if (GetComponent<Rigidbody>() && (GetComponent<Rigidbody>().velocity.sqrMagnitude > 2))
        {
            //smoothness of the slowdown is controlled by the 0.99f, 
            //0.5f is less smooth, 0.9999f is more smooth
            GetComponent<Rigidbody>().velocity *= 0.99f;
        }


        if (health <= 0)
        {
            //recycle enime
            //here is a solution for slow down specific objects
            //speed up or slow down a gameobject with anim speed and movement speed variables

            knockDie(dieForce);
            //GetComponent<Rigidbody2D>().AddTorque(2 * Time.fixedDeltaTime * 100f, ForceMode2D.Force);
            Destroy(GetComponent<Rigidbody2D>());
            Destroy(GetComponent<BoxCollider2D>());

            //this.gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(gameObject.name+"die");
           
                try
                {
                    GetComponent<Animator>().SetBool("die", true);
                }
                catch (Exception e)
                {
                    Debug.Log(e);
                }
           

            if (!GetComponent<Rigidbody2D>() && !GetComponent<BoxCollider2D>()&&!GetComponent<Rigidbody>()&&!GetComponent<BoxCollider>())
            {
                //Instantiate(gost, this.gameObject.transform.position, Quaternion.identity);
                if (tank.face)
                {
                    //flip  x true;
                   
                    poolManager.instance.ReuseObject(gostFlip, this.gameObject.transform.position, Quaternion.identity);

                }
                else
                {
                
                    poolManager.instance.ReuseObject(gost, this.gameObject.transform.position, Quaternion.identity);
                }

                GameManager.getInstance().playSfx("died");
               

                //this will change by different level manuelly
                if (f != Factions.white)
                {
                    Vector3 t = new Vector3(gameObject.transform.position.x + 0.2f, gameObject.transform.position.y, gameObject.transform.position.z);
                    poolManager.instance.ReuseObject(gainText, t, Quaternion.identity);

                    if (gameObject.name.Substring(0, 4) == "base")
                    {
                        // GameData.getInstance().coin += 500;
                        GameData.getInstance().main.gameWin();
                    }
                    GameData.getInstance().energy += 0.05f;
                    //GameData.getInstance().coin += 50;
                    //PlayerPrefs.SetInt("coin", GameData.getInstance().coin);
                    //GameData.getInstance().main.txtCoin.text = GameData.getInstance().coin.ToString();
                }
                else
                {
                    if (gameObject.name.Substring(0, 4) == "base") {
                        GameData.getInstance().main.gameFailed();
                    }
                      
                }


                this.gameObject.AddComponent<BoxCollider>();
                this.gameObject.AddComponent<Rigidbody>();
                GetComponent<Rigidbody>().constraints=RigidbodyConstraints.FreezeRotation;
                Invoke("changeLayer", 2f);
            }
         
            //slowMotion();


        }
        if (slowOn == false)
        {
            //Time.timeScale += (1f / slowDownLength) * Time.unscaledDeltaTime;
            //Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
        }

    }

    public void slowMotion()
    {

       // There is no need to hack "Time.fixedDeltaTime".Instead select "Interpolate" in Rigidbody.
//====================
//Good to mention. In your case changing "Time.fixedDeltaTime" will lead to inaccurate physics calculation, as long as internaly engine sees increase Physics frame rate.
//Why "inaccurate": physics engine is not perfect and results will always be different with different "Physics frame rate" values.
//====================
//For quick test DO:
//1. Remove Line #17 in TimeManager.cs (t.e. Time.fixedDeltaTime = Time.timeScale * .02f;)
//2. Every Cube's rigidbody should have "Interpolate" field set to "Interpolate"﻿


        Time.timeScale = slowDownFactor;
        
        //Time.fixedDeltaTime = Time.timeScale * 0.02f;
    }
    //damage by other creatures
    public void damage(int dam) {
        dazedTime = startDazed;
        //blood split
        //GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        //Invoke("recoveFromFreeze", 1);
        //knockBack(knockBackForce);
        knockUp(30);
        health -= dam;
    }

    //damage by player
    public void damageByU(int dam)
    {
        knockUpWard(50);
        health -= dam;

    }

    public void damageHeavy(int dam)
    {
        dazedTime = startDazed;
        //blood split
        knockUp(30);
        health -= dam;
    }

    private void knockBack(float knockForce)
    {
        if (GetComponent<TankAI>().face == true)
        {
            if (GetComponent<Rigidbody2D>()) {
                GetComponent<Rigidbody2D>().AddForce(new Vector2(-1 * knockForce, 0));
            }
           
        }
        else
        {
            if (GetComponent<Rigidbody2D>())
            {
                GetComponent<Rigidbody2D>().AddForce(new Vector2(1 * knockForce, 0));
            }
           
        }
    }

    private void knockDie(float knockForce)
    {
        if (GetComponent<TankAI>().face == true)
        {
            if (GetComponent<Rigidbody2D>())
            {
                GetComponent<Rigidbody2D>().AddForce(new Vector2(-1 * knockForce, 1 * knockForce));
            }
            
        }
        else
        {
            if (GetComponent<Rigidbody2D>())
            {
                GetComponent<Rigidbody2D>().AddForce(new Vector2(1 * knockForce, 1 * knockForce));
            }
            
        }
    }
    //the real knock up
    private void knockUpWard(float knockForce)
    {
        if (GetComponent<Rigidbody2D>())
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 1 * knockForce));
        }
        
    }

    private void knockUp(float knockForce)
    {
        if (GetComponent<TankAI>().face == true)
        {
            if (GetComponent<Rigidbody2D>())
            {
                GetComponent<Rigidbody2D>().AddForce(new Vector2(-1 * knockForce, 1 * knockForce));
            }
           
        }
        else
        {
            if (GetComponent<Rigidbody2D>())
            {
                GetComponent<Rigidbody2D>().AddForce(new Vector2(1 * knockForce, 1 * knockForce));
            }
            
        }
    }

    private void recoveFromFreeze()
    {
        if (GetComponent<Rigidbody2D>())
        {
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        }
       
    }

}
