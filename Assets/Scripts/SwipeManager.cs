using UnityEngine;
using System.Collections.Generic;
using System.Linq;

class CardinalDirection
{
    public static readonly Vector2 Up = new Vector2(0, 1);
    public static readonly Vector2 Down = new Vector2(0, -1);
    public static readonly Vector2 Right = new Vector2(1, 0);
    public static readonly Vector2 Left = new Vector2(-1, 0);
}

public enum Swipe
{
    None,
    Up,
    Down,
    Left,
    Right
};

public class SwipeManager : MonoBehaviour
{
    #region Inspector Variables

    [Tooltip("Min swipe distance (inches) to register as swipe")]
    [SerializeField] float minSwipeLength = 0.5f;

    [Tooltip("If true, a swipe is counted when the min swipe length is reached. If false, a swipe is counted when the touch/click ends.")]
    [SerializeField] bool triggerSwipeAtMinLength = false;

    #endregion

    const float fourDirAngle = 0.5f;
    const float defaultDPI = 72f;
    const float dpcmFactor = 2.54f;

    static Dictionary<Swipe, Vector2> cardinalDirections = new Dictionary<Swipe, Vector2>()
    {
        { Swipe.Up,         CardinalDirection.Up                 },
        { Swipe.Down,         CardinalDirection.Down             },
        { Swipe.Right,         CardinalDirection.Right             },
        { Swipe.Left,         CardinalDirection.Left             },
    };

    public delegate void OnSwipeDetectedHandler(Swipe swipeDirection, Vector2 swipeVelocity);

    static OnSwipeDetectedHandler _OnSwipeDetected;
    public static event OnSwipeDetectedHandler OnSwipeDetected
    {
        add
        {
            _OnSwipeDetected += value;
            autoDetectSwipes = true;
        }
        remove
        {
            _OnSwipeDetected -= value;
        }
    }
    
    public static Vector2 swipeVelocity;

    static float dpcm;
    static float swipeStartTime;
    static float swipeEndTime;
    static bool autoDetectSwipes = true;
    public static bool swipeEnded=true;
    static Swipe swipeDirection;
    static Vector2 firstPressPos;
    static Vector2 secondPressPos;
    static SwipeManager instance;


    void Awake()
    {
        instance = this;
        float dpi = (Screen.dpi == 0) ? defaultDPI : Screen.dpi;
        dpcm = dpi / dpcmFactor;
    }

    private void Start()
    {
        
    }

    void Update()
    {
        swipeDirection = Swipe.None;
        if (autoDetectSwipes)
        {
            DetectSwipe();
        }
    }

    /// <summary>
    /// Attempts to detect the current swipe direction.
    /// Should be called over multiple frames in an Update-like loop.
    /// </summary>
    static void DetectSwipe()
    {
        if (GetTouchInput() || GetMouseInput())
        {
            // Swipe already ended, don't detect until a new swipe has begun
            if (swipeEnded)
            {
                //Debug.Log("Свайпнул");
                return;
            }

            Vector2 currentSwipe = secondPressPos - firstPressPos;
            float swipeCm = currentSwipe.magnitude / dpcm;
            //Debug.Log(swipeCm);
            // Check the swipe is long enough to count as a swipe (not a touch, etc)
            if (swipeCm < instance.minSwipeLength)
            {
                swipeDirection = Swipe.None;

                if (!Application.isEditor)
                    return;

                //Debug.Log("[SwipeManager] Swipe was not long enough.");
                return;
            }
            swipeEndTime = Time.time;
            swipeVelocity = currentSwipe * (swipeEndTime - swipeStartTime);
            swipeDirection = GetSwipeDirByTouch(currentSwipe);
            swipeEnded = true;

            if (_OnSwipeDetected != null)
            {
                _OnSwipeDetected(swipeDirection, swipeVelocity);
            }

        }
        else
        {
            swipeDirection = Swipe.None;
        }
    }

    public static bool IsSwiping() { return swipeDirection != Swipe.None; }
    public static bool IsSwipingRight() { return IsSwipingDirection(Swipe.Right); }
    public static bool IsSwipingLeft() { return IsSwipingDirection(Swipe.Left); }
    public static bool IsSwipingUp() { return IsSwipingDirection(Swipe.Up); }
    public static bool IsSwipingDown() { return IsSwipingDirection(Swipe.Down); }

    #region Helper Functions

    static bool GetTouchInput()
    {
        if (Input.touches.Length <= 0) 
        {
            return false;
        }

        Touch t = Input.GetTouch(0);
        /*if (t.phase != TouchPhase.Began || t.phase != TouchPhase.Ended) 
        {
            secondPressPos = t.position;
            return instance.triggerSwipeAtMinLength;
        }
        else*/ 
        if (t.phase == TouchPhase.Began)
        {
            firstPressPos = t.position;
            swipeStartTime = Time.time;
            //Debug.Log("Офицер даун");
            swipeEnded = false;
            // Swipe/Touch ended
        }
        else if (t.phase == TouchPhase.Ended)
        {
            secondPressPos = t.position;
            //Debug.Log("Офицер ап");
            return true;
            // Still swiping/touching
        }
        else if (!swipeEnded&&t.phase!=TouchPhase.Ended)
        {
            secondPressPos = t.position;
            return instance.triggerSwipeAtMinLength;
        }

        return false;
    }

    static bool GetMouseInput()
    {

        // Swipe/Click started
        if (Input.GetMouseButtonDown(0))
        {
            firstPressPos = (Vector2)Input.mousePosition;
            swipeStartTime = Time.time;
            swipeEnded = false;
            //Debug.Log("Офицер даун");
            // Swipe/Click ended
        }
        else if (Input.GetMouseButtonUp(0))
        {
            secondPressPos = (Vector2)Input.mousePosition;
            //Debug.Log("Офицер ап");
            return true;
            // Still swiping/clicking
        }
        else if(!swipeEnded&&Input.GetMouseButton(0))
        {
            secondPressPos = (Vector2)Input.mousePosition;
            return instance.triggerSwipeAtMinLength;
        }

        return false;
    }

    static bool IsDirection(Vector2 direction, Vector2 cardinalDirection)
    {
        var angle = fourDirAngle;
        return Vector2.Dot(direction, cardinalDirection) > angle;
    }

    static Swipe GetSwipeDirByTouch(Vector2 currentSwipe)
    {

        currentSwipe.Normalize();
        var swipeDir = cardinalDirections.FirstOrDefault(dir => IsDirection(currentSwipe, dir.Value));
        //Debug.Log(swipeDir.Key);
        return swipeDir.Key;
    }

    static bool IsSwipingDirection(Swipe swipeDir)
    {
        //DetectSwipe();
        if(swipeDirection == swipeDir)
        {
            //swipeDirection = Swipe.None;
            return true;
        }
        return false;
    }

    #endregion
}
