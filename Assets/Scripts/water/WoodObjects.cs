using UnityEngine;

public class FloatingObject : MonoBehaviour
{
    public float floatHeight;
    public float liftForce;
    public float damping;
    public LayerMask waterLayer;
    private bool is_Floating=false;
    private Enime e;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //waterLayer = LayerMask.NameToLayer("Default");

        if (rb == null)
        {
            Debug.LogError("Rigidbody2D component not found. Make sure it's attached to the GameObject.");
        }
    }

//to make it work play around the physic layer setting, let dead layer object only collider with water layer, and default layer only collide with land layer
    private void Update(){
         //if(is_Floating&&rb){
                //Cast a ray straight down. only with waterlayer
                if(LayerMask.LayerToName(gameObject.layer)=="dead"){
                 RaycastHit2D hit;
                 hit=Physics2D.Raycast(transform.position,transform.TransformDirection(Vector2.down),Mathf.Infinity, waterLayer);
                  if (hit.collider != null){
                      Debug.Log(hit.collider.name);
                     
                  }
                 float distance = Mathf.Abs(hit.point.y - transform.position.y);
                 float heightError = floatHeight - distance;
                 //float force = liftForce * heightError - rb.velocity.y * damping;

                float force = liftForce * heightError - rb.velocity.y * damping;

                // Apply the force to the rigidbody.
                rb.AddForce(Vector2.up * force);
                //rb.AddForce(Vector2.up * 10);
         //}
                }
    }

    // private void FixedUpdate()
    // {
    //     // if (rb == null){
    //     //     rb = GetComponent<Rigidbody2D>();
    //     // }

    //     RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, waterLayer);
    //     Debug.DrawRay(transform.position, Vector2.down, Color.yellow);
    //     //bool ali=GetComponent<Enime>().alive;
    //     int currentLayer = gameObject.layer;
    //     string currentLayerName = LayerMask.LayerToName(currentLayer);

    //     if (hit.collider != null &&(currentLayerName=="dead"))//&&!ali
    //     {
    //         Debug.Log("hit");
    //         //Debug.DrawRay(transform.position, Vector2.down * hit.distance, Color.yellow);
    //         is_Floating=true; 
    //     }else{
    //         Debug.Log("not hit");
    //         is_Floating=false;
    //     }
    // }
}
