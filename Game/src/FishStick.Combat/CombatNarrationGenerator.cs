namespace FishStick.Combat
{
  public static class CombatNarrationGenerator
  {
    public static string GenerateEnemyAttackSentence(
      string enemyName,
      string weaponName,
      DamageTypeEnum damageType,
      bool hitResult,
      int damagePercentage = 0
    )
    {
      string attackPart = $"The {enemyName} attacks you with their {weaponName}";
      string hitResultConnector = $"{(hitResult ? " and " : ", but ")}";
      string hitMissFlavour = CombatWordTable.HitMissTable.GetRandomWord(hitResult);
      if (!hitResult)
      {
        // The Enemy missed.
        // In this case, the hisMissFlavour ends the sentence with a miss information.

        // "The unicorn attacks you with their horn, but misses."
        return $"{attackPart}{hitResultConnector}{hitMissFlavour}.";
      }
      // The enemy hit, we need additional flavour text.
      string verb = CombatWordTable.DamageTypeVerbTable.GetRandomWord(damageType);
      string bodyPart = CombatWordTable.PlayerBodyPartList.GetRandomWord();
      string damageSeverity = CombatWordTable.DamageSeverityTable.GetRandomWord(damagePercentage);

      // "The unicorn attacks you with their horn and hits. They pierce your chest, causing a minor injury."
      return $"{attackPart}{hitResultConnector}{hitMissFlavour}. They {verb} your {bodyPart}, causing {damageSeverity}.";
    }
  }
}