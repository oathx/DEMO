module("SystemPlugin", package.seeall)
setmetatable(SystemPlugin, {__index=GamePlugin})

function new(szPluginName)
	local obj = {
	}
	setmetatable(obj, {__index=SystemPlugin})
	obj:Init(szPluginName)
	
	return obj
end

function Init(self, szPluginName)
	GamePlugin.Init(self, szPluginName)

	self.cacheSystem = {
	}
end

function Install(self)
	GamePlugin.Install(self)
end

function Uninstall(self)
	GamePlugin.Uninstall(self)
end

function Startup(self)
	GamePlugin.Startup(self)
end

function Shutdown(self)
	GamePlugin.Shutdown(self)
end

function Load(self, szPath, active)
	local sys = GamePlugin.Load(self, szPath, active)
	if sys then
		table.insert(self.cacheSystem, {
				name = sys:Name(), active = active
			})
		
		INFO("Load system(%s) count(%d)", 
			sys:Name(), #self.cacheSystem)

		return sys
	end
end

function Unload(self, szPath)
	local unloadName = GamePlugin.Unload(self, szPath)
	
	for idx, cache in ipairs(self.cacheSystem) do
		if cache.name == unloadName then
			table.remove(self.cacheSystem, idx)
			break
		end
	end
	
	INFO("unload system(%s) count(%d)", 
		unloadName, #self.cacheSystem)

	return unloadName
end

function Back(self)
	local count = #self.cacheSystem

	-- unload current system
	local source = self.cacheSystem[count]
	if source then
		self:Unload(source.name)
	end

	-- load cache system
	count = #self.cacheSystem
	if count > 0 then
		local target = self.cacheSystem[count]
		if target then
			local sys = self:Query(target.name)
			if not sys then
				table.remove(
						self.cacheSystem, count
						)

				sys = self:Load(target.name, target.active)
			else
				sys:Active()
			end

			return sys
		end
	end
end


