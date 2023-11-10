using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enime : MonoBehaviour
{
    public int health;
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
    private bool win=false;
    private bool lose=false;
    private bool die=false;

    public GameObject gost;
    public GameObject gem;
    public GameObject gostFlip;
    public GameObject gainText;
    private Canvas canvasSp;

    private GameObject foeSpawner;

    private LayerMask layer;

    private TankAI tank;
    private GameObject player;

    private Rigidbody2D rb2d;

    public Factions f;

    //on enable dont have enough time to up date a lot of component add in update function
    private void OnEnable()
    {
        alive = true;

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
        if(f==null){
            f = Factions.white;
        }
        
        if(speed==0){
            speed = 0.1f;
        }
        tank = GetComponent<TankAI>();
        player = GameObject.FindGameObjectWithTag("Player");
       
        rb2d = GetComponent<Rigidbody2D>();
        poolManager.instance.CreatePool(gost, 10);
        if(gem){
            poolManager.instance.CreatePool(gem, 20);
        }
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
                try
                {
                    GetComponent<Animator>().SetBool("die", true);
                }
                catch (Exception e)
                {
                    Debug.Log(e);
                }
           
            //call foedrop if not null

           if(!die){
            //Instantiate(gost, this.gameObject.transform.position, Quaternion.identity);
                die=true;
                if(gem){
                    poolManager.instance.ReuseObject(gem, this.gameObject.transform.position, Quaternion.identity);
                    int co=PlayerPrefs.GetInt("coin");
                    co++;
                    Debug.Log(co);
                    PlayerPrefs.SetInt("coin",co);
                }
                
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
                Invoke("changeLayer", 2f);
                }
                //this will change by different level manuelly
                if (f != Factions.yellow)
                {
                    Vector3 t = new Vector3(gameObject.transform.position.x + 0.2f, gameObject.transform.position.y, gameObject.transform.position.z);
                    //Debug.Log("win");
                    if (gameObject.name.Substring(0, 4) == "base")
                    {
                        // GameData.getInstance().coin += 500;
                        if(!win){
                            poolManager.instance.ReuseObject(gainText, t, Quaternion.identity);
                            win=true;
                            GameData.getInstance().main.gameWin();
                        }
                    }
                }
                else
                {
                    if (gameObject.name.Substring(0, 4) == "base" && !lose) {
                        lose=true;
                        GameData.getInstance().main.gameFailed();
                    }
                      
                }

        }

        if (health <= 0)
        {
            alive = false;
            //Debug.Log("died");
            this.gameObject.layer = LayerMask.NameToLayer("dead");
           
            this.gameObject.GetComponent<TankAI>().StopRange();
            this.gameObject.GetComponent<TankAI>().StopFiring();

            if (gameObject.name.Substring(0, 4) == "base")
            {
                if (foeSpawner)
                {
                    foeSpawner.SetActive(false);
                }
            }
             try
                {
                    GetComponent<Animator>().SetBool("die", true);
                }
                catch (Exception e)
                {
                    Debug.Log(e);
                }

           
                //this will change by different level manuelly
                if (f != Factions.yellow)
                {
                    Vector3 t = new Vector3(gameObject.transform.position.x + 0.2f, gameObject.transform.position.y, gameObject.transform.position.z);
                    
                    Debug.Log("win");
                    if (gameObject.name.Substring(0, 4) == "base")
                    {
                        // GameData.getInstance().coin += 500;
                        if(!win){
                            poolManager.instance.ReuseObject(gainText, t, Quaternion.identity);
                            win=true;
                            GameData.getInstance().main.gameWin();
                        }
                    }
                }
                else
                {
                    if (gameObject.name.Substring(0, 4) == "base" && !lose) {
                        lose=true;
                        GameData.getInstance().main.gameFailed();
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
    }

    //damage by other creatures
    public void damage(int dam) {
        dazedTime = startDazed;
        knockUp(3);
        health -= dam;
    }

    //damage by player
    public void damageByU(int dam)
    {
        knockUpWard(5);
        health -= dam;
    }

    public void damageHeavy(int dam)
    {
        dazedTime = startDazed;
        //blood split
        knockUp(3);
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
