module("FightSystemEntity", package.seeall)
setmetatable(FightSystemEntity, {__index=BaseEntity})

function new(id)
	local entity = {
	}
	setmetatable(entity, {__index=FightSystemEntity})
	entity:Init(id)

	return entity
end

function Init(self, id)
	BaseEntity.Init(self, id)
	self.fights = {
	}
end

