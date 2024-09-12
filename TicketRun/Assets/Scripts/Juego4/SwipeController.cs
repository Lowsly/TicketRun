using UnityEngine;

public class SwipeController : MonoBehaviour
{
    private Vector2 startTouchPosition, endTouchPosition;
    private float minSwipeDistance = 50f; // Minimum distance for a swipe to be considered valid
    private bool inputReceived = false; // To track if an input has been received

    void Update()
    {
        #if UNITY_EDITOR || UNITY_STANDALONE
            DetectKeyboardInput();
        #endif

        DetectSwipe();
    }

    void DetectSwipe()
    {
        if (Input.touchCount > 0 && !inputReceived)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                startTouchPosition = touch.position;
                inputReceived = true; // Mark input as received
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
                inputReceived = false; // Reset input after swipe is processed
            }
        }
    }

    void DetectKeyboardInput()
    {
        if (!inputReceived)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                MovePlayer(Vector2.right);
                inputReceived = true;
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                MovePlayer(Vector2.left);
                inputReceived = true;
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                MovePlayer(Vector2.up);
                inputReceived = true;
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            {
                MovePlayer(Vector2.down);
                inputReceived = true;
            }
        }

        // Reset input flag if no key is pressed
        if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D) ||
            Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A) ||
            Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.W) ||
            Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.S))
        {
            inputReceived = false;
        }
    }

    void MovePlayer(Vector2 direction)
    {
        // Call a method to move the player
        FindObjectOfType<PlayerMovement>().Move(direction);
    }
}
