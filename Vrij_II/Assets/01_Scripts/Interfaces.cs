using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable {

    public virtual void Hit(float _dmg) {}

    public virtual void Die() {}
}