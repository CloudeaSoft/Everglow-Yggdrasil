﻿using System.Reflection;
using Everglow.Common.Enums;
using Everglow.Common.Interfaces;
using MonoMod.Cil;
using MonoMod.RuntimeDetour;
using MonoMod.Utils;
using Terraria.Graphics.Renderers;

namespace Everglow.Common.Hooks;

public class OnHookHandler : IHookHandler
{
	public Hook Hook { get; init; }

	public bool Enable
	{
		get => Hook.IsApplied;
		set
		{
			if (value)
			{
				Hook.Apply();
			}
			else
			{
				Hook.Undo();
			}
		}
	}
}

public class ILHookHandler : IHookHandler
{
	public ILHook Hook { get; init; }

	public bool Enable
	{
		get => Hook.IsApplied;
		set
		{
			if (value)
			{
				Hook.Apply();
			}
			else
			{
				Hook.Undo();
			}
		}
	}
}

public class HookHandler : IHookHandler
{
	public CodeLayer Layer { get; init; }
	public Action Hook { get; init; }
	public bool Enable { get; set; }
}

public class HookManager : ModSystem, IHookManager
{
	public IHookHandler AddHook(CodeLayer layer, Action hook)
	{
		var handler = new HookHandler
		{
			Layer = layer,
			Hook = hook,
		};
		hooks[layer].Add(handler);
		return handler;
	}

	public IHookHandler AddHook(MethodInfo target, Delegate hook)
	{
		var on = new Hook(target, hook);
		monoHooks.Add(on);
		return new OnHookHandler
		{
			Hook = on,
		};
	}

	public IHookHandler AddHook(MethodInfo target, ILContext.Manipulator hook)
	{
		var il = new ILHook(target, hook);
		monoHooks.Add(il);
		return new ILHookHandler
		{
			Hook = il,
		};
	}

	public void Dispose()
	{
		foreach (var hook in monoHooks)
		{
			hook.Dispose();
		}
		GC.SuppressFinalize(this);
	}

	public void RemoveHook(IHookHandler handler)
	{
		switch (handler)
		{
			case ILHookHandler il:
				il.Hook.Dispose();
				monoHooks.Remove(il.Hook);
				break;

			case OnHookHandler on:
				on.Hook.Dispose();
				monoHooks.Remove(on.Hook);
				break;

			case HookHandler hook:
				hooks[hook.Layer].Remove(hook);
				break;
		}
	}

	private List<IHookHandler> waitToRemove = new();

	private static readonly CodeLayer[] validLayers = new CodeLayer[]
	{
            //Draw
        CodeLayer.PostDrawFilter,
		CodeLayer.PostDrawTiles,
		CodeLayer.PostDrawProjectiles,
		CodeLayer.PostDrawDusts,
		CodeLayer.PostDrawNPCs,
		CodeLayer.PostDrawPlayers,
		CodeLayer.PostDrawMapIcons,
		CodeLayer.PostDrawBG,
            //Update
            CodeLayer.PostUpdateEverything,
		CodeLayer.PostUpdateProjectiles,
		CodeLayer.PostUpdatePlayers,
		CodeLayer.PostUpdateNPCs,
		CodeLayer.PostUpdateDusts,
		CodeLayer.PostUpdateInvasions,
            //Misc
            CodeLayer.PostEnterWorld_Single,
		CodeLayer.PostExitWorld_Single,
		CodeLayer.PostEnterWorld_Server,
		CodeLayer.ResolutionChanged
	};

	public bool DisableDrawNPCs { get; set; } = false;
	public bool DisableDrawSkyAndHell { get; set; } = false;
	public bool DisableDrawBackground { get; set; } = false;
	private Dictionary<CodeLayer, List<HookHandler>> hooks = validLayers.ToDictionary(l => l, l => new List<HookHandler>());
	private List<IDisposable> monoHooks = new List<IDisposable>();

