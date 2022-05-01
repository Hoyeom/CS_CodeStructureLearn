using UnityEngine;

namespace Runtime
{
    public class PlayerPhysic
    {
        private PlayerStatus _status;
        private Rigidbody2D _rigid;

        public PlayerPhysic(PlayerStatus status, Rigidbody2D rigid)
        {
            _status = status;
            _rigid = rigid;
        }

        public void PhysicUpdate()
        {
            _rigid.MovePosition(_rigid.position + _status.Velocity * Time.deltaTime);
        }
    }
}