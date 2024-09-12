using UnityEngine;

public class SwipeController : MonoBehaviour
{
    private Vector2 startTouchPosition, endTouchPosition;
    private float minSwipeDistance = 50f; // Minimum distance for a swipe to be considered valid

    void Update()
    {
        DetectSwipe();
    }

    void DetectSwipe()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                startTouchPosition = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                endTouchPosition = touch.position;
                Vector2 swipeVector = endTouchPosition - startTouchPosition;

                if (swipeVector.magnitude >= minSwipeDistance)
                {
                    float x = swipeVector.x;
                    float y = swipeVector.y;

                    if (Mathf.Abs(x) > Mathf.Abs(y))
                    {
                        if (x > 0)
                            MovePlayer(Vector2.right);
                        else
                            MovePlayer(Vector2.left);
                    }
                    else
                    {
                        if (y > 0)
                            MovePlayer(Vector2.up);
                        else
                            MovePlayer(Vector2.down);
                    }
                }
            }
        }
    }

    void MovePlayer(Vector2 direction)
    {
        // Call a method to move the player
        FindObjectOfType<PlayerMovement>().Move(direction);
    }
}
