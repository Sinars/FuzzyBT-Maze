using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Buff {
    public Effect BuffType { get; private set; }

    public float Rate { get; private set; }

    public float Damage { get; private set; }

    public float ElapsedTime { get; set; }
    

    public Buff(Effect buff, float rate, float damage) {
        BuffType = buff;
        Rate = rate;
        Damage = damage;
    }

}
