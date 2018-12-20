module("RootContext", mkSingleton)

function Init(self)
	EntityManager.GetSingleton():Startup({
		[resmng.SYS_LOGIN] = function(id) 
			return LoginSystemEntity.new(id) 
		end,

		[resmng.SYS_MENU] = function(id)
			return MenuSystemEntity.new(id)
		end,

		[resmng.SYS_TASK] = function(id)
			return TaskSystemEntity.new(id)
		end,

		[resmng.SYS_PLAYER] = function(id)
			return PlayerEntity.new(id)
		end,

		--[resmng.SYS_FIGHT] = function(id)
		--	return FightSystemEntity.new(id)
		--end
	})

	PluginManager.GetSingleton():Startup()
end

function Startup(self)
	Rpc:Init(Protocol.new())

	-- start game manager
	
	
	-- load system config
	local sys = resmng.propLocalById(resmng.GAME_LOCAL)
	if not sys then
		ELOG("Can't find system config")
	else
		if sys.MainLogicScriptPath then
			self.systemPlugin = PluginManager.GetSingleton():LoadPlugin(sys.MainLogicScriptPath)
		end
	end

	-- load game default plugin
	local result = UISystem.GetSingleton():Startup()
	if result then
		GameManager.GetSingleton():Startup()
	end
end

function Shutdown(self)
	EntityManager.GetSingleton():Shutdown()
	PluginManager.GetSingleton():Shutdown()
end

function LoadScene(self, nSceneID, complete)
	UISystem.GetSingleton():OpenWidget(UIStyle.LOADING, function(widget) 
		local prop = resmng.propSceneById(nSceneID)
		if not prop then
			ELOG("Can't find scene config (%d)", nSceneID)
		else	
			XRes.LoadSceneAsync(prop.Path, function(name)
				UISystem.GetSingleton():CloseWidget(UIStyle.LOADING)
				
				local obser
				local modID = prop.Script or 0

				if self.systemPlugin and modID ~= 0 then
					local pob = resmng.propSystemById(modID)
					if pob then
						obser = self.systemPlugin:Load(pob.Path, pob.Active ~= 0)
					end
				end
				
				if complete then
					complete(name, obser)
				end
			end)
		end	
	end)
end
