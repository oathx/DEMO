module("FightSystem", package.seeall)
setmetatable(FightSystem, {__index=EventModule})

function new(szName)
	local menu = {
	}
	setmetatable(menu, {__index=FightSystem})
	menu:Init(szName)

	return menu
end

function Init(self, szName)
	EventModule.Init(self, 
		GUtility.HashString(szName))

	-- get menu entity
	self.entity = EntityManager.GetSingleton():GetEntity(resmng.SYS_FIGHT)

	-- init task entity
	self.sysTask = EntityManager.GetSingleton():GetEntity(resmng.SYS_TASK)
	if not self.sysTask then
		ERROR("Can't get task system entity")
	end

	self.curTask = self.sysTask:GetCurrent()
end

function Active(self)
	UISystem.GetSingleton():Background(false)
end

function Detive(self)

end