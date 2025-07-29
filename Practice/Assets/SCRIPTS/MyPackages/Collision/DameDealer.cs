using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Collider2D))]
public abstract class DameDealer : MyBehaviour, IHitable
{
    [SerializeField] public Collider2D Box { get; set; }
    [SerializeField] protected float dame = 1;
    [SerializeField] protected bool CanDeal = true;
    [SerializeField] protected bool MultiObjHit;
    public float Dame { get {  return dame; } set { dame = value; } }

    protected virtual void OnEnable() {
        this.CanDeal = true;
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadBoxCollider();
    }
    protected virtual void LoadBoxCollider()
    {
        this.Box = GetComponent<Collider2D>();
        this.Box.isTrigger = true;
    }
    public virtual void Hit(IDameable dameable)
    {
        if(!CanDeal) return;
        if(!MultiObjHit) CanDeal = false;
        dameable.lasthit = this;
        this.DealDame(dame,dameable);
    }
    protected virtual void DealDame(float dame,IDameable dameable) {
        dameable.DeductHp(dame);
        Debug.Log(this.transform.name + " Deal to " + dameable.GetTransform().name + " "  + dame);
    }
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<IDameable>() != null)
        {
            this.Hit(collision.GetComponent<IDameable>());
        }
    }
    protected virtual void  OnTriggerStay2D(Collider2D other) {
        if(other.GetComponent<IDameable>() != null)
        {
            this.Hit(other.GetComponent<IDameable>());
        }        
    }
    protected virtual void OnCollisionEnter2D(Collision2D other) {
        if(other.transform.GetComponent<IDameable>() != null && !this.Box.isTrigger)
        {
            this.Hit(other.transform.GetComponent<IDameable>());
        }
    }
    public Transform GetTransform()
    {
       return this.transform;
    }

    public virtual void DoEffect(IDameable dameable)
    {
    }
}
