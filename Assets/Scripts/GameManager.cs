using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Rigidbody pokeballPrefabRB;

    private Vector2 startSwipePosition;

    private float startSwipeTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Process the script if the user mader a touch
        if(Input.touchCount != 1) 
            return;

        var touch = Input.GetTouch(0); //track the first touch

        if(touch.phase == TouchPhase.Began)
        {
            startSwipePosition = touch.position;
            startSwipeTime = Time.time;
        } 

        //when the user touches up, we want to calculate the Velocity
        //then throw the ball w/ that velocity 
        else if(touch.phase == TouchPhase.Ended) 
        {
            var currentPosition = touch.position; 
            var currentTime = Time.time; 
            var distance = (currentPosition - startSwipePosition).magnitude;
            var timeChange = currentTime - startSwipeTime;
            var velocity = distance/timeChange;
            throwBall(velocity); 
        }
    }

    private void throwBall(float velocity) 
    {
        //instantiate the ball at x position
        var position = Camera.main.transform.position + (Camera.main.transform.forward * 0.5f);
        var ball = Instantiate(pokeballPrefabRB, position, Camera.main.transform.rotation);

        //give ball a direction 
        var direction = Vector3.RotateTowards(Camera.main.transform.forward, Vector3.up, Mathf.Deg2Rad * 30, 0);

        //give ball a velocity 
        ball.velocity = direction * velocity * 0.001f;

        //give ball a rotation
        ball.angularVelocity = Random.insideUnitSphere * Random.Range(0.5f, 2);

    }
}