	/// <summary>
	/// 现在存在的问题就是，这里的method都是无参数的Action，但是如DrawMapIcon这样的方法就需要传参了，只好用这种这种定义字段的方法
	/// </summary>
	public (Vector2 mapTopLeft, Vector2 mapX2Y2AndOff, Rectangle? mapRect, float mapScale) MapIconInfomation
	{
		get; private set;
	}

	/// <summary>
	/// 移除所有被Disable的Action
	/// </summary>
	public void RemoveDisabledAction()
	{
		foreach (var op in validLayers)
		{
			hooks[op].RemoveAll(handler => !handler.Enable);
		}
	}

	public void HookLoad()
	{
		IL_Main.DoDraw += Main_DoDraw;
		On_Main.DrawDust += Main_DrawDust;
		On_Main.DrawProjectiles += Main_DrawProjectiles;
		On_Main.DrawNPCs += Main_DrawNPCs;
		On_LegacyPlayerRenderer.DrawPlayers += LegacyPlayerRenderer_DrawPlayers;
		On_WorldGen.playWorldCallBack += WorldGen_playWorldCallBack;

		On_WorldGen.SaveAndQuit += WorldGen_SaveAndQuit;
		On_Main.DrawMiscMapIcons += Main_DrawMiscMapIcons;
		On_WorldGen.serverLoadWorldCallBack += WorldGen_serverLoadWorldCallBack;
		On_Main.DrawBG += Main_DrawBG;
		On_Main.DrawBackground += Main_DrawBackground;
		On_Main.DoDraw_WallsTilesNPCs += Main_DoDraw_WallsTilesNPCs;
		Main.OnResolutionChanged += Main_OnResolutionChanged;
	}

	public void HookUnload()
	{
		IL_Main.DoDraw -= Main_DoDraw;
		On_Main.DrawDust -= Main_DrawDust;
		On_Main.DrawProjectiles -= Main_DrawProjectiles;
		On_Main.DrawNPCs -= Main_DrawNPCs;
		On_LegacyPlayerRenderer.DrawPlayers -= LegacyPlayerRenderer_DrawPlayers;
		On_WorldGen.playWorldCallBack -= WorldGen_playWorldCallBack;

		On_WorldGen.SaveAndQuit -= WorldGen_SaveAndQuit;
		On_Main.DrawMiscMapIcons -= Main_DrawMiscMapIcons;
		On_WorldGen.serverLoadWorldCallBack -= WorldGen_serverLoadWorldCallBack;
		On_Main.DrawBG -= Main_DrawBG;
		On_Main.DrawBackground -= Main_DrawBackground;
		On_Main.DoDraw_WallsTilesNPCs -= Main_DoDraw_WallsTilesNPCs;
		Main.OnResolutionChanged -= Main_OnResolutionChanged;
		ClearReflectionCache();
	}

	public static void ClearReflectionCache()
	{
		ReflectionHelper.AssemblyCache.Clear();
		ReflectionHelper.AssembliesCache.Clear();
		ReflectionHelper.ResolveReflectionCache.Clear();
	}

	internal void Invoke(CodeLayer op)
	{
		foreach (var handler in hooks[op])
		{
			if (handler.Enable)
			{
				try
				{
					handler.Invoke();
				}
				catch (Exception ex)
				{
					Debug.Fail($"{handler} 抛出了异常 {ex}");
					Mod.Logger.Error($"{handler} 抛出了异常 {ex}");
					handler.Enable = false;
				}
			}
		}
	}

	public override void PostUpdateInvasions()
	{
		Invoke(CodeLayer.PostUpdateInvasions);
	}

	public override void PostUpdateEverything()
	{
		Invoke(CodeLayer.PostUpdateEverything);
		UpdateTimer++;
		foreach (var (op, handler) in waitToRemove)
		{
			hooks[op].Remove(handler);
		}
	}

	public override void UpdateUI(GameTime gameTime)
	{
		UITimer++;
	}

	public override void PostUpdateNPCs()
	{
		Invoke(CodeLayer.PostUpdateNPCs);
	}

