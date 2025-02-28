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
using Hardmode.Darktech;
using Characters.AI.Mercenarys;
using Characters.Player;

namespace DeadlyOfWorld;

public class DeadlyPath
{
    [HarmonyPostfix]
    [HarmonyPatch((typeof(Character)), nameof(Character.InitializeActions))]
    static void CharacterPostfix(ref Character __instance)
    {
        if (Plugin.Death.Value < 1.0) Plugin.Death.Value = 1.0;
        if(Plugin.Death1.Value < 1.0) Plugin.Death1.Value = 1.0;
        if (Plugin.Death2.Value < 1.0) Plugin.Death2.Value = 1.0;
        if (Plugin.Death3.Value < 1.0) Plugin.Death3.Value = 1.0;


        if (__instance.type == Character.Type.Boss)
            __instance.stat.AttachValues(new Stat.Values([
        new(Stat.Category.Percent, Stat.Kind.Health, Plugin.Death.Value * 5),
        new(Stat.Category.Percent, Stat.Kind.AttackDamage, Plugin.Death1.Value * 5)
        ]));
        else if (__instance.type == Character.Type.Adventurer)
        {
            __instance.stat.AttachValues(new Stat.Values([
        new(Stat.Category.Percent, Stat.Kind.Health, Plugin.Death.Value * 3),
        new(Stat.Category.Percent, Stat.Kind.AttackDamage, Plugin.Death1.Value * 3)
        ]));
        }
        else if (__instance.type == Character.Type.Named || __instance.type == Character.Type.TrashMob)
        {
            __instance.stat.AttachValues(new Stat.Values([
        new(Stat.Category.Percent, Stat.Kind.Health, Plugin.Death2.Value),
        new(Stat.Category.Percent, Stat.Kind.AttackDamage, Plugin.Death3.Value)
        ]));
        }

        List<Character> characters = new List<Character>();
        characters.Add(__instance);
        DarkEnemySelector.instance.ElectIn(characters);
    }

    [HarmonyPrefix]
    [HarmonyPatch((typeof(PlayerComponents)), nameof(PlayerComponents.Initialize))]
    static bool Update(ref PlayerComponents __instance)
    {
        if (Plugin.Deal.Value > 1.5) Plugin.Deal.Value = 1.5;
        if (Plugin.Deal.Value < 0.0) Plugin.Deal.Value = 0.0;
        __instance.character.stat.AttachValues(new Stat.Values([
                new(Stat.Category.PercentPoint, Stat.Kind.PhysicalAttackDamage, Plugin.Deal.Value),
                new(Stat.Category.PercentPoint, Stat.Kind.MagicAttackDamage, Plugin.Deal.Value)
                ]));
        return true;
    }



