using FishStick.Combat.Narration;

namespace FishStick.Combat
{
  public static class CombatNarrationGenerator
  {
    /// <summary>
    /// Generates a couple of sentences describing an attack based on the parameters passed. Adding
    /// flavour to make combat more interesting.
    /// </summary>
    /// <param name="enemyName">Name of the enemy</param>
    /// <param name="enemyCreatureType">Type of the enemy</param>
    /// <param name="weaponName">Name of the weapon the player / enemy is using</param>
    /// <param name="damageType">Type of damage the weapon deals</param>
    /// <param name="hitResult">Whether the player / enemy hit their attack</param>
    /// <param name="subject">Subject of the sentence (are we decribing a player's or an enemy's action)</param>
    /// <param name="damagePercentage">The amount of damage relative to the subject's health that has been dealt</param>
    /// <returns></returns>
    public static string GenerateAttackNarration(
      string enemyName,
      CreatureTypeEnum enemyCreatureType,
      string weaponName,
      DamageTypeEnum damageType,
      bool hitResult,
      SubjectEnum subject,
      int damagePercentage = 0
    )
    {
      string firstSentence = GenerateFirstAttackSentence(enemyName, weaponName, hitResult, subject);
      string secondSentence = GenerateSecondAttackSentence(enemyCreatureType, damageType, damagePercentage, subject);
      return $"{firstSentence} {secondSentence}";
    }

    private static string GenerateFirstAttackSentence(
      string enemyName,
      string weaponName,
      bool hitResult,
      SubjectEnum subject
    )
    {
      // We get the first sentence components already mutated for the subject
      string attackFlavor = AttackList.GetRandomWord(subject); // "attack" or "attacks" with flavor
      string hitResultConnector = $"{(hitResult ? " and" : ", but")}"; // " and" or ", but"
      string hitOrMissFlavour = HitMissTable.GetRandomWord(hitResult, subject); // "hits" or "misses" with flavor

      // Then we build the first sentence based on the subject
      string firstSentence;
      if (subject == SubjectEnum.NPC)
        // The subject of the sentence is an NPC doing something to the player
        // "The unicorn attacks you with their horn, but misses." or "The unicorn attacks you with their horn and hits."
        firstSentence = $"The {{{enemyName}}} {attackFlavor} you with their {{{weaponName}}}{hitResultConnector} {{{hitOrMissFlavour}}}.";
      else
        // The subject of the sentence is the player doing something to an NPC
        // "You attack the unicorn with your sword, but miss." or "You attack the unicorn with your sword and hit."
        firstSentence = $"You {attackFlavor} the {{{enemyName}}} with your {{{weaponName}}}{hitResultConnector} {{{hitOrMissFlavour}}}.";
      return firstSentence;
    }

    private static string GenerateSecondAttackSentence(
      CreatureTypeEnum creatureType,
      DamageTypeEnum damageType,
      int damagePercentage,
      SubjectEnum subject
    )
    {
      // We get the second sentence components already mutated for the subject
      string verb = InjuryVerbTable.GetRandomWord(damageType);
      // Little magic here, the subject of the body part is always the opposing creature, so we
      // swap the subject to the opposite of the current subject. This is a short term solution
      // and won't work once we have more than two subjects
      SubjectEnum oppositeSubject = subject == SubjectEnum.NPC ? SubjectEnum.Player : SubjectEnum.NPC;
      string bodyPart = BodyPartGenerator.GetRandomBodyPart(creatureType, oppositeSubject);
      string injury = InjuryTable.GetRandomWordCombination(damagePercentage, damageType);
      string pronoun = subject == SubjectEnum.NPC ? "They" : "You";
      // Then we build the second sentence based on the subject with the correct pronoun
      // This is easier than the first sentence as we're only replacing one word in the sentence
      return $"{pronoun} {{{verb}}} {bodyPart}, causing {{{injury}}}.";
    }
  }
}