	public override void PostUpdateProjectiles()
	{
		Invoke(CodeLayer.PostUpdateProjectiles);
	}

	public override void PostUpdateDusts()
	{
		Invoke(CodeLayer.PostUpdateDusts);
	}

	public override void PostUpdatePlayers()
	{
		Invoke(CodeLayer.PostUpdatePlayers);
	}

	public override void PostDrawTiles()
	{
		Invoke(CodeLayer.PostDrawTiles);
	}

	private void Main_DrawBackground(On_Main.orig_DrawBackground orig, Main self)
	{
		if (DisableDrawBackground)
		{
			return;
		}
		orig(self);
	}

	private void Main_DrawBG(On_Main.orig_DrawBG orig, Main self)
	{
		if (DisableDrawSkyAndHell)
		{
			return;
		}
		orig(self);
	}

	private void Main_DoDraw_WallsTilesNPCs(On_Main.orig_DoDraw_WallsTilesNPCs orig, Main self)
	{
		Invoke(CodeLayer.PostDrawBG);
		orig(self);
	}

	private void WorldGen_serverLoadWorldCallBack(On_WorldGen.orig_serverLoadWorldCallBack orig)
	{
		orig();
		Invoke(CodeLayer.PostEnterWorld_Server);
	}

	private void Main_DrawMiscMapIcons(On_Main.orig_DrawMiscMapIcons orig, Main self, SpriteBatch spriteBatch, Vector2 mapTopLeft, Vector2 mapX2Y2AndOff, Rectangle? mapRect, float mapScale, float drawScale, ref string mouseTextString)
	{
		orig(self, spriteBatch, mapTopLeft, mapX2Y2AndOff, mapRect, mapScale, drawScale, ref mouseTextString);
		MapIconInfomation = (mapTopLeft, mapX2Y2AndOff, mapRect, mapScale);
		Invoke(CodeLayer.PostDrawMapIcons);
	}

	private void WorldGen_SaveAndQuit(On_WorldGen.orig_SaveAndQuit orig, Action callback)
	{
		orig(callback);
		Invoke(CodeLayer.PostExitWorld_Single);
	}

	private void WorldGen_playWorldCallBack(On_WorldGen.orig_playWorldCallBack orig, object threadContext)
	{
		orig(threadContext);
		Invoke(CodeLayer.PostEnterWorld_Single);
	}

	private void Main_OnResolutionChanged(Vector2 obj)
	{
		Invoke(CodeLayer.ResolutionChanged);
	}

	private void LegacyPlayerRenderer_DrawPlayers(On_LegacyPlayerRenderer.orig_DrawPlayers orig, LegacyPlayerRenderer self, Terraria.Graphics.Camera camera, IEnumerable<Player> players)
	{
		orig.Invoke(self, camera, players);
		Invoke(CodeLayer.PostDrawPlayers);
	}

	private void Main_DrawNPCs(On_Main.orig_DrawNPCs orig, Main self, bool behindTiles)
	{
		orig.Invoke(self, behindTiles);
		if (!behindTiles)
		{
			Invoke(CodeLayer.PostDrawNPCs);
		}
	}

	private void Main_DrawDust(On_Main.orig_DrawDust orig, Main self)
	{
		orig.Invoke(self);
		Invoke(CodeLayer.PostDrawDusts);
	}

	private void Main_DrawProjectiles(On_Main.orig_DrawProjectiles orig, Main self)
	{
		orig.Invoke(self);
		Invoke(CodeLayer.PostDrawProjectiles);
	}

	// TODO 不要给这种大函数直接IL了
	private void Main_DoDraw(ILContext il)
	{
		var cursor = new ILCursor(il);
		if (!cursor.TryGotoNext(MoveType.Before, ins => ins.MatchLdcI4(36)))
		{
			throw new Exception("Main_DoDraw_NotFound_1");
		}
		cursor.EmitDelegate(() => Invoke(CodeLayer.PostDrawFilter));
	}
}