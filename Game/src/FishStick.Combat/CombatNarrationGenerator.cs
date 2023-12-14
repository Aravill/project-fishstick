namespace FishStick.Combat
{
  public static class CombatNarrationGenerator
  {
    public static string GenerateEnemyAttackSentence(
      string enemyName,
      CreatureTypeEnum enemyCreatureType,
      string weaponName,
      DamageTypeEnum damageType,
      bool hitResult,
      int damagePercentage = 0
    )
    {
      string attackFlavor = Narration.AttackList.GetRandomWord();
      string hitResultConnector = $"{(hitResult ? " and" : ", but")}";
      string hitOrMissFlavour = Narration.HitMissTable.GetRandomWord(hitResult);
      // "The unicorn attacks you with their horn, but misses." or "The unicorn attacks you with their horn and hits."
      string firstSentence = $"The {{{enemyName}}} {attackFlavor} with their {{{weaponName}}}{hitResultConnector} {{{hitOrMissFlavour}}}.";
      if (!hitResult)
      {
        // The Enemy missed.
        // In this case, the hisMissFlavour ends the sentence. The player doesn't need more information.
        return firstSentence;
      }
      // The enemy hit, we want additional flavour text.
      string verb = Narration.InjuryVerbTable.GetRandomWord(damageType);
      string bodyPart = Narration.BodyPartGenerator.GetRandomBodyPart(enemyCreatureType, true);
      string injury = Narration.InjuryTable.GetRandomWordCombination(damagePercentage, damageType);
      // "They pierce your chest, causing a minor injury."
      string secondSentence = $"They {{{verb}}} {bodyPart}, causing {{{injury}}}.";
      // Now just join the sentences
      return $"{firstSentence} {secondSentence}";
    }
  }
}