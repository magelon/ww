using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Attributes
{
    public int hp;
    public int atk;
    public int mess;
    public float speed;
    public float energy;
    public int force;
    public int rate;
    public string element;
}

[System.Serializable]
public class Item
{
    public string itemsName;
    public Attributes attributes;
}

[System.Serializable]
public class ItemsData
{
    public List<Item> items;
}

//button spawners and energy generate
public class ButtonSpawner : MonoBehaviour
{
    public Transform po;
    public GameObject white;
    //public GameObject yellow;
    public float spawnTime=1f;
    public float startTime;
    private Image im;
    public Text text;
    public float energyCost;
    private bool loaded;

    public TextAsset jsonFile;

    private string imgName;

    public List<Item> loadedItems;

    public SpriteRenderer spriteRenderer; // For a 2D sprite, use SpriteRenderer
    public Image illstrate; // For UI Image, use this (optional)
    public float fadeDuration = 2f; // Duration of the fade-out in seconds
    private float startAlpha = 1f; // Initial alpha, set to fully opaque
    private Vector3 startScale = new Vector3(4f, 4f, 1f); // Initial scale (2x size)
    private Vector3 endScale = new Vector3(1f, 1f, 1f); // Final scale (1x size)

    private Sprite originalSprite;
    private Vector3 originalScale;
    private Color originalColor;
    
    private void Start()
    {
        loaded=false;
        //Invoke("laterStart", 2f);
        //text.text = energyCost*100+"";
         if (illstrate != null)
        {
            originalSprite = illstrate.sprite;
            originalColor = illstrate.color;
            originalScale = illstrate.transform.localScale;
        }
    }

    private void ResetUIImage()
    {
        illstrate.color = new Color(originalColor.r, originalColor.g, originalColor.b, startAlpha); // Reset alpha
        illstrate.transform.localScale = startScale;  // Reset scale to the larger size
    }

    IEnumerator FadeOutAndShrinkSpriteRenderer()
    {
        Color color = spriteRenderer.color;
        float elapsedTime = 0f;

        // Set initial alpha and scale
        color.a = startAlpha;
        spriteRenderer.color = color;
        transform.localScale = startScale;

        // Loop over time to fade alpha down to 0 and scale down to 1
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / fadeDuration;

            // Update alpha
            color.a = Mathf.Lerp(startAlpha, 0f, progress);
            spriteRenderer.color = color;

            // Update scale
            transform.localScale = Vector3.Lerp(startScale, endScale, progress);

            yield return null;
        }

        // Ensure it is fully transparent and at the final scale at the end
        color.a = 0f;
        spriteRenderer.color = color;
        transform.localScale = endScale;
    }

    IEnumerator FadeOutAndShrinkUIImage()
    {
        Color color = illstrate.color;
        float elapsedTime = 0f;

        // Set initial alpha and scale
        color.a = startAlpha;
        illstrate.color = color;
        illstrate.transform.localScale = startScale;

        // Loop over time to fade alpha down to 0 and scale down to 1
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / fadeDuration;

            // Update alpha
            color.a = Mathf.Lerp(startAlpha, 0f, progress);
            illstrate.color = color;

            // Update scale
            illstrate.transform.localScale = Vector3.Lerp(startScale, endScale, progress);

            yield return null;
        }

        // Ensure it is fully transparent and at the final scale at the end
        color.a = 0f;
        illstrate.color = color;
        illstrate.transform.localScale = endScale;
    }

    void laterStart()
    {
        if(!loaded){
            im =transform.Find("Image").GetComponent<Image>();
            if (im.sprite && jsonFile != null)
            {
                loaded=true;
                
                string jsonText = jsonFile.text;
                ItemsData itemsData = JsonUtility.FromJson<ItemsData>(jsonText);

                imgName = im.sprite.name;
                loadedItems = itemsData.items;
                Item item = loadedItems.Find(i => i.itemsName == imgName);
                Debug.Log(imgName);

                if(item!=null){
                    energyCost = item.attributes.energy;
                    text.text = energyCost * 100 + "";
                    //white = Resources.Load<GameObject>("sumPrefabs/" + imgName);
                    white = Resources.Load<GameObject>("rightPrefabs/" + imgName);
                    white.GetComponent<Enime>().f=Factions.yellow;
                    poolManager.instance.CreatePool(white, 15);
                }
                
            }
        }
    }

    void Update()
    {
        startTime += Time.deltaTime;
        if (im != null)
        {
            im.fillAmount = startTime / spawnTime;
        }
        laterStart();
    }

    public void spawn()
    {
        if (startTime > spawnTime)
        {
            GameManager.getInstance().playSfx("buttonSpawn");
            if (GameData.getInstance().energy > energyCost)
            {
                startTime = 0;
                poolManager.instance.ReuseObject(white, 
                new Vector3(po.position.x, po.position.y + 0.2f, 0), Quaternion.identity);
                GameData.getInstance().energy -= energyCost;
                ResetUIImage();
                illstrate.sprite=Resources.Load<Sprite>("sumPrefabs/illistrate/" + imgName);

                if (spriteRenderer != null)
                {
                    StartCoroutine(FadeOutAndShrinkSpriteRenderer());
                }
                else if (illstrate != null)
                {
                    StartCoroutine(FadeOutAndShrinkUIImage());
                }
            }
        }
    }
}
