using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceSplitScripter
{
    internal static class Constants
    {
        // Script variable names
        public const string LOOTSPLIT_CONTAINER_VARIABLE_NAME = "faceSplitContainer";
        public const string TREASURE_MAP_TOME_VARIABLE_NAME = "faceSplitTmapTome";
        public const string ASPECT_TOME_VARIABLE_NAME = "faceSplitAspectTome";
        public const string SKILLSCROLL_TOME_VARIABLE_NAME = "faceSplitScrollTome";
        public const string SKLL_ORB_VARIABLE_NAME = "faceSplitSkillOrb";
        public const string MCD_VARIABLE_NAME = "faceSplitMcd";
        public const string MISSING_ITEMS_LIST_NAME = "faceSplitMissingList";

        // Item Types
        public const string TOME_ITEM_TYPE = "29104";
        public const string SKILL_ORB_ITEM_TYPE = "22336";
        public const string MCD_ITEM_TYPE = "17087";

        // Item Hues
        public const string ASPECT_TOME_HUE = "2618";
        public const string TMAP_TOME_HUE = "2990";
        public const string SKILLSCROLL_TOME_HUE = "2963";
        
        // Script configs
        public const int GLOBAL_COOLDOWN_IN_MSECS = 1000;

        // Gump IDs
        public const string ASPECT_TOME_GUMP_ID = "265325939";
        public const string TMAP_TOME_GUMP_ID = "1863945839";
        public const string SKILLSCROLL_TOME_GUMP_ID = "2125225775";

        // Gump specifics
        public const string NEXT_PAGE_FOR_SKILLSCROLL_TOME = "5";

        // Identifiers from the sheet to determine what the loot type is.  Skillscroll is the default.
        public const string CORE_IDENTIFIER = "core";
        public const string EXTRACT_IDENTIFIER = "extr";
        public const string TREASURE_MAP_IDENTIFIER = "lvl";
        public const string SKILL_ORB_IDENTIFIER = "skill orb";
        public const string MCD_IDENTIFIER = "mcd";

        public const string FULL_SPLIT_STRING = @"[B] Aegis Keep Damage(1) ,[B] Alchemy/Healing/Vet(1) ,[B] Backstab Damage(1) ,[B] Barding Effect Durations(1) ,[B] Barding Reset/Break Ignore Chance(1) ,[B] Cavernam Damage(1) ,[B] Chest Success Chance / Progress(1) ,[B] Chivalry Skill(1) ,[B] Damage on Ships(1) ,[B] Damage to Barded Creatures(1) ,[B] Damage to Bosses(1) ,[B] Damage to Daemonic Creatures(1) ,[B] Damage to Poisoned Creatures(1) ,[B] Darkmire Temple Damage(1) ,[B] Effective Barding Skill(1) ,[B] Effective Lockpicking Skill(1) ,[B] Follower Damage(1) ,[B] Gold/Doubloon Drop Increase(1) ,[B] Healing Received(1) ,[B] Inferno Damage(1) ,[B] Mausoleum Damage(1) ,[B] Melee Accuracy(1) ,[B] Melee Accuracy/Defence(1) ,[B] Melee Aspect Effect Chance(1) ,[B] Melee Aspect Effect Modifier(1) ,[B] Melee Damage(1) ,[B] Melee Damage / Armor Ignore Chance(1) ,[B] Melee Special Chance/ Special Damage(1) ,[B] Melee Swing Speed(1) ,[B] Mount Petram Damage(1) ,[B] Necromancy Skill(1) ,[B] Nusero Damage(1) ,[B] Ossuary Damage(1) ,[B] Pulma Damage(1) ,[B] Shadowspire Cathedral Damage(1) ,[B] Ship Cannon Damage(1) ,[B] Special/Rare Loot Chance(1) ,[B] Spell Aspect Special Chance(1) ,[B] Spell Aspect Effect Modifier(1) ,[B] Spirit Speak/Inscription(1) ,[B] Trap & Wand Damage(1) ,[B] Wilderness Damage(1) ,[B] JUNK Bronze(1) ,[S] Aegis Keep Damage(1) ,[S] Alchemy/Healing/Vet(1) ,[S] Backstab Damage(1) ,[S] Barding Effect Durations(1) ,[S] Barding Reset/Break Ignore Chance(1) ,[S] Cavernam Damage(1) ,[S] Chest Success Chance / Progress(1) ,[S] Chivalry Skill(1) ,[S] Damage on Ships(1) ,[S] Damage to Barded Creatures(1) ,[S] Damage to Bosses(1) ,[S] Damage to Daemonic Creatures(1) ,[S] Damage to Poisoned Creatures(1) ,[S] Darkmire Temple Damage(1) ,[S] Effective Barding Skill(1) ,[S] Effective Lockpicking Skill(1) ,[S] Follower Damage(1) ,[S] Gold/Doubloon Drop Increase(1) ,[S] Healing Received(1) ,[S] Inferno Damage(1) ,[S] Mausoleum Damage(1) ,[S] Melee Accuracy(1) ,[S] Melee Accuracy/Defence(1) ,[S] Melee Aspect Effect Chance(1) ,[S] Melee Aspect Effect Modifier(1) ,[S] Melee Damage(1) ,[S] Melee Damage / Armor Ignore Chance(1) ,[S] Melee Special Chance/ Special Damage(1) ,[S] Melee Swing Speed(1) ,[S] Mount Petram Damage(1) ,[S] Necromancy Skill(1) ,[S] Nusero Damage(1) ,[S] Ossuary Damage(1) ,[S] Pulma Damage(1) ,[S] Shadowspire Cathedral Damage(1) ,[S] Ship Cannon Damage(1) ,[S] Special/Rare Loot Chance(1) ,[S] Spell Aspect Special Chance(1) ,[S] Spell Aspect Effect Modifier(1) ,[S] Spirit Speak/Inscription(1) ,[S] Trap & Wand Damage(1) ,[S] Wilderness Damage(1) ,[S] JUNK Silver(1) ,[G] Aegis Keep Damage(1) ,[G] Alchemy/Healing/Vet(1) ,[G] Backstab Damage(1) ,[G] Barding Effect Durations(1) ,[G] Barding Reset/Break Ignore Chance(1) ,[G] Cavernam Damage(1) ,[G] Chest Success Chance / Progress(1) ,[G] Chivalry Skill(1) ,[G] Damage on Ships(1) ,[G] Damage to Barded Creatures(1) ,[G] Damage to Bosses(1) ,[G] Damage to Daemonic Creatures(1) ,[G] Damage to Poisoned Creatures(1) ,[G] Darkmire Temple Damage(1) ,[G] Effective Barding Skill(1) ,[G] Effective Lockpicking Skill(1) ,[G] Follower Damage(1) ,[G] Gold/Doubloon Drop Increase(1) ,[G] Healing Received(1) ,[G] Inferno Damage(1) ,[G] Mausoleum Damage(1) ,[G] Melee Accuracy(1) ,[G] Melee Accuracy/Defence(1) ,[G] Melee Aspect Effect Chance(1) ,[G] Melee Aspect Effect Modifier(1) ,[G] Melee Damage(1) ,[G] Melee Damage / Armor Ignore Chance(1) ,[G] Melee Special Chance/ Special Damage(1) ,[G] Melee Swing Speed(1) ,[G] Mount Petram Damage(1) ,[G] Necromancy Skill(1) ,[G] Nusero Damage(1) ,[G] Ossuary Damage(1) ,[G] Pulma Damage(1) ,[G] Shadowspire Cathedral Damage(1) ,[G] Ship Cannon Damage(1) ,[G] Special/Rare Loot Chance(1) ,[G] Spell Aspect Special Chance(1) ,[G] Spell Aspect Effect Modifier(1) ,[G] Spirit Speak/Inscription(1) ,[G] Trap & Wand Damage(1) ,[G] Wilderness Damage(1) ,[G] JUNK Gold(1) ,[C] Corrupted - (1) ,aegis spell hue deed(1) ,Metallic cerulean carpet dye(1) ,SSC Cloth(1) ,Greater demon figurine(1) ,Powder lemon accesorry dye(1) ,Wilderness cloth(1) ,Metallic Junpier cloth(1) ,Metallic Pewter cloth(1) ,SSC carpet dye(1) ,dark forest hair dye(1) ,great sunken serpent collectible card(1) ,Fortune Aspect Cloth(1) ,Powder Cloud Cloth(1) ,Dyeable carpet tile(1) ,Dyeable rug tile(1) ,Dark denim furniture dye(1) ,Inferno collectable card(1) ,corrupted spirituality banner(1) ,Book of Chivalry (bless)(1) ,Skill Orb(1) ,MCD(1) ,LVL1(1) ,LVL2(1) ,LVL3(1) ,LVL4(1) ,LVL5(1) ,LVL6(1) ,CORE Air(1) ,CORE Artisan(1) ,CORE Blood(1) ,CORE Command(1) ,CORE Death(1) ,CORE Discipline(1) ,CORE Earth(1) ,CORE Eldritch(1) ,CORE Fire(1) ,CORE Fortune(1) ,CORE Holy(1) ,CORE Lyric(1) ,CORE Poison(1) ,CORE Shadow(1) ,CORE Void(1) ,CORE War(1) ,CORE Water(1) ,EXTR Air(1) ,EXTR Artisan(1) ,EXTR Blood(1) ,EXTR Command(1) ,EXTR Death(1) ,EXTR Discipline(1) ,EXTR Earth(1) ,EXTR Eldritch(1) ,EXTR Fire(1) ,EXTR Fortune(1) ,EXTR Holy(1) ,EXTR Lyric(1) ,EXTR Poison(1) ,EXTR Shadow(1) ,EXTR Void(1) ,EXTR War(1) ,EXTR Water(1) ,Alchy(1) ,Animal Lore(1) ,Animal Tame(1) ,Arms Lore(1) ,Beg(1) ,Blacksmith(1) ,Camp(1) ,Carpentry(1) ,Carto(1) ,Chivalry(1) ,Cook(1) ,Detect(1) ,Discordance(1) ,Fish(1) ,Forensic(1) ,Herd(1) ,Inscript(1) ,Item ID(1) ,Lockpick(1) ,Lumber(1) ,Mine(1) ,Music(1) ,Necromancy(1) ,Peace(1) ,Poison(1) ,Provo(1) ,Remove(1) ,Spirit(1) ,Stealth(1) ,Tailor(1) ,Taste(1) ,Tinker(1) ,Track(1) ,Veterinary(1)";
    }
}
