using System;
using Runtime;
using Runtime.Player;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerStatusContainer),typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    private PlayerPhysic _physic;
    private PlayerStatus _status;
    private Rigidbody2D _rigid;
    
    private void Awake()
    {
        _status = GetComponent<PlayerStatusContainer>().Status;
        _rigid = GetComponent<Rigidbody2D>();
        _physic = new PlayerPhysic(_status, _rigid);
    }

    private void FixedUpdate()
    {
        _physic.PhysicUpdate();
    }

    public void InputMove(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        switch (context)
        {
            case {phase: InputActionPhase.Performed}:
                _status.SetMoveInput(input);
                break;
            case {phase: InputActionPhase.Canceled}:
                _status.SetMoveInput(input);
                break;
        }
    }
}