using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G_BallBehaviour : MonoBehaviour
{
    private int gridX, gridY;
    private Color color;
    private bool isAnchored, onGrid;
    private SpriteRenderer SpriteRenderer;

    // Use this for initialization
    void Start()
    {
        Initialize();
    }

    void Initialize()
    {
        // tag the ball
        gameObject.tag = "Ball";
        // updates position variables
        gridX = (int)transform.position.x;
        gridY = (int)transform.position.y;
        // captures the color of the current ball in the color variable
        color = transform.GetComponent<SpriteRenderer>().color;
        // updates the values of the grid to represent this ball 
        Grid.colorGrid[gridX, gridY] = color;
        Grid.grid[gridX, gridY] = transform;
        Grid.anchoredGrid[gridX, gridY] = true;
        // sets this balls values for being on grid and anchored
        onGrid = true;
        isAnchored = true;


    }








    void FixedUpdate()
    {

        // pops the ball if it is flagged
        if (onGrid && Grid.colorGrid[gridX, gridY] == Color.clear)
            kill();
        // sets the bool of anchored property from the grid
        if (onGrid)
            isAnchored = Grid.anchoredGrid[gridX, gridY];
        // if the ball is not anchored, pop it
        if (onGrid && !isAnchored && Grid.checkAnchorDone)
            kill();
    }

    
    /*
	 * Method that calls all other functions that have to do with killing this bubble
	 */
    public void kill()
    {
        // stop all coroutines
        StopAllCoroutines();
        // start the kill routine
        StartCoroutine(ScaleTo(0.15f));
    }

    /*
	 * coroutine that scales the ball down to 0 then destroys the gameobject
	 * @param {float} speed the scale is changed
	 */
    IEnumerator ScaleTo(float duration)
    {

        float timeThrough = 0.0f;
        Vector3 scale = new Vector3(0, 0, 0);
        Vector3 initialScale = transform.localScale;

        while (transform.localScale.x >= 0.1)
        {
            timeThrough += Time.deltaTime;
            Vector3 target = Vector3.Lerp(initialScale, scale, timeThrough / duration);
            transform.localScale = target;
            yield return null;
        }
        if (transform.localScale.x <= 0.1)
        {
            Destroy(transform.GetComponent<Rigidbody>());
            Destroy(transform.GetComponent<Collider>());
            Grid.grid[gridX, gridY] = null;
            Grid.anchoredGrid[gridX, gridY] = false;
            Grid.colorGrid[gridX, gridY] = Color.clear;
            Grid.CAStarter();
            Destroy(this.gameObject);
        }
    }
}


