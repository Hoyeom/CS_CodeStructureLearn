using System;
using System.Collections.Generic;
using Runtime.Player;
using UnityEngine;

namespace Runtime
{
    [CreateAssetMenu(fileName = "PlayerStatus")]
    public class PlayerStatus : ScriptableObject, ISerializationCallbackReceiver, IPlayerStatusEvent
    {

    #region STATUS

    [SerializeField] private float _maxHealth;
    private float _health;
    [NonSerialized] public float MaxHealth;
    public float Health
    {
        get => _health;
        set
        {
            if (_health <= 0)
            {
                return;
            }

            _health = value;
            RaiseChangedHealth(Health, MaxHealth);

            if (_health <= 0)
            {
                RaisePlayerDied();
            }
        }
    }

    [SerializeField] private float _speed;
    private float curSpeed;
    public Vector2 Velocity => curSpeed * moveInput;
    
    #endregion

    #region INPUT

    private Vector2 moveInput = Vector2.zero;
    public void SetMoveInput(Vector2 input)
    {
        input.Normalize();
        moveInput = input;
    }

    #endregion
    

    #region EVENT


    private List<PlayerEventListener> _listeners =
        new List<PlayerEventListener>();

    private void RaisePlayerDied()
    {
        for (int i = 0; i < _listeners.Count; i++)
        {
            _listeners[i].OnPlayerDied();
        }
    }

    private void RaiseChangedHealth(float curHealth, float maxHealth)
    {
        for (int i = 0; i < _listeners.Count; i++)
        {
            _listeners[i].OnChangedHealth(curHealth, maxHealth);
        }
    }

    public void RegisterListener(PlayerEventListener listener) =>
        _listeners.Add(listener);

    public void UnregisterListener(PlayerEventListener listener) =>
        _listeners.Remove(listener);


    #endregion

    #region INITIALIZE

    public void OnAfterDeserialize()
    {
        _health = MaxHealth = _maxHealth;
        curSpeed = _speed;
    }

    public void OnBeforeSerialize()
    {
    }

    #endregion

    }
}