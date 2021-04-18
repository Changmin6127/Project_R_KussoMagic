namespace Anvil
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public enum DamageType { None, Damage, Heal }

    public partial class DamageObject : MonoBehaviour   //Data Field
    {
        [SerializeField]
        private DamageType damageType = DamageType.None;
        [SerializeField]
        private float knockBackSpeed = 500;
        [SerializeField]
        private int value = 0;
    }

    public partial class DamageObject : MonoBehaviour   //Function Field
    {
        public DamageType GetDamageType()
        {
            return damageType;
        }

        public int GetDamageValue()
        {
            return value;
        }

        public float GetKnockBackSpeed()
        {
            return knockBackSpeed;
        }
    }
}