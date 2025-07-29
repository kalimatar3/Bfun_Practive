using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IDameable
{
    public Collider2D Box {  get; set; } 
    public IHitable lasthit {get; set ;}
    public void DeductHp(float dame);
    public IEnumerator GetStun(float time);
    public void Knockback(IHitable hitable,float power);
    public void Dead();
    public Transform GetTransform();
}
