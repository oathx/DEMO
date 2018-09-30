module("MenuSystemEntity", package.seeall)
setmetatable(MenuSystemEntity, {__index=BaseEntity})

function new(id)
	local entity = {
	}
	setmetatable(entity, {__index=MenuSystemEntity})
	entity:Init(id)

	return entity
end

function Init(self, id)
	BaseEntity.Init(self, id)

	self.masterName = self:Name()
	self.masterLev = 0
	self.masterMoney = 0
	self.defHeroID = resmng.HERO_CAOPI
end

function GetDefaultHeroID(self)
	return self.defHeroID
end