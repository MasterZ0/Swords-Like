using Z3.UIBuilder.Core;
using UnityEngine;

namespace Z3.DemoSkull.Data
{
    [System.Serializable]
    public class PlayerStatusSettings
    {

        [Title("Start Values")]
        [Min(1)]
        [SerializeField] private int baseHP = 25;
        [Min(1)]
        [SerializeField] private int baseMP = 25;
        [Min(1)]
        [SerializeField] private int baseSP = 25;

        [Title("Multipliers")]
        [Range(0, 1)] public float style;   
        [Range(0, 5)] public int damage;
        [Range(0, 5)] public int agility;
        [Range(0, 5)] public int support;
        [Range(0, 5)] public int resistence;

        [Title("Stamina")]
        [Range(0, 2f)]
        [SerializeField] private float lowStaminaRegeneration = 1f; 
        [Range(0, 2f)]
        [SerializeField] private float defaultStaminaRegeneration = .25f;
        [Range(0, 2f)]
        [SerializeField] private float defaultManaRegeneration = .25f;

        // Start Values
        public int BaseHP => baseHP;
        public int BaseMP => baseMP;
        public int BaseSP => baseSP;

        // Stamina
        public float DefaultStaminaRegeneration => defaultStaminaRegeneration;
        public float LowStaminaRegeneration => lowStaminaRegeneration;
        public float DefaultManaRgeneration => defaultManaRegeneration;


        /*
    public int maxLife;         // Resistence
    public float moveSpeed;       // Agility

    public int attackDamage;    // Physical
    public int abilityPower;    // Magic (Damage/Support)
    //public int armor;             100 / (100 + 100)
    //public int magicResistence;    // 10 armadura = 10 de vida = 11 vida?

    // Enum hability style {Mana, Furia, None} -> Mana = Value
    //Lifesteel, vampirismo magico , velocidade de attack, penetração de armadura,  penetração magica, dano verdadeiro, tempo de recarga, %regeneração de vida/mana, % acerto critico
    // debuff: slow, stun, %dano da vida máxima, Aceleração de Habilidade, escudo, %cura, redução de cura (não soma, apenas aumenta até o limite)
    //Buff: Extra XP, Sorte
         */
    }
}