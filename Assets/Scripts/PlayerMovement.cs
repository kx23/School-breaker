using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement
{

    public enum PositionState
    {
        LeftEdge,
        Left,
        Middle,
        Right,
        RightEdge
    }

    private Dictionary<PositionState, float> positionsX = new Dictionary<PositionState, float>()
    {
        { PositionState.LeftEdge, -8f },
        { PositionState.Left, -4f  },
        { PositionState.Middle, 0f },
        { PositionState.Right, 4f  },
        { PositionState.RightEdge, 8f  },
    };

    private PositionState currentPosState = PositionState.Middle;
    private PositionState pastPosition = PositionState.Middle;
    private Player player;
    private Coroutine fallCoroutine = null;
    public float lowJumpMultiplier = 4.5f;
    public PlayerMovement(Player player) 
    {
        this.player = player;
    }
    private float swipeSpeed = 10f;
    public void ToMiddlePos() => currentPosState = PositionState.Middle;
    public void Move()
    {

        player.transform.position = Vector3.Lerp(player.transform.position, new Vector3(positionsX[currentPosState],
                                                                        player.transform.position.y,
                                                                        player.transform.position.z),
                                                                        swipeSpeed * Time.deltaTime);
    }
    public void DetectMovements() 
    {
        if (SwipeManager.IsSwipingLeft())
        {
            SideMovement(PositionState.LeftEdge, "Strafe_Left", -1);
            player.AudioSource.Play();
            return;
        }

        if (SwipeManager.IsSwipingRight())
        {
            SideMovement(PositionState.RightEdge, "Strafe_Right", 1);
            player.AudioSource.Play();
            return;
        }
        if (player.Rigidbody.velocity.y > 0)
        {
            player.Rigidbody.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
        if (SwipeManager.IsSwipingUp())
        {

            if (!player.GetGroundChecker.IsGrounded) return;

            player.Rigidbody.velocity = Vector3.zero;

            player.Rigidbody.AddForce(new Vector3(0, 18f, 0), ForceMode.Impulse);
            player.Animator.SetTrigger("Jump");
            if (fallCoroutine != null)
                player.StopCoroutine(fallCoroutine);
            fallCoroutine = player.StartCoroutine(Fall());
            player.AudioSource.Play();
            return;
        }

        if (SwipeManager.IsSwipingDown())
        {
            if (player.Animator.GetCurrentAnimatorStateInfo(0).IsName("Rol")) return;

            if (!player.GetGroundChecker.IsGrounded) player.Rigidbody.AddForce(new Vector3(0, -1000f, 0));
            player.Animator.Play("Rol");
            player.AudioSource.Play();
            return;
        }
    }


    private void SideMovement(PositionState state, string animationName, int curPosStateChange) 
    {
        if (currentPosState == state || player.IsStunned)
            return;

        if (player.GetGroundChecker.IsGrounded && !player.Animator.GetCurrentAnimatorStateInfo(0).IsName("Rol"))
            player.Animator.Play(animationName);

        pastPosition = currentPosState;
        currentPosState+= curPosStateChange;
    }

    private IEnumerator Fall()
    {
        yield return new WaitForSeconds(.5f);
        if (!player.GetGroundChecker.IsGrounded)
        {
            player.Rigidbody.velocity = Vector3.zero;
            player.Rigidbody.AddForce(0, -10f, 0, ForceMode.Impulse);
        }
    }

    public void PastPos()
    {
        currentPosState = pastPosition;
    }


}