        [HarmonyPostfix]
    [HarmonyPatch((typeof(DarkEnemySelector)), nameof(DarkEnemySelector.ElectIn))]
    static void SetCounters(object[] __args, ref DarkAbilityConstructor[] ____constructors)
    {
        if (Plugin.isNoDarker.Value)
        {
            return;
        }
        if (Plugin.TrueAllDark.Value == true)
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
                            c.key != Key.CarleonRecruitInCannon &&
                            c.key != Key.Unspecified)
                    {
                        ____constructors[Singleton<HardmodeManager>.Instance.currentLevel].Provide(c);
                    }
                }
            }
        }
        else
        {
            if (Plugin.Step.Value == Plugin.DarkEnum.Percent)
            {
                System.Random random = new System.Random();
                int num = random.Next(1, 101);
                if (num <= Plugin.isSetP.Value)
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
                                c.key != Key.Ent &&
                                c.key != Key.CannonSpecialist &&
                                c.key != Key.GiantMushroomEnt &&
                                c.key != Key.CarleonRecruitInCannon &&
                                c.key != Key.CarleonRecruit &&
                                c.key != Key.Unspecified)
                            {
                                ____constructors[Singleton<HardmodeManager>.Instance.currentLevel].Provide(c);
                            } // 모든적이 나와요
                        }
                    }
                }
                return;
            }

            if (Plugin.Step.Value == Plugin.DarkEnum.All)
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
                                c.key != Key.Ent &&
                                c.key != Key.GiantMushroomEnt &&
                                c.key != Key.CarleonRecruitInCannon &&
                                c.key != Key.CarleonRecruit &&
                                c.key != Key.Unspecified)
                        {
                            ____constructors[Singleton<HardmodeManager>.Instance.currentLevel].Provide(c);
                        }
                    }
                }
            }
            else if (Plugin.Step.Value == Plugin.DarkEnum.Common)
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
                            if (c.type == Character.Type.TrashMob &&
                                c.key != Key.Hound &&
                                c.key != Key.SpiritInFlask &&
                                c.key != Key.UnstableFlask &&
                                c.key != Key.Flask &&
                                c.key != Key.UnstableFlasksSpirit &&
                                c.key != Key.Ent &&
                                c.key != Key.CannonSpecialist &&
                                c.key != Key.GiantMushroomEnt &&
                                c.key != Key.CarleonRecruitInCannon &&
                                c.key != Key.CarleonRecruit &&
                                c.key != Key.Unspecified)
                            {
                                ____constructors[Singleton<HardmodeManager>.Instance.currentLevel].Provide(c);
                            }
                        }
                    }
                }
            }
            else if (Plugin.Step.Value == Plugin.DarkEnum.Rare)
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
                            if (c.type == Character.Type.TrashMob &&
                                c.key != Key.Hound &&
                                c.key != Key.SpiritInFlask &&
                                c.key != Key.UnstableFlask &&
                                c.key != Key.UnstableFlasksSpirit &&
                                c.key != Key.Ent &&
                                c.key != Key.CannonSpecialist &&
                                c.key != Key.GiantMushroomEnt &&
                                c.key != Key.CarleonRecruitInCannon &&
                                c.key != Key.CarleonRecruit &&
                                c.key != Key.Unspecified)
                            {
                                ____constructors[Singleton<HardmodeManager>.Instance.currentLevel].Provide(c);
                            }
                        }
                    }
                }
            }
            else if (Plugin.Step.Value == Plugin.DarkEnum.Uniq)
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
                            if (c.type == Character.Type.TrashMob &&
                                c.key != Key.Hound &&
                                c.key != Key.SpiritInFlask &&
                                c.key != Key.UnstableFlask &&
                                c.key != Key.UnstableFlasksSpirit &&
                                c.key != Key.Ent &&
                                c.key != Key.CannonSpecialist &&
                                c.key != Key.GiantMushroomEnt &&
                                c.key != Key.CarleonRecruitInCannon &&
                                c.key != Key.CarleonRecruit &&
                                c.key != Key.Unspecified)
                            {
                                ____constructors[Singleton<HardmodeManager>.Instance.currentLevel].Provide(c);
                            }
                        }
                    }
                }
            }
            else if (Plugin.Step.Value == Plugin.DarkEnum.Legend)
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
                            if (c.type == Character.Type.TrashMob &&
                                c.key != Key.Hound &&
                                c.key != Key.SpiritInFlask &&
                                c.key != Key.UnstableFlask &&
                                c.key != Key.UnstableFlasksSpirit &&
                                c.key != Key.Ent &&
                                c.key != Key.CannonSpecialist &&
                                c.key != Key.GiantMushroomEnt &&
                                c.key != Key.CarleonRecruitInCannon &&
                                c.key != Key.CarleonRecruit &&
                                c.key != Key.Unspecified)
                            {
                                ____constructors[Singleton<HardmodeManager>.Instance.currentLevel].Provide(c);
                            }
                        }
                    }
                }
            }
            else
            {
                Plugin.Step.Value = Plugin.DarkEnum.All;
                foreach (ICollection<Character> candidates in __args)
                {
                    Debug.LogError(candidates.Count);
                    foreach (Character c in candidates)
                    {
                        if (c.type == Character.Type.TrashMob &&
                                c.key != Key.Hound &&
                                c.key != Key.SpiritInFlask &&
                                c.key != Key.UnstableFlask &&
                                c.key != Key.UnstableFlasksSpirit &&
                                c.key != Key.Ent &&
                                c.key != Key.CannonSpecialist &&
                                c.key != Key.GiantMushroomEnt &&
                                c.key != Key.CarleonRecruitInCannon &&
                                c.key != Key.CarleonRecruit &&
                                c.key != Key.Unspecified)
                        {
                            ____constructors[Singleton<HardmodeManager>.Instance.currentLevel].Provide(c);
                        }
                    }
                }
            }
        }
    }
}
