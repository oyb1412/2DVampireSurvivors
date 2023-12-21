using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;
    public int penetrate;


    public void Init(int damage, int penetrate)
    {
        this.damage = damage;
        this.penetrate = penetrate;
    }
}
