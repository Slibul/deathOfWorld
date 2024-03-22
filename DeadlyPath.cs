using System;
using System.Collections.Generic;
using Characters;
using Characters.Abilities.Darks;
using Services;
using Singletons;
using HarmonyLib;
using Level;
using Hardmode;
using UnityEngine;
using Data;
using Runnables;
using Level.Waves;

namespace DeadlyOfWorld;

public class DeadlyPath
{
    [HarmonyPostfix]
    [HarmonyPatch((typeof(Character)), nameof(Character.InitializeActions))]
    static void CharacterPostfix(ref Character __instance)
    {
        List<Character> characters = new List<Character>();
        characters.Add(__instance);
        DarkEnemySelector.instance.ElectIn(characters);
    }


    [HarmonyPostfix]
    [HarmonyPatch((typeof(DarkEnemySelector)), nameof(DarkEnemySelector.ElectIn))]
    static void SetCounters(object[] __args, ref DarkAbilityConstructor[] ____constructors)
    {
        if (Plugin.isNoDarker.Value)
        {
            return;
        }

        if (Plugin.isParamet.Value == true)
        {
            System.Random random = new System.Random();
            int num = random.Next(1, 101);
            if (num >= Plugin.isSetP.Value)
            {
                foreach (ICollection<Character> candidates in __args)
                {
                    foreach (Character c in candidates)
                    {
                        if (c.type == Character.Type.TrashMob && 
                            c.key != Key.Hound && 
                            c.key != Key.SpiritInFlask && 
                            c.key != Key.UnstableFlask &&
                            c.key != Key.UnstableFlasksSpirit &&
                            c.key != Key.CarleonRecruit &&
                            c.key != Key.Unspecified)
                            ____constructors[Singleton<HardmodeManager>.Instance.currentLevel].Provide(c); // 모든적이 나와요
                    }
                }
            }
            return;
        }

        if (Plugin.Step.Value == 1)
        {
            foreach (ICollection<Character> candidates in __args)
            {
                foreach (Character c in candidates)
                {
                    if (c.type == Character.Type.TrashMob && c.key != Key.Hound && c.key != Key.SpiritInFlask && c.key != Key.UnstableFlasksSpirit)
                        ____constructors[Singleton<HardmodeManager>.Instance.currentLevel].Provide(c); // 모든적이 나와요
                }
            }
        }
        else if (Plugin.Step.Value == 11)
        {
            RarityPossibilities _rarityPossibilities = new RarityPossibilities();
            Rarity _rarity;
            Chapter currentChapter = Singleton<Service>.Instance.levelManager.currentChapter;

            System.Random random = new System.Random(
            GameData.Save.instance.randomSeed
            + 2028506624
            + (int)currentChapter.type * 256
            + currentChapter.stageIndex * 16
            + currentChapter.currentStage.pathIndex
            );
            int num = random.Next(0, 100) + 1;
            _rarity = _rarityPossibilities.Evaluate(random);
            if (_rarity == Rarity.Common)
            {
                foreach (ICollection<Character> candidates in __args)
                {
                    Debug.LogError(candidates.Count);
                    foreach (Character c in candidates)
                    {
                        if (c.type == Character.Type.TrashMob && c.key != Key.Hound && c.key != Key.SpiritInFlask && c.key != Key.UnstableFlasksSpirit)
                            ____constructors[Singleton<HardmodeManager>.Instance.currentLevel].Provide(c);
                    }
                }
            }
        }
        else if (Plugin.Step.Value == 12)
        {
            RarityPossibilities _rarityPossibilities = new RarityPossibilities();
            Rarity _rarity;
            Chapter currentChapter = Singleton<Service>.Instance.levelManager.currentChapter;

            System.Random random = new System.Random(
            GameData.Save.instance.randomSeed
            + 2028506624
            + (int)currentChapter.type * 256
            + currentChapter.stageIndex * 16
            + currentChapter.currentStage.pathIndex
            );
            int num = random.Next(0, 100) + 1;
            _rarity = _rarityPossibilities.Evaluate(random);
            if (_rarity == Rarity.Rare)
            {
                foreach (ICollection<Character> candidates in __args)
                {
                    Debug.LogError(candidates.Count);
                    foreach (Character c in candidates)
                    {
                        if (c.type == Character.Type.TrashMob && c.key != Key.Hound && c.key != Key.SpiritInFlask && c.key != Key.UnstableFlasksSpirit)
                            ____constructors[Singleton<HardmodeManager>.Instance.currentLevel].Provide(c);
                    }
                }
            }
        }
        else if (Plugin.Step.Value == 13)
        {
            RarityPossibilities _rarityPossibilities = new RarityPossibilities();
            Rarity _rarity;
            Chapter currentChapter = Singleton<Service>.Instance.levelManager.currentChapter;

            System.Random random = new System.Random(
            GameData.Save.instance.randomSeed
            + 2028506624
            + (int)currentChapter.type * 256
            + currentChapter.stageIndex * 16
            + currentChapter.currentStage.pathIndex
            );
            int num = random.Next(0, 100) + 1;
            _rarity = _rarityPossibilities.Evaluate(random);
            if (_rarity == Rarity.Unique)
            {
                foreach (ICollection<Character> candidates in __args)
                {
                    Debug.LogError(candidates.Count);
                    foreach (Character c in candidates)
                    {
                        if (c.type == Character.Type.TrashMob && c.key != Key.Hound && c.key != Key.SpiritInFlask && c.key != Key.UnstableFlasksSpirit)
                            ____constructors[Singleton<HardmodeManager>.Instance.currentLevel].Provide(c);
                    }
                }
            }
        }
        else if (Plugin.Step.Value == 14)
        {
            RarityPossibilities _rarityPossibilities = new RarityPossibilities();
            Rarity _rarity;
            Chapter currentChapter = Singleton<Service>.Instance.levelManager.currentChapter;

            System.Random random = new System.Random(
            GameData.Save.instance.randomSeed
            + 2028506624
            + (int)currentChapter.type * 256
            + currentChapter.stageIndex * 16
            + currentChapter.currentStage.pathIndex
            );
            int num = random.Next(0, 100) + 1;
            _rarity = _rarityPossibilities.Evaluate(random);
            if (_rarity == Rarity.Legendary)
            {
                foreach (ICollection<Character> candidates in __args)
                {
                    Debug.LogError(candidates.Count);
                    foreach (Character c in candidates)
                    {
                        if (c.type == Character.Type.TrashMob && c.key != Key.Hound && c.key != Key.SpiritInFlask && c.key != Key.UnstableFlasksSpirit)
                            ____constructors[Singleton<HardmodeManager>.Instance.currentLevel].Provide(c);
                    }
                }
            }
        }
        else
        {
            Plugin.Step.Value = 1;
            foreach (ICollection<Character> candidates in __args)
            {
                Debug.LogError(candidates.Count);
                foreach (Character c in candidates)
                {
                    if (c.type == Character.Type.TrashMob && c.key != Key.Hound && c.key != Key.SpiritInFlask && c.key != Key.UnstableFlasksSpirit)
                        ____constructors[Singleton<HardmodeManager>.Instance.currentLevel].Provide(c);
                }
            }
        }


    }
}
