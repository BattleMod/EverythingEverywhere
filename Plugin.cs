using System;
using System.Collections.Generic;
using Attachments;
using BepInEx;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;
using Il2CppInterop.Runtime.Injection;
using Il2CppSystem.Collections.Generic;
using PlayerLoadout.Items;
using UnityEngine;
using UserInterface.InGameBehaviours;
using UserInterface.InGameBehaviours.DeployScreenSub.LoadoutSub;

namespace EverythingEverywhere;

[BepInPlugin("link.ryhn.battlemod.everythingeverywhere", "Everything Everywhere", "1.0.0.0")]
public class Plugin : BasePlugin
{
	public static Plugin Instance;

	public override void Load()
	{
		Instance = this;
		var h = new Harmony(MyPluginInfo.PLUGIN_GUID);
		h.PatchAll();

		var allAttachments = new AWeaponAllowedAttachment()
		{
			mScopes = new Il2CppSystem.Collections.Generic.List<AScope>(),
			mScopesMap = new Il2CppSystem.Collections.Generic.Dictionary<AScope, int>(),

			mBarrels = new Il2CppSystem.Collections.Generic.List<ABarrel>(),
			mBarrelsMap = new Il2CppSystem.Collections.Generic.Dictionary<ABarrel, int>(),

			mUnderRails = new Il2CppSystem.Collections.Generic.List<AUnderRail>(),
			mUnderRailsMap = new Il2CppSystem.Collections.Generic.Dictionary<AUnderRail, int>(),

			mCanteds = new Il2CppSystem.Collections.Generic.List<ACanted>(),
			mCantedsMap = new Il2CppSystem.Collections.Generic.Dictionary<ACanted, int>(),

			mTopSights = new Il2CppSystem.Collections.Generic.List<ATopSight>(),
			mTopSightsMap = new Il2CppSystem.Collections.Generic.Dictionary<ATopSight, int>(),

			mSideRails = new Il2CppSystem.Collections.Generic.List<ASideRail>(),
			mSideRailsMap = new Il2CppSystem.Collections.Generic.Dictionary<ASideRail, int>(),

			mBoltActions = new Il2CppSystem.Collections.Generic.List<ABoltAction>(),
			mBoltActionsMap = new Il2CppSystem.Collections.Generic.Dictionary<ABoltAction, int>()
		};

		Tools.instance.AllAttachments.Finalize();
		{
			int i = 0;
			foreach (var vk in Tools.Name2Scopes)
			{
				var s = vk.value;
				allAttachments.mScopes.Add(s);
				allAttachments.mScopesMap.Add(s, i);
				i++;
			}
			i = 0;
			foreach (var vk in Tools.Name2Barrels)
			{
				var s = vk.value;
				allAttachments.mBarrels.Add(s);
				allAttachments.mBarrelsMap.Add(s, i);
				i++;
			}
			i = 0;
			foreach (var vk in Tools.Name2UnderRails)
			{
				var s = vk.value;
				allAttachments.mUnderRails.Add(s);
				allAttachments.mUnderRailsMap.Add(s, i);
				i++;
			}
			i = 0;
			foreach (var vk in Tools.Name2TopSights)
			{
				var s = vk.value;
				allAttachments.mTopSights.Add(s);
				allAttachments.mTopSightsMap.Add(s, i);
				i++;
			}
			i = 0;
			foreach (var vk in Tools.Name2Canted)
			{
				var s = vk.value;
				allAttachments.mCanteds.Add(s);
				allAttachments.mCantedsMap.Add(s, i);
				i++;
			}
			i = 0;
			foreach (var vk in Tools.Name2SideRails)
			{
				var s = vk.value;
				allAttachments.mSideRails.Add(s);
				allAttachments.mSideRailsMap.Add(s, i);
				i++;
			}
			i = 0;
			foreach (var vk in Tools.Name2BoltActions)
			{
				var s = vk.value;
				allAttachments.mBoltActions.Add(s);
				allAttachments.mBoltActionsMap.Add(s, i);
				i++;
			}
		}

		var allclass = new AvailableRoles()
		{
			LeaderAllowed = true,
			AssaultAllowed = true,
			MedicAllowed = true,
			EngineerAllowed = true,
			SupportAllowed = true,
			ReconAllowed = true
		};
		var available = new Avaibility()
		{
			AllowedClassRoles = allclass
		};

		foreach (var t in Tools.instance.AllTools)
		{
			if (t.isWeapon)
			{
				var w = t.asWeapon;
				w.Permissions = available;
				w.AllowedAttachments = allAttachments;
			}
			else if (t.isGadget)
			{
				var g = t.asGadget;
				g.Permissions = available;
				if (g.LoadoutIndex != EnumPublicSealedvaPrSeFiToThTo8vToUnique.Throwable)
					g.LoadoutIndex = EnumPublicSealedvaPrSeFiToThTo8vToUnique.ToolAB;
			}
		}
	}
}