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
end