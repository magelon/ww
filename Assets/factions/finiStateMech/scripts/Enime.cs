using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum Element{
    normal,
    fire,
    water,
    rock,
    light,
    electric,
    ice,
    grass,
    dark
}

public class Enime : MonoBehaviour
{
    public int health;
    public float energy;
    public float speed;
    public float dazedTime;
    public float startDazed;
    public float knockBackForce;
    public float dieForce = 500f;
    public string Eelement;

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
    private int storeHP;
    private Color storeCol;

    private Rigidbody2D rb2d;

    public Factions f;
    public GameObject damageNumberPrefab;
    public GameObject healNumberPrefab;
    [SerializeField] private Canvas canvas;

    private Dictionary<Element, Dictionary<Element, float>> elementCounterTable;

    //on enable dont have enough time to up date a lot of component add in update function
    private void OnEnable()
    {
        alive = true;

        GetComponent<Animator>().enabled = true;
        
        this.gameObject.layer = LayerMask.NameToLayer("Default");
        
    }

    private void Start()
    {
        InitializeCounterTable();
        // Get the SpriteRenderer component attached to this GameObject
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
        Debug.LogWarning("No SpriteRenderer found on this GameObject.");
        return;
        }
        storeCol=spriteRenderer.color;
        storeHP=health;
        Debug.Log(storeHP);
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

    void InitializeCounterTable()
    {
        elementCounterTable = new Dictionary<Element, Dictionary<Element, float>>();

        // Fire counters
        elementCounterTable[Element.fire] = new Dictionary<Element, float>
        {
            { Element.grass, 2f }, // Fire is weak against water
            { Element.water, .5f }
        };

        // Water counters
        elementCounterTable[Element.water] = new Dictionary<Element, float>
        {
            { Element.fire, 2f },  // Water is strong against fire
            { Element.grass, .5f }
        };

         // Water counters
        elementCounterTable[Element.rock] = new Dictionary<Element, float>
        {
            { Element.light, 2f },  // Water is strong against fire
            { Element.water, .5f }
        };

         // Water counters
        elementCounterTable[Element.light] = new Dictionary<Element, float>
        {
            { Element.dark, 2f },  // Water is strong against fire
        };

         // Water counters
        elementCounterTable[Element.electric] = new Dictionary<Element, float>
        {
            { Element.ice, 2f },  // Water is strong against fire
            { Element.water, .5f }
        };

         // Water counters
        elementCounterTable[Element.grass] = new Dictionary<Element, float>
        {
            { Element.water, 2f },  // Water is strong against fire
            { Element.fire, .5f }
        };
         elementCounterTable[Element.ice] = new Dictionary<Element, float>
        {
            { Element.water, 2f },  // Water is strong against fire
            { Element.fire, .5f }
        };

    }

    public float GetElementEffectiveness(Element attacker, Element defender)
    {
        if (elementCounterTable.ContainsKey(attacker) &&
            elementCounterTable[attacker].ContainsKey(defender))
        {
            return elementCounterTable[attacker][defender];
        }
        return 1.0f; // Default neutral effectiveness
    }

