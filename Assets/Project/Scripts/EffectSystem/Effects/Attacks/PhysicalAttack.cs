
        public void Apply(IDamageable applyTarget) => applyTarget.Apply(this);

        public int CalculateDamage(ResistanceComponent resistanceComponent)
            => IAttack.CalculateDamage(Amount, resistanceComponent.flatDamageReduction,
                resistanceComponent.physicalResistance);
    }
}