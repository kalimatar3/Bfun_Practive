using System.Collections;
using UnityEngine;
[RequireComponent(typeof(Collider2D))]
public abstract class DameReciver : MyBehaviour, IDameable
{
    [SerializeField] protected float hp;
    [SerializeField] protected bool isDead;
    public float Hp { get { return hp; } set {  hp = value; } }
    public bool IsDead {get {return isDead;} set {isDead = value;}}
    public Collider2D Box { get; set; }
    public IHitable lasthit { get ; set ; }

    protected virtual void LoadBox()
    {
        this.Box = GetComponent<Collider2D>();
        this.Box.isTrigger = true;
    }
    protected virtual void OnEnable()
    {
        this.isDead = false;
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadBox();
    }
    public virtual void Dead()
    {
        if(isDead) return;
        Debug.Log(this.transform.name + "Dead");
        this.isDead = true;
    }
    public virtual void DeductHp(float dame)
    {
        if (isDead) return;
        if(hp - dame <= 0)
        {
            this.Dead();
            hp = 0;
            return;
        }
        hp -= dame;
        Debug.Log(hp);
    }
    public virtual void IcrHP(float number)
    {
        this.hp += number;
        Debug.Log(this.transform.parent.name + " + " + number + "HP");
    }

    public Transform GetTransform()
    {
        return this.transform;
    }

    public virtual IEnumerator GetStun(float time)
    {
       yield return null;
    }

    public virtual void Knockback(IHitable hitable, float power)
    {
    }
}