    private void changeLayer()
    {
        GetComponent<Animator>().enabled = false;
        health=storeHP;
        GetComponent<SpriteRenderer>().color = storeCol;
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
                if(gem && f != Factions.yellow){
                    poolManager.instance.ReuseObject(gem, this.gameObject.transform.position, Quaternion.identity);
                    int co=PlayerPrefs.GetInt("coin");
                    co++;
                    //Debug.Log(co);
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

                //GameManager.getInstance().playSfx("died");
                
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

        if(health>=1){
            this.gameObject.layer = LayerMask.NameToLayer("Default");
        }

        if (health <= 0)
        {
            alive = false;
           
            Invoke("changeLayer", 1f);

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

    public Element ConvertStringToElement(string elementString)
    {
    Element elementEnum;
    if (Enum.TryParse(elementString, true, out elementEnum))
    {
        // Successfully converted string to enum
        return elementEnum;
    }
    else
    {
        Debug.LogWarning("Invalid element: " + elementString);
        // Return a default value or handle the error as needed
        return Element.fire; // Example: default to Fire
    }
    }
    //damage by other creatures
    public void damage(int dam,string element) {

        // Get the SpriteRenderer component attached to this GameObject
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        // Set the color based on the element
        Color newColor;
        // Check if the SpriteRenderer is available
        if (spriteRenderer == null)
        {
        Debug.LogWarning("No SpriteRenderer found on this GameObject.");
        return;
        }

        newColor= spriteRenderer.color;
        Color currentColor = spriteRenderer.color;

        switch (element.ToLower())
        {
        case "fire":
            newColor = Color.red; // Fire could be represented by red
            break;
        case "water":
            newColor = Color.blue; // Water could be represented by blue
            break;
        case "grass":
            newColor = Color.green; // Earth could be represented by green
            break;
        case "electric":
            newColor = new Color(0.5f, 0f, 0.5f);// Lightning could be represented by yellow
            break;
        case "ice":
            newColor = Color.cyan; // Wind could be represented by cyan
            break;
        case "rock":
            newColor = new Color(0.545f, 0.271f, 0.075f);
            break;
        case "light":
            newColor = Color.yellow;
            break;
        case "dark":
            newColor = Color.black;
            break;          
        default:
            //newColor = Color.white; // Default to white if no element is matched
            break;
        }

        // Apply the new color to the sprite
        spriteRenderer.color = newColor;

        string currentElement = ConvertColorToElement(currentColor);
        string reactionResult = CheckElementReaction(currentElement, element);

        if (!string.IsNullOrEmpty(reactionResult))
        {
        // Reaction occurred, apply extra damage
        int extradam=ApplyReactionDamage(reactionResult,dam);
        // Log the reaction
        Debug.Log($"Element reaction: {currentElement} + {element} = {reactionResult}. Extra damage {extradam} applied!");
        health -= extradam;
        if(damageNumberPrefab!=null){
            GameObject damageNumber2 = Instantiate(damageNumberPrefab, transform.position, Quaternion.identity, canvas.transform);
            if(f == Factions.yellow){
                damageNumber2.transform.eulerAngles = new Vector3(
                damageNumber2.transform.eulerAngles.x,  // Keep current X rotation
                180,                                  // Set Y rotation to 180 degrees
                damageNumber2.transform.eulerAngles.z   // Keep current Z rotation
                );
            }
            DamageNumber dnScript2 = damageNumber2.GetComponent<DamageNumber>();
            dnScript2.SetValue(extradam,reactionResult);
        }

        spriteRenderer.color= storeCol;
        }

        dam=(int)(dam*GetElementEffectiveness(ConvertStringToElement(element),ConvertStringToElement(Eelement)));

        if(damageNumberPrefab!=null){
            GameObject damageNumber = Instantiate(damageNumberPrefab, transform.position, Quaternion.identity, canvas.transform);
            if(f == Factions.yellow){
                damageNumber.transform.eulerAngles = new Vector3(
                damageNumber.transform.eulerAngles.x,  // Keep current X rotation
                180,                                  // Set Y rotation to 180 degrees
                damageNumber.transform.eulerAngles.z   // Keep current Z rotation
                );
            }
            DamageNumber dnScript = damageNumber.GetComponent<DamageNumber>();
            dnScript.SetValue(dam,element);
        }
        
        dazedTime = startDazed;
        knockUp((float)dam*10);
        health -= dam;
    }

    string ConvertColorToElement(Color color)
    {
    if (color == Color.red) return "fire";
    if (color == Color.blue) return "water";
    if (color == Color.green) return "grass";
    if (color == new Color(1f, 1f, 0f)) return "electric";
    if (color == Color.cyan) return "ice";
    if (color == new Color(0.545f, 0.271f, 0.075f)) return "rock";
    if (color == Color.yellow) return "light";
    if (color == Color.black) return "dark";
    return "none"; // Default if no match
    }

    string CheckElementReaction(string element1, string element2)
    {
    if ((element1 == "fire" && element2 == "water") || (element1 == "water" && element2 == "fire"))
    {
        return "steam";
    }
    if ((element1 == "electric" && element2 == "water") || (element1 == "water" && element2 == "electric"))
    {
        return "conductivity";
    }
    if ((element1 == "fire" && element2 == "grass") || (element1 == "grass" && element2 == "fire"))
    {
        return "burn";
    }
    if ((element1 == "electric" && element2 == "fire") || (element1 == "fire" && element2 == "electric"))
    {
        return "overload";
    }
    // Add more element reactions as needed
    return null; // No reaction by default
    }

    int ApplyReactionDamage(string reaction,int dam)
    {
        switch (reaction.ToLower())
        {
        case "steam":
            return dam*2; // Add extra damage for steam reaction
        case "conductivity":
            return dam*2; // Add extra damage for conductivity reaction
        case "burn":
            return dam*2; // Add extra damage for burn reaction
        case "overload":
            knockUp((float)dam*60);
            return dam*2; // Add extra damage for burn reaction    
        default:
            return 0;
        }

        
        health -= dam*2;

    }

    public void healing(int dam) {
        
        if(healNumberPrefab!=null){
            GameObject healNumber = Instantiate(healNumberPrefab, transform.position, Quaternion.identity, canvas.transform);
            if(f == Factions.yellow){
                healNumber.transform.eulerAngles = new Vector3(
                healNumber.transform.eulerAngles.x,  // Keep current X rotation
                180,                                  // Set Y rotation to 180 degrees
                healNumber.transform.eulerAngles.z   // Keep current Z rotation
                );
            }
            HealNumber dnScript = healNumber.GetComponent<HealNumber>();
            dnScript.SetValue(dam);
        }
        health += dam;
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

