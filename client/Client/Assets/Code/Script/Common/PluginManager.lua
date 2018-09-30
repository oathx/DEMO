module("PluginManager", mkSingleton)
setmetatable(PluginManager, {__index=LuaBehaviour})

function Init(self)
	-- current plugin list
	self.plugins = {
	}
end

-- start plugin manager
function Startup(self)
	self.gameObject = GameObject("PluginManager")
	if not self.gameObject then
		error("Can't create PluginManager")
	end

	GameObject.DontDestroyOnLoad(self.gameObject)
	
	self:AddLuaBehaviour(self.gameObject)

	return true
end

function LoadPlugin(self, szPluginPath)
	local name = GetFileName(szPluginPath)

	local plugin = self.plugins[name]
	if not plugin then
		local mod = require(szPluginPath)
		if not mod then
			ERROR("Can't find plugin %s", szPluginPath)
		end

		plugin = _G[name].new(name)
		if plugin then
			if plugin.transform then
				plugin.transform.parent = self.transform
			end

			plugin:Install()
		end

		INFO("Load plugin %s", szPluginPath)

		self.plugins[name] = plugin
	end

	return plugin
end

function QueryPlugin(self, szPluginPath)
	return self.plugins[GetFileName(szPluginPath)]
end

function UnloadPlugin(self, szPluginPath)
	local name = GetFileName(szPluginPath)

	local plugin = self.plugins[name]
	if not plugin then
		plugin:Uninstall()

		INFO("Unload plugin %s", szPluginPath)

		-- shutdown the plugin
		plugin:Shutdown()

		if plugin.gameObject then
			GameObject.Destroy(plugin.gameObject)
		end
	end

	self.plugins[name] = nil
end

function Shutdown(self)
	for name, plugin in pairs(self.plugins) do
		self:UnloadPlugin(name)
	end

	self.plugins = {}
end

-- send a plugin event
-- @param szPluginName
-- @param nID
-- @param evtArgs
-- @param szObserverName
function SendEvent(self, szPluginName, nID, evtArgs, szObserverName)
	local plugin = self:QueryPlugin(szPluginName)
	if plugin then
		plugin:SendEvent(nID, evtArgs, szObserverName)
	end
end

-- send a plugin event
-- @param nID
-- @param evtArgs
function FireEvent(self, nID, evtArgs)
	if _EDITOR then
		INFO("FireEvent(id=%s) evtArgs(%s)", tostring(nID), tostring(evtArgs))
	end

	for idx, plugin in pairs(self.plugins) do
		local bResult = plugin:SendEvent(nID, evtArgs)
		if bResult then
			break
		end
	end
end
